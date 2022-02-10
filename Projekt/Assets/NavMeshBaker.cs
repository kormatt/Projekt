using UnityEngine;
using UnityEngine.AI;

public class NavMeshBaker : MonoBehaviour
{
    public static NavMeshSurface[] navMeshSurfaces = new NavMeshSurface[8];


    private void Awake() {

    }
    public static void BakeMesh() {
        for (int i = 0; i < navMeshSurfaces.Length; i++) {
            navMeshSurfaces[i].BuildNavMesh();
        }
    }
}
