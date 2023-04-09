using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningParticle : MonoBehaviour
{
    [SerializeField]
    private ParticleSystem _rotateParticle1;
    [SerializeField]
    private ParticleSystem _rotateParticle2;

    [SerializeField]
    private float _testAngle = -45f;

    private bool _isPlaySound;

    // [ContextMenu("ChangeRotate")]
    // private void ChangeRotate()
    // {
    //     SetParticleRotationAndPositon(_testAngle);
    // }


    public void SetParticleRotationAndPositon(float rotate, Vector3 position)
    {
        transform.rotation = Quaternion.Euler(0f,0f,rotate - 90f);

        float radian = rotate * Mathf.Deg2Rad;

        transform.position = position + new Vector3(Mathf.Cos(radian) , Mathf.Sin(radian), 0f) * 4f;


        // Debug.Log(transform.localPosition);

        _rotateParticle2.startRotation = (90f - rotate) * Mathf.Deg2Rad;
        _rotateParticle1.startRotation = _rotateParticle2.startRotation + 180f;

        // Debug.Break();
    }

    private void OnEnable()
    {
        _isPlaySound = true;
    }


    // protected override void Attack()
    // {
    //     if (_isPlaySound)
    //     {
    //         SoundManager.Instance.Play(AudioType.SFX, _attackSound);
    //         _isPlaySound = false;
    //     }
    // }

    
}
