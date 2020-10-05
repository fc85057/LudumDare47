using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewStats", menuName = "New Stats")]
public class Stats : ScriptableObject
{

    [SerializeField] int maxHealth;
    [SerializeField] float speed;
    [SerializeField] float jumpForce;
    [SerializeField] float attackTime;
    [SerializeField] int damageAmount;
    [SerializeField] int points;
    [SerializeField] float targetDistance;

    public int MaxHealth {  get { return maxHealth; } }
    public float Speed { get { return speed; } }
    public float JumpForce { get { return jumpForce; } }
    public float AttackTime { get { return attackTime; } }
    public int DamageAmount { get { return damageAmount; } }
    public int Points { get { return points; } }
    public float TargetDistance { get { return targetDistance; } }
}
