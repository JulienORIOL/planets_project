using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LapManager : MonoBehaviour
{
    public List<Checkpoint> checkpoints;
    public GameObject prefab;
    public UIManager uiManager;
    public int totalLaps = 3;
    private int lastPlayerCheckpoint = -1;
    private int currentPlayerLap = 0;
    private bool isWinner = false;
    public RaceManager raceManager; // Référence vers le gestionnaire de course

    void UpdatePlayerLap(int lap)
    {
        string lapText = "Tour " + lap.ToString() + " / " + totalLaps.ToString();
        uiManager.UpdateLapText(lapText);
    }

    void UpdatePlayerLap(string lap)
    {
        string lapText = lap;
        uiManager.UpdateLapText(lapText);
    }

    void Start()
    {
        ListenCheckpoints(true);
        UpdatePlayerLap("Découverte");
    }

    private void ListenCheckpoints(bool subscribe)
    {
        foreach (Checkpoint checkpoint in checkpoints)
        {
            if (subscribe) checkpoint.onCheckpointEnter.AddListener(CheckpointActivated);
            else checkpoint.onCheckpointEnter.RemoveListener(CheckpointActivated);
        }
    }

    public void CheckpointActivated(GameObject car, Checkpoint checkpoint)
    {
        int lapNumber = 0;
        int checkpointNumber = checkpoints.IndexOf(checkpoint);
        float time = (float) Time.time;

        if (!checkpoints.Contains(checkpoint)) return;
        
        
        // Do we know this checkpoint ?
        if (car.CompareTag("Player")) // Player car
        {
            if (raceManager.GetRaceStatus() != 1) return; // Si la course n'est pas en cours, ne rien faire
            // first time ever the car reach the first checkpoint
            bool startingFirstLap = checkpointNumber == 0 && lastPlayerCheckpoint == -1;
            // finish line checkpoint is triggered & last checkpoint was reached
            bool lapIsFinished = checkpointNumber == 0 && lastPlayerCheckpoint >= checkpoints.Count - 1;
            if (startingFirstLap || lapIsFinished)
            {
                currentPlayerLap += 1;
                lastPlayerCheckpoint = 0;

                // if this was the final lap
                if (currentPlayerLap > totalLaps)
                {
                    UpdatePlayerLap("Course terminée");
                    raceManager.UpdateRaceTime(car, currentPlayerLap, lastPlayerCheckpoint, time);
                    int rank = raceManager.GetPlayerRanking();
                    raceManager.SetRaceStatus(2);
                    string classement = rank + (rank > 1 ? "ème" : "er");
                    if (rank == 1) 
                    {
                        UpdatePlayerLap("Course terminée\n\nBravo princesse, tu as relevé ce premier défi !\nCeci n'est que le début de ton aventure\nUn portail s'est ouvert pour toi");
                        isWinner = true;
                    }
                    else
                    {
                        UpdatePlayerLap("Course terminée\n\nTu as fini " + classement + " !\n\nCe n'est pas suffisant pour vaincre mes sbires,\nappuie sur R pour recommencer");
                        return;
                    }
                    StartMovingPrefab();
                    return;
                }
                if (currentPlayerLap == totalLaps)
                {
                    UpdatePlayerLap("Dernier tour");
                }
                else
                {
                    UpdatePlayerLap(currentPlayerLap);
                }

            }
            // next checkpoint reached
            else if (checkpointNumber == lastPlayerCheckpoint + 1) lastPlayerCheckpoint += 1;

            lapNumber = currentPlayerLap;
            checkpointNumber = lastPlayerCheckpoint;
        } 
        else // AI car
        {
            AICarController controller = car.GetComponent<AICarController>();
            if (checkpointNumber == 0)
            {
                controller.addTurn();
            }
            controller.setLastCheckpoint(checkpointNumber);

            lapNumber = controller.getTurn();
            checkpointNumber = controller.getLastCheckpoint();
        }

        // Si la course n'est pas commencée, ne rien faire, idem si course est terminée
        if (raceManager.GetRaceStatus() != 1) return;
        // Mettre à jour le classement
        raceManager.UpdateRaceTime(car, lapNumber, checkpointNumber, time);
    }

    public Checkpoint getLastCheckpoint()
    {
        return checkpoints[checkpoints.Count - 1];
    }

    public void Reset()
    {
        currentPlayerLap = 0;
        lastPlayerCheckpoint = -1;
        UpdatePlayerLap("Reconnaissance");
    }

    private IEnumerator MovePrefabOnYAxis(GameObject prefab, float startY, float endY, float duration)
    {
        float time = 0f;
        Vector3 startPosition = new Vector3(prefab.transform.position.x, startY, prefab.transform.position.z);
        Vector3 endPosition = new Vector3(prefab.transform.position.x, endY, prefab.transform.position.z);

        while (time < duration)
        {
            time += Time.deltaTime;
            prefab.transform.position = Vector3.Lerp(startPosition, endPosition, time / duration);
            yield return null;
        }

        // Assurez-vous que la position finale est exactement endY après la boucle
        prefab.transform.position = endPosition;
    }

    // Utilisez cette méthode pour commencer le mouvement, par exemple :
    public void StartMovingPrefab()
    {
        StartCoroutine(MovePrefabOnYAxis(prefab, 31f, -8f, 0f)); // Déplace prefab de -13 à 8 sur l'axe Y en 3 secondes
    }

    public bool getIsWinner()
    {
        return isWinner;
    }
}
