using System;
using System.Collections.Generic;
using UnityEngine;

public class AnimationCtrl<T> where T : Enum
    {
        private readonly int STATE_HASH = Animator.StringToHash("State");

        //private readonly Dictionary<string, AnimationClip> _animationClipDict = new Dictionary<string, AnimationClip>();

        public Animator Animator{get; init;}

        private T _currentAnimationState;

        public AnimationCtrl(Animator animator)
        {
            Animator = animator;

            // for (int j = 0; j < Animator.runtimeAnimatorController.animationClips.Length; ++j)
            // {
            //     AnimationClip clip = Animator.runtimeAnimatorController.animationClips[j];
            //
            //     if (!_animationClipDict.TryAdd(clip.name, clip))
            //     {
            //         Debug.LogError("Animation Name is duplicate");
            //     }
            // }
        }

        // public float GetAnimationLength(string name)
        // {
        //     if (_animationClipDict.TryGetValue(name, out AnimationClip value))
        //     {
        //         return value.length;
        //     }
        //     else
        //     {
        //         throw new System.Exception("Name is Wrong or None in Dictionary");
        //     }
        // }

        public void SetAnimationState(T animationState)
        {
            _currentAnimationState = animationState;
            Animator.SetInteger(STATE_HASH, Define.EnumToInt(animationState));
        }

        public void TrySetAnimationState(T animationState)
        {
            int currentState = Define.EnumToInt(_currentAnimationState);
            int parameterState = Define.EnumToInt(animationState);
            
            if (currentState != parameterState)
                SetAnimationState(animationState);
        }
        //
        // public AnimationEvent GetAnimEvent(string funcName, float eventTime, UnityEngine.Object param = null)
        // {
        //     AnimationEvent e = new AnimationEvent {
        //         functionName = funcName,
        //         time = eventTime
        //     };
        //
        //     if (param != null)
        //     {
        //         e.objectReferenceParameter = param;
        //     }
        //
        //     return e;
        // }
    }