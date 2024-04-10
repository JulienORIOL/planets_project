using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class AICarController : MonoBehaviour
{
    public Rigidbody rg;
    public float forwardMoveSpeed;
    public float backwardMoveSpeed;
    public float steerSpeed;
    public bool isInvisible = false;
    public List<AIPoint> aipoints;
    private int initialPlacement = 0;
    private int currentAIPointIndex = 0;
    private int lastPlayerCheckpoint = -1;
    private int currentTurn = 0;
    private RaceManager raceManager;
    private PlacementManager placementManager;

    public void OnAIPointEnter(GameObject gameObject, AIPoint aipoint)
    {
        if (aipoint == aipoints[currentAIPointIndex])
        {
            currentAIPointIndex++;
            if (currentAIPointIndex >= aipoints.Count)
            {
                if (gameObject.GetComponent<AICarController>().isInvisible
                && gameObject.GetComponent<AICarController>().getTurn() == 1)
                // si la voiture invisible termine son 1er tour on la détruit et on commence la course
                {
                    raceManager.SetRaceStatus(1);
                    gameObject.SetActive(false);
                    return;
                }
                currentAIPointIndex = 0;
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        raceManager = GameObject.Find("RaceManager").GetComponent<RaceManager>();
        foreach (AIPoint aipoint in aipoints)
        {
            aipoint.Subscribe(this);
        }
    }

    // Update is called once per frame
    void Update()
    { }
    void FixedUpdate()
    {
        if (aipoints.Count == 0) // Si la liste de points de repère est vide, il n'y a rien à faire
            return;

        if (raceManager.GetRaceStatus() == 0 && !isInvisible) // Si la course n'est pas commencée, ne rien faire sauf pour la voiture invisible
        {
            return;
        }

        AIPoint nextAIPoint = aipoints[currentAIPointIndex];
        Vector3 direction = nextAIPoint.transform.position - transform.position;
        float speed = direction.magnitude > 0 ? forwardMoveSpeed : 0;
        rg.AddForce(this.transform.forward * speed, ForceMode.Acceleration);
        float distance = direction.magnitude;
        float angle = Vector3.SignedAngle(transform.forward, direction, Vector3.up);
        if (Math.Pow(angle, 2) > 9 && distance > 1) // Si l'angle est supérieur à 3 degrés, tourner
        {
            float rotation = angle > 0 ? steerSpeed : -steerSpeed;
            transform.Rotate(0, rotation * Time.fixedDeltaTime, 0, Space.World);
        }
    }

    public void addTurn()
    {
        currentTurn++;
    }

    public int getTurn()
    {
        return currentTurn;
    }

    public void setLastCheckpoint(int checkpoint)
    {
        lastPlayerCheckpoint = checkpoint;
    }

    public int getLastCheckpoint()
    {
        return lastPlayerCheckpoint;
    }

    
    public void reset(PlacementManager placementManager)
    {
        currentTurn = 0;
        lastPlayerCheckpoint = -1;
        currentAIPointIndex = 0;
        if (isInvisible)
        {
            transform.position = placementManager.GetInvisibleCarPosition();
        }
        else
        {
            if (placementManager != null) {
                placementManager = GameObject.Find("Grid").GetComponent<PlacementManager>();
                transform.position = placementManager.GetStartingPosition(initialPlacement);
            } else {
                transform.position = new Vector3(0, 0, 0);
            }
        }
        transform.rotation = Quaternion.Euler(0, 0, 0);
        Debug.Log("reset car " + gameObject.name + " to " + transform.position + "(" + initialPlacement + ")");
    }

    public void setInitialPlacement(int placement)
    {
        initialPlacement = placement;
    }

    public int getInitialPlacement()
    {
        return initialPlacement;
    }
}
