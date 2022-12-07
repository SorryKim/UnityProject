using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private static GameManager m_instance;
    public Text scoreText;
    public GameObject startUI, gameoverUI, selectUI, inGameUI, menuUI, levelUpUI, GameWinUI;
    public static bool isGamePaused;
    public float score;
    public int levelUpButton;
    public float EXP = 0;
    public bool isGameover {  get; private set; }
    public bool isGameStart;
    public EnemySpawner enemySpawner;
    public Player player;
    public Weapon weapon;
    public AudioSource audioSource;

    public static GameManager instance
    {
        get
        {
            if(m_instance == null)
            {
                m_instance = FindObjectOfType<GameManager>();
            }
            return m_instance;
        }
    }

    private void Awake()
    {
        if(instance != this)
        {
            Destroy(gameObject);
        }
        score = 0;
        isGameStart = false;
        isGamePaused = true;
        audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        
    }

    private void Update()
    {

        // esc 누른 경우
        if (Input.GetButtonDown("Cancel"))
        {
            if (menuUI.activeSelf)
            {
                menuUI.SetActive(false);
                OnApplicationResume();
            }
            else
            {
                menuUI.SetActive(true);
                OnApplicationPause();
            }
        }
            
        if(EXP > 1000)
        {
            EXP = 0;
            player.level++;
            if(player.level <= 10)
                LevelUp();
        }

        UpdateScoreText();
    }
    
    // 플레이어가 죽은 경우
    public void EndGame()
    {
        gameoverUI.SetActive(true);
        OnApplicationPause();
        isGameover = true;
    }

    // 처음에 시작을 누른 경우
    public void InClickStartButton()
    {
        audioSource.Play();
        startUI.SetActive(false);
        selectUI.SetActive(true);
    }

    // select YES btn
    public void onYesButton()
    {
        audioSource.Play();
        selectUI.SetActive(false);
        inGameUI.SetActive(true);
        isGameStart = true;
        OnApplicationResume();

    }
    public void onNoButton()
    {
        audioSource.Play();
        selectUI.SetActive(false);
        inGameUI.SetActive(true);
        isGameStart = true;
        OnApplicationResume();
    }

    public void UpdateScoreText()
    {
        scoreText.text = "SCORE : " + score;
    }

    public void GameExit()
    {
        Application.Quit();
    }

    public void OnApplicationPause()
    {
        Time.timeScale = 0;
    }

    public void OnApplicationResume()
    {
        Time.timeScale = 1f;
    }

    public void MenuResume()
    {
        audioSource.Play();
        menuUI.SetActive(false);
        OnApplicationResume();
    }

    public void OnRestart()
    {
        audioSource.Play();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void LevelUp()
    {
        OnApplicationPause();
        levelUpButton = 0;
        levelUpUI.SetActive(true);
        
    }

    public void selectLv()
    {
        switch (levelUpButton)
        {
            case 1:
                player.speed += 1f;
                break;
            case 2:
                player.startingHealth += 20f;
                player.health += 10f;
                break;
            case 3:
                weapon.damage += 10f;
                weapon.timeBetFire += 50f;
                break;
        }

    }

    public void OnLevelUpButton1()
    {
        levelUpButton = 1;
        selectLv();
        OnApplicationResume();
        levelUpUI.SetActive(false);
    }

    public void OnLevelUpButton2()
    {
        levelUpButton = 2;
        selectLv();
        OnApplicationResume();
        levelUpUI.SetActive(false);
    }

    public void OnLevelUpButton3()
    {
        levelUpButton = 3;
        selectLv();
        OnApplicationResume();
        levelUpUI.SetActive(false);
    }

    public void WinGame()
    {
        GameWinUI.SetActive(true);
    }

}
