using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource = null;
    [SerializeField] private GameObject invisibilityEffect = null;
    private float horizontalInput;
    private float verticalInput;

    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");

        // play footstep sound when moving
        if (Input.GetAxis("Horizontal") != 0f|| Input.GetAxis("Vertical") != 0f) {
            if (!audioSource.isPlaying) {
                audioSource.Play();
            }
        } else {
            audioSource.Stop();
        }
    }

    public void SetInvisibility(bool invisible) {
        invisibilityEffect.SetActive(invisible);
    }
}
