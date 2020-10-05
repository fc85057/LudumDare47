using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    [SerializeField] float speed;

    public bool facingRight;

    void FixedUpdate()
    {
        Move();
    }

    void Move()
    {
        if (facingRight)
        {
            transform.position = Vector2.MoveTowards(transform.position,
                new Vector2(transform.position.x + 1, transform.position.y), speed * Time.deltaTime);
        }
        else
        {
            transform.position = Vector2.MoveTowards(transform.position,
                new Vector2(transform.position.x - 1, transform.position.y), speed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerController player = GetComponent<PlayerController>();
    }


}
