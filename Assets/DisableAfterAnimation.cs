using UnityEngine;

public class DisableAfterAnimation : MonoBehaviour
{
    public Animator animator;  // Assurez-vous d'assigner cet élément dans l'éditeur Unity
    public float delay = 1f; // Délai supplémentaire après la fin de l'animation si nécessaire
    public bool hasDialogEnded = false;

    private void Start()
    {
        gameObject.SetActive(true);
        Debug.Log("On vient de set le canvas de transition à true");
    }

    void Update()
    {
        // Vérifie si l'animation est terminée
        if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1 && !animator.IsInTransition(0))
        {
            // Attendre un délai supplémentaire
            delay -= Time.deltaTime;
            if (delay <= 0 && hasDialogEnded == false)
            {
                gameObject.SetActive(false); // Désactive le GameObject
                Debug.Log("On vient de set le canvas de transition à false");
            }
        }
    }

    public void EndDialogue()
    {
        // Code à exécuter après la fin du dialogue
        gameObject.SetActive(true); // Réactive l'écran noir ou d'autres éléments
        // Vous pouvez aussi envoyer un message ou déclencher un événement ici
        Debug.Log("Dialogue terminé!");
        hasDialogEnded = true;
    }
}
