using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using System.Collections;

public class TriggerAnimationWithPopup : MonoBehaviour
{
    public UnityEvent myEvent;
    public GameObject popup;
    public GameObject prefab;
    public Transform Spawnpoint;
    private bool isPlayerNear = false;

    void Start()
    {
        popup.SetActive(false);
        Debug.Log("Nom : " + popup.name);
        Debug.Log("Autre nom : " + prefab.name);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNear = true;
            popup.SetActive(true);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNear = false;
            popup.SetActive(false);
        }
    }

    void Update()
    {
        if (isPlayerNear && Input.GetKeyDown(KeyCode.E))
        {
            myEvent.Invoke();
            popup.SetActive(false);
            StartCoroutine(InstantiateAfterDelay(8f)); // Start the coroutine with a 4 second delay
        }
    }

    private IEnumerator InstantiateAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        Instantiate(prefab, Spawnpoint.position, Spawnpoint.rotation);
        Debug.Log("On vient de faire apparaître le portail");
    }
}
