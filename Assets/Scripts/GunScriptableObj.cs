using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "ScriptableObjects/GunScriptableObject")]
public class GunScriptableObj : ScriptableObject
{
    public int damageAmount = 0;
    new public string name;
    public Sprite sprite;
}
