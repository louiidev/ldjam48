using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorController : MonoBehaviour
{
    Camera camera;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
        camera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
       Vector3 mousePos = camera.ScreenToWorldPoint(Input.mousePosition);
       mousePos.z = 0;
       transform.position = mousePos;
    }
}
