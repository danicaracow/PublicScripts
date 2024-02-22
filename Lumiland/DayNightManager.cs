using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayNightManager : MonoBehaviour
{
    [SerializeField] private SpriteRenderer fondoSprite;
    [SerializeField] private SpriteRenderer troncoDetrasSprite;
    [SerializeField] private SpriteRenderer troncoDelanteSprite;
    [SerializeField] private SpriteRenderer plantasSprite;

    [SerializeField] private bool isDay;
    [SerializeField] private float alpha;
    [SerializeField] private float transitionSpeed;

    private float current_switchTimer;
    [SerializeField] private float switchTimer;



    private void Start()
    {
        current_switchTimer = switchTimer;
    }

    private void Update()
    {
        if (current_switchTimer > 0)
            current_switchTimer -= Time.deltaTime;
        else
        {
            current_switchTimer = switchTimer;

            if (isDay)
            {
                StartNight();
            }
            else
            {
                StartDay();
            }

            
            
        }
            


        if (isDay)
        {
            if (alpha > 0)
            {
                //Debug.Log("transition to night");
                alpha -= Time.deltaTime * transitionSpeed;
                fondoSprite.color = new Color(1, 1, 1, alpha);
                troncoDetrasSprite.color = new Color(1, 1, 1, alpha);
                troncoDelanteSprite.color = new Color(1, 1, 1, alpha);
                plantasSprite.color = new Color(1, 1, 1, alpha);
            }
            

        }
        else
        {
            if (alpha < 1)
            {
                //Debug.Log("transition to day");
                alpha += Time.deltaTime * transitionSpeed;
                fondoSprite.color = new Color(1, 1, 1, alpha);
                troncoDetrasSprite.color = new Color(1, 1, 1, alpha);
                troncoDelanteSprite.color = new Color(1, 1, 1, alpha);
                plantasSprite.color = new Color(1, 1, 1, alpha);
            }
            
        }
    }

    public void StartDay()
    {
        isDay = true;
        Actions.OnDay();
    }

    public void StartNight()
    {
        isDay = false;
        Actions.OnNight();
    }
}
