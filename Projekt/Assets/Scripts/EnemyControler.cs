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
    [SerializeField] private int damage = 3;

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
        if (!GetComponent<Health>().IsDead()) {
            rb.velocity = transform.forward * Speed;
            var rocktargrot = Quaternion.LookRotation(targer.position - transform.position);
            rb.MoveRotation(Quaternion.RotateTowards(transform.rotation, rocktargrot, TurnSpeed));
        }
        else {
            this.GetComponent<BoxCollider>().isTrigger = false;
        }

    }
    private void OnTriggerEnter(Collider other) {
        if (other.tag == "Player")
            other.GetComponent<Health>().GetDamage(damage);
        //we we to mi zmien
    }


}
