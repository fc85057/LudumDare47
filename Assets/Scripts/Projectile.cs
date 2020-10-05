using System;
using UnityEngine;

public class Projectile : MonoBehaviour
{

    [SerializeField] float speed;

    public bool goRight;

    public static event Action<Enemy> OnEnemyHit = delegate { };

    private void Start()
    {
        Destroy(gameObject, 2f);
    }

    private void Update()
    {
        if (goRight)
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
        Enemy hitEnemy = collision.GetComponent<Enemy>();

        if (hitEnemy != null)
        {
            OnEnemyHit(hitEnemy);
            Destroy(gameObject);
        }
    }

}
