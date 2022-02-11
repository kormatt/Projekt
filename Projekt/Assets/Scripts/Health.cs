using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class Health : MonoBehaviour {
    public float health = 10.0f;
    private float starthealth;
    private Animator animator;

    private Volume volume;
    private Vignette vignette;
    private GameObject postProcessing;
    private bool isDead = false;
    void Start() {
        postProcessing = GameObject.Find("/PostProcessing");
        starthealth = health;
        animator = GetComponent<Animator>();
    }

    public void GetDamage(float damage) {
        if (gameObject.tag == "Player") {
            volume = postProcessing.GetComponent<Volume>();
            volume.profile.TryGet(out vignette);
            FindObjectOfType<AudioManager>().Play("GetHit");

            float currper = 1 - (health / starthealth);
            vignette.intensity.value = currper;
        }

        if (health > 0) {
            if ((health - damage) > 0) {
                //animator.SetTrigger("getDamage");
            }
            health -= damage;
        }

        if (health <= 0) {
            Die();
        }
    }

    public void Die() {
        if (gameObject.name == "Enemy" && !isDead) {
            Destroy(GameObject.Find("Burning"));
            isDead = true;
            Destroy(gameObject, 10.0f);
            PlayerStats.EnemiesKilled++;
        }

        if (gameObject.tag != "Player" && !isDead) {
            isDead = true;
            animator.SetTrigger("Death");
            Destroy(gameObject, 10.0f);
            PlayerStats.EnemiesToKill--;
            PlayerStats.EnemiesKilled++;
            if (gameObject.name == "Boss") {
                animator = GameObject.Find("WIN").GetComponent<Animator>();
                animator.SetTrigger("Win");
            }
        }
    }

    public bool IsDead() {
        if (health <= 0) {
            return true;
        }
        return false;
    }
}