using UnityEngine;
using System.Collections;


[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(CapsuleCollider))]
[RequireComponent(typeof(Health))]
public class ZoombieControler : MonoBehaviour {

    private bool lookAtPlayer = false;
    private Transform playerTransform;
    private Vector3 playerPosition;
    private GameObject playerObject;
    private Transform zoombie;

    //Stats
    public float minimumDistanceFromPlayer = 2.0f;
    public float movementSpeed = 5f;
    public float rotationSpeed = 12.5f;
    public float visionRange = 20.0f;
    public float randomization = 1.0f;


    public float attackInterval = 0.5f;
    public float countdownToAttack = 0f;
    public float zoombieAttackDamage = 10.0f;

    void Start() {
        GetComponent<CapsuleCollider>().isTrigger = true;
        GetComponent<Rigidbody>().isKinematic = true;
        GetComponent<Rigidbody>().useGravity = true;
        zoombie = transform;

        GetComponent<Rigidbody>().freezeRotation = true;

        playerObject = GameObject.FindWithTag("Player");
        playerTransform = playerObject.transform;

        GetComponent<Animator>().SetBool("Attack", false);
        GetComponent<Animator>().SetBool("Run", false);

        movementSpeed += Random.Range(-randomization, randomization);
        rotationSpeed += Random.Range(-randomization, randomization);


    }

    void Update() {
        if (!PlayerIsDead()) {
            ZoombieMovement();
        }
    }

    private void ZoombieAttack() {
        Health playerHealth = playerObject.GetComponent<Health>();
        if (!playerHealth.IsDead()) {
            if (countdownToAttack < attackInterval) {
                countdownToAttack += Time.deltaTime;
            }

            if (countdownToAttack >= attackInterval) {
                countdownToAttack = 0;

                GetComponent<Animator>().SetBool("Attack", true);
                GetComponent<Animator>().SetBool("Run", false);
                playerHealth.GetDamage(zoombieAttackDamage);
            }
        }
    }

    private void ZoombieMovement() {
        playerPosition = new Vector3(playerTransform.position.x, zoombie.position.y, playerTransform.position.z);

        float distance = Vector3.Distance(zoombie.position, playerTransform.position);
        lookAtPlayer = false;
        //animator set bool run false



        if (distance <= visionRange && distance > minimumDistanceFromPlayer && !ZoombieIsDead()) {
            lookAtPlayer = true;
            zoombie.position = Vector3.MoveTowards(zoombie.position, playerPosition, movementSpeed * Time.deltaTime);

            GetComponent<Animator>().SetBool("Attack", false);
            GetComponent<Animator>().SetBool("Run", true);
        }
        else if (distance <= minimumDistanceFromPlayer && !ZoombieIsDead()) {
            lookAtPlayer = true;
            ZoombieAttack();
        }

        if (!ZoombieIsDead()) {
            RootationToPlayer();
        }
        else //ten else prawdopodobnie nie potrzebny
        {
            if (GetComponent<Rigidbody>()) {
                GetComponent<Rigidbody>().freezeRotation = false;
                //destroy?
            }
        }
    }

    private void RootationToPlayer() {
        if (lookAtPlayer == true) {
            Quaternion rotation = Quaternion.LookRotation(playerPosition - zoombie.position);
            zoombie.rotation = Quaternion.Slerp(zoombie.rotation, rotation, Time.deltaTime * rotationSpeed);
        }
    }

    private bool ZoombieIsDead() {
        Health shooterHealth = gameObject.GetComponent<Health>();
        if (shooterHealth != null) {
            return shooterHealth.IsDead();
        }
        return false;
    }

    private bool PlayerIsDead() {
        Health playerHealth = GameObject.FindWithTag("Player").GetComponent<Health>();
        if (playerHealth != null) {
            return playerHealth.IsDead();
        }
        return false;
    }
}