using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    [SerializeField]
    private int durr = 5; 
    private void OnTriggerEnter(Collider other) {
        if (other.tag == "Player") {
            StartCoroutine(Affect(other));
            
        }
    }

     IEnumerator Affect (Collider player) {
        GunControler gunControler = GunControler.Instance;
        gunControler.PowerMoreBullets(1);
        gunControler.PowerFasterFireRate(1);
        gunControler.PowerBetterAccu(1);

        PlayerStats stats = player.GetComponent<PlayerStats>();
        stats.jumpHeight += 0.01f;
        stats.playerSpeed += 0.1f;
        stats.hp = 1;

        GetComponent<MeshRenderer>().enabled = false;
        GetComponent<Collider>().enabled = false;

        yield return new WaitForSeconds(durr);
        stats.hp = 0;
        Destroy(gameObject);
    }

}
