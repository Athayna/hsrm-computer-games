using UnityEngine;

public class PortalEffect : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.TryGetComponent(out Inventory player))
        {
            return;
        }

        FindObjectOfType<MainSceneManager>().CompleteGameCoroutine();
    }
}
