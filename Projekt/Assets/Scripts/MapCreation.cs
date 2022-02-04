using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
public class MapCreation : MonoBehaviour
{
    
    public int radius;
    [SerializeField]
    private GameObject floorObjectWalkable;
    [SerializeField]
    private GameObject floorObjectNonWalkable;
    public GameObject bridge;

    // Start is called before the first frame update
    void Start() {
        Vector2 pos = new Vector2(transform.position.x, transform.position.z);
        Debug.Log(pos);
        for (int i = -radius; i < radius; i++)
            for (int j = -radius; j < radius; j++) {

                float dis = Vector3.Distance(new Vector3(i + pos.x, 0f, j + pos.y), new Vector3(pos.x, 0f, pos.y)) + Random.Range(0f, 10f);
                if (dis <= radius) {
                    var tileNonWalkable = Instantiate(floorObjectNonWalkable, new Vector3(i, Random.Range(-0.1f, 0.1f), j), Quaternion.identity);
                    //var tileWalkable = Instantiate(floorObjectWalkable, new Vector3(i, 0.3f, j), Quaternion.identity);
                    tileNonWalkable.transform.parent = gameObject.transform;
                    //tileWalkable.transform.parent = gameObject.transform;
                }
            }
        CombineMap();

        for (int i = -radius; i < radius; i++)
            for (int j = -radius; j < radius; j++) {
                if (i == -1  || i == 0 || i == 1)
                    if (j == -1 || j == 0 || j == 1)
                        if(!(j==0 && i==0)){ 
                            var _bridge = Instantiate(bridge, new Vector3(i, 0, j), Quaternion.identity);
                            _bridge.transform.parent = gameObject.transform;
                    }
            }
    }

    void CombineMap() {
        MeshFilter[] meshFilters = GetComponentsInChildren<MeshFilter>();
        Mesh finalMesh = new Mesh();
        CombineInstance[] mapPeace = new CombineInstance[meshFilters.Length];

        for (int i = 0; i < meshFilters.Length; i++) {
            if (meshFilters[i].transform == transform)
                continue;
            mapPeace[i].subMeshIndex = 0;
            mapPeace[i].mesh = meshFilters[i].sharedMesh;
            mapPeace[i].transform = meshFilters[i].transform.localToWorldMatrix;
            
        }
        finalMesh.CombineMeshes(mapPeace);
        finalMesh.Optimize();
        GetComponent<MeshFilter>().sharedMesh = finalMesh;

        for (int i = 0; i < transform.childCount; i++)
            Destroy(transform.GetChild(i).gameObject);
            //transform.GetChild(i).gameObject.SetActive(false);
        MeshCollider meshCollider = gameObject.AddComponent<MeshCollider>();
        meshCollider.sharedMesh = finalMesh;
    }


}
