using UnityEngine;

public class Enemy : Sound
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
            PlaySound(sounds[0]);
        }
    }

    private void Die()
    {
        animator.SetBool("isDead", true);

        this.enabled = false;
        GetComponent<CapsuleCollider2D>().enabled = false;
        GetComponent<CircleCollider2D>().enabled = false;
    }

    private void notActive()
    {
        gameObject.SetActive(false);
    }
}
