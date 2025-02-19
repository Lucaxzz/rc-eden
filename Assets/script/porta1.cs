using System.Collections;
using UnityEngine;

public class porta1 : MonoBehaviour
{
    private bool isPlayerNear = false;
    private Animator anim;
    private AudioSource audioSource;
    private BoxCollider2D doorCollider;

    [SerializeField] private string animationTrigger = "Open"; // Nome do trigger no Animator
    [SerializeField] private AudioClip doorSound; // Som da porta

    private bool isDoorOpened = false; 

    private void Start()
    {
        anim = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        doorCollider = GetComponent<BoxCollider2D>();

        anim.speed = 0; // Pausa a animação no início
    }

    private void Update()
    {
        if (isPlayerNear && Input.GetKeyDown(KeyCode.E) && !isDoorOpened)
        {
            isDoorOpened = true;
            anim.speed = 1; // Ativa a animação

            if (doorSound != null && audioSource != null)
            {
                audioSource.PlayOneShot(doorSound);
            }

            if (doorCollider != null)
            {
                doorCollider.enabled = false; // Remove colisão da porta
            }

            StartCoroutine(StopAnimationAtEnd());
        }
    }

    private IEnumerator StopAnimationAtEnd()
    {
        yield return new WaitForSeconds(anim.GetCurrentAnimatorClipInfo(0)[0].clip.length + 0.1f);
        anim.speed = 0; // Pausa a animação no último frame
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNear = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNear = false;
        }
    }
}
