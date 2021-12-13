using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
[RequireComponent(typeof(Rigidbody))]
public class EnemyControler : MonoBehaviour
{
    private Transform rockettarget;
    private Rigidbody rb;

    public float turnspeed = 1f;
    public float rocketflyspeed = 10f;

    public int health = 3;

    // Start is called before the first frame update
    void Start()
    {
 
        //Collider enemyCol = GetComponent<Collider>();
        //enemyCol.isTrigger = true;
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
        if (other.tag == "Player")
            other.GetComponent<PlayerControler>().GetDamage();
    }

    public void GetDamage(int amount) {
        health -= 1;
        if (health <= 0)
            Destroy(gameObject);
    }
}
