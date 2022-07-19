using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseControll : MonoBehaviour
{
    protected enum State
    {
        IS_MOVE,
        IS_JUMP,
        IS_DEAD,
        IS_SKILL
    }

    [SerializeField]
    protected float _speed = 5.0f;
    protected Vector3 _rayHitPosition = Vector3.zero;

    protected bool _isMove = false;
    protected CapsuleCollider _capsuleCol;
    protected Animator _anim;

    protected virtual void Start()
    {
        _anim = GetComponent<Animator>();
        _capsuleCol = GetComponent<CapsuleCollider>();

        _rayHitPosition = transform.position;
    }

    protected virtual void Update()
    {
    }
}
