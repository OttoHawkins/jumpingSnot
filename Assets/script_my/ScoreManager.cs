using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;          

    public Text scoreText;                     
    public Text finalScoreText;                   
    public Text recordText;                        
    public GameObject gameOverPanel;               

    public Transform playerTransform;            

    private float playerHighestY = 0f;         
    private int score = 0;                         
    private int record = 0;                        

    void Start()
    {
        if (instance == null)
        {
            instance = this; 
        }

        record = PlayerPrefs.GetInt("HighScore", 0);

        gameOverPanel.SetActive(false);
        UpdateScoreText();           
    }

    void Update()
    {
        if (playerTransform.position.y > playerHighestY)
        {
            playerHighestY = playerTransform.position.y;
            UpdateScore();
        }
    }

    // Метод для обновления очков
    void UpdateScore()
    {
        score = Mathf.FloorToInt(playerHighestY);
        UpdateScoreText(); 
    }

    void UpdateScoreText()
    {
        scoreText.text = "Score: " + score.ToString();
    }

    public int GetScore()
    {
        return score;
    }

    public void GameOver()
    {
        if (score > record)
        {
            record = score;
            PlayerPrefs.SetInt("HighScore", record);
            PlayerPrefs.Save();
        }

    
        finalScoreText.text = "Final Score: " + score.ToString();
        recordText.text = "Record: " + record.ToString();
        gameOverPanel.SetActive(true);
        Time.timeScale = 0f;
    }
}
