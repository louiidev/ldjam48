﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    Manager manager;
    // Start is called before the first frame update
    void Start()
    {
        manager = FindObjectOfType(typeof(Manager)) as Manager;
    }
}
