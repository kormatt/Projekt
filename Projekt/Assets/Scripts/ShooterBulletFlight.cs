using UnityEngine;
using System.Collections;

public class ShooterBulletFlight : MonoBehaviour {

	public float bulletSpeed = 12.0f;

	void FixedUpdate()
	{
		transform.Translate(transform.forward * Time.deltaTime * bulletSpeed, Space.World);
	}
}