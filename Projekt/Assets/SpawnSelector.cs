using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnSelector : MonoBehaviour {


    public GameObject[] selectorArr;

    // Start is called before the first frame update
    void Start() {
        float radius = 2f;
        Vector3 currPosition = new Vector3(Random.insideUnitCircle.x*radius, 2f, Random.insideUnitCircle.y * radius);
        currPosition += this.transform.position;
        currPosition.y = 2f;
        int whatToSpawn = Random.Range(0, selectorArr.Length - 1);
        for (int i = 0; i < Random.Range(3, 6); i++) {
            currPosition += new Vector3(Random.insideUnitCircle.x * radius, 0f, Random.insideUnitCircle.y * radius);
            Instantiate(selectorArr[whatToSpawn], currPosition, Quaternion.identity);
        }
    }

}

