using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed;
    public float strafeSpeed = 3f;
    public float minX = -0.35f;
    public float maxX = 1.85f;
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
        float strafe = 0;
        if (Keyboard.current != null)
        {
            if (Keyboard.current.aKey.isPressed) strafe -= 1;
            if (Keyboard.current.dKey.isPressed) strafe += 1;
        }

        Vector3 move = transform.forward * moveSpeed + transform.right * strafe * strafeSpeed;
        rb.linearVelocity = new Vector3(move.x, rb.linearVelocity.y, move.z);

        Vector3 pos = rb.position;
        if (pos.x < minX || pos.x > maxX)
        {
            pos.x = Mathf.Clamp(pos.x, minX, maxX);
            rb.position = pos;
        }
    }
}
