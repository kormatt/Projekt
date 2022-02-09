using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour {

    public static float playerSpeed;
    public static float jumpHeight;
    public static int OpenIslands;
    public static int EnemiesToKill;

    private float startPlayerSpeed = 10.0f;
    private float startJumpHeight = 1.0f;

    private void Start() {
        EnemiesToKill = 0;
        jumpHeight = startJumpHeight;
        playerSpeed = startPlayerSpeed;
        OpenIslands = 0;
    }

}
