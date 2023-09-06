using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class MenuManager : MonoBehaviour
{
    public GameObject menu;
    public GameObject howToPlay;
    public GameObject transition;
    private Animator transitionAnimator;

    //first buttons//
    public GameObject startButton;
    public GameObject backButton;

    //Sound
    private AudioSource audioSource;
    public AudioClip startSound;

    // Start is called before the first frame update
    void Start()
    {
        transitionAnimator = transition.GetComponent<Animator>();

        audioSource = GetComponent<AudioSource>();

        ResumeGame();
        CleanButtons(startButton);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void HowToPlay()
    {
        menu.SetActive(false);
        howToPlay.SetActive(true);
        CleanButtons(backButton);
    }

    public void Back()
    {
        menu.SetActive(true);
        howToPlay.SetActive(false);
        CleanButtons(startButton);
    }

    public void StartGame()
    {
        audioSource.PlayOneShot(startSound, 1f);
        transition.SetActive(true);
        transitionAnimator.enabled = true;
        Invoke("LoadGame", 4f);
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("QUIT!");
    }

    public void ResumeGame()
    {
        Time.timeScale = 1f;
    }

    public void CleanButtons(GameObject firstButton)
    {
        // Limpiar el objeto seleccionado en el menu
        EventSystem.current.SetSelectedGameObject(null);
        // Asignar el objeto selecinoado en el menu
        EventSystem.current.SetSelectedGameObject(firstButton);
    }

    public void LoadGame()
    {
        SceneManager.LoadScene("Scene 1");
    }
}
