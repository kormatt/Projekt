using UnityEngine;
using System.Collections;

public class Health : MonoBehaviour
{
	public float health = 100.0f;

	//private Animator animator;

	void Start()
	{
		//animator = GetComponent<Animator>();
		//StartCoroutine(waiter());
	}

    public void GetDamage(float damage)
    {
        Debug.Log(damage+" damage dealt");
        if (health > 0)
        {
            if ((health - damage) > 0)
            {
                //animator.SetTrigger("getDamage");
            }
            health -= damage;
        }

        if (health <= 0)
        {
            Debug.Log("DEAD!");
            Die();
        }
    }

    public void Die()
    {
        //animator.SetBool("isDead", true);
        Destroy(gameObject, 2.5f);
        Destroy(gameObject.GetComponent<BoxCollider>(), 1.7f);
    }

    public bool IsDead()
    {
        if (health <= 0)
        {
            return true;
        }
        return false;
    }

    /*IEnumerator waiter()
    {
        yield return new WaitForSeconds(4);
    }*/
}