using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AIPoint : MonoBehaviour
{
    private List<AICarController> subscribers = new List<AICarController>();
    private CarControler playerCar;

    public void Subscribe(AICarController subscriber)
    {
        subscribers.Add(subscriber);
    }

    public void Subscribe(CarControler subscriber)
    {
        playerCar = subscriber;
    }

    public void Unsubscribe(AICarController subscriber)
    {
        subscribers.Remove(subscriber);
    }

    void OnTriggerEnter(Collider collider)
    {
        // if entering object is tagged as AI
        if (collider.gameObject.CompareTag("AI"))
        {
            AICarController aiCarController = collider.gameObject.GetComponent<AICarController>();
            if (aiCarController != null)
            {
                aiCarController.OnAIPointEnter(collider.gameObject, this);
            }
        }
        else if (collider.gameObject.CompareTag("Player"))
        {
            CarControler playerCar = collider.gameObject.GetComponent<CarControler>();
            if (playerCar != null)
            {
                playerCar.OnAIPointEnter(collider.gameObject, this);
            }
        }
    }
}
