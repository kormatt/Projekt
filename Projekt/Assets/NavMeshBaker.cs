using UnityEngine;
using UnityEngine.AI;

public class NavMeshBaker : MonoBehaviour
{
    public static NavMeshSurface[] navMeshSurfaces = new NavMeshSurface[16];


    private void Awake() {

    }
    public static void BakeMesh() {
        for (int i = 0; i < navMeshSurfaces.Length; i++) {
            navMeshSurfaces[i].BuildNavMesh();
        }
    }
}
