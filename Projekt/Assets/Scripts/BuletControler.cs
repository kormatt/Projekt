using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(CapsuleCollider))]
public class BuletControler : MonoBehaviour {
    [SerializeField]
    private float rot;
    private float rotspeed;

    // Update is called once per frame
    void Update() {
        transform.Rotate(rotspeed, rotspeed, rotspeed, Space.World);
        transform.parent.Translate(0f, 0f, 0.5f);
    }

    private void Start() {




        var randomNumberX = Random.Range(-90f, 90f);
        var randomNumberY = Random.Range(-90f, 90f);
        var randomNumberZ = Random.Range(-90f, 90f);
        transform.Rotate(randomNumberX, randomNumberY, randomNumberZ);
        rotspeed = Random.Range(-rot, rot);
    }

    private void OnTriggerEnter(Collider other) {
        if (other.tag == "Enemy")
            other.GetComponent<EnemyControler>().GetDamage();
        Destroy(gameObject);

    }
    

}
