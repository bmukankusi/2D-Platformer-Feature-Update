using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public int playerLives = 3;
    private Vector3 lastSafePosition;

    public GameObject gameOverPanel;
    public Button playButton;
    public Button replayButton;
    public Button quitButton;

    private GameObject player;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    void Start()
    {
        gameOverPanel.SetActive(false);

        playButton.onClick.AddListener(StartGame);
        replayButton.onClick.AddListener(RestartGame);
        quitButton.onClick.AddListener(QuitGame);

        player = GameObject.FindGameObjectWithTag("Player");

        if (player != null)
        {
            lastSafePosition = player.transform.position;
        }
        else
        {
            Debug.LogError("Player not found! Make sure the Player GameObject has the 'Player' tag.");
        }
    }

    public void UpdateSafePosition(Vector3 position)
    {
        lastSafePosition = position;
    }

    public void PlayerDied(Vector3 deathPosition)
    {
        playerLives--;

        if (playerLives > 0)
        {
            Debug.Log("Respawning Player...");
            player.transform.position = lastSafePosition;
        }
        else
        {
            Debug.Log("Game Over!");
            gameOverPanel.SetActive(true);
        }
    }

    void StartGame()
    {
        SceneManager.LoadScene("GameScene");
    }

    void RestartGame()
    {
        playerLives = 3;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void QuitGame()
    {
        Application.Quit();
    }
}
