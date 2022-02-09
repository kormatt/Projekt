using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnSelector : MonoBehaviour {


    [SerializeField] private GameObject[] enemies;

    // Start is called before the first frame update
    void Start() {
        float radius = 2f;
        Vector3 currPosition = new Vector3(Random.insideUnitCircle.x*radius, 0f, Random.insideUnitCircle.y * radius);
        currPosition += this.transform.position;
        currPosition.y = 0.5f;
        int whatToSpawn = Random.Range(0, enemies.Length);
        for (int i = 0; i < Random.Range(3, 6); i++) {
            PlayerStats.EnemiesToKill++;
            currPosition += new Vector3(Random.insideUnitCircle.x * radius, 0f, Random.insideUnitCircle.y * radius);
            Instantiate(enemies[whatToSpawn], currPosition, Quaternion.identity);
        }
    }

}

