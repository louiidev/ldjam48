using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Molotov : MonoBehaviour
{
    public GameObject damageArea;
    public int timeInFireActive = 4;

    private void OnTriggerEnter2D(Collider2D col)
    {
       if(col.gameObject.tag == "Player" || col.gameObject.tag == "Wall")
       {
            GameObject mObj = Instantiate(damageArea, transform.position, Quaternion.identity);
            mObj.GetComponent<DamageArea>().timeInFire = timeInFireActive;
        }

    }
}
