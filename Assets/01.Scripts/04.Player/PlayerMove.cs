using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMove : BaseEntityMove, IUpdateAble
{

    private Rigidbody2D _rb;

    private void Awake() {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void OnEnable() {
        UpdateManager.Instance.RegisterObject(this);
    }

    private void OnDisable() {
        UpdateManager.Instance.UnRegisterObject(this);
    }

    protected override void Move()
    {
        Vector2 dir = Vector2.zero;
        SetInput(ref dir);
        
        _rb.velocity = dir.normalized * _speed;
    }

    private void SetInput(ref Vector2 input)
    {
        input.x = Input.GetAxisRaw("Horizontal");
        input.y = Input.GetAxisRaw("Vertical");
    }

    public void OnUpdate()
    {
        Move();
    }
}
