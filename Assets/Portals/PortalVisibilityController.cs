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
            if (spotlightScript.GetCurrentLevel() >= spotlightScript.GetPortalRotationsLength() - 1)
            {
                StartCoroutine(DeactivateAfterDelay(2)); // Appelle la coroutine avec un délai de 2 secondes
            }
        }
    }

    IEnumerator DeactivateAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay); // Attend avant de continuer
        gameObject.SetActive(false); // Désactive le GameObject
    }
}
