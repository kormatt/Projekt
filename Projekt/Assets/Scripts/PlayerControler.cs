using UnityEngine;

//Character Controller is required to run that script
[RequireComponent(typeof(CharacterController))]
public class PlayerControler : MonoBehaviour {

    // BASED ON UNITY DOC EXAMPLE
    private CharacterController controller;
    private Vector3 playerVelocity;
    private bool groundedPlayer;

    public Transform cameraTransform;
    private bool deadSoundPlayed = false;

    [SerializeField]
    private float gravityValue = -9.81f;

    private InputManager inputManager;
    private GunControler gunControler;

    public GameObject ResumeButton;

    public Animator transition;
    private void Start() {
        controller = GetComponent<CharacterController>();
        inputManager = InputManager.Instance;
        gunControler = GunControler.Instance;
        deadSoundPlayed = false;
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
        if (move.x != 0 || move.z != 0)
            GetComponent<AudioSource>().volume = 0.3f;
        else
            GetComponent<AudioSource>().volume = 0f;
        move.Normalize();
        controller.Move(move * Time.deltaTime * PlayerStats.playerSpeed);

        // Changes the height position of the player..
        if (inputManager.PlayerJumped() && groundedPlayer) {
            playerVelocity.y += Mathf.Sqrt(PlayerStats.jumpHeight * -3.0f * gravityValue);
        }

        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);


        if (transform.position.y <= -0)
            GetComponent<Health>().GetDamage(int.MaxValue);


        if (IsDead() && !deadSoundPlayed) {
            Debug.Log("PLAYER HAS DIED");
            FindObjectOfType<AudioManager>().Play("PlayerDeath");
            deadSoundPlayed = true;
            transition.SetTrigger("PlayerHasDied");
            ResumeButton.SetActive(false);            
        }
    }

    public bool IsDead() {
        Health playerHealth = gameObject.GetComponent<Health>();
        if (playerHealth != null && playerHealth.IsDead()) {
            return true;
        }
        return false;
    }
}
