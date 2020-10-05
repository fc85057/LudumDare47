using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (fileName = "NewWeapon", menuName = "New Weapon")]
public class WeaponStats : ScriptableObject
{

    [SerializeField] string weaponName;
    [SerializeField] bool canMelee;
    [SerializeField] int meleeDamage;
    [SerializeField] bool canRange;
    [SerializeField] int rangeDamage;
    [SerializeField] float attackRadius;
    [SerializeField] GameObject rangePrefab;

    public string WeaponName { get { return weaponName; } }
    public bool CanMelee { get { return canMelee; } }
    public int MeleeDamage { get { return meleeDamage; } }
    public bool CanRange { get { return canRange; } }
    public int RangeDamage { get { return rangeDamage; } }
    public float AttackRadius { get { return attackRadius; } }
    public GameObject RangePrefab { get { return rangePrefab; } }

}
