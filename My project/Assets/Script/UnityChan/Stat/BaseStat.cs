using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseStat : MonoBehaviour
{
    [SerializeField]
    protected float _curHp;
    [SerializeField]
    protected float _maxHp;
    // + 안바뀌는 값이면 여기서 초기화하고
    // 바뀔 수도 있는 값이면 무조건 start같은 곳에서 초기화를 해야한대.

    void Start()
    {
        _maxHp = 100;
        _curHp = _maxHp;
    }

    private void OnEnable()
    {
        _curHp = _maxHp;
    }

    public void Damaged(int attack)
    {
        _curHp -= attack;

        if (_curHp < 0)
            _curHp = 0;
    }

    public bool IsDead()
    {
        return _curHp <= 0;
    }

    public float HpRatio()
    {
        return (_curHp / _maxHp);
    }
}
