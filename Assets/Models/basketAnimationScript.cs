using UnityEngine;

public class basketAnimationScript : MonoBehaviour
{
    [SerializeField] private Animator animator;

    void Start()
    {
        // Assure-toi que cet objet a un composant Animator
        animator = GetComponent<Animator>();
    }

    // Cette fonction est appelée quand un autre Collider entre dans le trigger
    private void OnTriggerEnter(Collider other)
    {
        // Vérifie si l'objet entrant est bien le joueur
        if (other.CompareTag("Player"))
        {
            // Lance l'animation
            Debug.Log("Triggered");

            animator.SetBool("playBall", true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Vérifie si l'objet entrant est bien le joueur
        if (other.CompareTag("Player"))
        {
            // Lance l'animation
            animator.SetBool("playBall", false);
        }
    }
}
