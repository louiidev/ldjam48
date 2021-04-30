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

    GameController _GameController;

    Transform player;

    Actor actor;

    private void Start()
    {
        _GameController = FindObjectOfType(typeof(GameController)) as GameController;
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
            Debug.Log(collision.GetComponent<Bullet>().bulletDirection);
            actor.Knockback(collision.GetComponent<Bullet>().bulletDirection);
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
        var direction = _GameController.drugLevel > 0 && IsInRangeOfPlayer() ? MoveTowardsPlayer() : Vector2.zero;
        actor.Move(direction);
    }

    Vector2 MoveTowardsPlayer()
    {
        return player.position - transform.position;
    }

}
