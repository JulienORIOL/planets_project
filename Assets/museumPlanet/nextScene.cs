using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class nextScene : MonoBehaviour
{
    public string scenename;
    public SpotlightScript spotlightScript; // Référence au script Spotlight

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if(spotlightScript)
            {
                Debug.Log("On a un spotlightScript");
                spotlightScript.GoToNextLevel(); // Informe le SpotlightScript avant de changer de scène
            }
            
            SceneManager.LoadScene(scenename);
        }
    }
}