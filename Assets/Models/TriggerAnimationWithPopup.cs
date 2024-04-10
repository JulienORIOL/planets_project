using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class TriggerAnimationWithPopup : MonoBehaviour
{
    public UnityEvent myEvent;
    public GameObject popup;
    private bool isPlayerNear = false;

    void Start()
    {
        popup.SetActive(false);
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
        }
    }
}
