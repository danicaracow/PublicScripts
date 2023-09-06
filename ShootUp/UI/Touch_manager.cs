using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Touch_manager : MonoBehaviour
{
    public PlayerControl playerControl;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void MoveLeft()
    {

        playerControl.leftPressed = true;
        
    }

    public void StopMoveLeft()
    {

        playerControl.leftPressed = false;

    }

    public void MoveRight()
    {
        playerControl.rightPressed = true;

    }

    public void StopMoveRight()
    {
        playerControl.rightPressed = false;

    }

    public void Fire()
    {
        playerControl.firePressed = true;
    }

    public void StopFire()
    {
        playerControl.firePressed = false;
    }
}
