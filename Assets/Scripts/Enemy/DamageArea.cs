using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageArea : MonoBehaviour
{
    public Transform[] spawnPoints;
    public GameObject spawnObject;

    [HideInInspector]
    public float timeInFire;

    void Start()
    {
        for (int i = 0; i < spawnPoints.Length; i++)
        {
            Instantiate(spawnObject, spawnPoints[i]);
        }

        Destroy(this.gameObject, timeInFire);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.tag == "Player")
        {
            col.gameObject.SendMessage("StartPutFire", SendMessageOptions.DontRequireReceiver);
        }
    }
}
