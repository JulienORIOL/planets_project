using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementVisibilityController : MonoBehaviour
{
    public SpotlightScript spotlightScript; // Référence au SpotlightScript

    void Start()
    {
        MeshRenderer[] renderers = GetComponentsInChildren<MeshRenderer>();
        foreach (MeshRenderer renderer in renderers)
        {
            renderer.enabled = false;
        }
    }

    public void ActivateRenderers()
    {
        // Accède à tous les MeshRenderers dans ce GameObject et ses enfants
        MeshRenderer[] renderers = GetComponentsInChildren<MeshRenderer>();
        foreach (MeshRenderer renderer in renderers)
        {
            renderer.enabled = true; // Active chaque MeshRenderer
        }
    }

    void Update()
    {
        // Vérifie si la référence au SpotlightScript est présente
        if (spotlightScript != null)
        {
            Debug.Log("On a un bail");
            // Vérifie si le niveau actuel dépasse le nombre de rotations de portail
            if (spotlightScript.GetCurrentLevel() >= spotlightScript.GetPortalRotationsLength() - 2)
            {
                Debug.Log("C'est la fin car la valeur du compteur est de spotlightScript.GetCurrentLevel()");
                // Active ce GameObject (l'élément) pour le faire "apparaître"
                ActivateRenderers();
            }
        }
    }
}
