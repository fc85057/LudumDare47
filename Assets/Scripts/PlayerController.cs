using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public static event Action<int, int> OnHealthChanged = delegate { };

    [SerializeField] GameObject[] weaponObjects;

    List<Weapon> weapons;

    [SerializeField] Stats stats;

    [SerializeField] Transform groundCheck;
    [SerializeField] LayerMask ground;

    Animator animator;
    Rigidbody2D rb;

    float currentMovement;
    Vector3 facingRight = new Vector3(0f, 0f, 0f);
    Vector3 facingLeft = new Vector3(0f, 180f, 0f);

    Weapon currentWeapon;
    int weaponIndex;

    int currentHealth;
    float nextAttackTime;

    private void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();

        weapons = new List<Weapon>();
        foreach (GameObject go in weaponObjects)
        {
            weapons.Add(go.GetComponent<Weapon>());
        }

        currentHealth = stats.MaxHealth;
        currentWeapon = weapons[0];
    }

    private void Update()
    {
        if (currentHealth <= 0)
        {
            Die();
        }

        if (Input.GetMouseButtonDown(0))
        {
            MeleeAttack();
        }
        else if (Input.GetMouseButtonDown(1))
        {
            StartCoroutine(RangeAttack());
        }

        if (Input.GetAxis("Mouse ScrollWheel") > 0f)
        {
            SwitchWeapons(true);
        }
        else if (Input.GetAxis("Mouse ScrollWheel") < 0f)
        {
            SwitchWeapons(false);
        }

    }

    private void Die()
    {
        animator.SetTrigger("Die");
        enabled = false;
    }

    private void FixedUpdate()
    {
        currentMovement = Input.GetAxisRaw("Horizontal");

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }

        if (currentMovement != 0)
        {
            Move();
        }
        else
        {
            animator.SetFloat("Speed", 0);
        }
    }

    void Move()
    {
        if (currentMovement > 0)
        {
            transform.eulerAngles = facingRight;
        }
        else
        {
            transform.eulerAngles = facingLeft;
        }

        animator.SetFloat("Speed", Mathf.Abs(currentMovement));
        Vector2 newPosition = new Vector2(transform.position.x + currentMovement, transform.position.y);
        transform.position = Vector2.MoveTowards(transform.position, newPosition, stats.Speed * Time.deltaTime);

    }

    void Jump()
    {
        bool isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.5f, ground);
        Debug.Log("Is Grounded is " + isGrounded);
        if (isGrounded)
        {
            animator.SetTrigger("Jump");
            // play sound
            rb.velocity = Vector2.up * stats.JumpForce;
        }
    }

    void MeleeAttack()
    {
        if (Time.time > nextAttackTime)
        {
            animator.SetTrigger("Melee");
            Debug.Log("Current weapon is " + currentWeapon.Stats.WeaponName);
            if (transform.eulerAngles == facingRight)
                currentWeapon.MeleeAttack(true);
            else
                currentWeapon.MeleeAttack(false);
            nextAttackTime += stats.AttackTime;
        }
        
    }

    IEnumerator RangeAttack()
    {
        if (Time.time > nextAttackTime)
        {
            animator.SetTrigger("Range");
            yield return new WaitForSeconds(.2f);
            if (transform.eulerAngles == facingRight)
                currentWeapon.RangeAttack(true);
            else
                currentWeapon.RangeAttack(false);
            nextAttackTime += stats.AttackTime;
        }
        
    }

    void SwitchWeapons(bool up)
    {
        if (up)
        {
            weaponIndex++;
        }
        else
        {
            weaponIndex--;
        }
        
        if (weaponIndex > weapons.Count - 1)
        {
            weaponIndex = 0;
        }
        else if (weaponIndex < 0)
        {
            weaponIndex = weapons.Count - 1;
        }

        currentWeapon.gameObject.SetActive(false);
        currentWeapon = weapons[weaponIndex];
        currentWeapon.gameObject.SetActive(true);
    }

    public void TakeDamage(int damageAmount)
    {
        currentHealth -= damageAmount;
        OnHealthChanged(currentHealth, stats.MaxHealth);
    }

}
