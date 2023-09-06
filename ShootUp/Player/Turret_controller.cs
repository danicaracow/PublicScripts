using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret_controller : MonoBehaviour
{
    public GameObject target;
    public bool TargetDetected;
    public float rotationSpeed;
    private TurretScanner turretScanner;

    //Shooting//
    public bool isReloading;
    public float reloadingTime;
    private float currentReloadingTime;
    Animator animator;
    public bool isActivated;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        turretScanner = GameObject.FindWithTag("turret_scanner").GetComponent<TurretScanner>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isActivated)
        {
            if (turretScanner.isActivated != true)
            {
                turretScanner.isActivated = true;
            }

            //Check whether target is destroyed or not
            if (target != null)
            {
                if (target.GetComponent<Parachute>().isFalling == false)
                {
                    TurretAim();
                    TurretFire();
                }
                else
                {
                    TargetDetected = false;
                }
            }
            else
            {
                TargetDetected = false;
            }

            Reload();
        }
        


    }

    public void TurretFire()
    {
        if (isReloading == false)
        {
            animator.SetTrigger("Shot");
            target.GetComponent<Parachute>().health -= 1;
            isReloading = true;
            currentReloadingTime = reloadingTime;
        }

    }

    private void TurretAim()
    {
        //Look at Target//
        Vector2 direction = target.transform.position - transform.position;
        Quaternion targetRotation = Quaternion.FromToRotation(Vector3.up, direction);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }

    private void Reload()
    {
        if (currentReloadingTime > 0 && isReloading == true)
        {
            currentReloadingTime -= Time.deltaTime;
        }
        else
        {
            isReloading = false;
            currentReloadingTime = reloadingTime;
        }
    }
}
