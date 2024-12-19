using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverUI : MonoBehaviour
{
    public GameObject gameOverPanel;
    public Text scoreText;           
    public Text highScoreText;      

    private void Start()
    {
        gameOverPanel.SetActive(false); 
    }

    public void ShowGameOverScreen(int score)
    {
        gameOverPanel.SetActive(true);
        scoreText.text = "Your Score: " + score.ToString();

        int highScore = PlayerPrefs.GetInt("HighScore", 0);
        if (score > highScore)
        {
            highScore = score;
            PlayerPrefs.SetInt("HighScore", highScore);
        }

        highScoreText.text = "High Score: " + highScore.ToString();
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void GoToMenu()
    {

        SceneManager.LoadScene("MenuScene");
    }
}
