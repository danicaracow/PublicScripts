using UnityEngine;
using UnityEngine.EventSystems;

public class BeamShot : MonoBehaviour
{
    public PlayerVariables playerVariables;
    public GameObject playerCamera;
    public Transform reticula;
    public MeshRenderer beamMesh;
    public bool isShooting = false;
    public Transform beamTransform;
    private Vector3 hitdistance;
    private Vector3 beamDir;
    private Vector3 mousePos;
    public ParticleSystem beam_particles;
    private AudioSource sparks_sound;
    public HUDManager HUDmanager;
    public float shotRange;

    //sound//
    public AudioSource beam_start_sound;
    public AudioSource beam_loop_sound;
    public AudioSource beam_end_sound;
    private bool JustOnce = false;
    private bool JustOnce2 = false;
    private bool loopIsPlaying = false;

    //score//
    private HUDManager hudManager;


    private void Start()
    {
        sparks_sound = beam_particles.GetComponent<AudioSource>();
        hudManager = GameObject.FindGameObjectWithTag("manager").GetComponent<HUDManager>();
    }
    void Update()
    {

        //BEAM POSITION STICK TO THE U.F.O//
        //beamTransform.position = gameObject.transform.position;
        if (HUDManager.gameIsPaused == false)
        {
            BeamDamage();


            //beam sound//
            if (isShooting == true && beam_start_sound.isPlaying == false && JustOnce == false)
            {
                beam_start_sound.Play();
                JustOnce = true;
                JustOnce2 = false;
            }
            else if (isShooting == false)
            {
                JustOnce = false;
            }

            if (isShooting == true && beam_loop_sound.isPlaying == false && JustOnce2 == false)
            {
                beam_loop_sound.Play();
                sparks_sound.Play();
                loopIsPlaying = true;
            }
            else if (isShooting == false && loopIsPlaying == true && JustOnce2 == false || playerVariables.current_beam_ammo <= 0 && JustOnce2 == false)
            {
                beam_loop_sound.Stop();
                sparks_sound.Stop();
                beam_end_sound.Play();
                JustOnce2 = true;
            }
        }


        


        //beam particles//

        




    }

    void BeamDamage()
    {
        if (referenceManager.playerisalive == true && !EventSystem.current.IsPointerOverGameObject())
        {
            if (Input.GetMouseButton(0) && playerVariables.current_beam_ammo > 0)
            {
                isShooting = true;
            }
            else
            {
                isShooting = false;
            }



            LayerMask hitLayers = LayerMask.GetMask("Player") | LayerMask.GetMask("map_limits");


            //calculo el punto de interseccion de un ray de la camara con el raton
            RaycastHit hitmouse;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hitmouse, Mathf.Infinity, ~hitLayers))
                mousePos = hitmouse.point;
            beamDir = (mousePos - gameObject.transform.position).normalized;


            //beam orientation and length cuando no das a nada
            //beamTransform.LookAt(hitmouse.point);
            //hitdistance = hitmouse.point - gameObject.transform.position;
            //beamTransform.localScale = new Vector3(0.02f, 0.02f, hitdistance.magnitude / 2);


            //trazo un ray desde el OVNI hasta el punto calculado anteriormente   
            RaycastHit hit;
            if (Physics.Raycast(gameObject.transform.position, beamDir, out hit, shotRange, ~hitLayers))
            {
                Destroyable destroyable = hit.transform.GetComponent<Destroyable>();
                if (destroyable != null)
                {
                    HUDmanager.CursorHit();

                    if (isShooting == true)
                    {
                        destroyable.hit = true;
                        hudManager.getCurrentScore = true;
                    }
                    else
                    {
                        hudManager.getCurrentScore = false;
                    }
                    
                    
                            
                }
                

                Enemy_destroyable enemy_destroyable = hit.transform.GetComponent<Enemy_destroyable>();
                if (enemy_destroyable != null)
                {
                    if (enemy_destroyable.isDead == false)
                    {
                        HUDmanager.CursorHit();

                        if (isShooting == true)
                        {
                            enemy_destroyable.hit = true;
                            hudManager.getCurrentScore = true;
                        }
                        else
                        {
                            hudManager.getCurrentScore = false;
                        }
                    }

                    else
                    {
                        hudManager.getCurrentScore = false;
                    }
                    

                }
                


                Enemy_destroyable_parts enemy_destroyable_parts = hit.transform.GetComponent<Enemy_destroyable_parts>();
                if (enemy_destroyable_parts != null)
                {
                    if (enemy_destroyable_parts.reference.isDead == false)
                    {
                        hudManager.CursorHit();

                        if (isShooting == true)
                        {
                            enemy_destroyable_parts.hit = true;
                            hudManager.getCurrentScore = true;
                        }
                        else
                        {
                            hudManager.getCurrentScore = false;
                        }
                    }
                    else
                    {
                        hudManager.getCurrentScore = false;
                    }

                }
                

                //Cursor normal cuando no apuntas enemigos//
                if (destroyable == null & enemy_destroyable == null & enemy_destroyable_parts == null)
                {
                    hudManager.CursorNoHit();
                    hudManager.getCurrentScore = false;
                }


                //sparks//
                if (isShooting == true && referenceManager.playerisalive == true)
                {
                    beam_particles.Play();
                }
                if (isShooting == false | playerVariables.current_beam_ammo <= 0)
                {
                    beam_particles.Stop();
                }

                beam_particles.transform.position = hit.point;

                //Beam orientation and length cuando das al target//
                beamTransform.LookAt(hit.point);
                hitdistance = hit.point - gameObject.transform.position;
                beamTransform.localScale = new Vector3(0.02f, 0.02f, hitdistance.magnitude / 2);

            }
            else
            {
                HUDmanager.CursorNoHit();
                beam_particles.Stop();
            }
            
            
            
        }
    }
}
