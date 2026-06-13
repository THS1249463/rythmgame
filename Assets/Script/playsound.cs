using UnityEngine;

public class playsound : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip critical, miss;
    void Update()
    {
        
    }

    public void critsound() {
        audioSource.PlayOneShot(critical);
    }
    
}
