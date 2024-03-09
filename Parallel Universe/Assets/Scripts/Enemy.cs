using UnityEngine;
using UnityEngine.SceneManagement;

public class Enemy : MonoBehaviour
{
    private Animator animator;

    public int maxHealth = 100;
    int currentHealth;
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        animator = GetComponent<Animator>();
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        animator.SetTrigger("Hurt");

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        animator.SetBool("isDead", true);
        if (gameObject.layer == 7 && SceneManager.GetActiveScene().buildIndex != 4)
        {
            PlayerPrefs.SetInt("Players", PlayerPrefs.GetInt("Players", 0) - 1);
        } else
        {
            PlayerPrefs.SetInt("Head", PlayerPrefs.GetInt("Head", 0) + 1);
        }
        this.enabled = false;
        GetComponent<CapsuleCollider2D>().enabled = false;
        GetComponent<CircleCollider2D>().enabled = false;
    }

    private void notActive()
    {
        gameObject.SetActive(false);
    }
}
