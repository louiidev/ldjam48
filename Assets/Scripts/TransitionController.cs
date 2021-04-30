using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionController : MonoBehaviour
{
    public float animationTime;
    private GameController _GameController;

    private void Start()
    {
        _GameController = FindObjectOfType(typeof(GameController)) as GameController;
        StartCoroutine("DelayToNextScene");
    }

    IEnumerator DelayToNextScene()
    {
        yield return new WaitForSeconds(animationTime);
        _GameController.NextScene();
    }
}
