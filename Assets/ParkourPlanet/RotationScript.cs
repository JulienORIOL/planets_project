using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationScript : MonoBehaviour
{
    public float rotationSpeed = 50f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
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

        // Effectuer la rotation de l'objet autour de l'axe vertical (Y)
        transform.Rotate(Vector3.up, horizontalRotationAmount, Space.World);

        // Effectuer la rotation de l'objet autour de l'axe horizontal (X)
        transform.Rotate(Vector3.right, verticalRotationAmount, Space.World);
    }
}
