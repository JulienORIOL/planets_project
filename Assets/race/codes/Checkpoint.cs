using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Checkpoint : MonoBehaviour
{
    public UnityEvent<GameObject, Checkpoint> onCheckpointEnter;
    private Dictionary<GameObject, float> raceTimes = new Dictionary<GameObject, float>();

    void OnTriggerEnter(Collider collider)
    {
        // Enregistrez le temps de passage pour cette voiture
        float currentTime = Time.time;
        // Vérifiez si l'objet entrant est étiqueté en tant que joueur
        if (collider.gameObject.CompareTag("Player"))
        {
            if (!raceTimes.ContainsKey(collider.gameObject))
            {
                raceTimes.Add(collider.gameObject, currentTime);
            }
            else
            {
                raceTimes[collider.gameObject] = currentTime;
            }

            // Déclenchez un événement en fournissant l'objet entrant et ce point de contrôle
            onCheckpointEnter.Invoke(collider.gameObject, this);
        }
        else if (collider.gameObject.CompareTag("AI"))
        {
            // Si l'objet entrant n'est pas un joueur, vérifiez s'il s'agit d'une IA
            onCheckpointEnter.Invoke(collider.gameObject, this);
        }
    }

    public void reset() {
        raceTimes.Clear();
    }
}
