using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MonsterControll : BaseControll
{
    public GameObject _monsterInfo;
    Slider _hpSlider;

    MonsterStat _stat;

    private Vector3 _movePos = Vector3.zero;
    private Vector3 _originPos;
    [SerializeField]
    private float _movingDistance;

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

        StartCoroutine("C0_MonsterAIMove");

        _movingDistance = 5.0f;
    }

    private void OnEnable()
    {
        _movePos = transform.position;
        _originPos = transform.position;
    }

    private void OnDisable()
    {
        StartCoroutine("C0_MonsterAIMove");
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

    IEnumerator C0_MonsterAIMove()
    {
        Debug.Log("AIMove 호출");
        float x = transform.position.x;
        float z = transform.position.z;

        while(true)
        {
           x = Random.Range(- 1.0f, 1.0f);
           z = Random.Range(- 1.0f, 1.0f);

           _movePos = new Vector3(x, 0, z);
           _movePos.Normalize();

            Vector3 temp = (transform.position + _movePos) - _originPos; //거리량
            if (temp.magnitude > _movingDistance)
            {
                continue;
            }

            break;
        }

        Debug.Log(_movePos);

        yield return new WaitForSeconds(7.0f);

        StartCoroutine("C0_MonsterAIMove");
    }

    void RandMove()
    {
        transform.Translate(_movePos * Time.deltaTime);
    }

    protected override void UpdateMove()
    {
        base.UpdateMove();
    }

    protected override void UpdateIdle()
    {
        base.UpdateIdle();
        RandMove();
    }

    protected override void UpdateAttack()
    {
        base.UpdateAttack();
    }
}
