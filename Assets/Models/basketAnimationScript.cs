using UnityEngine;

public class basketAnimationScript : MonoBehaviour
{
    private Animator animator;

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
            animator.SetTrigger("StartAnimation"); // Assure-toi que "StartAnimation" est le nom de ton trigger dans l'Animator
        }
    }
}
