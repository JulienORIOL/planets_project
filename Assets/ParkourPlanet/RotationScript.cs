using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationScript : MonoBehaviour
{
    public float rotationSpeed = 50f;
    public PlayerCubeDetector playerCubeDetector;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (playerCubeDetector == null) return; // Assurez-vous qu'une référence à PlayerCubeDetector est établie

        // Récupérer la position actuelle du cube depuis PlayerCubeDetector
        Vector3 cubePosition = playerCubeDetector.GetCurrentCubePosition();

        // Récupérer les entrées clavier pour la rotation
        float horizontalInput = 0f;
        float verticalInput = 0f;

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            horizontalInput = Input.GetAxis("Horizontal"); ; // Avancer
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            horizontalInput = Input.GetAxis("Horizontal"); ; // Reculer
        }

        if (Input.GetKey(KeyCode.UpArrow))
        {
            verticalInput = Input.GetAxis("Vertical");  // Aller à gauche
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            verticalInput = Input.GetAxis("Vertical");  // Aller à droite
        }

        // Calculer la rotation en fonction des entrées clavier
        float horizontalRotationAmount = horizontalInput * rotationSpeed * Time.deltaTime;
        float verticalRotationAmount = verticalInput * rotationSpeed * Time.deltaTime;

        transform.RotateAround(cubePosition, Vector3.forward, horizontalRotationAmount);
        transform.RotateAround(cubePosition, Vector3.right, verticalRotationAmount);
    }
}