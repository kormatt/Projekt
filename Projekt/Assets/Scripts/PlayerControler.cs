using UnityEngine; 

//Character Controller is required to run that script
[RequireComponent(typeof(CharacterController))]
public class PlayerControler : MonoBehaviour
{

    // BASED ON UNITY DOC EXAMPLE
    private CharacterController controller;
    private Vector3 playerVelocity;
    private bool groundedPlayer;

    public Transform cameraTransform;


    //private float playerSpeed = 2.0f;
    //private float jumpHeight = 1.0f;


    [SerializeField]
    private float gravityValue = -9.81f;

    private InputManager inputManager;
    private GunControler gunControler;


    private void Start() {
        controller = GetComponent<CharacterController>();
        inputManager = InputManager.Instance;  
        gunControler = GunControler.Instance;
    }

    void Update() {
        PlayerStats stats = GetComponent<PlayerStats>();

        groundedPlayer = controller.isGrounded;
        if (groundedPlayer && playerVelocity.y < 0) {
            playerVelocity.y = 0f;
        }

        Vector2 movment = inputManager.GetPlayerMovement();
        Vector3 move = new Vector3(movment.x, 0f, movment.y);        
        move = cameraTransform.forward * move.z + cameraTransform.right * move.x;        
        move.y = 0f;

        move.Normalize();
        controller.Move(move * Time.deltaTime * stats.playerSpeed);

        // Changes the height position of the player..
        if (inputManager.PlayerJumped() && groundedPlayer) {
            playerVelocity.y += Mathf.Sqrt(stats.jumpHeight * -3.0f * gravityValue);
        }

        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);



        if (transform.position.y < -10f) {
            transform.position = new Vector3(0f, 10f, 0f);
            
        }
            
    }

    public void GetDamage() {
        if (GetComponent<PlayerStats>().hp <= 0)
            Debug.Log("PLAYER DEAD!");
    }
}
