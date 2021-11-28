using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControler : MonoBehaviour
{
    private Transform rockettarget;
    private Rigidbody rb;

    public float turnspeed = 1f;
    public float rocketflyspeed = 10f;

    // Start is called before the first frame update
    void Start()
    {
        rockettarget = GameObject.Find("Main Camera").transform;
        rb = GetComponent<Rigidbody>();
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rb.velocity = transform.forward * rocketflyspeed;
        var rocktargrot = Quaternion.LookRotation(rockettarget.position - transform.position);
        rb.MoveRotation(Quaternion.RotateTowards(transform.rotation, rocktargrot, turnspeed));

    }
    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "Enemy") {
            Physics.IgnoreCollision(other.GetComponent<Collider>(), other);
        }
        if (other.tag=="Player")
            Debug.Log("Player!");

    }
}
