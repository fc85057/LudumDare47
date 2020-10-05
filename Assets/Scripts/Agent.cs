using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Agent : Enemy
{

    new void Update()
    {
        base.Update();
        if (!isDead) TakeAction();
    }

    void TakeAction()
    {
        bool actionTaken = false;

        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, stats.TargetDistance);

        foreach (var hit in hits)
        {

            PlayerController player = hit.GetComponent<PlayerController>();
            if (player != null && actionTaken == false)
            {
                if (player.transform.position.x < transform.position.x)
                {
                    transform.eulerAngles = new Vector3(0f, 180f, 0f);
                }
                else
                {
                    transform.eulerAngles = new Vector3(0f, 0f, 0f);
                }

                if (Mathf.Abs(transform.position.x - player.transform.position.x) < 5)
                {
                    if (Time.time > nextAttackTime)
                    {
                        RangeAttack();
                    }
                }
                else
                {
                    Vector2 target = new Vector2(player.transform.position.x, transform.position.y);
                    transform.position = Vector2.MoveTowards(transform.position, target, stats.Speed * Time.deltaTime);
                }

                actionTaken = true;

            }
        }

    }

    protected override void RangeAttack()
    {
        if (!(Time.time > nextAttackTime))
            return;

        nextAttackTime += stats.AttackTime;

        animator.SetTrigger("Attack");

        GameObject bullet = Instantiate(rangePrefab, attackPoint.position, Quaternion.identity);

    }

}
