using UnityEngine;

public class Bush : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource = null;

    void OnTriggerEnter(Collider other)
    {
        if (!other.TryGetComponent(out Player player)) return;
        
        audioSource.Play();
    }
}
