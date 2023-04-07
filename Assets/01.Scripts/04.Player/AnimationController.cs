using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class AnimationController<T> : MonoBehaviour, IGetComponentAble where T : Enum
{

    public AnimationCtrl<T> AnimationCtrl{ get; private set; }
    protected virtual void Awake() {
        Animator animator = GetComponent<Animator>();
        AnimationCtrl = new(animator);
    }

}
