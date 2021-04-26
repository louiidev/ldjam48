using System.Collections;
using System.Collections.Generic;
using UnityEngine.Rendering;
using UnityEngine;

public class PostProcessController : MonoBehaviour
{
    private Manager manager;
    private Volume volume;
    public float newWeight = 0.2f;

    private void Start()
    {
        manager = FindObjectOfType(typeof(Manager)) as Manager;
        volume = GetComponent<Volume>();
        volume.weight = newWeight * manager.drugLevel;
    }
}
