using UnityEngine;
using System.Collections;

public class ShooterBulletDamage : MonoBehaviour {

	public float damage = 20.0f;

	void OnTriggerEnter(Collider collider)
	{
		GameObject hitObject = collider.gameObject;
		if(hitObject == GameObject.FindWithTag("Player"))
        {
			Health health = /*(Health)*/hitObject.GetComponent<Health>();
			if (health != null)
			{
				health.GetDamage(damage);
				Destroy(gameObject);
			}
		}
        else
        {
			Destroy(gameObject);
		}
		Destroy(gameObject,8.0f);
		//powinienem sprawdzi� te� czy nab�j przemierzy� max zasi�g, je�li tak to destroy
	}
}