using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
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
                    //var tileNonWalkable = Instantiate(floorObjectNonWalkable, new Vector3(i, Random.Range(-0.1f, 0.1f), j), Quaternion.identity);
                    var tileWalkable = Instantiate(floorObjectWalkable, new Vector3(i, 0.3f, j), Quaternion.identity);
                    //tileNonWalkable.transform.parent = gameObject.transform;
                    tileWalkable.transform.parent = gameObject.transform;
                }
            }
        CombineMap();
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

        for(int i = 0; i <transform.childCount; i++)
            transform.GetChild(i).gameObject.SetActive(false);
        MeshCollider meshCollider = gameObject.AddComponent<MeshCollider>();
        meshCollider.sharedMesh = finalMesh;
    }


}
