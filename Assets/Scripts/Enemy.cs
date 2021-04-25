using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Actor))]
public class Enemy : MonoBehaviour
{
    [SerializeField]
    int health = 3;

    [SerializeField]
    float attackRange = 2;

    Manager manager;

    Transform player;

    Actor actor;

    private void Start()
    {
        manager = Manager.Instance;
        actor = GetComponent<Actor>();
        player = FindObjectOfType<PlayerController>().transform;
    }


    public void Damage()
    {
        health -= 1;
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            actor.Knockback(transform.position - collision.transform.position);
            Destroy(collision.gameObject);
            Damage();
        }
    }

    bool IsInRangeOfPlayer()
    {
        return Vector2.Distance(player.position, transform.position) <= attackRange;
    }

    private void FixedUpdate()
    {
        var direction = manager.drugLevel > 0 && IsInRangeOfPlayer() ? MoveTowardsPlayer() : Vector2.zero;
        actor.Move(direction);
    }

    Vector2 MoveTowardsPlayer()
    {
        return player.position - transform.position;
    }

}
