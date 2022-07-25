using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MonsterControll : BaseControll
{
    public GameObject _monsterInfo;
    Slider _hpSlider;
    GameObject _player;

    MonsterStat _stat;

    private Vector3 _movePos = Vector3.zero;
    private Vector3 _originPos;
    [SerializeField]
    private float _movingDistance;
    [SerializeField]
    private float _detectDistance = 5.0f;
    [SerializeField]
    private float _attackRange = 1.0f;


    protected override void Start()
    {
        base.Start();

        if (_monsterInfo == null)
            _monsterInfo = transform.Find("Monster_Info").gameObject;

        _hpSlider = _monsterInfo.GetComponentInChildren<Slider>();

        if (_hpSlider == null)
            Debug.LogError("못찾음");

        _stat = GetComponent<MonsterStat>();

        _speed = 10.0f;
        _state = Define.State.IDLE;

        StartCoroutine("Co_MonsterAIMove");

        _movingDistance = 5.0f;
        _detectDistance = 5.0f;
        _attackRange = 1.0f;
        _speed = 2.0f;

        _player = GameObject.FindGameObjectWithTag("Player");
    }

    private void OnEnable()
    {
        _movePos = transform.position;
        _originPos = transform.position;
    }

    private void OnDisable()
    {
        StopCoroutine("Co_MonsterAIMove");
    }

    protected override void Update()
    {
        base.Update();
        SetHpBar();

        gameObject.SetActive(!_stat.IsDead());
    }

    void SetHpBar()
    {
        Transform parent = _monsterInfo.transform.parent;
        _monsterInfo.transform.position = parent.position + 
            Vector3.up * (parent.GetComponent<Collider>().bounds.size.y);
        _monsterInfo.transform.rotation = Camera.main.transform.rotation;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "PlayerWeapon")
        {
            Debug.Log("몬스터 공격 당함!!");
            _stat.Damaged(30);
            _hpSlider.value = _stat.HpRatio();
        }
    }

    IEnumerator Co_MonsterAIMove()
    {
        if (_isMove == false)
            yield return new WaitForSeconds(3.0f);

        State = Define.State.MOVE;
        _isMove = true;
        float x;
        float z;

        x = Random.Range(- 5.0f + _originPos.x, 5.0f + _originPos.x);
        z = Random.Range(- 5.0f + _originPos.z, 5.0f + _originPos.z);

        _movePos = new Vector3(x, 0.0f, z);

        Debug.Log(_movePos);
        yield return new WaitForSeconds(3.0f);
        StartCoroutine("Co_MonsterAIMove");
    }

    void RandMove()
    {
        if (Detect() == true)
            return;

        Vector3 temp = _movePos - transform.position;
        
        if (temp.magnitude < 0.1f)
        {
            //Debug.Log("Reach");
            State = Define.State.IDLE;

            return;
        }

        transform.rotation = Quaternion.LookRotation(temp);
        transform.position += (temp.normalized * Time.deltaTime * 5.0f);
    }
    
    bool Detect()
    {
        if (_player == null)
            _player = GameObject.FindGameObjectWithTag("Player");

        float distance = (_player.transform.position - transform.position).magnitude;

        if (distance < _detectDistance)
        {
            State = Define.State.MOVE;
            Follow();
            return true;
        }

        return false;
    }

    void Follow()
    {
        if (_player == null)
            _player = GameObject.FindGameObjectWithTag("Player");

        Vector3 moveVector = _player.transform.position - transform.position;

        if (moveVector.magnitude <= _attackRange)
        {
            Attack();
            return;
        }

        transform.position += moveVector.normalized * _speed * Time.deltaTime;
    }

    void Attack()
    {
        if (_isAttack)
            return;

        State = Define.State.ATTACK;
        _isAttack = true;
    }

    protected override void UpdateMove()
    {
        base.UpdateMove();
        RandMove();
    }

    protected override void UpdateIdle()
    {
        base.UpdateIdle();
        Detect();
        _isMove = false;
    }

    protected override void UpdateAttack()
    {
        base.UpdateAttack();
        Detect();
    }
}
