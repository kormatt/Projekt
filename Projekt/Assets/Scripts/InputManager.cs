using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour {
    private PlayerControls playerControls;


    //Singleton pattern 
    private static InputManager _instance;
    public static InputManager Instance {
        get {
            return _instance;
        }
    }

    private void Awake() {
        //checking if that instance exist, if yes destroy this object, singleton pattern 
        if (_instance != null && _instance != this) {
            Destroy(this.gameObject);
        }
        else {
            _instance = this;
        }

        //Cursor.visible = false;

        playerControls = new PlayerControls();
    }
    
    private void OnEnable() {
        playerControls.Enable();
    }
    private void OnDisable() {
        playerControls.Disable();
    }


    //Reading input keys defined for movment from input manager
    public Vector2 GetPlayerMovement() {
        return playerControls.Player.Movment.ReadValue<Vector2>();
    }

    //Reading mouse delta defined for movment from input manager
    public Vector2 GetMouseDelta() {
        return playerControls.Player.Look.ReadValue<Vector2>();
    }

    //Reading jump button defined for movment from input manager
    public bool PlayerJumped() {
        return playerControls.Player.Jump.triggered;
    }
}