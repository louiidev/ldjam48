using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[ExecuteInEditMode]
public class GunPickup : MonoBehaviour
{
    [SerializeField]
    public GunScriptableObj gun;


    private void Start()
    {
       if (gun != null)
        {
            GetComponent<SpriteRenderer>().sprite = gun.sprite;
        }
    }
}
