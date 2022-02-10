using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{


    // Update is called once per frame
    void Update()
    {
        GetComponent<Text>().text = "Zabito: " + PlayerStats.EnemiesKilled;
    }
}
