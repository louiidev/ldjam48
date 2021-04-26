using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Actor))]
public class MolotovNPC : MonoBehaviour
{
    public GameObject molotovPrefab;
    public Transform gun;
    [SerializeField]
    int health = 3;
    [SerializeField]
    float attackRange = 5;
    public float distanceToRun = 2;
    public float bulletSpeed;
    public float timeToThrow;
    private bool isAttack;
    Transform player;

    Actor actor;

    private void Start()
    { 
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

    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(transform.position, attackRange);
        Gizmos.DrawSphere(transform.position, distanceToRun);
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

    bool IsInSecurityArea()
    {
        return Vector2.Distance(player.position, transform.position) <= distanceToRun;
    }

    private void FixedUpdate()
    {
        var direction = IsInSecurityArea() ? KeepDistancePlayer() : Vector2.zero;
        actor.Move(direction);

        if (IsInRangeOfPlayer() && !isAttack)
        {
            isAttack = true;
            StartCoroutine("ThrowMolotov");
        }
    }

    void Fire()
    {
        gun.localRotation = Quaternion.LookRotation(GetDirectionOfPlayer());
        GameObject obj = Instantiate(molotovPrefab, gun.position, Quaternion.identity);
        obj.transform.right = GetDirectionOfPlayer();
        obj.transform.position = Vector3.MoveTowards(obj.transform.position, player.transform.position, bulletSpeed * Time.deltaTime);
    }

    IEnumerator ThrowMolotov()
    {
        Fire();
        yield return new WaitForSeconds(timeToThrow);
        isAttack = false;
    }

    Vector2 KeepDistancePlayer()
    {
        return (player.position - transform.position) * -1;
    }

    Vector2 GetDirectionOfPlayer()
    {
        return player.position - gun.position;
    }

}

