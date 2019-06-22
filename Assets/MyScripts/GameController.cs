using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEditor;

public class GameController : MonoBehaviour
{
    private static GameController instance;
    public static GameController Instance { get { return instance; } }

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            DestroyImmediate(gameObject);
        }
        // DontDestroyOnLoad(gameObject);

        //  Screen.sleepTimeout = SleepTimeout.NeverSleep;

    }

    public GameObject player;
    public int playerHealth = 0;
    public Canvas Menu;
    public Canvas HUD;
    public Text scoreText;
    public Text finalScoreText;
    public RawImage gameOver;
    public Text healthText;
    public AudioSource backgroundMusic;
    public AudioSource inGameMusic;
    [Range(0.0f, 1.0f)]
    public float menuMusicVolume;
    [Range(0.0f,1.0f)]
    public float gameMusicVolume;
    private Slider volumeSlider;
    private int playerScore = 0;
    

    private bool freezeAllMotorFunctions;
    private bool gameStarted;

    // Start is called before the first frame update
    void Start()
    {
        scoreText.text = "Score: " + playerScore;
        healthText.text = "Health: " + playerHealth;
        HUD.enabled = false;
        freezeAllMotorFunctions = true;
        volumeSlider = Menu.gameObject.GetComponentInChildren<Slider>();
        if (volumeSlider != null)
        {
            volumeSlider.onValueChanged.AddListener(delegate { OnVolumeChange(); });
        }
        else
        {
            Debug.Log("Error :: Could not retrieve volume slider");
        }

        // Start off with designer defined music level
        backgroundMusic.volume = menuMusicVolume;
        inGameMusic.volume = gameMusicVolume;
    }

    // Update is called once per frame
    void Update()
    {
        Cursor.visible = false;
        // Bring back menu
        if (Input.GetKeyDown(KeyCode.Escape) && Menu.enabled == false)
        {
            freezeAllMotorFunctions = true;
            HUD.enabled = false;
            Menu.enabled = true;
        }
        else if (Menu.enabled && Input.GetKeyDown(KeyCode.Escape) && (gameStarted == true))
        {
            freezeAllMotorFunctions = false;
            Menu.enabled = false;
            HUD.enabled = true;
            Cursor.visible = false;
        }
        
        
        // Freeze time unless player selects Start
        if (freezeAllMotorFunctions == true)
        {
            Time.timeScale = 0.0f;
            inGameMusic.volume = 0;
            backgroundMusic.volume = menuMusicVolume;
        }
        else
        {
            Time.timeScale = 1.0f;
            backgroundMusic.volume = 0;
            inGameMusic.volume = gameMusicVolume;
        }

        // GameOver
        if (playerHealth <= 0)
        {
            gameStarted = false;
            freezeAllMotorFunctions = true;
            Time.timeScale = 0f;
            Menu.enabled = true;
            HUD.enabled = false;
            gameOver.gameObject.SetActive(true);
            finalScoreText.gameObject.SetActive(true);
            finalScoreText.text = "Score: " + playerScore;
        }
    }

    public void OnStartButtonClick()
    {
        if(playerHealth <= 0)
        {
            SceneManager.LoadScene("MainLevel");
            gameStarted = true;
        }
        // TO DO
        // Hide Menu UI
        // Show HUD UI - health score etc
        HUD.enabled = true;
        Menu.enabled = false;
        freezeAllMotorFunctions = false;
        gameStarted = true;
        inGameMusic.Play();
        inGameMusic.volume = gameMusicVolume;
    }

    public void OnVolumeChange()
    {
        // Best practise is to include a volume mixer so we change one value for all sounds.
        backgroundMusic.volume = volumeSlider.value;
        inGameMusic.volume = gameMusicVolume;
    }

    public void OnQuitButtonClick()
    {
        Application.Quit();
    }

    public void UpdateScore(int score)
    {
        playerScore = playerScore + score;
        scoreText.text = "Score: " + playerScore;
    }

    public void UpdateHealth(int health)
    {
        playerHealth = playerHealth + health;
        healthText.text = "Health: " + playerHealth;
    }

    public int GetHealth()
    {
        return playerHealth;
    }

    public bool GameStart()
    {
        return gameStarted;
    }
}
