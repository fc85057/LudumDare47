using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] WeaponStats stats;
    [SerializeField] GameObject bloodPrefab;

    public WeaponStats Stats { get { return stats; } }

    SpriteRenderer sr;
    [SerializeField] Transform attackPoint;

    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        Projectile.OnEnemyHit += DealRangeDamage;
    }

    public void MeleeAttack(bool facingRight)
    {
        if (stats.CanMelee)
        {
            Collider2D[] hits = Physics2D.OverlapCircleAll(attackPoint.position, stats.AttackRadius);
            foreach (Collider2D hit in hits)
            {
                Enemy hitEnemy = hit.GetComponent<Enemy>();
                if (hitEnemy != null)
                {
                    GameObject blood = Instantiate(bloodPrefab, attackPoint.position, Quaternion.identity);
                    Destroy(blood, .2f);
                    hitEnemy.TakeDamage(stats.MeleeDamage);
                }
                else
                {
                    Debug.Log("No enemy hit.");
                    Debug.Log("Hit " + hit.name);
                }
            }
        }
        else
        {
            RangeAttack(facingRight);
        }
    }

    public void RangeAttack(bool facingRight)
    {
        if (stats.CanRange)
        {
            GameObject newProjectile = Instantiate(stats.RangePrefab, attackPoint.position, Quaternion.identity);
            newProjectile.GetComponent<Projectile>().goRight = facingRight;
        }
        else
        {
            MeleeAttack(facingRight);
        }
    }

    private void DealRangeDamage(Enemy hitEnemy)
    {
        GameObject blood = Instantiate(bloodPrefab, hitEnemy.transform.position, Quaternion.identity);
        Destroy(blood, 0.2f);
        hitEnemy.TakeDamage(stats.RangeDamage);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(attackPoint.position, stats.AttackRadius);
    }

}
