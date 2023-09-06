using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class PauseManager : MonoBehaviour
{
    //SCREENS//
    public GameObject pauseMenu;
    public GameObject deadScreen;
    public GameObject victoryScreen;
    public GameObject transitionScreen;
    private Animator transitionScreenAnimator;

    //BUTTONS//
    public GameObject resumeButton;
    public GameObject playAgainDeadButton;
    public GameObject playAgainVictoryButton;


    private bool isAlive = true;
    public static bool isPaused = false;
    private bool gameFinished = false;

    //TIMER//
    private float currentTime;
    public float victoryTime;
    public TextMeshProUGUI timerText;

    //DEADCOUNT//
    public TextMeshProUGUI deadCountText;
    public Animator deadCountAnimator;

    private void Awake()
    {
        //WorldManager.pauseManager = this;
    }


    private void Start()
    {
        Debug.Log("restarted");
        transitionScreenAnimator = transitionScreen.GetComponent<Animator>();
        transitionScreen.SetActive(true);
        pauseMenu.SetActive(false);
        deadScreen.SetActive(false);
        Time.timeScale = 1f;
        currentTime = 0f;

        DisplayDeadCountText();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) | Input.GetButtonDown("Start")) // Change the input key as per your preference
        {
            isPaused = !isPaused;
            if (isPaused)
            {
                PauseGame();
            }
            else
            {
                ResumeGame();
            }
        }

        if (isAlive)
        Timer();

        if (currentTime >= victoryTime && gameFinished == false)
        {
            DisplayVictoryScreen();
            gameFinished = true;
        }
        
    }

    private void OnEnable()
    {
        Actions.OnPickUp += PlayPickUpScreenAnimation;
    }

    private void OnDisable()
    {
        Actions.OnPickUp -= PlayPickUpScreenAnimation;
    }

    private void PauseGame()
    {
        Time.timeScale = 0f;
        pauseMenu.SetActive(true);
        CleanButtons(resumeButton);
    }

    public void ResumeGame()
    {
        isPaused = false;
        Time.timeScale = 1f;
        pauseMenu.SetActive(false);
    }

    public void MainMenu()
    {
        pauseMenu.SetActive(false);
        SceneManager.LoadScene("MainMenu");
    }

    public void PlayAgain()
    {
        SceneManager.LoadScene("Scene 1");

    }

    public void DisplayDeadScreen()
    {
        deadScreen.SetActive(true);
        isAlive = false;
        CleanButtons(playAgainDeadButton);
    }

    public void DisplayVictoryScreen()
    {
        Time.timeScale = 0f;
        victoryScreen.SetActive(true);
        CleanButtons(playAgainVictoryButton);
    }

    public void Timer()
    {
        // Calculate the elapsed time since the start
        currentTime += Time.deltaTime;

        timerText.text = currentTime.ToString("0.00");
    }

    public void CleanButtons(GameObject firstButton)
    {
        // Limpiar el objeto seleccionado en el menu
        EventSystem.current.SetSelectedGameObject(null);
        // Asignar el objeto selecinoado en el menu
        EventSystem.current.SetSelectedGameObject(firstButton);
    }

    public void DisplayDeadCountText()
    {
        deadCountText.text = WorldManager.totalEnemiesKilled.ToString();
    }

    public void DisplayDeadCountAnimation()
    {
        deadCountAnimator.Play("dead_count", -1, 0f);
    }

    public void PlayPickUpScreenAnimation()
    {
        transitionScreenAnimator.Play("pick_up_animation", -1, 0f);
    }
}

    
