using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Actor))]
public class PlayerController : MonoBehaviour
{
    Vector3 moveDirection = Vector3.zero;

    Transform gun;

    new Camera camera;

    Manager manager;

    Actor actor;

    // Start is called before the first frame update
    void Start()
    {
        gun = transform.Find("Gun");
        camera = Camera.main;
        manager = Manager.Instance;
        actor = GetComponent<Actor>();
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
    }

    void RotateTowardsMouse()
    {
        Vector2 mousePos = Input.mousePosition;
        Vector3 objectPos = camera.WorldToScreenPoint(gun.position);

        mousePos.x = mousePos.x - objectPos.x;
        mousePos.y = mousePos.y - objectPos.y;

        float angle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;
        gun.rotation = Quaternion.Euler(new Vector3(0, 0, angle - 90));
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.tag == "Drug") {
            manager.OnPickup(other.gameObject);
        }
    }
}
