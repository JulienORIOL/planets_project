using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class PlayerCubeDetector : MonoBehaviour
{
    private Transform currentCubeTransform;
    private float timeSinceLastCube = 0f; // Le temps écoulé depuis le départ du dernier cube

    void Update()
    {
        if (currentCubeTransform == null)
        {
            Debug.Log("On a quitté un cube");
            // Incrémente le timer si le joueur n'est pas sur un cube
            timeSinceLastCube += Time.deltaTime;

            // Si le timer dépasse 2 seconde, recharge la scène
            if (timeSinceLastCube >= 2f)
            {
                // Utilisez SceneManager pour recharger la scène actuelle
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }
        else
        {
            // Réinitialise le timer si le joueur est sur un cube
            timeSinceLastCube = 0f;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Vérifie si l'objet avec lequel on est entré en collision est un cube
        if (collision.gameObject.CompareTag("Cube")) // Assurez-vous que vos cubes ont le tag "Cube"
        {
            currentCubeTransform = collision.transform; // Sauvegarde la référence du cube actuel
            timeSinceLastCube = 0f;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        // Vérifie si l'objet avec lequel on a fini la collision est le cube actuel
        if (collision.transform == currentCubeTransform)
        {
            currentCubeTransform = null; // Réinitialise la référence lorsque le joueur quitte le cube
        }
    }


    // Utilisez cette fonction pour accéder à la position du cube actuel à partir d'autres scripts
    public Vector3 GetCurrentCubePosition()
    {
        if (currentCubeTransform != null)
        {
            return currentCubeTransform.position;
        }

        return Vector3.zero; // Retourne une position par défaut si aucun cube n'est actuellement sous le joueur
    }
}
