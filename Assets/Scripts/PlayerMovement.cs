using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed;
    public AudioClipGroup step_sound;
    public float stepInterval = 0.5f;

    private float nextStep;

    Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
    }

    void Update()
{
    if (Time.time >= nextStep)
    {
        AudioSourcePool.Instance.Play(step_sound);
        nextStep = Time.time + stepInterval;
    }
}

    private void FixedUpdate()
    {
        Vector3 forwardMovement = transform.forward * moveSpeed;

        rb.linearVelocity = new Vector3(forwardMovement.x, rb.linearVelocity.y, forwardMovement.z);
    }
}
