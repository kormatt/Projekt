using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunSelector : MonoBehaviour
{
    public int type;
    private void OnTriggerEnter(Collider other) {
        if (other.tag == "Player") {
           Affect();

        }
    }
    
    private void Affect() {
        GunControler gunControler = GunControler.Instance;
        gunControler.SetWeapon(type);
    }
}
