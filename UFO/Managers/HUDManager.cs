using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HUDManager : MonoBehaviour
{
    private referenceManager reference;
    public PlayerVariables playerVariables;
    public Text plutonio_green_text;
    public Text plutonio_red_text;
    public Text round_text;
    public Image health_bar;
    public Image beam_bar;
    public Image shield_bar;
    public Image special_bar;
    public Image multiplier_bar;
    private SpawnManager world;
    private float max_health;
    private float current_health;
    private float beam_ammo;
    private float current_beam_ammo;
    private float shield_ammo;
    private float current_shield_ammo;
    private float special_ammo;
    private float current_special_ammo;

    //Score variables//
    public bool getCurrentScore = false;
    private float currentPoints;
    private int currentPoints_int;
    private int totalPoints = 0;
    public int multiplier;
    public float multiplier_timer = 10f;
    public float current_multiplier_timer;
    public Text currentPointsDisplay;
    public Text totalPointsDisplay;
    public Text multiplierDisplay;

    //Damage_screen//
    public GameObject damage_taken;
    public float vanish_speed_damage = 3f;

    //Game Over//
    public GameObject gameOver;

    //Pause//
    public GameObject pause;
    public static bool gameIsPaused = false;

    //Cursor//
    public Texture2D cursor_menu;
    public Texture2D cursor_nohit;
    public Texture2D cursor_hit;
    private Vector2 cursorHotspot;

    //Special//
    public GameObject special_screen;
    public float vanish_speed_special = 6f;
    private bool JustOnce = false;

    //Scene transition//
    public GameObject transition_screen;
    public float transition_screen_alpha = 1f;
    public bool scene_switch = false;

    private void Awake()
    {
        gameOver.SetActive(false);
        pause.SetActive(false);
        scene_switch = false;
    }

    private void Start()
    {
        world = GetComponent<SpawnManager>();
        CursorNoHit();
        totalPointsDisplay.text = totalPoints.ToString();
        current_multiplier_timer = multiplier_timer;
        vanish_speed_damage = 0;
        vanish_speed_special = 0;
    }

    void Update()
    {
        //Money//
        plutonio_green_text.text = playerVariables.plutonio_green_counter.ToString();
        plutonio_red_text.text = playerVariables.plutonio_red_counter.ToString();

        //Round Number//
        round_text.text = "ROUND " + world.current_roundNumber.ToString();

        //Health_bar//
        max_health = playerVariables.maxhealth;
        current_health = playerVariables.health ;
        health_bar.fillAmount = current_health / max_health;


        //Beam_bar//
        beam_ammo = playerVariables.beam_ammo;
        current_beam_ammo = playerVariables.current_beam_ammo;
        beam_bar.fillAmount = current_beam_ammo / beam_ammo;

        //Shield_bar//
        shield_ammo = playerVariables.shield_ammo;
        current_shield_ammo = playerVariables.current_shield_ammo;
        shield_bar.fillAmount = current_shield_ammo / shield_ammo;

        //Special_bar//
        special_ammo = playerVariables.special_ammo;
        current_special_ammo = playerVariables.current_special_ammo;
        special_bar.fillAmount = current_special_ammo / special_ammo;


        //SCORE//
        if (referenceManager.playerisalive == true)
        {
            if (getCurrentScore == true)
            {
                currentPoints += Time.deltaTime * 60 * playerVariables.base_damage;


            }
            else
            {
                currentPoints = 0;
            }
        }
        else
        {
            currentPoints = 0;
        }
        


        currentPoints_int = Mathf.RoundToInt(currentPoints);
        currentPointsDisplay.text = currentPoints_int.ToString();

        if (multiplier > 1)
        {
            if (current_multiplier_timer >= 0)
            {
                current_multiplier_timer -= Time.deltaTime * multiplier;
            }
            if (current_multiplier_timer <= 0)
            {
                multiplier -= 1;
                current_multiplier_timer = multiplier_timer;
            }
        }

        multiplier_bar.fillAmount = current_multiplier_timer / multiplier_timer;

        multiplierDisplay.text = "X" + multiplier.ToString();


        //Game Over//

        if (referenceManager.playerisalive == false)
        {
            gameOver.SetActive(true);
        }

        //Pause//
        if (referenceManager.playerisalive == true)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (gameIsPaused == false)
                {
                    PauseGame();
                }
                else
                {
                    ResumeGame();
                }

            }
        }
        

        //Damage Screen//

        if (vanish_speed_damage >= 0)
        {
            vanish_speed_damage -= Time.deltaTime;
        }

        if (vanish_speed_damage <= 0)
        {
            vanish_speed_damage = 0;
        }

        damage_taken.GetComponent<Image>().color = new Color(255, 255, 255, vanish_speed_damage);

        //Special Screen//

        if (playerVariables.specialActivated == true && JustOnce == false)
        {
            vanish_speed_special += 1f;
            JustOnce = true;
        }
        

        if (vanish_speed_special > 0 && playerVariables.specialActivated == false)
        {
            vanish_speed_special -= Time.deltaTime;
            JustOnce = false;
        }

        special_screen.GetComponent<Image>().color = new Color(255, 255, 255, vanish_speed_special);

        //Transition Screen//
        if (scene_switch == false && transition_screen_alpha > 0)
        {
            transition_screen_alpha -= Time.deltaTime * 0.4f;
        }
        transition_screen.GetComponent<Image>().color = new Color(0, 0, 0, transition_screen_alpha);

        if (scene_switch == true && transition_screen_alpha < 1)
        {
            transition_screen_alpha += Time.deltaTime * 0.4f;
        }

        //CURSOR//
        if (gameIsPaused == true)
        {
            MenuCursor();
        }

    }

    public void MenuCursor()
    {
        cursorHotspot = new Vector2(0, 0);
        Cursor.SetCursor(cursor_menu, cursorHotspot, CursorMode.Auto);
    }

    public void CursorNoHit()
    {
        cursorHotspot = new Vector2(cursor_nohit.width / 2, cursor_nohit.height / 2);
        Cursor.SetCursor(cursor_nohit, cursorHotspot, CursorMode.Auto);
    }

    public void CursorHit()
    {
        cursorHotspot = new Vector2(cursor_hit.width / 2, cursor_hit.height / 2);
        Cursor.SetCursor(cursor_hit, cursorHotspot, CursorMode.Auto);
    }

    public void GainScore()
    {
        totalPoints += currentPoints_int * multiplier;
        totalPointsDisplay.text = totalPoints.ToString();
    }

    public void PlayAgain()
    {
        scene_switch = true;
        StartCoroutine(PlayAgainWaiter());

    }

    public void MainMenu()
    {
        scene_switch = true;
        StartCoroutine(MenuWaiter());
    }
    public void PauseGame()
    {
        pause.SetActive(true);
        Time.timeScale = 0f;
        gameIsPaused = true;
    }

    public void ResumeGame()
    {
        pause.SetActive(false);
        Time.timeScale = 1f;
        gameIsPaused = false;
    }

    private IEnumerator MenuWaiter()
    {
        ResumeGame();
        yield return new WaitForSeconds(2.5f);
        SceneManager.LoadScene(0);
    }
    private IEnumerator PlayAgainWaiter()
    {
        ResumeGame();
        yield return new WaitForSeconds(2.5f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

}
