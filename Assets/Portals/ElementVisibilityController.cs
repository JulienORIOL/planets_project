using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementVisibilityController : MonoBehaviour
{
    public SpotlightScript spotlightScript; // Référence au SpotlightScript

    void Start()
    {
        // Pour réinitialiser mon compteur à la main
        //PlayerPrefs.SetInt("ElementVisible", 0);
        //PlayerPrefs.Save();

        if (PlayerPrefs.GetInt("ElementVisible", 0) == 1)
        {
            gameObject.SetActive(true);
            Debug.Log("On a bien set à true");
        }
        else
        {
            gameObject.SetActive(false);
            Debug.Log("On a bien set à false");
        }

        if (spotlightScript != null)
        {
            spotlightScript.OnLevelChanged += CheckVisibility;
        }
    }

    void CheckVisibility()
    {
        Debug.Log("Niveau actuel : " + spotlightScript.GetCurrentLevel());
        Debug.Log("Longueur de ma liste de positions : " + spotlightScript.GetPortalRotationsLength());
        if (spotlightScript.GetCurrentLevel() >= spotlightScript.GetPortalRotationsLength() - 1)
        {
            Debug.Log("On set mon gameObject à true");
            gameObject.SetActive(true);
            PlayerPrefs.SetInt("ElementVisible", 1);
        }
        else
        {
            Debug.Log("On set mon gameObject à false");
            gameObject.SetActive(false);
            PlayerPrefs.SetInt("ElementVisible", 0);
        }
        PlayerPrefs.Save();
    }

    void OnDestroy()
    {
        // Se désabonne de l'événement pour éviter des fuites de mémoire
        if (spotlightScript != null)
        {
            spotlightScript.OnLevelChanged -= CheckVisibility;
        }
    }
}
