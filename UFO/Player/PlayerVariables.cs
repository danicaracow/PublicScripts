using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerVariables : MonoBehaviour
{
    
    //damage//
    public float base_damage = 2f;
    public float current_damage;
    public int cannon_number = 1;
    public int damage_level = 0;
    
    //health//
    public int health = 100;
    public int maxhealth = 100;
    
    //money//
    public int plutonio_green_counter = 0;
    public int plutonio_red_counter = 0;

    //beam//
    public float beam_ammo;
    public float current_beam_ammo;

    //shield//
    public ShieldScript shield;
    public float shield_ammo;
    public float current_shield_ammo;
    public float shield_regeneration;
    public bool shieldIsActivated;

    //special//
    public float special_ammo;
    public float current_special_ammo;
    public float special_degeneration;
    public bool specialActivated = false;
    public BeamShotMeshOnly beamcannon1;
    public BeamShotMeshOnly beamcannon2;
    public BeamShotMeshOnly beamcannon3;

    //sound//
    public ambience_sounds playerCamera;
    private Player_sounds playerSounds;
    private bool JustOnce = false;
    private bool JustOncedeath = false;
    private bool JustOnceSpecial = false;

    //other variables//
    public MeshRenderer player_mesh;
    public BeamShot beamShot;
    public ParticleSystem smoke;
    public ParticleSystem sparks;
    public GameObject explosionEffect;
    private bool particles_activated = false;
    public HUDManager HUD;





    private void Start()
    {
        current_beam_ammo = beam_ammo;
        current_shield_ammo = shield_ammo;
        current_special_ammo = 0;
        playerSounds = GetComponent<Player_sounds>();
        
    }

    void Update()
    {
        if (HUDManager.gameIsPaused == false)
        {
            //Player heal//
            if (health <= 0)
            {
                health = 0;
                smoke.Stop();
                sparks.Stop();
            }

            if (health <= 30 && particles_activated == false)
            {
                smoke.Play();
                sparks.Play();
                particles_activated = true;
            }

            if (health > 30 && particles_activated == true | health <= 0)
            {
                smoke.Stop();
                sparks.Stop();
                particles_activated = false;
            }


            //Player damage//

            current_damage = base_damage * cannon_number;


            //Player dies -> disable mesh//
            if (referenceManager.playerisalive == false && player_mesh.enabled == true)
            {
                player_mesh.enabled = false;
                Instantiate(explosionEffect, gameObject.transform.position, gameObject.transform.rotation);
                GetComponent<Player_sounds>().PlayDeadSound();
                playerCamera.PlayDeathMusic();
            }



            //Beam ammo//
            if (current_beam_ammo >= 0 && beamShot.isShooting == true && specialActivated == false)
            {
                current_beam_ammo -= 0.5f * Time.deltaTime;
            }


            if (current_beam_ammo <= beam_ammo && beamShot.isShooting == false && !Input.GetMouseButton(0) | EventSystem.current.IsPointerOverGameObject())
            {
                current_beam_ammo += 0.2f * Time.deltaTime;
            }


            ///SPECIAL///

            // Activate/Deactivate Special //
            if (Input.GetKey("space") && current_special_ammo >= special_ammo)
            {
                specialActivated = true;
                current_beam_ammo = beam_ammo;
            }

            if (current_special_ammo <= 0)
            {
                specialActivated = false;
                JustOnceSpecial = false;
            }

            // Special Ready Sound //
            if (current_special_ammo >= special_ammo && JustOnceSpecial == false)
            {
                playerSounds.SpecialReadySound();
                JustOnceSpecial = true;
            }


            // Special Ammo //
            if (specialActivated == true && current_special_ammo >= 0)
            {
                current_special_ammo -= special_degeneration * Time.deltaTime;
            }
            if (current_special_ammo >= special_ammo)
            {
                current_special_ammo = special_ammo;
            }



            // Special Damage and Mesh //
            if (specialActivated == true)
            {
                cannon_number = 2;
                beamcannon1.beamEnabled = false;
                beamcannon2.beamEnabled = true;
                beamcannon3.beamEnabled = true;
            }
            else
            {
                cannon_number = 1;
                beamcannon1.beamEnabled = true;
                beamcannon2.beamEnabled = false;
                beamcannon3.beamEnabled = false;
            }

            //special MUSIC//
            if (specialActivated == true && JustOnce == false)
            {
                playerCamera.PlayRaveUFO();
                playerCamera.PlayYeehaSound();
                JustOnce = true;
                playerCamera.audiosourceingameMusic.Pause();
            }

            if (specialActivated == false && JustOnce == true)
            {
                playerCamera.StopRaveUFO();
                JustOnce = false;
                playerCamera.audiosourceingameMusic.UnPause();
            }


            //shield//

            if (shield.shieldIsActivated == false)
            {
                shieldIsActivated = false;
            }
            else
            {
                shieldIsActivated = true;
            }


        }
        
    }

    public void GetDamaged(int damage_taken)
    {
        health -= damage_taken;
        HUD.vanish_speed_damage += 1;
    }


}
