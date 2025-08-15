using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    private int score = 0;
    private bool isGameOver = false;

    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private GameObject gameOverUI;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        isGameOver = false;
        UpDateScore();
    } 
    public void AddScore(int points)
    {
        score += points;
        UpDateScore();
    }   
    public void UpDateScore()
    {
        scoreText.text = score.ToString();
    }     
    public void GameOver()
    {
        isGameOver = true;
        Time.timeScale = 0;
        score = 0;
        gameOverUI.SetActive(true);
    }
    public void RestartGame()
    {
        SceneManager.LoadScene("Game");
    }
    public void QuitGame()
    {
        Application.Quit();
    }    
}
