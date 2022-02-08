using UnityEngine;
using System.Collections;

public class ShooterBulletDamage : MonoBehaviour {

	public float damage = 20f;

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
		//powinienem sprawdziæ te¿ czy nabój przemierzy³ max zasiêg, jeœli tak to destroy
	}
}