using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{

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


    private void Start()
    {
        if (bulletPrefab == null)
        {
            bulletPrefab = Resources.Load<GameObject>("Prefabs/Bullet");
        }
       
        firePoint = transform.Find("FirePoint");
        cameraController = Camera.main.GetComponent<CameraController>();
    }


    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            // Add a bit of noise(randomness) to firing of bullets
            var rot = transform.rotation;
            rot *= Quaternion.Euler(0, 0, Random.Range(-shotRotationRandomness, shotRotationRandomness));
            Instantiate(bulletPrefab, firePoint.position, rot);
            cameraController.Shake(0.2f);
        }
    }
}
