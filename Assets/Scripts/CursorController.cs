using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorController : MonoBehaviour
{
    Camera cam;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
       Vector3 mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
       mousePos.z = 0;
       transform.position = mousePos;
    }
}
