using UnityEngine;

public class SpawnBoss : MonoBehaviour
{
    public GameObject boss;
    void Update()
    {
        if (PlayerStats.OpenIslands >= 8 && PlayerStats.EnemiesToKill < 1)
            Instantiate(boss, transform.position, Quaternion.identity);
    }
}
