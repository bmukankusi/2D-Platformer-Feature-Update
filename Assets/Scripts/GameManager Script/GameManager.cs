using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public int playerLives = 3; // Player has 3 lives
    private Vector3 lastSafePosition; // Last safe position before falling into water

    public GameObject gameOverPanel; // UI Panel for Game Over
    public Button playButton; // Play button
    public Button replayButton; // Replay button
    public Button quitButton; // Quit button

    void Awake()
    {
        // Singleton pattern to ensure only one GameManager exists
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        // Initially, disable the Game Over panel
        gameOverPanel.SetActive(false);

        // Assign button actions
        playButton.onClick.AddListener(StartGame);
        replayButton.onClick.AddListener(RestartGame);
        quitButton.onClick.AddListener(QuitGame);

        // Store the initial safe position
        lastSafePosition = GameObject.FindGameObjectWithTag("Player").transform.position;
    }

    public void UpdateSafePosition(Vector3 position)
    {
        // Update the last safe position (called when player is on solid ground)
        lastSafePosition = position;
    }

    public void PlayerDied(Vector3 deathPosition)
    {
        playerLives--;

        if (playerLives > 0)
        {
            // Respawn player near the water instead of at the start
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            player.transform.position = lastSafePosition;
        }
        else
        {
            // Show Game Over screen
            gameOverPanel.SetActive(true);
        }
    }

    void StartGame()
    {
        // Load the main gameplay scene
        SceneManager.LoadScene("GameScene");
    }

    void RestartGame()
    {
        // Reset lives and reload scene
        playerLives = 3;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void QuitGame()
    {
        Application.Quit();
    }
}
