using UnityEngine; 

//Character Controller is required to run that script
[RequireComponent(typeof(CharacterController))]
public class PlayerControler : MonoBehaviour
{

    // BASED ON UNITY DOC EXAMPLE
    private CharacterController controller;
    private Vector3 playerVelocity;
    private bool groundedPlayer;

    private Transform cameraTransform;

    [SerializeField]
    private float playerSpeed = 2.0f;
    [SerializeField]
    private float jumpHeight = 1.0f;
    [SerializeField]
    private float gravityValue = -9.81f;

    private InputManager inputManager;

    private void Start() {
        controller = GetComponent<CharacterController>();
        inputManager = InputManager.Instance;
        cameraTransform = Camera.main.transform;
    }

    void Update() {
        groundedPlayer = controller.isGrounded;
        if (groundedPlayer && playerVelocity.y < 0) {
            playerVelocity.y = 0f;
        }

        Vector2 movment = inputManager.GetPlayerMovement();
        Vector3 move = new Vector3(movment.x, 0f, movment.y);        
        move = cameraTransform.forward * move.z + cameraTransform.right * move.x;        
        move.y = 0f;

        move.Normalize();
        controller.Move(move * Time.deltaTime * playerSpeed);

        // Changes the height position of the player..
        if (inputManager.PlayerJumped() && groundedPlayer) {
            playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
        }

        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);
    }
}
