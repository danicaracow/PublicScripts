using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeManager : MonoBehaviour
{
    //Hearths//
    public GameObject hearth1;
    public GameObject hearth2;
    public GameObject hearth3;
    public bool ishearth1 = true;
    public bool ishearth2 = true;
    public bool ishearth3 = true;
    private Animator hearth1_animator;
    private Animator hearth2_animator;
    private Animator hearth3_animator;

    // Start is called before the first frame update
    void Start()
    {
        hearth1_animator = hearth1.GetComponent<Animator>();
        hearth2_animator = hearth2.GetComponent<Animator>();
        hearth3_animator = hearth3.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoseOneLife()
    {
        if (ishearth1)
        {
            hearth1_animator.Play("life_destroy_anim", -1, 0f);
            ishearth1 = false;
        }
        else if (ishearth2)
        {
            hearth2_animator.Play("life_destroy_anim", -1, 0f);
            ishearth2 = false;
        }
        else
        {
            hearth3_animator.Play("life_destroy_anim", -1, 0f);
            ishearth3 = false;
        }
    }

    public void GainOneLife()
    {
        if (ishearth2)
        {
            hearth1_animator.Play("New State", -1, 0f);
            ishearth1 = true;
        }
        else if (ishearth3)
        {
            hearth2_animator.Play("New State", -1, 0f);
            ishearth2 = true;
        }

    }
}
