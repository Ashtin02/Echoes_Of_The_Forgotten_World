using System;
using UnityEngine;

/// <summary>
/// Manages the boss ship's health, explosion effects, and animation triggers
/// based on current health. Notifies listeners when the boss is defeated.
/// </summary>
public class L3_Boss_Health : MonoBehaviour
{
    public int health = 10;
    private int currentHealth;
    public GameObject explosion;
    private Animator animator;
    public Action onBossDefeated;

    /// <summary>
    /// Initializes boss health at the start and gets the animator for animation changes
    /// </summary>
    void Start()
    {
        currentHealth = health;
        animator = GetComponent<Animator>();
    }

    /// <summary>
    /// Applies damage to the boss and triggers explosion if health reaches zero.
    /// </summary>
    public void takeDamage()
    {
        currentHealth--;
        if (currentHealth <= 0)
        {
            explode();
        }
    }

    /// <summary>
    /// Instantiates explosion effect, triggers defeat callback, and destroys the boss object.
    /// </summary>
    public void explode()
    {
        Instantiate(explosion, transform.position, transform.rotation);
        onBossDefeated?.Invoke();
        Destroy(gameObject);
    }

    /// <summary>
    /// Triggers damage animations based on remaining health percentage.
    /// </summary>
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