using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Actor))]
public class PlayerController : MonoBehaviour
{
    private Animator anim;
    private SpriteRenderer sr;
    Vector3 moveDirection = Vector3.zero;
    public int maxHp = 20;
    private int currentHp;
    public int heartHp;
    Transform gun;

    new Camera camera;

    Manager manager;

    Actor actor;

    GunController gunController;
    public Color damageFireColor;
    public Color damageColor;
    public Color recoveryColor;
    public int secondsInFire;
    public int timesTakeDamageInFire;
    public bool isInFire; //if take fire
    public bool isLookLeft;

    private bool isTakeDamage; //if take damage for anything
    private bool isRun;

    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        gun = transform.Find("Gun");
        gunController = gun.gameObject.GetComponent<GunController>();
        camera = Camera.main;
        manager = Manager.Instance;
        actor = GetComponent<Actor>();

        currentHp = maxHp;
    }

    private void Update()
    {
        moveDirection.x = Input.GetAxisRaw("Horizontal");
        moveDirection.y = Input.GetAxisRaw("Vertical");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        actor.Move(moveDirection);
        RotateTowardsMouse();

        if (moveDirection != Vector3.zero)
        {
            isRun = true;
            if (moveDirection.y != 0)
            {
                anim.SetFloat("y", moveDirection.y * -1);
            }
        }
        else
        {
            isRun = false;
        }

        anim.SetBool("isRun", isRun);
    }

    void RotateTowardsMouse()
    {
        Vector2 mousePos = Input.mousePosition;
        Vector3 objectPos = camera.WorldToScreenPoint(gun.position);

        mousePos.x = mousePos.x - objectPos.x;
        mousePos.y = mousePos.y - objectPos.y;


        if (!isLookLeft && mousePos.x < 0)
        {
            Flip();
        }
        else if(isLookLeft && mousePos.x > 0)
        {
            Flip();
        }

        float angle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;
        gun.rotation = Quaternion.Euler(new Vector3(0, 0, angle - 90));
    }

    public void StartPutFire() //call be damageAreaScript
    {
        StartCoroutine("PutFire");
    }

    IEnumerator PutFire()
    {
        if (!isInFire)
        {
            isInFire = true;

            for (int i = 0; i <= timesTakeDamageInFire; i++)
            {
                StartCoroutine("Fired");
                yield return new WaitForSeconds(secondsInFire);
            }
        }
        isInFire = false;
        sr.color = Color.white;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        switch (other.gameObject.tag)
        {
            case "Drug":
                manager.OnPickup(other.gameObject);
                break;

            case "Gun":
                gunController.PickUp(other.gameObject);
                break;

            case "Damage":
                Damage();
                break;

            case "Heart":
                if(currentHp < maxHp)
                {
                    Recovery(heartHp);

                    Destroy(other.gameObject);
                }
                break;
        }

    }

    void Recovery(int potionHp)
    {
        StartCoroutine("RecoveryHp");
        currentHp += potionHp;
        if(currentHp >= maxHp)
        {
            currentHp = maxHp;
        }
    }

    IEnumerator RecoveryHp()
    {
        sr.color = recoveryColor;
        yield return new WaitForSeconds(0.7f);
        if(!isInFire || !isTakeDamage)
        {
            sr.color = Color.white;
        }
    }

    IEnumerator Fired()
    {
        Damage();
        sr.color = damageColor;
        yield return new WaitForSeconds(0.2f);
        sr.color = damageFireColor;
    }

    public void Damage()
    {
        if (currentHp <= 0)
        {
            gameObject.SetActive(false);
            //gameOver Screen
        }
        else
        {
            currentHp--;
            StartCoroutine("TakeDamage");
        }
    }

    IEnumerator TakeDamage()
    {
        sr.color = damageColor;
        yield return new WaitForSeconds(0.3f);
        if (!isInFire)
        {
            sr.color = Color.white;
        }
        else
        {
            sr.color = damageFireColor;
        }

        isTakeDamage = false;
    }

    void Flip()
    {
        isLookLeft = !isLookLeft;
        float x = transform.localScale.x;
        transform.localScale = new Vector3(x * -1, transform.localScale.y, transform.localScale.z);
    }
}
