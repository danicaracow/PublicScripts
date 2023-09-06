using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneManager : MonoBehaviour
{
    //Telon//
    public Animator telonLeftAnimator;
    public Animator telonRightAnimator;


    //Scenes//
    public GameObject splashScene;
    public GameObject gameSelectionScene;
    public GameObject controlsScene;
    public GameObject gameplayScene;
    public GameObject winnerScene;
    public AudioSource music;
    public AudioClip[] musicas;

    //Winner//
    public GameObject player1Wins;
    public GameObject player2Wins;

    //Buttons/
    public GameObject jugarSplashButton;
    public GameObject twoPlayersButton;
    public GameObject jugarControlesButton;
    public GameObject volverAJugarButton;
    public GameObject menuButton;
    public GameObject salirButton;
    public GameObject MANAGEROBJECT;
    
    void Start()
    {
        StartCoroutine(Starting());
    }


    IEnumerator Starting()
    {
        yield return new WaitForSecondsRealtime(0.5f);
        music.clip = musicas[0];
        music.Play();

    }

    public void GameSelectionClose()
    {
        music.Stop();
        music.clip = musicas[1];
        music.Play();
        Debug.Log("Hit");
        telonLeftAnimator.SetTrigger("Close");
        telonRightAnimator.SetTrigger("Close");
        //splashScene.SetActive(false);
        //gameSelectionScene.SetActive(true);
        Invoke("GameSelectionOpen", 2f);
        
    }

    public void ControlsClose()
    {
        //music.Stop();
        //music.clip = musicas[1];
        //music.Play();
        telonLeftAnimator.SetTrigger("Close");
        telonRightAnimator.SetTrigger("Close");
        //gameSelectionScene.SetActive(false);
        //controlsScene.SetActive(true);
        Invoke("ControlsOpen", 2f);
    }

    public void GameplayClose()
    {
        music.Stop();
        telonLeftAnimator.SetTrigger("Close");
        telonRightAnimator.SetTrigger("Close");
        //controlsScene.SetActive(false);
        //gameplayScene.SetActive(true);
        Invoke("GameplayOpen", 2f);
    }

    public void WinnerClose()
    {
        telonLeftAnimator.SetTrigger("Close");
        telonRightAnimator.SetTrigger("Close");
        Invoke("WinnerOpen", 2f);
    }


    public void GameSelectionOpen()
    {
        jugarSplashButton.SetActive(false);
        twoPlayersButton.SetActive(true);
        splashScene.SetActive(false);
        gameSelectionScene.SetActive(true);
        telonLeftAnimator.SetTrigger("Open");
        telonRightAnimator.SetTrigger("Open");
        //telonLeftAnimator.Play("Telon_right_anim", -1, 1f);
    }
    public void ControlsOpen()
    {
        twoPlayersButton.SetActive(false);
        jugarControlesButton.SetActive(true);
        gameSelectionScene.SetActive(false);
        controlsScene.SetActive(true);
        telonLeftAnimator.SetTrigger("Open");
        telonRightAnimator.SetTrigger("Open");
    }
    public void GameplayOpen()
    {
        twoPlayersButton.SetActive(false);
        controlsScene.SetActive(false);
        gameplayScene.SetActive(true);
        MANAGEROBJECT.SetActive(true);
        telonLeftAnimator.SetTrigger("Open");
        telonRightAnimator.SetTrigger("Open");

    }

    public void WinnerOpen()
    {
        gameplayScene.SetActive(false);
        winnerScene.SetActive(true);
        volverAJugarButton.SetActive(true);
        menuButton.SetActive(true);
        salirButton.SetActive(true);
        telonLeftAnimator.SetTrigger("Open");
        telonRightAnimator.SetTrigger("Open");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
