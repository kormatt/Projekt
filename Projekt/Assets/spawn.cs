using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawn : MonoBehaviour
{
    public GameObject en;
    private float FireRate = 0.5f;
    private float nextFire;
    // Start is called before the first frame update
    void Start()
    {       
            
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > nextFire) {
            nextFire = Time.time + FireRate;
            Instantiate(en, new Vector3(Random.Range(-5, 5), transform.position.y, Random.Range(-5, 5)), transform.rotation);
        }
        
    }
}
