using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering;


[RequireComponent(typeof(Actor))]
public class PlayerController : MonoBehaviour
{
    public AudioController _AudioController;
    public Volume damageVolume;
    private Animator anim;
    private SpriteRenderer sr;
    public SpriteRenderer gunSr;
    Vector3 moveDirection = Vector3.zero;
    public float maxHp = 35;
    public float currentHp;
    public int heartHp;
    Transform gun;

    private new Camera camera;

    private GameController _GameController;

    Actor actor;
    public GameObject ballPrefab;
    GunController gunController;
    public Color damageFireColor;
    public Color damageColor;
    public Color recoveryColor;
    public int secondsInFire;
    public int timesTakeDamageInFire;
    public bool isInFire; //if take fire
    public bool isLookLeft;

    private bool isTakeDamage; //if take damage for anything
    private bool isRecovery;
    private bool isRun;

    // Start is called before the first frame update
    void Start()
    {
        currentHp = maxHp;
        _GameController = FindObjectOfType(typeof(GameController)) as GameController;
        _AudioController = FindObjectOfType(typeof(AudioController)) as AudioController;
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        gun = transform.Find("Gun");
        gunController = gun.gameObject.GetComponent<GunController>();
        camera = Camera.main;
        actor = GetComponent<Actor>();
        _GameController.player = this;
        _GameController.currentState = GameState.GAMEPLAY;
    }

    private void Update()
    {
        if(_GameController.currentState != GameState.GAMEPLAY) { return; }
        moveDirection.x = Input.GetAxisRaw("Horizontal");
        moveDirection.y = Input.GetAxisRaw("Vertical");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (_GameController.currentState != GameState.GAMEPLAY) { return; }
        actor.Move(moveDirection);
        RotateTowardsMouse();

        if (moveDirection != Vector3.zero)
        {
            isRun = true;
        }
        else
        {
            isRun = false;
        }

        Vector3 posCam = new Vector3(transform.position.x, transform.position.y, -10);
        camera.transform.position = posCam;
        anim.SetBool("isRun", isRun);

        damageVolume.weight = currentHp / maxHp;
    }

    void RotateTowardsMouse()
    {
        Vector2 mousePos = Input.mousePosition;
        Vector3 objectPos = camera.WorldToScreenPoint(gun.position);

        mousePos.x = mousePos.x - objectPos.x;
        mousePos.y = mousePos.y - objectPos.y;

        if (!isRecovery)
        {
            if (mousePos.y > 0)
            {
                anim.SetLayerWeight(1, 1);
                anim.SetLayerWeight(2, 0);
                anim.SetLayerWeight(3, 0);
                gunSr.sortingOrder = 0;
            }
            else
            {
                anim.SetLayerWeight(1, 0);
                anim.SetLayerWeight(2, 0);
                anim.SetLayerWeight(3, 0);
                gunSr.sortingOrder = 2;
            }
        }
        else
        {
            if (mousePos.y > 0)
            {
                anim.SetLayerWeight(1, 0);
                anim.SetLayerWeight(2, 0);
                anim.SetLayerWeight(3, 1);
                gunSr.sortingOrder = 0;
            }
            else
            {
                anim.SetLayerWeight(1, 0);
                anim.SetLayerWeight(2, 1);
                anim.SetLayerWeight(3, 0);
                gunSr.sortingOrder = 2;
            }
        }


        if (!isLookLeft && mousePos.x < 0)
        {
            Flip();
        }
        else if (isLookLeft && mousePos.x > 0)
        {
            Flip();
        }

        float angle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;
        gun.rotation = Quaternion.Euler(new Vector3(0, 0, angle - 90));
    }

    public void StartPutFire() //call be damageAreaScript
    {
        if (_GameController.currentState != GameState.GAMEPLAY) { return; }
        StartCoroutine("PutFire");
    }

    public void PlayStep()
    {
        _AudioController.PlayFX(_AudioController.RandomWalk());
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
        if (_GameController.currentState != GameState.GAMEPLAY) { return; }
        switch (other.gameObject.tag)
        {
            case "Drug":
                _AudioController.PlayFX(_AudioController.pickupItemFX);
                _GameController.OnPickup(other.gameObject);
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
        if (currentHp >= maxHp)
        {
            currentHp = maxHp;
        }
        _GameController.UpdateUI();
    }

    IEnumerator RecoveryHp()
    {
        isRecovery = true;
        sr.color = recoveryColor;
        yield return new WaitForSeconds(0.7f);
        if (!isInFire || !isTakeDamage)
        {
            sr.color = Color.white;
        }
        isRecovery = false;
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
            _GameController.currentState = GameState.PAUSE;
            anim.SetTrigger("die");
            StartCoroutine("Die");
        }
        else
        {
            currentHp--;
            _GameController.UpdateUI();
            StartCoroutine("TakeDamage");
        }
    }

    IEnumerator Die()
    {

        yield return new WaitForSeconds(2.3f);
        gameObject.SetActive(false);
        _AudioController.PlayFX(_AudioController.deathFX);
        _GameController.ResetLevel();
    }

    IEnumerator TakeDamage()
    {
        sr.color = damageColor;
        _AudioController.PlayFX(_AudioController.hitFX);
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
        sr.flipX = !sr.flipX;
    }
}
