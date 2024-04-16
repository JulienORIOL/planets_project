using UnityEngine;

public class RotateSelf : MonoBehaviour
{
    // Vitesse de rotation en degrés par seconde
    public float rotationSpeed = 50.0f;

    void Update()
    {
        // Fait tourner l'objet autour de son axe Y à chaque frame
        transform.Rotate(0, rotationSpeed * Time.deltaTime, 0);
    }
}
