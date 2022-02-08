using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
[RequireComponent(typeof(Rigidbody))]
public class EnemyControler : MonoBehaviour
{
    private Transform targer;
    private Rigidbody rb;

    public float TurnSpeed = 1f;
    public float Speed = 10f;

    public int Health = 3;

    // Start is called before the first frame update
    void Start()
    {
 
        //Collider enemyCol = GetComponent<Collider>();
        //enemyCol.isTrigger = true;
        targer = GameObject.Find("Player").transform;
        rb = GetComponent<Rigidbody>();
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Health > 0) {
            rb.velocity = transform.forward * Speed;
            var rocktargrot = Quaternion.LookRotation(targer.position - transform.position);
            rb.MoveRotation(Quaternion.RotateTowards(transform.rotation, rocktargrot, TurnSpeed));
        }

    }
    private void OnTriggerEnter(Collider other) {
        if (other.tag == "Player")
            other.GetComponent<PlayerControler>().GetDamage();
    }

    public void GetDamage(int amount) {
        Health -= 1;
        if (Health <= 0) {
            this.GetComponent<BoxCollider>().isTrigger = false;
        }
            //Destroy(gameObject);
    }
}
