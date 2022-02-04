using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateBridge : MonoBehaviour
{
    [SerializeField]
    private GameObject bridge;
    private void Start() {
        transform.parent.LookAt(transform.parent.parent);
        var rotationVector = transform.parent.rotation.eulerAngles;
        rotationVector.x = 0;
        rotationVector.z = 0;
        transform.parent.rotation = Quaternion.Euler(rotationVector);
    }
    private void OnTriggerEnter(Collider other) {
        if (other.tag == "Player") {
            var newbridge = Instantiate(bridge, new Vector3(0, 0, 0), transform.parent.rotation);
            newbridge.transform.parent = transform.parent.parent.transform;
            Destroy(gameObject);
        }
            
    }
}
