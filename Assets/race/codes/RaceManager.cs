using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class RaceManager : MonoBehaviour
{
    public UIManager uiManager;
    private Dictionary<GameObject, float> raceAdvance = new();
    private FollowPlayer followPlayer;
    private int numberOfCheckpoints;
    private int raceStatus;
    private RaceManager raceManager;
    private GameObject invisibleAI;

    void Start()
    {
        // Pour cacher la souris
        Cursor.visible = false;
        raceManager = GameObject.Find("RaceManager").GetComponent<RaceManager>();
        numberOfCheckpoints = GameObject.FindGameObjectsWithTag("Checkpoint").Length;

        // get all the cars
        GameObject[] cars = GameObject.FindGameObjectsWithTag("AI");
        int i = 0;
        foreach (GameObject car in cars)
        {
            AICarController carController = car.GetComponent<AICarController>();
            if (!carController.isInvisible)
            {
                carController.setInitialPlacement(i);
                carController.reset();
                i++;
            }
            else
            {
                carController.reset();
            }
        }

        GameObject player = GameObject.FindGameObjectWithTag("Player");
        player.GetComponent<CarControler>().setInitialPlacement(i);
        player.GetComponent<CarControler>().reset();



        followPlayer = GameObject.Find("Main Camera").GetComponent<FollowPlayer>();
        // switch to invisible mode for the camera
        invisibleAI = followPlayer.switchToInvisible();
    }

    // Met à jour les temps de course pour une voiture spécifique
    public void UpdateRaceTime(GameObject car, int lapNumber, int checkpointNumber, float time)
    {
        if (raceManager.GetRaceStatus() != 1) return;
        if (!raceAdvance.ContainsKey(car))
        {
            raceAdvance.Add(car, 0);
        }
        float score = (lapNumber * numberOfCheckpoints + checkpointNumber) * 10 - time;

        // insert the car in the ranking
        raceAdvance[car] = score;
        string ranking = StringTransformer(GetCurrentRanking());
        uiManager.UpdateRanking(ranking);
    }

    // Récupère le classement actuel des voitures
    public List<GameObject> GetCurrentRanking()
    {
        // pour trier les voiture on se base sur le passage des checkpoints
        // Chaque checkpoint passé ajoute 10 points et le temps du dernier checkpoint passé est enlevé
        // Une voiture qui a passé le checkpoint 4 au bout de 30 secondes aura 4 * 10 - 30 = 10 points
        // Une voiture qui a passé le checkpoint 4 au bout de 32 secondes aura 4 * 10 - 32 = 8 points
        // On trie les voitures en fonction de leur nombre de points
        // Tri des voitures en fonction de leur temps de course
        // Make it a dictionary to keep track of the score
        List<GameObject> ranking = new List<GameObject>();
        foreach (KeyValuePair<GameObject, float> entry in raceAdvance)
        {
            // if the entry is not the player and the car is invisible, we skip it
            if (!entry.Key.CompareTag("Player") && entry.Key.GetComponent<AICarController>().isInvisible)
            {
                continue;
            }

            // if the race is ended, the ranking will not change anymore
            if (raceStatus == 2)
            {
                continue;
            }
            GameObject car = entry.Key;
            float score = entry.Value;
            int i = 0;
            while (i < ranking.Count && raceAdvance[ranking[i]] > score)
            {
                i++;
            }
            ranking.Insert(i, car);
        }
        return ranking;
    }

    public string StringTransformer(List<GameObject> ranking)
    {
        string rankingString = "Classement\n";
        for (int i = 0; i < ranking.Count; i++)
        {
            rankingString += (i + 1) + ". " + ranking[i].name + "\n";
        }
        return rankingString;
    }

    public int GetRaceStatus()
    {
        return raceStatus;
    }

    public void SetRaceStatus(int status)
    {
        if (status == 1)
        {
            uiManager = GameObject.Find("Paneau").GetComponent<UIManager>();
            followPlayer.switchToRace();
            StartCoroutine(StartRaceCountdown());
            return;
        }
        else if (status == 2)
        {
            CarControler player = GameObject.FindGameObjectWithTag("Player").GetComponent<CarControler>();
            followPlayer.switchToDrone();
            raceStatus = status;
        }
        else
        {
            raceStatus = status;
        }
    }


    public int GetPlayerRanking()
    {
        List<GameObject> ranking = GetCurrentRanking();
        for (int i = 0; i < ranking.Count; i++)
        {
            if (ranking[i].tag == "Player")
            {
                return i + 1;
            }
        }
        return -1;
    }

    // if the player press R, we restart
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R)) // if the player press R, we restart
        {
            if (raceStatus != 2) return; // if the race isn't finished, we don't restart
            if (GameObject.Find("Arrival").GetComponent<LapManager>().getIsWinner()) return; // if the player has won, we don't restart, we wait for the portal
            RestartGame();
        }
        else if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
        else if (Input.GetKeyDown(KeyCode.Space)) // if the player press space on discover turn, we start the race
        {
            Debug.Log("Space pressed on race status " + raceStatus);
            if (raceStatus == 0)
            {
                SetRaceStatus(1);
                // destroy the invisible car
                GameObject.Find("invisibleAI").SetActive(false);
            }
        }
    }

    void RestartGame()
    {
        // reset the race status
        raceStatus = -1;

        // call back invisible car
        invisibleAI.SetActive(true);

        // reset the player and AI positions
        ResetCars();

        // reset the checkpoints
        ResetCheckpoints();

        // reset the LapManager
        GameObject.Find("Arrival").GetComponent<LapManager>().Reset();

        System.Threading.Thread.Sleep(20);

        // reset the race status
        raceStatus = 0;

        Start();
    }

    void ResetCars()
    {
        GameObject[] cars = GameObject.FindGameObjectsWithTag("AI");
        foreach (GameObject car in cars)
        {
            car.GetComponent<AICarController>().reset();
        }

        GameObject player = GameObject.FindGameObjectWithTag("Player");
        player.GetComponent<CarControler>().reset();
    }

    void ResetCheckpoints()
    {
        GameObject[] checkpoints = GameObject.FindGameObjectsWithTag("Checkpoint");
        foreach (GameObject checkpoint in checkpoints)
        {
            checkpoint.GetComponent<Checkpoint>().reset();
        }
    }

    IEnumerator StartRaceCountdown()
    {
        uiManager = GameObject.Find("Paneau").GetComponent<UIManager>();
        uiManager.UpdateLapText("Départ dans 3");

        yield return new WaitForSeconds(1);

        uiManager.UpdateLapText("Départ dans 2");

        yield return new WaitForSeconds(1);

        uiManager.UpdateLapText("Départ dans 1");

        yield return new WaitForSeconds(1);

        uiManager.UpdateLapText("C'est parti !");
        raceStatus = 1;
    }
}
