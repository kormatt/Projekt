using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IslandGenerator : MonoBehaviour
{
    public int amount;
    public GameObject Island;
    private int sepDist;

    // point will be center of the map
    private int[,] map = new int[20,20];
    void Start()
    {
        sepDist = Island.GetComponent<MapCreation>().radius*3;

        int currx = map.GetLength(0)/2;
        int curry = map.GetLength(1)/2;

        transform.position = new Vector3(currx*50, 0, curry * 50);
        map[currx, curry] = 1;

        for (int i = 0; i <= amount; i++) {

            int dirx = Random.Range(-1, 2);
            int diry = Random.Range(-1, 2);

            if (dirx == 0 && diry == 0) {
                dirx = 1;
            }
            do {
                currx += dirx;
                curry += diry;
            } while (map[currx, curry] == 1);
            map[currx, curry] = 1;
        }
        //for(int i = 1; i < map.GetLength(0); i++)
        //    for(int j = 1; j < map.GetLength(1); j++) {
        //        if(map[i,j]==1)
        //            if(map[i+1,j] != 1 && map[i - 1, j] != 1 && map[i , j+1] != 1 && map[i, j-1] != 1) {
                    
        //                if (map[i + 1, j + 1] == 1) {
        //                    Debug.Log((i + 1) + " " + (j + 1)+" dodawanie nowej wyspy - "+ i + (j + 1));
        //                    map[i , j + 1] = 1;
        //                }else if (map[i + 1, j - 1] == 1) { 
        //                    map[i , j - 1] = 1;
        //                }
        //                else if (map[i - 1, j - 1] == 1) {
        //                    map[i , j - 1] = 1;
        //                }
        //                else if (map[i - 1, j + 1] == 1 ) {
        //                    map[i , j - 1] = 1;
        //                }
        //            }
        //    }

        for (int i = 1; i < map.GetLength(0); i++)
            for (int j = 1; j < map.GetLength(1); j++)
                if (map[i, j] == 1) {
                    var island = Instantiate(Island, new Vector3(i * sepDist, 0, j * sepDist), Quaternion.identity);                    
                    island.transform.parent = gameObject.transform;
                    island.name = "Island:"+i.ToString() + " " + j.ToString();
                }





            }
}
