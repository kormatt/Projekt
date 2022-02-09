using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class Health : MonoBehaviour
{
	public float health = 100.0f;
	private float starthealth;
    //private Animator animator;

    private Volume v;
    private Bloom b;
    private Vignette vg;
    private GameObject postProcessing;

    void Start()
	{
        postProcessing = GameObject.Find("/PostProcessing");
        starthealth = health;
        //animator = GetComponent<Animator>();
        //StartCoroutine(waiter());
    }

    public void GetDamage(float damage)
    {
        if (gameObject.tag == "Player") {
            v = postProcessing.GetComponent<Volume>();
            v.profile.TryGet(out vg);
            Debug.Log("Player get hit!");

            float currper = 1 - (health / starthealth);
            vg.intensity.value = currper;
        }
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
        
        if(gameObject.tag != "Player") {
            Destroy(gameObject);
            //Destroy(gameObject.GetComponent<BoxCollider>(), 1.7f);
            //animator.SetBool("isDead", true);
        }


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