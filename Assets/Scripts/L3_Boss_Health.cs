using System;
using UnityEngine;

public class L3_Boss_Health : MonoBehaviour
{
    public int health = 10;
    private int currentHealth;
    public GameObject explosion;
    private Animator animator;
    public Action onBossDefeated;

    void Start()
    {
        currentHealth = health;
        animator = GetComponent<Animator>();
    }

    public void takeDamage()
    {
        currentHealth--;
        if (currentHealth <= 0)
        {
            explode();
        }
    }

    public void explode()
    {
        Debug.Log("Boss should explode now!");
        Instantiate(explosion, transform.position, transform.rotation);
        onBossDefeated?.Invoke();
        Destroy(gameObject);

    }

    void Update()
{
    if (currentHealth <= (health - (health * .66)))
    {
        animator.SetTrigger("HighDamage");
    }
    else if (currentHealth <= (health - (health * .33)))
    {
        animator.SetTrigger("LowDamage");
    }
}


}