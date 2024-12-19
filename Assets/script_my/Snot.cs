using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Snot : MonoBehaviour
{
    public static Snot instance;     

    public Rigidbody2D SnotRigid;      
    public GameObject gameOverPanel;    
    public Button restartButton;        
    public Button menuButton;            
    public Text finalScoreText; 

    public AudioClip bounceSound;      
    private AudioSource audioSource;     

    private float horizontal;          
    private bool isGameOver = false;  

    private ScoreManager scoreManager; 
    public float squashDuration = 0.1f;
    public Vector3 squashScale = new Vector3(1.2f, 0.5f, 1f);

    private Vector3 originalScale;

    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }

        scoreManager = Object.FindFirstObjectByType<ScoreManager>();
        if (scoreManager == null)
        {
            Debug.LogError("ScoreManager не найден на сцене!");
        }

        audioSource = gameObject.AddComponent<AudioSource>();
        if (bounceSound != null)
        {
            audioSource.clip = bounceSound;
        }

        gameOverPanel.SetActive(false);

        restartButton.onClick.AddListener(RestartGame);
        menuButton.onClick.AddListener(GoToMenu);
        originalScale = transform.localScale;
    }

    void FixedUpdate()
    {
        if (isGameOver) return;

         if (Application.platform == RuntimePlatform.Android)
         {
             horizontal = Input.acceleration.x; // Управление акселерометром для сборки
         }

        // Временное управление кнопками для движка
      //  horizontal = 0f;
     //   if (Input.GetKey(KeyCode.A))
     //   {
     //       horizontal = -1f;
     //   }
     //   else if (Input.GetKey(KeyCode.D))
     //   {
      //      horizontal = 1f;
     //   }

        if (horizontal < 0)
        {
            gameObject.GetComponent<SpriteRenderer>().flipX = false;
        }
        else if (horizontal > 0)
        {
            gameObject.GetComponent<SpriteRenderer>().flipX = true;
        }

        SnotRigid.linearVelocity = new Vector2(horizontal * 10f, SnotRigid.linearVelocity.y);
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.name == "DeadZone" && !isGameOver)
        {
            GameOver(); 
        }

   
        if (collision.collider.CompareTag("Platform"))
        {
            SquashEffect(); 
            PlayBounceSound(); 
        }
    }

    void SquashEffect()
    {
        StartCoroutine(SquashCoroutine());
    }

    private IEnumerator SquashCoroutine()
    {
        transform.localScale = squashScale;
        yield return new WaitForSeconds(squashDuration);
        transform.localScale = originalScale;
    }

    void PlayBounceSound()
    {
        if (audioSource != null && bounceSound != null)
        {
            audioSource.Play();
        }
    }

    void GameOver()
    {
        isGameOver = true;
        Time.timeScale = 0f;
        gameOverPanel.SetActive(true);
        int finalScore = scoreManager != null ? scoreManager.GetScore() : 0;
        finalScoreText.text = "Твой Счет: " + finalScore.ToString();
    }

    void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    void GoToMenu()
    {
   

        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }
}
