using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuButtons : MonoBehaviour
{
    public Texture2D cursor_menu;
    private Vector2 cursorHotspot;
   
    //Scene transition//
    public GameObject transition_screen;
    public float transition_screen_alpha = 1f;
    public bool scene_switch = false;


    void Awake()
    {
        scene_switch = false;
        transition_screen_alpha = 1f;
    }



    void Update()
    {
        cursorHotspot = new Vector2(0, 0);
        Cursor.SetCursor(cursor_menu, cursorHotspot, CursorMode.Auto);

        //Transition//
        if (scene_switch == false && transition_screen_alpha > 0)
        {
            transition_screen_alpha -= Time.deltaTime * 0.4f;
        }
        transition_screen.GetComponent<Image>().color = new Color(0, 0, 0, transition_screen_alpha);

        if (scene_switch == true && transition_screen_alpha < 1)
        {
            transition_screen_alpha += Time.deltaTime * 0.4f;
        }
    }

    public void StartGame()
    {
        scene_switch = true;
        StartCoroutine(StartGameWaiter());
    }

    public void ExitGame()
    {
        Application.Quit();
        Debug.Log("Quit!");
    }

    private IEnumerator StartGameWaiter()
    {
        yield return new WaitForSeconds(2.5f);
        SceneManager.LoadScene(1);
    }

}
