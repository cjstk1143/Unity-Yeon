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
    private float _attackRange = 2.0f;

    private bool _dectMoving = false;

    private void OnEnable()
    {
        _movePos = transform.position;
        _originPos = transform.position;
    }

    protected override void Start()
    {
        base.Start();

        if (_monsterInfo == null)
            _monsterInfo = transform.Find("Monster_Info").gameObject;

        _hpSlider = _monsterInfo.GetComponentInChildren<Slider>();

        if (_hpSlider == null)
            Debug.LogError("못찾음");

        _stat = GetComponent<MonsterStat>();

        _state = Define.State.IDLE; //처음 상태 애니 설정.

        StartCoroutine("Co_MonsterAIMove");

        _movingDistance = 5.0f;
        _detectDistance = 5.0f;
        _attackRange = 1.1f;
        _speed = 2.0f;

        _player = GameObject.FindGameObjectWithTag("Player");
    }

    protected override void Update()
    {
        base.Update(); //걍 계속 확인하는 거. 함수호출까지.
        SetHpBar();

        gameObject.SetActive(!_stat.IsDead());
    }


    private void OnDisable()
    {
        StopCoroutine("Co_MonsterAIMove");
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
            Managers.Effect.PlayEffect("Attack", other.ClosestPoint(_player.transform.position));
            _stat.Damaged(30);
            _hpSlider.value = _stat.HpRatio();
        }
    }

    IEnumerator Co_MonsterAIMove()
    {
        if (_isMove == false)
            yield return new WaitForSeconds(3.0f);

        float x;
        float z;

        x = Random.Range(- 5.0f + _originPos.x, 5.0f + _originPos.x);
        // 처음 x위치(32.65)-5.0 => 27.65  처음 x위치(32.65)+5.0 => 37.65
        // ==>> 27.65 와 37.65 사이의 랜덤값
        z = Random.Range(- 5.0f + _originPos.z, 5.0f + _originPos.z);
        // 처음 z위치(45.77)-5.0 => 40.77  처음 x위치(45.77)+5.0 => 50.77
        // ==>> 40.77 와 50.77 사이의 랜덤값

        _movePos = new Vector3(x, transform.position.y, z);

        State = Define.State.MOVE;
        _isMove = true;

        Debug.Log(_movePos);
        yield return new WaitForSeconds(3.0f);
        StartCoroutine("Co_MonsterAIMove");
    }

    void RandMove()
    {
        if (_dectMoving == true)
            return;

        Vector3 temp = _movePos - transform.position;

        if (temp.magnitude < 0.1f)
        {
            State = Define.State.IDLE;

            return;
        }

        transform.rotation = Quaternion.LookRotation(temp);
        transform.position += (temp.normalized * Time.deltaTime * 5.0f);
        }

    bool Detect()
    {
        if(_player == null)
            _player = GameObject.FindGameObjectWithTag("Player");

        float distance = (_player.transform.position - transform.position).magnitude;

        if (distance < _detectDistance)
        {
            _dectMoving = true;
            return true;
        }

        _dectMoving = false;
        return false;
    }

    void Follow()
    {
         State = Define.State.MOVE;

        if (_player == null)
            _player = GameObject.FindGameObjectWithTag("Player");

        Vector3 moveVector = _player.transform.position - transform.position;

        transform.position += moveVector.normalized * _speed * Time.deltaTime;
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(moveVector), 0.1f);
    }

    void Attack()
    {
        if (_isAttack)
            return;

        State = Define.State.ATTACK;
        _isAttack = true;

        return;
    }

    void AttackEnd()
    {
        State = Define.State.IDLE;
        _isAttack = false;

        return;
    }

    protected override void UpdateMove()
    {
        base.UpdateMove();
        RandMove();
        if (Detect() == true)
        {
            Follow();

            Vector3 attackCheck = _player.transform.position - transform.position;

            if (attackCheck.magnitude <= _attackRange)
                State = Define.State.ATTACK;
            else
                State = Define.State.MOVE;
        }
    }

    protected override void UpdateIdle()
    {
        base.UpdateIdle();
        if (Detect() == true)
        {
            Follow();
        }
    }

    protected override void UpdateAttack()
    {
        base.UpdateAttack();
        Detect();
    }
}
