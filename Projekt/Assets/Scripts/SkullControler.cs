using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
[RequireComponent(typeof(Rigidbody))]
public class SkullControler : MonoBehaviour {
    private Rigidbody skullRigidbody;
    private Transform skullTransform;

    public float skullTurnSpeed = 1f;
    public float skullSpeed = 10f;
    [SerializeField] private int skullDamage = 3;

    void Start() {
        skullTransform = GameObject.Find("Player").transform;
        skullRigidbody = GetComponent<Rigidbody>();
    }

    void FixedUpdate() {
        if (!GetComponent<Health>().IsDead()) {
            skullRigidbody.velocity = transform.forward * skullSpeed;
            var rocktargrot = Quaternion.LookRotation(skullTransform.position - transform.position);
            skullRigidbody.MoveRotation(Quaternion.RotateTowards(transform.rotation, rocktargrot, skullTurnSpeed));
        }
        else {
            this.GetComponent<BoxCollider>().isTrigger = false;
        }
    }
    private void OnTriggerEnter(Collider other) {
        if (other.tag == "Player")
            other.GetComponent<Health>().GetDamage(skullDamage);
    }
}
