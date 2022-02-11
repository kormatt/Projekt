using UnityEngine.AI;
using UnityEngine;
using System.Collections;

public class ShooterController : MonoBehaviour {

	private Transform playerTransform;
	private Vector3 playerPosition;
	private Transform shooterTransform;
	private ShooterAttack attack;

	//ShooterStats
	private bool lookAtPlayer = false;
	public float minimumDistanceFromPlayer = 3.0f;
	public float movementSpeed = 2.5f;
	public float rotationSpeed = 12.5f;
	public float visionRange = 20.0f;

	NavMeshAgent navMeshAgent;
	void Start() {
		GetComponent<Animator>().SetBool("Run", false);

		if (GetComponent<Rigidbody>()) {
			GetComponent<Rigidbody>().freezeRotation = true;
		}
		attack = gameObject.GetComponent<ShooterAttack>();
		navMeshAgent = this.GetComponent<UnityEngine.AI.NavMeshAgent>();
		playerTransform = GameObject.FindWithTag("Player").transform;
		shooterTransform = transform;
	}

	void Update() {
		if (!PlayerIsDead()) {
			ShooterMovement();
		}
	}

	private void ShooterMovement() {
		lookAtPlayer = false;
		GetComponent<Animator>().SetBool("Attack", false);

		playerPosition = new Vector3(playerTransform.position.x, shooterTransform.position.y, playerTransform.position.z);
		float distance = Vector3.Distance(shooterTransform.position, playerTransform.position);

		if (distance <= visionRange && distance > minimumDistanceFromPlayer && !ShooterIsDead()) {
			GetComponent<Animator>().SetBool("Run", true);

			lookAtPlayer = true;
			navMeshAgent.SetDestination(playerPosition);
			attack.Shot();
		}
		else if (distance <= minimumDistanceFromPlayer && !ShooterIsDead()) {
			lookAtPlayer = true;
			GetComponent<Animator>().SetBool("Attack", true);
			attack.Shot();
		}

		else if (distance > visionRange) {
			GetComponent<Animator>().SetBool("Run", false);
		}

		if (!ShooterIsDead()) {
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
			Quaternion rotation = Quaternion.LookRotation(playerPosition - shooterTransform.position);
			shooterTransform.rotation = Quaternion.Slerp(shooterTransform.rotation, rotation, Time.deltaTime * rotationSpeed);
		}
	}

	private bool ShooterIsDead() {
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

	public float GetVisionRange() {
		return visionRange;
	}
}