using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseControll : MonoBehaviour
{
    [SerializeField]
    protected float _speed = 5.0f;
    [SerializeField]
    protected Define.State _state = Define.State.IDLE;

    protected Vector3 _rayHitPosition = Vector3.zero;

    protected bool _isMove = false;
    protected bool _isAttack = false;
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
        switch (_state)
        {
            case Define.State.MOVE:
                UpdateMove();
                break;

            case Define.State.IDLE:
                UpdateIdle();
                break;

            case Define.State.ATTACK:
                UpdateAttack();
                break;
        }
    }

    public virtual Define.State State
    {
        get { return _state; }
        set
        {
            if (_state == value)
                return;

            _state = value;

            if (_anim == null)
                _anim = GetComponent<Animator>();

            switch(_state)
            {
                case Define.State.DEAD:
                    break;

                case Define.State.MOVE:
                    _anim.CrossFade("RUN", 0.1f, 0, 0.0f);
                    break;

                case Define.State.IDLE:
                    _anim.CrossFade("WAIT", 0.3f, 0, 0.0f);
                    break;

                case Define.State.ATTACK:
                    _anim.CrossFade("ATTACK", 0.1f);
                    break;
            }
        }
    }

    protected virtual void UpdateMove() { }
    protected virtual void UpdateIdle() { }
    protected virtual void UpdateAttack() { }
}
