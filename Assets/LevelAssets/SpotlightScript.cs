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
        PlayerPrefs.SetInt("CurrentLevel", 3);
        PlayerPrefs.Save();

        portalRotations = new Vector3[]
        {
            new Vector3(125.825f, -53.39301f, -48.38098f),
            new Vector3(124.859f, 1.662994f, -4.427979f),
            new Vector3(129.118f, 55.272f, 48.58501f),
            new Vector3(120.433f, 0f, 0f)
        };
        // S'assure que le spotlight est orienté vers le premier portail au début
    }

    public void GoToNextLevel()
    {
        Debug.Log("On passe dans la méthode NextLevel");
        currentLevel = currentLevel + 1; // Incrémente et boucle le niveau
        PlayerPrefs.SetInt("CurrentLevel", currentLevel);
        PlayerPrefs.Save();
        UpdateSpotlightRotation(); // Met à jour la rotation du spotlight
    }

    void UpdateSpotlightRotation()
    {
        Debug.Log(currentLevel);
        if(currentLevel < portalRotations.Length)
        {
            spotlight.transform.rotation = Quaternion.Euler(portalRotations[currentLevel]); // Oriente le spotlight

            if (currentLevel == portalRotations.Length - 1)
            {
                spotlight.spotAngle = 60;
                spotlight.intensity = 7;
            }
        } else
        {
            Debug.Log("On a pas de position a update");
        }
        
    }

    public int GetCurrentLevel()
    {
        return currentLevel;
    }

    public int GetPortalRotationsLength()
    {
        return portalRotations.Length;
    }
}
