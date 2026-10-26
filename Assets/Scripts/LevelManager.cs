using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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
    public Key resetKey = Key.R;

    [Header("Death")]
    public GameObject deathScreen;

    [Header("End screens")]
    public Text finishScoreText;
    public Text deathScoreText;
    public Button[] retryButtons;

    private bool finished;
    private float levelLength;

    public bool IsPlaying => !finished;
    public int Score { get; private set; }

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
        if (deathScreen != null)
        {
            deathScreen.SetActive(false);
        }
        foreach (Button button in retryButtons)
        {
            button.onClick.AddListener(ResetLevel);
        }
    }

    void Update()
    {
        if (finished)
        {
            if (Keyboard.current != null && Keyboard.current[resetKey].wasPressedThisFrame)
            {
                ResetLevel();
            }
            return;
        }

        float distanceLeft = Vector3.Distance(player.position, finishPoint.position);
        HUD.Instance.SetLevelProgress(1 - distanceLeft / levelLength);
    }

    public void AddScore(int amount)
    {
        if (finished) return;
        Score += amount;
        HUD.Instance.SetScore(Score);
    }

    public void FinishLevel()
    {
        if (finished) return;
        HUD.Instance.SetLevelProgress(1);
        EndLevel(finishScreen, finishScoreText);
    }

    public void FailLevel()
    {
        if (finished) return;
        EndLevel(deathScreen, deathScoreText);
    }

    private void EndLevel(GameObject screen, Text scoreText)
    {
        finished = true;

        // Stop the player from running past the finish
        player.GetComponent<PlayerMovement>().enabled = false;
        Rigidbody rb = player.GetComponent<Rigidbody>();
        rb.linearVelocity = Vector3.zero;
        rb.isKinematic = true;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        if (scoreText != null)
        {
            scoreText.text = Score.ToString();
        }
        if (screen != null)
        {
            screen.SetActive(true);
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
