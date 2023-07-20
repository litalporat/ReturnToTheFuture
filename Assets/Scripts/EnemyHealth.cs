using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
public class EnemyHealth : MonoBehaviour
{
    [SerializeField] float hitPoints = 100f;
    public FloatingHealthBar healthBar;
    NavMeshAgent navMeshAgent;

    Rigidbody rb;

    bool isDead = false;

    public bool IsDead()
    {
        return isDead;
    }

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        healthBar = GetComponentInChildren<FloatingHealthBar>();
    }

    public void TakeDamage(float damage)
    {
        BroadcastMessage("OnDamageTaken");
        hitPoints -= damage;
        healthBar.UpdateHealthBar(hitPoints,100);
        if (hitPoints <= 0)
        {
            healthBar.UpdateHealthBar(0,100);
            Die();
        }
    }

    private void Die()
    {
        if (isDead) return;
        isDead = true;
        GetComponent<Animator>().SetTrigger("die");
        Destroy(gameObject,3);
    }

    
}
