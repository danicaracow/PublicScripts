using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float movementSpeed;
    [SerializeField] private float stopDistance;
    private PlayerAnimationManager animationManager;
    public bool isShotMode;

    private void Start()
    {
        animationManager = GetComponent<PlayerAnimationManager>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 playerPosition = transform.position;
        Vector2 moveDir = (mousePosition - playerPosition).normalized;

        if (Input.GetKey(KeyCode.LeftShift))
        {
            isShotMode = true;
            animationManager.ShotModeON();
        }
        else
        {
            isShotMode = false;
            animationManager.ShotModeOFF();
        }

        if (!isShotMode)
        {
            
            float playerDistance = (mousePosition - playerPosition).magnitude;

            if (playerDistance > stopDistance)
                transform.position += new Vector3(moveDir.x, moveDir.y, 0f) * movementSpeed * Time.deltaTime;
        }

        transform.up = mousePosition - playerPosition;


    }

    public void ChangeSpeed(float changeSpeedValue)
    {
        movementSpeed += changeSpeedValue;
    }
}

