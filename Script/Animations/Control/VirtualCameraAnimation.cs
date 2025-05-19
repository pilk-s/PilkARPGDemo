using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VirtualCameraAnimation : MonoBehaviour
{
   private Animation anim;

    private void Awake()
    {
        anim = GetComponent<Animation>();
    }

    public  void PlayAnimation()
    {
        if (anim == null) return;
        anim.Play();
    }
}
