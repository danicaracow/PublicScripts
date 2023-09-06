using UnityEngine;

public class Abduction : MonoBehaviour
{
    public GameObject indicator;
    public float speed = 500f;
    private Vector3 height = new Vector3(0f, 0.05f, 0f);
    public GameObject abductionLights;
    LayerMask hitLayers;
    public AudioSource sound;
    private bool JustOnce = false;
    private Abductable abductable;

    void Start()
    {
        hitLayers = LayerMask.GetMask("Player") | LayerMask.GetMask("map_limits") | LayerMask.GetMask("Tank");
        abductionLights.SetActive(true);
    }

    private void Update()
    {
        if (HUDManager.gameIsPaused == false && referenceManager.playerisalive == true)
        {
            //Abduction of abductable-tagged objects//
            if (Input.GetKey(KeyCode.Mouse1))
            {
                abductionLights.SetActive(true);

                if (JustOnce == false)
                {
                    sound.Play();
                    JustOnce = true;
                }
            }
            else
            {
                abductionLights.SetActive(false);
                sound.Stop();
                JustOnce = false;
            }

            //LayerMask hitLayers = LayerMask.GetMask("Player") | LayerMask.GetMask("map_limits"); (moved to Start())
            if (Physics.SphereCast(transform.position, 1f, transform.TransformDirection(Vector3.down), out RaycastHit hitinfo, 100f, ~hitLayers))
            {

                if (hitinfo.transform.tag == "plutonio_green" | hitinfo.transform.tag == "plutonio_red")
                {
                    abductable = hitinfo.transform.GetComponent<Abductable>();

                    if (Input.GetKey(KeyCode.Mouse1))
                    {
                        abductable.isFlying = true;
                    }
                    else
                    {
                        abductable.isFlying = false;
                    }
                }
                else if(abductable != null)
                {
                    abductable.isFlying = false;
                }

                


            }            
                


            //Indicator placement over the ground//
            RaycastHit[] hits = Physics.RaycastAll(transform.position, transform.TransformDirection(Vector3.down), 30f);
            {
                foreach (RaycastHit hit in hits)
                {
                    if (hit.collider.tag == "Ground")
                    {
                        indicator.transform.position = hit.point + height;
                    }
                }


            }

        }

    }
        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position + transform.TransformDirection(Vector3.down) * 10f, 1f);
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.down) * 20f, Color.red);
        }

}
