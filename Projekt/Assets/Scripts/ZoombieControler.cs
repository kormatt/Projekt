using UnityEngine;
using UnityEngine.AI;
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
	public float movementSpeed = 2.5f;
	public float rotationSpeed = 12.5f;
	public float visionRange = 20.0f;


	public float attackInterval = 0.5f;
	public float countdownToAttack = 0f;
	public float zoombieAttackDamage = 10.0f;

	//cyk
	public float randomImpactOnMovmentSpeed;
	public float randomTarget;
	private Vector3 targetPosition;
	private Transform targetTransform;
	private Transform targetTransform2;
	private Transform targetTransform3;
	private Transform targetTransform4;


	NavMeshAgent navMeshAgent;

	void Start() {

		navMeshAgent = this.GetComponent<NavMeshAgent>();

		GetComponent<CapsuleCollider>().isTrigger = true;
		GetComponent<Rigidbody>().isKinematic = true;
		GetComponent<Rigidbody>().useGravity = true;
		zoombie = transform;

		GetComponent<Rigidbody>().freezeRotation = true;

		playerObject = GameObject.FindWithTag("Player");
		playerTransform = playerObject.transform;

		GetComponent<Animator>().SetBool("Attack", false);
		GetComponent<Animator>().SetBool("Run", false);

		randomImpactOnMovmentSpeed = Random.Range(-1.0f, 3.0f);
		randomTarget = Random.Range(0.0f, 3.0f);

		targetTransform = playerTransform.Find("Target_For_Zoombie1").transform;
		targetTransform2 = playerTransform.Find("Target_For_Zoombie2").transform;
		targetTransform3 = playerTransform.Find("Target_For_Zoombie3").transform;
		targetTransform4 = playerTransform.Find("Target_For_Zoombie4").transform;
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
		SelectTarget();
		playerPosition = new Vector3
			(playerTransform.position.x,
			zoombie.position.y,
			playerTransform.position.z
			);

		float distance = Vector3.Distance(zoombie.position, playerTransform.position);
		lookAtPlayer = false;

		if (distance <= visionRange && distance > minimumDistanceFromPlayer && !ZoombieIsDead()) {
			lookAtPlayer = true;
			//zoombie.position = Vector3.MoveTowards(zoombie.position, targetPosition, (movementSpeed + randomImpactOnMovmentSpeed) * Time.deltaTime);
			navMeshAgent.SetDestination(playerPosition);



			GetComponent<Animator>().SetBool("Attack", false);
			GetComponent<Animator>().SetBool("Run", true);
		}
		else if (distance <= minimumDistanceFromPlayer && !ZoombieIsDead()) {
			lookAtPlayer = true;
			ZoombieAttack();
		}
		else if (distance > visionRange) {
			GetComponent<Animator>().SetBool("Run", false);
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

	private void SelectTarget() {
		if (randomTarget <= 1.0f) {
			if (randomTarget <= 0.5f) {
				targetPosition = new Vector3
				(targetTransform3.position.x,
				zoombie.position.y,
				targetTransform3.position.z
				);
			}
			else {
				targetPosition = new Vector3
				(targetTransform.position.x,
				zoombie.position.y,
				targetTransform.position.z
				);
			}

		}
		else if (randomTarget > 1.0f && randomTarget <= 2.0f) {
			if (randomTarget <= 1.5f) {
				targetPosition = new Vector3
				(targetTransform2.position.x,
				zoombie.position.y,
				targetTransform2.position.z
				);
			}
			else {
				targetPosition = new Vector3
				(targetTransform4.position.x,
				zoombie.position.y,
				targetTransform4.position.z
				);
			}
		}
		else {
			targetPosition = new Vector3
			(playerTransform.position.x,
			zoombie.position.y,
			playerTransform.position.z
			);
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