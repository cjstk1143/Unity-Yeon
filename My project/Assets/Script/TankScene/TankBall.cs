using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankBall : MonoBehaviour
{
    TankBallInfo _ballInfo;

    public struct TankBallInfo
    {
        public Vector3 _direction;
        public float _attackDamage;
        public float _speed;
        public float _lifeTime;
    }

    public TankBallInfo ballinfo
    {
        get { return _ballInfo; }
        set { _ballInfo = value; }
    }

    void Start()
    {
    }

    private void OnEnable()
    {
        _ballInfo._direction = Vector3.zero;
        _ballInfo._attackDamage = 30.0f;
        _ballInfo._speed = 30.0f;
        _ballInfo._lifeTime = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        _ballInfo._lifeTime += Time.deltaTime;

        if (_ballInfo._lifeTime >= 3.0f)
        {
            gameObject.SetActive(false);
        }

        transform.position += _ballInfo._direction * _ballInfo._speed * Time.deltaTime;
    }

    public void SetDirection(Vector3 dir)
    {
        _ballInfo._direction = dir;
        transform.rotation = Quaternion.LookRotation(dir);
    }
}
