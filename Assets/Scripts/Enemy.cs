using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Enemy : MonoBehaviour
{
  [SerializeField]
  int health = 3;
  
    public void Damage() {
        health-= 1;
        if (health <= 0) {
            Destroy(gameObject);
        }
    }

}
