using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour {

    public static float playerSpeed;
    public static float jumpHeight;
    public static int OpenIslands;


    private float startPlayerSpeed = 10.0f;
    private float startJumpHeight = 1.0f;

    private void Start() {
        jumpHeight = startJumpHeight;
        playerSpeed = startPlayerSpeed;
        OpenIslands = 0;
    }

}
