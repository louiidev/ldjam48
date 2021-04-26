using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fade : MonoBehaviour
{
    private Animator anim;
    public bool isFadeCompleted;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void OnFadeComplete()
    {
        isFadeCompleted = true;
    }

    public void FadeIn()
    {
        anim.SetTrigger("fadeIn");
    }
}
