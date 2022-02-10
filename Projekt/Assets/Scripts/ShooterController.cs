using UnityEngine;
using System.Collections;

public class ShooterController : MonoBehaviour {

	private bool lookAtPlayer = false;
	private Transform playerTransform;
	private Vector3 playerPosition;
	private Transform shooter;

	private ShooterAttack attack;

	//ShooterStats
	public float minimumDistanceFromPlayer = 3.0f;
	public float movementSpeed = 2.5f;
	public float rotationSpeed = 12.5f;
	public float visionRange = 20.0f;

	//cyk
	public float randomImpactOnMovmentSpeed;
	public float randomTarget;
	private Vector3 targetPosition;
	private Transform targetTransform;
	private Transform targetTransform2;
	private Transform targetTransform3;
	private Transform targetTransform4;

	void Start() {
		shooter = transform;
		if (GetComponent<Rigidbody>()) {
			GetComponent<Rigidbody>().freezeRotation = true;
		}
		playerTransform = GameObject.FindWithTag("Player").transform;

		GetComponent<Animator>().SetBool("Run", false);

		attack = gameObject.GetComponent<ShooterAttack>();

		randomImpactOnMovmentSpeed = Random.Range(-1.0f, 3.0f);
		randomTarget = Random.Range(0.0f, 3.0f);

		targetTransform = playerTransform.Find("Target_For_Shooter (1)").transform;
		targetTransform2 = playerTransform.Find("Target_For_Shooter (2)").transform;
		targetTransform3 = playerTransform.Find("Target_For_Shooter (3)").transform;
		targetTransform4 = playerTransform.Find("Target_For_Shooter (4)").transform;
	}

	void Update() {
		if (!PlayerIsDead()) {
			ShooterMovement();
		}
	}

	private void ShooterMovement() {
		SelectTarget();
		playerPosition = new Vector3(playerTransform.position.x, shooter.position.y, playerTransform.position.z);

		float distance = Vector3.Distance(shooter.position, playerTransform.position);

		lookAtPlayer = false;
		//GetComponent<Animator>().SetBool("Run", false);
		GetComponent<Animator>().SetBool("Attack", false);

		if (distance <= visionRange && distance > minimumDistanceFromPlayer && !ShooterIsDead()) {
			lookAtPlayer = true;
			shooter.position = Vector3.MoveTowards(shooter.position, targetPosition, (movementSpeed + randomImpactOnMovmentSpeed) * Time.deltaTime);
			GetComponent<Animator>().SetBool("Run", true);

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
		else //obiekt martwy
		{
			if (GetComponent<Rigidbody>()) {
				GetComponent<Rigidbody>().freezeRotation = false;
			}
		}
	}

	private void SelectTarget() {
		if (randomTarget <= 1.0f) {
			if (randomTarget <= 0.5f) {
				targetPosition = new Vector3
				(targetTransform3.position.x,
				shooter.position.y,
				targetTransform3.position.z
				);
			}

			else {
				targetPosition = new Vector3
				(targetTransform.position.x,
				shooter.position.y,
				targetTransform.position.z
				);
			}

		}
		else if (randomTarget > 1.0f && randomTarget <= 2.0f) {
			if (randomTarget <= 1.5f) {
				targetPosition = new Vector3
				(targetTransform2.position.x,
				shooter.position.y,
				targetTransform2.position.z
				);
			}
			else {
				targetPosition = new Vector3
				(targetTransform4.position.x,
				shooter.position.y,
				targetTransform4.position.z
				);
			}
		}
		else {
			targetPosition = new Vector3
			(playerTransform.position.x,
			shooter.position.y,
			playerTransform.position.z
			);
		}
	}

	private void RootationToPlayer() {
		if (lookAtPlayer == true) {
			Quaternion rotation = Quaternion.LookRotation(playerPosition - shooter.position);
			shooter.rotation = Quaternion.Slerp(shooter.rotation, rotation, Time.deltaTime * rotationSpeed);
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