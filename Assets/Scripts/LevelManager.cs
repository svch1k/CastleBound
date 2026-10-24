using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;

    [Header("Level")]
    public Transform player;
    public Transform startPoint;
    public Transform finishPoint;

    [Header("Finish")]
    public GameObject finishScreen;
    public float resetDelay = 3;
    public KeyCode resetKey = KeyCode.R;

    private bool finished;
    private float levelLength;

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        player.SetPositionAndRotation(startPoint.position, startPoint.rotation);
        levelLength = Vector3.Distance(startPoint.position, finishPoint.position);

        if (finishScreen != null)
        {
            finishScreen.SetActive(false);
        }
    }

    void Update()
    {
        if (finished)
        {
            if (Input.GetKeyDown(resetKey))
            {
                ResetLevel();
            }
            return;
        }

        float distanceLeft = Vector3.Distance(player.position, finishPoint.position);
        HUD.Instance.SetLevelProgress(1 - distanceLeft / levelLength);
    }

    public void FinishLevel()
    {
        if (finished) return;
        finished = true;

        HUD.Instance.SetLevelProgress(1);

        // Stop the player from running past the finish
        player.GetComponent<PlayerMovement>().enabled = false;
        Rigidbody rb = player.GetComponent<Rigidbody>();
        rb.linearVelocity = Vector3.zero;
        rb.isKinematic = true;

        if (finishScreen != null)
        {
            finishScreen.SetActive(true);
        }

        if (resetDelay > 0)
        {
            Invoke(nameof(ResetLevel), resetDelay);
        }
    }

    public void ResetLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
