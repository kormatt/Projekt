using UnityEngine;
using UnityEngine.AI;
using System.Collections;


[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(CapsuleCollider))]
[RequireComponent(typeof(Health))]
public class EnemyControler : MonoBehaviour {

	private Transform enemyTransform;
	private GameObject playerObject;
	private Vector3 playerPosition;
	private Transform playerTransform;

	//Stats
	private bool lookAtPlayer = false;
	public float attackInterval = 0.5f;
	public float countdownToAttack = 0.0f;
	public float minimumDistanceFromPlayer = 2.0f;
	public float rotationSpeed = 12.5f;
	public float visionRange = 20.0f;
	public float enemyAttackDamage = 10.0f;

	NavMeshAgent navMeshAgent;

	void Start() {
		GetComponent<CapsuleCollider>().isTrigger = true;
		GetComponent<Rigidbody>().isKinematic = true;
		GetComponent<Rigidbody>().useGravity = true;
		GetComponent<Rigidbody>().freezeRotation = true;
		GetComponent<Animator>().SetBool("Attack", false);
		GetComponent<Animator>().SetBool("Run", false);

		navMeshAgent = this.GetComponent<NavMeshAgent>();
		playerObject = GameObject.FindWithTag("Player");
		playerTransform = playerObject.transform;
		enemyTransform = transform;
	}

	void Update() {
		if (!PlayerIsDead()) {
			EnemyMovement();
		}
	}

	private void EnemyAttack() {
		Health playerHealth = playerObject.GetComponent<Health>();
		if (!playerHealth.IsDead()) {
			if (countdownToAttack < attackInterval) {
				countdownToAttack += Time.deltaTime;
			}

			if (countdownToAttack >= attackInterval) {
				GetComponent<Animator>().SetBool("Attack", true);
				GetComponent<Animator>().SetBool("Run", false);

				countdownToAttack = 0;
				playerHealth.GetDamage(enemyAttackDamage);
			}
		}
	}

	private void EnemyMovement() {
		lookAtPlayer = false;

		playerPosition = new Vector3(playerTransform.position.x, enemyTransform.position.y, playerTransform.position.z);
		float distance = Vector3.Distance(enemyTransform.position, playerTransform.position);

		if (distance <= visionRange && distance > minimumDistanceFromPlayer && !EnemyIsDead()) {
			GetComponent<Animator>().SetBool("Attack", false);
			GetComponent<Animator>().SetBool("Run", true);

			lookAtPlayer = true;
			navMeshAgent.SetDestination(playerPosition);
		}
		else if (distance <= minimumDistanceFromPlayer && !EnemyIsDead()) {
			lookAtPlayer = true;
			EnemyAttack();
		}
		else if (distance > visionRange) {
			GetComponent<Animator>().SetBool("Run", false);
		}

		if (!EnemyIsDead()) {
			RootationToPlayer();
		}
		else {
			navMeshAgent.SetDestination(transform.position);
			if (GetComponent<Rigidbody>()) {
				GetComponent<Rigidbody>().freezeRotation = false;
			}
		}
	}

	private void RootationToPlayer() {
		if (lookAtPlayer == true) {
			Quaternion rotation = Quaternion.LookRotation(playerPosition - enemyTransform.position);
			enemyTransform.rotation = Quaternion.Slerp(enemyTransform.rotation, rotation, Time.deltaTime * rotationSpeed);
		}
	}

	private bool EnemyIsDead() {
		Health enemyHealth = gameObject.GetComponent<Health>();
		if (enemyHealth != null) {
			return enemyHealth.IsDead();
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