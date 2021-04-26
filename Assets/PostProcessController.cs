using System.Collections;
using System.Collections.Generic;
using UnityEngine.Rendering;
using UnityEngine;

public class PostProcessController : MonoBehaviour
{
    private Volume volume;
    public float newWeight;

    private void Start()
    {
        volume = GetComponent<Volume>();
        volume.weight = newWeight;
    }
}
