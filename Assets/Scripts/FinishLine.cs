using UnityEngine;

public class FinishLine : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Rigidbody rb = other.attachedRigidbody;
        if (other.CompareTag("Player") || (rb != null && rb.CompareTag("Player")))
        {
            LevelManager.Instance.FinishLevel();
        }
    }
}
