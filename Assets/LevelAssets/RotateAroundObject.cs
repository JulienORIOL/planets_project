using UnityEngine;

public class RotateAroundObject : MonoBehaviour
{
    // Référence à l'objet autour duquel cet objet va tourner
    public GameObject pivotObject;

    // Vitesse de rotation en degrés par seconde
    public float rotationSpeed = 50.0f;

    void Update()
    {
        if (pivotObject != null)
        {
            // Fait tourner l'objet autour du pivotObject, ici autour de l'axe Y pour une rotation horizontale
            transform.RotateAround(pivotObject.transform.position, Vector3.up, rotationSpeed * Time.deltaTime);
        }
    }
}
