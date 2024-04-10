using UnityEngine;

public class treeAnimationScript : MonoBehaviour
{
    [SerializeField] private Animator animator;

    void Start()
    {
        // Assure-toi que cet objet a un composant Animator
        animator = GetComponent<Animator>();
    }

    // Cette fonction est appel�e quand un autre Collider entre dans le trigger
    private void OnTriggerEnter(Collider other)
    {
        // V�rifie si l'objet entrant est bien le joueur
        if (other.CompareTag("Player"))
        {
            // Lance l'animation
                    Debug.Log("Triggered");

            animator.SetBool("playTree", true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // V�rifie si l'objet entrant est bien le joueur
        if (other.CompareTag("Player"))
        {
            // Lance l'animation
            animator.SetBool("playTree", false);
        }
    }
}
