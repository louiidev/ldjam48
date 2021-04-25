using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{

    [SerializeField]
    GunScriptableObj gun;

    [SerializeField]
    GameObject bulletPrefab;

    [SerializeField]
    float bulletSpeed = 10f;

    [SerializeField]
    float shotRotationRandomness = 10;

    [SerializeField]
    float shotShakeAmount = 0.2f;

    Transform firePoint;

    CameraController cameraController;

    SpriteRenderer spriteRenderer;


    private void Start()
    {
        if (bulletPrefab == null)
        {
            bulletPrefab = Resources.Load<GameObject>("Prefabs/Bullet");
        }

        spriteRenderer = transform.Find("Sprite").GetComponent<SpriteRenderer>();

        firePoint = transform.Find("FirePoint");
        cameraController = Camera.main.GetComponent<CameraController>();
    }


    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && gun != null)
        { 
            // Add a bit of noise(randomness) to firing of bullets
            var rot = transform.rotation;

            rot *= Quaternion.Euler(0, 0, Random.Range(-shotRotationRandomness, shotRotationRandomness));
            var bullet = Instantiate(bulletPrefab, firePoint.position, rot);
            // for push back effect, ignoring bullet randomness
            bullet.GetComponent<Bullet>().bulletDirection = transform.up;
            cameraController.Shake(0.2f);
        }

        var rotation = transform.rotation.eulerAngles.z;

        if (rotation >= 0 && rotation <=180)
        {
            spriteRenderer.transform.localScale = new Vector3(1, -1, 1);
        } else
        {
            spriteRenderer.transform.localScale = new Vector3(1, 1, 1);
        }
    }

    public void PickUp(GameObject pickup)
    {
        gun = pickup.GetComponent<GunPickup>().gun;
        spriteRenderer.sprite = gun.sprite;
        
        Destroy(pickup);
    }
}
