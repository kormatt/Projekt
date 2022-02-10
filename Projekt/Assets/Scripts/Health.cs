using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class Health : MonoBehaviour
{
	public float health = 100.0f;
	private float starthealth;
    private Animator animator;

    private Volume v;
    private Bloom b;
    private Vignette vg;
    private GameObject postProcessing;
    private bool isDead = false;
    void Start()
	{
        postProcessing = GameObject.Find("/PostProcessing");
        starthealth = health;
        animator = GetComponent<Animator>();
        //StartCoroutine(waiter());
    }

    public void GetDamage(float damage)
    {
        if (gameObject.tag == "Player") {
            v = postProcessing.GetComponent<Volume>();
            v.profile.TryGet(out vg);
            FindObjectOfType<AudioManager>().Play("GetHit");

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

            
        if (gameObject.name == "Enemy" && !isDead)
        {
            Destroy(GameObject.Find("Burning"));
            isDead = true;
            Destroy(gameObject, 10.0f);
            PlayerStats.EnemiesKilled++;
            //Destroy(gameObject.GetComponent<BoxCollider>(), 1.7f);            
        }

        if (gameObject.tag != "Player" && !isDead) {
            isDead = true;
            animator.SetTrigger("Death");
            Destroy(gameObject,10.0f);
            PlayerStats.EnemiesToKill--;
            PlayerStats.EnemiesKilled++;
            if (gameObject.name == "Boss") {
                animator = GameObject.Find("WIN").GetComponent<Animator>();
                animator.SetTrigger("Win");
            }
            //Destroy(gameObject.GetComponent<BoxCollider>(), 1.7f);            
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

/*    IEnumerator waiter()
    {        
        yield return new WaitForSeconds(4);
        
    }*/
}