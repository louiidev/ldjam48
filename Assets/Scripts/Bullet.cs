using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 10f;
    public Vector2 bulletDirection = Vector2.zero;

    SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.up * speed * Time.deltaTime;
        // Basic clean up
        if (!spriteRenderer.isVisible)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        switch (col.gameObject.tag)
        {
            case "Wall":
                Destroy(this.gameObject);
                break;

            case "Destructable":
                Destroy(col.gameObject);
                Destroy(this.gameObject);
                break;
        }

    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
