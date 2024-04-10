using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalVisibilityController : MonoBehaviour
{
    public SpotlightScript spotlightScript; // Référence au SpotlightScript

    void Update()
    {
        // Vérifie si la référence au SpotlightScript est présente
        if (spotlightScript != null)
        {
            // Vérifie si le niveau actuel dépasse le nombre de rotations de portail
            if (spotlightScript.GetCurrentLevel() >= spotlightScript.GetPortalRotationsLength())
            {
                // Désactive ce GameObject (le portail) pour le faire "disparaître"
                gameObject.SetActive(false);
            }
        }
    }
}
