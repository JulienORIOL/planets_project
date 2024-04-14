using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Threading;
using UnityEditor.MPE;
using UnityEngine;


public class CarControler : MonoBehaviour
{
    private float inputX;
    private float inputY;
    public Rigidbody rg;
    public float forwardMoveSpeed;
    public float backwardMoveSpeed;
    public float steerSpeed;
    public RaceManager raceManager;
    private int currentTurn = 0;
    private int lastPlayerCheckpoint = -1;
    public List<AIPoint> aipoints;
    private int currentAIPointIndex = 0;
    private int initialPlacement = 0;

    void Start()
    {
        rg = GetComponent<Rigidbody>();
        raceManager = GameObject.Find("RaceManager").GetComponent<RaceManager>();


        foreach (AIPoint aipoint in aipoints)
        {
            aipoint.Subscribe(this);
        }
    }

    void Update() // Get keyboard inputs
    {
        inputY = Input.GetAxis("Vertical");
        inputX = Input.GetAxis("Horizontal");
        // "v" key to switch to drone mode
    }

    void FixedUpdate() // Apply physics here
    {
        // avant le début de la course, on ne peut pas bouger
        if (raceManager.GetRaceStatus() == 0) return;

        // si la course est en cours, on peut bouger avec les touches directionnelles
        else if (raceManager.GetRaceStatus() == 1)
        {
            float speed = inputY > 0 ? forwardMoveSpeed : backwardMoveSpeed;
            if (inputY > 0.001)
            {
                rg.AddForce(this.transform.forward * speed, ForceMode.Acceleration);
            } else if (inputY < -0.001)
            {
                rg.AddForce(this.transform.forward * speed, ForceMode.Acceleration);
            }
            float rotation = inputX * steerSpeed * Time.fixedDeltaTime;
            transform.Rotate(0, rotation, 0, Space.World);
        }

        else if (raceManager.GetRaceStatus() == 2) // si la course est terminée, l'IA prend le relais
        {
            AIPoint nextAIPoint = aipoints[currentAIPointIndex];
            Vector3 direction = nextAIPoint.transform.position - transform.position;
            float speed = direction.magnitude > 0 ? forwardMoveSpeed * 0.9f : 0; // 0.9f pour ralentir l'IA poru la scène de fin
            rg.AddForce(this.transform.forward * speed, ForceMode.Acceleration);
            float distance = direction.magnitude;
            float angle = Vector3.SignedAngle(transform.forward, direction, Vector3.up);
            if (Math.Pow(angle, 2) * distance > 60) // Si l'angle est supérieur à 2 degrés, tourner
            {
                float rotation = angle > 0 ? steerSpeed : -steerSpeed;
                transform.Rotate(0, rotation * Time.fixedDeltaTime, 0, Space.World);
            }
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


    public void OnAIPointEnter(GameObject gameObject, AIPoint aipoint)
    {
        // Si la course n'est pas terminée, on ne passe pas les points ia
        if (raceManager.GetRaceStatus() != 2) return;
        if (aipoint == aipoints[currentAIPointIndex])
        {
            currentAIPointIndex++;
            if (currentAIPointIndex >= aipoints.Count)
            {
                currentAIPointIndex = 0;
            }
        }
    }

    public void reset()
    {
        currentTurn = 0;
        lastPlayerCheckpoint = -1;
        currentAIPointIndex = 0;
        rg.velocity = new Vector3(0, 0, 0);
        rg.rotation = Quaternion.Euler(0, 0, 0);
        PlacementManager placementManager = GameObject.Find("Grid").GetComponent<PlacementManager>();
        rg.position = placementManager.GetStartingPosition(initialPlacement);
    }

    public void setInitialPlacement(int placement)
    {
        initialPlacement = placement;
    }
}