using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpotlightScript : MonoBehaviour
{
    public Light spotlight; // Référence au Spotlight
    public Vector3[] portalRotations = new Vector3[0];
    private int currentLevel; // Compteur de niveau

    private void OnCollisionExit(Collision collision)
    {
        // Vérifie si le joueur entre sur la plateforme
        if (collision.gameObject.CompareTag("Player")) // Assurez-vous que votre joueur a le tag "Player"
        {
            UpdateSpotlightRotation();
        }
    }

    //private void OnCollisionEnter(Collision collision)
    //{
    //    // Vérifie si le joueur quitte la plateforme
    //    if (collision.gameObject.CompareTag("Player"))
    //    {
    //        spotlight.enabled = false; // Désactive le Spotlight
    //    }
    //}

    private void Awake()
    {
        currentLevel = PlayerPrefs.GetInt("CurrentLevel", 0);

        // Pour réinitialiser mon compteur à la main
        //PlayerPrefs.SetInt("CurrentLevel", 0);
        //PlayerPrefs.Save();

        portalRotations = new Vector3[]
        {
            new Vector3(52.197f, 87.567f, 88.067f),
            new Vector3(53.388f, 133.82f, 123.055f),
            new Vector3(54.313f, 183.925f, 159.199f),
            new Vector3(52.699f, 226.3f, 194.462f),
            new Vector3(50.54f, 274.493f, 229.645f)
        };
        // S'assure que le spotlight est orienté vers le premier portail au début
    }

    public void GoToNextLevel()
    {
        Debug.Log("On passe dans la méthode NextLevel");
        currentLevel = (currentLevel + 1) % portalRotations.Length; // Incrémente et boucle le niveau
        PlayerPrefs.SetInt("CurrentLevel", currentLevel);
        PlayerPrefs.Save();
        UpdateSpotlightRotation(); // Met à jour la rotation du spotlight
    }

    void UpdateSpotlightRotation()
    {
        Debug.Log(currentLevel);
        spotlight.transform.rotation = Quaternion.Euler(portalRotations[currentLevel]); // Oriente le spotlight
    }
}
