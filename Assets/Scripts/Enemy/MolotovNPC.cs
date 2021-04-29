using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Actor))]
public class MolotovNPC : MonoBehaviour
{
    public AudioController _AudioController;
    private SpriteRenderer sr;
    private Animator anim;
    public Color takeDamageColor;
    public GameObject molotovPrefab;
    public GameObject drop;
    public Transform gun;
    [SerializeField]
    int health = 3;
    [SerializeField]
    float attackRange = 5;
    public float dropPercent = 15;
    public float distanceToRun = 2;
    public float distanceView = 6;
    public float bulletSpeed;
    public float timeToThrow;
    public float delayToFire = 0.2f;
    public bool isLookLeft;
    public bool isReady = false;
    private bool isAttack;
    private bool isWalk;
    Transform player;
    Vector2 direction = new Vector2(0, 0);
    private bool isTakeDamage;

    Actor actor;

    private void Start()
    {
        _AudioController = FindObjectOfType(typeof(AudioController)) as AudioController;
        actor = GetComponent<Actor>();
        player = FindObjectOfType<PlayerController>().transform;
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }


    public void Damage()
    {
        health -= 1;
        if (!isTakeDamage) { StartCoroutine("ChangeColor"); }
        if (health <= 0)
        {
            int rand = Random.Range(0, 100);

            if (rand > dropPercent)
            {
                Instantiate(drop, transform.position, Quaternion.identity);
            }

            Destroy(this.gameObject);
        }
    }

    IEnumerator ChangeColor()
    {
        isTakeDamage = true;
        sr.color = takeDamageColor;
        yield return new WaitForSeconds(0.5f);
        sr.color = Color.white;
        isTakeDamage = false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(transform.position, attackRange);
        Gizmos.DrawSphere(transform.position, distanceToRun);
        Gizmos.DrawSphere(transform.position, distanceView);
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

    bool IsInView()
    {
        return Vector2.Distance(player.position, transform.position) <= distanceView;
    }

    private void FixedUpdate()
    {
        if(!IsInView())
        {
            direction = Vector2.zero;
        }
        else if (IsInSecurityArea())
        {
            direction = KeepDistancePlayer();
        }
        else if (!IsInRangeOfPlayer())
        {
            direction = GetDirectionOfPlayer();
        }
        else
        {
            direction = Vector2.zero;
        }

        if (!isLookLeft && GetDirectionOfPlayer().x < 0)
        {
            Flip();
        }
        else if (isLookLeft && GetDirectionOfPlayer().x > 0)
        {
            Flip();
        }

        actor.Move(direction);

        gun.right = GetDirectionOfPlayer();

        if (IsInRangeOfPlayer() && !isAttack)
        {
            isAttack = true;
            StartCoroutine("ThrowMolotov");
        }

        isWalk = direction != Vector2.zero;

        if (anim != null)
        {
            anim.SetBool("isWalk", isWalk);
        }
    }

    void Fire()
    {
        if (anim != null)
        {
            anim.SetTrigger("attack");
        }

        StartCoroutine("DelayFire");
    }

    IEnumerator DelayFire()
    {
        yield return new WaitForSeconds(delayToFire);
        GameObject obj = Instantiate(molotovPrefab, gun.position, gun.localRotation);
        obj.GetComponent<Rigidbody2D>().velocity = gun.right * bulletSpeed;
        _AudioController.PlayFX(_AudioController.fireFX2);
    }

    void Flip()
    {
        isLookLeft = !isLookLeft;
        sr.flipX = !sr.flipX;
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
        return player.position - transform.position;
    }

}

