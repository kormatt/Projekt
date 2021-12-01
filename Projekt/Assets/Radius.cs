using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Radius : MonoBehaviour
{
    public Transform player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       if(Vector3.Distance(transform.parent.position, player.position) >= 10f) {
            /*
            if (Vector3.Distance(transform.position, player.position) >= 6f) {
                GetComponent<MeshRenderer>().enabled = false;
                GetComponent<Collider>().enabled = false;
            }*/
            transform.position = new Vector3(transform.position.x, -0.5f*Vector3.Distance(transform.parent.position, player.position)+5, transform.position.z);
        }
        else {            
            GetComponent<MeshRenderer>().enabled = true;
            GetComponent<Collider>().enabled = true;
            transform.position = new Vector3(transform.position.x, 0 , transform.position.z);
        }


    }
}
