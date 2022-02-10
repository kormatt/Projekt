using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CreateBridge : MonoBehaviour {
    public GameObject Island;
    public GameObject tile;
    private int length;
    [SerializeField]
    private int width;

    public Material TargetMaterial;
    public Material EffectMaterial;

    private float islandState = 1f;


    // Start is called before the first frame update
    void Start() {
        
        PlayerStats.OpenIslands++;
        Debug.Log(PlayerStats.OpenIslands+" - islands open");
        Vector2 pos = new Vector2(transform.position.x - width / 2, transform.position.z);

        length = Island.GetComponent<MapCreation>().radius * 3;
        for (int j = 0; j < width; j++)
            for (int i = 0; i < length; i++) {
                var bridge = Instantiate(tile, new Vector3(pos.x + j, -0.2f, pos.y + i), Quaternion.identity);
                bridge.transform.parent = gameObject.transform;
                bridge.name = "bridge";
                bridge.transform.parent = gameObject.transform;
            }
        CombineMap();
        var newIsland = Instantiate(Island, new Vector3(0, 0, 0), Quaternion.identity);
        newIsland.transform.parent = transform;
        newIsland.transform.localPosition = new Vector3(0f, 0f, 90f);
        newIsland.GetComponent<MapCreation>().bridge = null;

        NavMeshBaker.navMeshSurfaces[PlayerStats.OpenIslands + 9] = GetComponent<NavMeshSurface>();
        //NavMeshBaker.BakeMesh();

    }

    private void Update() {

        if (islandState >= -1f) {
            this.GetComponent<MeshRenderer>().material = EffectMaterial;
            this.GetComponent<MeshRenderer>().sharedMaterial.SetFloat("State", islandState);
            islandState -= Time.deltaTime;
        }
        else {
            this.GetComponent<MeshRenderer>().material = TargetMaterial;
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
        MeshCollider meshCollider = gameObject.AddComponent<MeshCollider>();
        meshCollider.sharedMesh = finalMesh;
    }
}
