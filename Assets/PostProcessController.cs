using System.Collections;
using System.Collections.Generic;
using UnityEngine.Rendering;
using UnityEngine;

public class PostProcessController : MonoBehaviour
{
    private GameController _GameController;
    private Volume volume;
    public float newWeight = 0.2f;

    private void Start()
    {
        _GameController = FindObjectOfType(typeof(GameController)) as GameController;
        volume = GetComponent<Volume>();
        volume.weight = newWeight * _GameController.drugLevel;
    }
}
