using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Molotov : MonoBehaviour
{
    private AudioController _AudioController;
    public GameObject damageArea;
    public int timeInFireActive = 4;

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player" || col.gameObject.tag == "Wall")
        {
            _AudioController = FindObjectOfType(typeof(AudioController)) as AudioController;
            _AudioController.PlayFX(_AudioController.glassFX);
            GameObject mObj = Instantiate(damageArea, transform.position, Quaternion.identity);
            mObj.GetComponent<DamageArea>().timeInFire = timeInFireActive;
            _AudioController.PlayFX(_AudioController.explosionFX);
            Destroy(this.gameObject);
        }
    }

    private void OnBecameInvisible()
    {
        Destroy(this.gameObject);
    }
}
