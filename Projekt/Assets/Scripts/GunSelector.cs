using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunSelector : MonoBehaviour
{
    public int type;

    private Vector3 _startPosition;

    private void Start() {
        _startPosition = transform.position;
    }

    private void Update() {
        transform.position = _startPosition + new Vector3(0.0f, Mathf.Sin(Time.time) / 5, 0.0f);
    }

    private void OnTriggerEnter(Collider other) {
        if (other.tag == "Player") {
           Affect();
            Destroy(transform.parent.gameObject);

        }
    }
    
    private void Affect() {
        GunControler gunControler = GunControler.Instance;
        gunControler.SetWeapon(type);
    }
}
