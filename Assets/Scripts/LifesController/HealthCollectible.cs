using System.Collections;
using UnityEngine;

public class HealthCollectible : MonoBehaviour
{
    [SerializeField] private float healthValue;
    private AudioSource audioSource; 

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>(); 
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            other.GetComponent<Health>().AddHealth(healthValue);
            audioSource.Play(); 

            
            StartCoroutine(DeactivateAfterSound());
        }
    }

    private IEnumerator DeactivateAfterSound()
    {
       
        yield return new WaitForSeconds(audioSource.clip.length);
        gameObject.SetActive(false);
    }
}