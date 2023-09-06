using UnityEngine;

public class Timer : MonoBehaviour
{
    float fullhp = 0f;
    float damage = 1f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (fullhp >= 0 && Input.GetMouseButton(0))
        {
            fullhp += damage * Time.deltaTime;
            
        }
        if (fullhp <= 0)
        {
            fullhp = 0;
        }
        if (fullhp > 0.01 && !Input.GetMouseButton(0))
        {
            fullhp -= 1 * Time.deltaTime;
        }
        print(fullhp);
        if (fullhp >= 5)
        {
            Destroy(gameObject);
        }
    }
}
