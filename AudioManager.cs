using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioSource clickAudioSource = null;
 
    public void PlayClickSound()
    {
        clickAudioSource.Play();
    }
}
