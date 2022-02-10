using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawn : MonoBehaviour
{
    public GameObject en;
    private float spawnRate = 720f;
    private float nextSpawn;
    private float radius = 50f;
    private float spawnAcceleration = 0.05f;
    // Start is called before the first frame update
    void Start()
    {       
            
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > nextSpawn) {
            nextSpawn = Time.time + spawnRate;
            Vector2 randomPlaceInCircle = Random.insideUnitCircle * radius;
            Vector3 targetPlace = new Vector3(randomPlaceInCircle.x, transform.position.y, randomPlaceInCircle.y);
            Quaternion targetRotation = Quaternion.Euler(0, Random.Range(0, 360), 0);            
            Instantiate(en, targetPlace, targetRotation);
        }
        if (spawnRate >= 0.5f)
            spawnRate -= Time.deltaTime * spawnAcceleration;
        
    }
}
