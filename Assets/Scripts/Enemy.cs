using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Actor))]
public class Enemy : MonoBehaviour
{
    [SerializeField]
    int health = 3;

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

    private void FixedUpdate()
    {
        
        if (manager.drugLevel > 0)
        {
            MoveTowardsPlayer();
        }
    }

    void MoveTowardsPlayer()
    {
        actor.Move(player.position - transform.position);
    }

}
