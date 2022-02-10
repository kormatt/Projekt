using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSpawner : MonoBehaviour
{
    public GameObject Boss;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerStats.OpenIslands >= 8 && PlayerStats.EnemiesToKill < 1) {
            Instantiate(Boss, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
            
    }
}
