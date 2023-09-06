using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public int SCORE_P1 = 0;
    public int SCORE_P2= 0;

    public int COMBO_P1 = 0;
    public int COMBO_P2 = 0;
    private int[] comboserie = new int[] { 10, 15, 20, 25, 30};

    public bool ingame = false;
    public bool instart = true;

    public float timer = 60;
    public int timeleft = 0;
    private float inittimer = 0;

    private float inittimepositiva = 0;
    private float intervaloramdompositiva = 0;

    public AudioSource Player;
    public AudioSource adlibitum;
    public AudioSource music;


    public GameObject CountdownScreen;
    public GameObject GameMechanicPlayer1;
    public GameObject GameMechanicPlayer2;
    public GameObject GameMechanicSCORES;
    public GameObject GameMechanicGENERAL;
    public GameObject ScoreEndGameObject;
    public TextMeshProUGUI ScorePlayer1;
    public TextMeshProUGUI ScorePlayer2;
    public GameObject P1WIN;
    public GameObject P2WIN;

    public AudioClip[] Startgame;
    public AudioClip[] positivas;
    public AudioClip[] negativas;
    public AudioClip[] combo;
    public AudioClip[] Scorings;
    public AudioClip tilt;
    public AudioClip[] Others;
    public AudioClip[] Musicas;

    public GameObject CD3;
    public GameObject CD2;
    public GameObject CD1;

    public AudioClip[] CDSelected;
    public AudioClip[] CDLanguages;

    // Start is called before the first frame update
    void Start()
    {
        CountdownScreen.SetActive(true);
        GameMechanicPlayer1.SetActive(false);
        GameMechanicPlayer2.SetActive(false);
        GameMechanicSCORES.SetActive(false);
        ScoreEndGameObject.SetActive(false);
        instart = true;
        ingame = false;
        StartCoroutine(Starting());
    }

    void Seleccionaidioma(int grupo) //0-4
    {
        for (int ac=0; ac < CDSelected.Length; ac++)
        {
            CDSelected[ac] = CDLanguages[(grupo*3) + ac];
        }


}

    IEnumerator Starting()
    {
            P1WIN.SetActive(false); 
            P2WIN.SetActive(false); 
            Seleccionaidioma(Random.Range(0, 4));
            yield return new WaitForSecondsRealtime(3);
                play_cd_selected(3);
                CD3.SetActive(true);
                CD2.SetActive(false);
                CD1.SetActive(false);
            yield return new WaitForSecondsRealtime(2);
                play_cd_selected(2);
                CD3.SetActive(false);
                CD2.SetActive(true);
                CD1.SetActive(false);
            yield return new WaitForSecondsRealtime(2);
                play_cd_selected(1);
                CD3.SetActive(false);
                CD2.SetActive(false);
                CD1.SetActive(true);
            yield return new WaitForSecondsRealtime(2);
        //comienza el juego
                CD3.SetActive(false);
                CD2.SetActive(false);
                CD1.SetActive(false);
                PlayStart();
                CountdownScreen.SetActive(false);
                GameMechanicPlayer1.SetActive(true);
                GameMechanicPlayer2.SetActive(true);
                //music.Loop = true;
                music.clip = Musicas[2];
                music.Play();
                GameMechanicSCORES.SetActive(true);
                ScoreEndGameObject.SetActive(false);
                instart = false;
                ingame = true;
                inittimer = Time.time;
                SCORE_P1 = 0;
                SCORE_P2 = 0;
                COMBO_P1 = 0;
                COMBO_P2 = 0;
    }
    public void PlayScorings()
    {
        if (SCORE_P1> SCORE_P2)
        {
            P1WIN.SetActive(true);
            P2WIN.SetActive(false);

        }
        else
        {
            P1WIN.SetActive(false);
            P2WIN.SetActive(true);

        }

        Player.PlayOneShot(Scorings[Random.Range(0, Scorings.Length)]);
        adlibitum.PlayOneShot(Others[0],0.5f);
        music.PlayOneShot(Musicas[3], 0.33f);

    }
    public void PlayStart()
    {
        Player.PlayOneShot(Startgame[Random.Range(0, Startgame.Length)]);
    }


    // Update is called once per frame
    void Update()
    {
        ////////////////// STARTING //////////////////
        if (instart)
        {

        }

        ////////////////// INGAME //////////////////
        if (ingame)
        {
            timeleft = (int) (timer - (Time.time - inittimer));

            if(timeleft == 0)
            {
                FinPartida();
            } else
            {
                ScorePlayer1.text = SCORE_P1.ToString();
                ScorePlayer2.text = SCORE_P2.ToString();
                if (COMBO_P1 + COMBO_P2 > 0) {
                    if (Time.time > inittimepositiva + intervaloramdompositiva)
                    {
                        playPositiva();
                        inittimepositiva = Time.time;
                        intervaloramdompositiva = Random.Range(4, 8);
                    }

                }
            }

            
        } else
        {
            if (timeleft > 0)
            {
                inittimer = Time.time;
                timer = timeleft;
            }
        }
    }

    public void IniciaPartida()
    {
        timer = 60;
        ingame = true;
        inittimer = Time.time;

    }


    public void FinPartida()
    {
        music.Stop();
        ingame = false;
        GameMechanicPlayer1.SetActive(ingame);
        GameMechanicPlayer2.SetActive(ingame);
        GameMechanicGENERAL.SetActive(ingame);
        GameMechanicSCORES.SetActive(ingame);
        ScoreEndGameObject.SetActive(!ingame);
        PlayScorings();
    }

    public void VolverAJugar()
    {
        CountdownScreen.SetActive(true);
        GameMechanicGENERAL.SetActive(true);
        GameMechanicPlayer1.SetActive(false);
        GameMechanicPlayer2.SetActive(false);
        GameMechanicSCORES.SetActive(false);
        ScoreEndGameObject.SetActive(false);
        P1WIN.SetActive(false);
        P2WIN.SetActive(false);
        instart = true;
        ingame = false;
        StartCoroutine(Starting());
    }

    public void playcombo(int punto)
    {
        Player.PlayOneShot(combo[punto-1]);
        
    }

    public void play_cd_selected(int punto)
    {
        Player.PlayOneShot(CDSelected[punto-1]);
    }


    public void playPositiva()
    {
        adlibitum.PlayOneShot(positivas[Random.Range(0, positivas.Length)]);

    }

    public void playNegativa()
    {
        adlibitum.PlayOneShot(negativas[Random.Range(0, negativas.Length)]);
        
    }

    public void playTilt()
    {
        if (ingame)
        {
            Player.PlayOneShot(tilt);
        }
    }

    public void AciertoP1()
    {
        if (ingame)
        {
            if (COMBO_P1 + COMBO_P2 == 0)
            {
                inittimepositiva = Time.time;
                intervaloramdompositiva = Random.Range(4, 8);
            }
            COMBO_P1++;

            if (COMBO_P1 >= 31)
            {
                COMBO_P1 = 31;
            }

            for (int ac = 0; ac < comboserie.Length; ac++)
            {
                if ((COMBO_P1 / comboserie[ac] == 1) && (COMBO_P1 % comboserie[ac] == 0))
                {
                    playcombo(ac + 1);
                }
            }

            SCORE_P1 = SCORE_P1 + COMBO_P1;
        }
    }

    public void FalloP1()
    {
        if (ingame)
        {
            COMBO_P1 = 0;
            if ((int)Random.Range(0, 8) == 5)
            {
                playNegativa();
            }
        }
    }

    public void AciertoP2()
    {
        if (ingame)
        {
            if (COMBO_P1 + COMBO_P2 == 0)
            {
                inittimepositiva = Time.time;
                intervaloramdompositiva = Random.Range(4, 8);
            }
            COMBO_P2++;

            if (COMBO_P2>=31)
            {
                COMBO_P2 = 31;
            }

            for (int ac = 0; ac < comboserie.Length; ac++)
            {
                if ((COMBO_P2 / comboserie[ac] == 1) && (COMBO_P2 % comboserie[ac] == 0))
                {
                    playcombo(ac + 1);
                }
            }

            SCORE_P2 = SCORE_P2 + COMBO_P2;
        }
    }

    public void FalloP2()
    {
        if (ingame)
        {
            COMBO_P2 = 0;
            if ((int)Random.Range(0, 8) == 5)
            {
                playNegativa();
            }
        }
    }
}
