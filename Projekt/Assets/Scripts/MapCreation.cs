using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapCreation : MonoBehaviour
{
    [SerializeField]
    private int radius;
    [SerializeField]
    private GameObject floorObjectWalkable;
    [SerializeField]
    private GameObject floorObjectNonWalkable;
    // Start is called before the first frame update
    void Start()
    {
        for(int i = -radius; i< radius; i++)
            for(int j = -radius; j < radius; j++) {
                float dis = Vector3.Distance(new Vector3(i, 0f, j), new Vector3(0f, 0f, 0f)) + Random.Range(0f,10f);
                if (dis <= radius){
                    var tileWalkable = Instantiate(floorObjectNonWalkable, new Vector3(i, Random.Range(-0.1f, 0.1f), j), Quaternion.identity);
                    var tileNonWalkable = Instantiate(floorObjectWalkable, new Vector3(i, 0f, j), Quaternion.identity);
                    tileWalkable.transform.parent = gameObject.transform;
                    tileNonWalkable.transform.parent = gameObject.transform;
                }
            }
    }


}
