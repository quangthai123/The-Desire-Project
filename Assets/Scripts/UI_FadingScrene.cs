using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_FadingScrene : MonoBehaviour
{
    private Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();

    }

    public void FadeOut()
    {
        Debug.Log(1);
        anim.SetTrigger("fadeOut");
    }
    public void FadeIn() => anim.SetTrigger("fadeIn");
}
