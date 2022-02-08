using UnityEngine;
using System.Collections;

public class ShooterAttack : MonoBehaviour {


	private Quaternion bulletRotation;
	private GameObject playerObject;
	private Vector3 playerPosition;
	private Transform playerTransform;
	private Transform shooterTransform;

	public float countdownToShot = 0f;
	public float shotInterval = 0.5f;

	public GameObject shooterBulletPrefab; //nie zd¹¿y³e sprawdziæ czy mo¿e byæ private

	void Start()
	{
		playerObject = GameObject.FindWithTag("Player");
		playerTransform = playerObject.transform;
		shooterTransform = transform;
	}

    private bool Targeting(float range)
	{
		Ray ray = new Ray(shooterTransform.position, shooterTransform.forward);
		RaycastHit hitInfo;
		
		
		if (Physics.Raycast(ray, out hitInfo, range))
		{
			GameObject hitObject = hitInfo.collider.gameObject;

			if (hitObject.name.Equals(playerObject.name))
			{
				return true;
			}
		}
		return false;
	}

	public Quaternion GetBulletRotation()
	{
		//chwilowo y - 0.5
		playerPosition = new Vector3(playerTransform.position.x, playerTransform.position.y-0.5f, playerTransform.position.z);
		bulletRotation = Quaternion.LookRotation(playerPosition - transform.position);
		return bulletRotation;
	}

	public float GetVisionRange()
	{
		ShooterController shooterController = gameObject.GetComponent<ShooterController>();
		return shooterController.GetVisionRange();
	}

	public void Shot()
	{
		Health playerHealth = playerObject.GetComponent<Health>();
		if (!playerHealth.IsDead())
		{
			if (countdownToShot < shotInterval)
			{
				countdownToShot += Time.deltaTime;
			}

			if (countdownToShot >= shotInterval && Targeting(GetVisionRange()))
			{
				countdownToShot = 0;

				//chwilowo w taki sposób
				Vector3 riflePosition	 = shooterTransform.position + shooterTransform.forward;
                riflePosition.y = 2.2f;

                Instantiate(shooterBulletPrefab, riflePosition, GetBulletRotation());
			}
		}
	}
}