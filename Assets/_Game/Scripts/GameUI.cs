using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.SocialPlatforms.Impl;


public class GameUI : MonoBehaviour
{
    private int score = 0;
    private bool isGameOver = false;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private GameObject gameOverUI;
    [SerializeField] private Button restartButton;
    [SerializeField] private Button retryButton;
    [SerializeField] private Button quitButton;
    [SerializeField] private Button nextLevel;

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
    public void NextLevel()
    {

    }
}
