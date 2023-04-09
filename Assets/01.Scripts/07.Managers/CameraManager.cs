using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraManager : MonoSingleton<CameraManager>
{
    public CinemachineVirtualCamera VCam
    {
        get
        {
            _vCam ??= GameObject.FindObjectOfType<CinemachineVirtualCamera>();
            return _vCam;
        }
    }
    private CinemachineVirtualCamera _vCam;

    private CinemachineImpulseSource _impulseSource;
    
    protected override void Start() {
        base.Start();
        _impulseSource = VCam.GetComponent<CinemachineImpulseSource>();
    }

    public void CameraShake(Vector3 velocity, float strength)
    {
        _impulseSource.GenerateImpulseWithVelocity(velocity * strength);
    }
}
