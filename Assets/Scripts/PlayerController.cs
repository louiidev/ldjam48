using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
public class PlayerController : MonoBehaviour
{
    [SerializeField]
    float speed = 6;

    Rigidbody2D rigidbody2D;
    Vector3 moveDirection = Vector3.zero;

    Transform gun;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        gun = transform.Find("Gun");
    }

    private void Update()
    {
        moveDirection.x = Input.GetAxisRaw("Horizontal");
        moveDirection.y = Input.GetAxisRaw("Vertical");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Move(moveDirection.normalized);
        RotateTowardsMouse();
    }

    void RotateTowardsMouse()
    {
        Vector2 mousePos = Input.mousePosition;
        Vector3 objectPos = Camera.main.WorldToScreenPoint(gun.position);

        mousePos.x = mousePos.x - objectPos.x;
        mousePos.y = mousePos.y - objectPos.y;

        float angle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;
        gun.rotation = Quaternion.Euler(new Vector3(0, 0, angle - 90));
    }


    void Move(Vector3 direction)
    {
        // transform.position += direction * speed * Time.deltaTime;
        rigidbody2D.velocity = direction * speed;
    }
}
