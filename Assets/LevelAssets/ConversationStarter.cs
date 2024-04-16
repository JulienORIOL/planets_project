using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DialogueEditor;

public class ConversationStarter : MonoBehaviour
{
    [SerializeField] private NPCConversation myConversation;

    private void OnTriggerStay(Collider other)
    {
        Debug.Log("On rentre en collision");
        if(other.CompareTag("Player"))
        {
            if(Input.GetKeyDown(KeyCode.F))
            {
                Debug.Log("On est bien un Player");
                ConversationManager.Instance.StartConversation(myConversation);
            }
            
        }
    }
}
