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

	void Start()
	{
		shooter = transform;
		if (GetComponent<Rigidbody>())
		{
			GetComponent<Rigidbody>().freezeRotation = true;
		}
		playerTransform = GameObject.FindWithTag("Player").transform;

		GetComponent<Animator>().SetBool("Run", false);

		attack = gameObject.GetComponent<ShooterAttack>();
	}

	void Update()
	{
		if (!PlayerIsDead())
		{			
			ShooterMovement();
		}
	}

	private void ShooterMovement()
	{
		playerPosition = new Vector3(playerTransform.position.x, shooter.position.y, playerTransform.position.z);

		float distance = Vector3.Distance(shooter.position, playerTransform.position);

		lookAtPlayer = false;
		//GetComponent<Animator>().SetBool("Run", false);
		GetComponent<Animator>().SetBool("Attack", false);

		if (distance <= visionRange && distance > minimumDistanceFromPlayer && !ShooterIsDead())
		{
			lookAtPlayer = true;
			shooter.position = Vector3.MoveTowards(shooter.position, playerPosition, movementSpeed * Time.deltaTime);
			GetComponent<Animator>().SetBool("Run", true);

			attack.Shot();
		}
		else if (distance <= minimumDistanceFromPlayer && !ShooterIsDead())
		{
			lookAtPlayer = true;
			GetComponent<Animator>().SetBool("Attack", true);
			attack.Shot();
		}

		else if (distance > visionRange)
        {
			GetComponent<Animator>().SetBool("Run", false);
		}

		if (!ShooterIsDead())
		{
			RootationToPlayer();
		}
		else //obiekt martwy
		{
			if (GetComponent<Rigidbody>())
			{
				GetComponent<Rigidbody>().freezeRotation = false;
			}
		}
	}

	private void RootationToPlayer()
	{
		if (lookAtPlayer == true)
		{
			Quaternion rotation = Quaternion.LookRotation(playerPosition - shooter.position);
			shooter.rotation = Quaternion.Slerp(shooter.rotation, rotation, Time.deltaTime * rotationSpeed);
		}
	}

	private bool ShooterIsDead()
	{
		Health shooterHealth = gameObject.GetComponent<Health>();
		if (shooterHealth != null)
		{
			return shooterHealth.IsDead();
		}
		return false;
	}

	private bool PlayerIsDead()
	{
		Health playerHealth = GameObject.FindWithTag("Player").GetComponent<Health>();
		if (playerHealth != null)
		{
			return playerHealth.IsDead();
		}
		return false;
	}

	public float GetVisionRange()
	{
		return visionRange;
	}
}