using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    public static event Action<GameObject> OnEnemyDie;

    [SerializeField] protected Stats stats;
    [SerializeField] protected Transform attackPoint;
    [SerializeField] protected float attackRadius;
    [SerializeField] protected GameObject rangePrefab;
    [SerializeField] protected GameObject bloodPrefab;

    protected Animator animator;
    protected Rigidbody2D rb;
    protected int currentHealth;
    protected float nextAttackTime;
    protected bool isDead;

    public int Points { get { return stats.Points; } }

    protected virtual void MeleeAttack() { }
    protected virtual void RangeAttack() { }

    private void Start()
    {
        animator = GetComponent<Animator>();
        currentHealth = stats.MaxHealth;
    }

    protected void Update()
    {
        if (currentHealth <= 0 && !isDead)
        {
            Die();
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        Debug.Log("Enemy took " + damage);
    }

    protected virtual void Die()
    {
        isDead = true;
        animator.SetTrigger("Die");
        Debug.Log("Is Dead");
        OnEnemyDie(this.gameObject);
        Destroy(gameObject, 1f);
    }

}
