using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;

public class PlayerControll : BaseControll
{
    public GameObject _hpBar;
    public Dictionary<string, GameObject> _swordTable;

    private GameObject _curSword;
     
    protected override void Start()
    {
        base.Start();

        InputManager input = Managers.Input;
        input.MouseAction -= OnClick; // 오류 방지 코드
        input.MouseAction += OnClick;
        input.KeyAction -= Attack;
        input.KeyAction += Attack;
        input.KeyAction -= Jump;
        input.KeyAction += Jump;

        _swordTable = new Dictionary<string, GameObject>();
        GameObject[] swords = GameObject.FindGameObjectsWithTag("PlayerWeapon");

        foreach (GameObject go in swords)
        {
            _swordTable[go.name] = go;
            go.GetComponent<Collider>().enabled = false;
            go.SetActive(false);
        }

        _swordTable["sword_epic"].SetActive(true);
        _curSword = _swordTable["sword_epic"];
    }

    protected override void Update()
    {
        base.Update();
    }

    private bool OnKeyBoard()
    {
        if (State == Define.State.ATTACK)
            return false;

        if (Input.GetKey(KeyCode.W))
        {
            Quaternion q = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.forward), 0.2f);
            transform.rotation = q;

            if (Input.GetKey(KeyCode.A))
            {
                Quaternion qA = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.left), 0.2f);
                transform.rotation = qA;
                transform.Translate(Vector3.forward * Time.deltaTime * _speed);

                return true;
            }
            if (Input.GetKey(KeyCode.D))
            {
                Quaternion qD = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.right), 0.2f);
                transform.rotation = qD;
                transform.Translate(Vector3.forward * Time.deltaTime * _speed);

                return true;
            }

            transform.Translate(Vector3.forward * Time.deltaTime * _speed);
            return true;
        }

        if (Input.GetKey(KeyCode.S))
        {
            Quaternion q = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.back), 0.2f);
            transform.rotation = q;

            if (Input.GetKey(KeyCode.A))
            {
                Quaternion qA = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.left), 0.2f);
                transform.rotation = qA;
                transform.Translate(Vector3.forward * Time.deltaTime * _speed);

                return true;
            }
            if (Input.GetKey(KeyCode.D))
            {
                Quaternion qD = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.right), 0.2f);
                transform.rotation = qD;
                transform.Translate(Vector3.forward * Time.deltaTime * _speed);

                return true;
            }

            transform.Translate(Vector3.forward * Time.deltaTime * _speed);
            return true;
        }

        if (Input.GetKey(KeyCode.A))
        {
            Quaternion q = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.left), 0.2f);
            transform.rotation = q;
            transform.Translate(Vector3.forward * Time.deltaTime * _speed);
            _isMove = true;

            return true;
        }

        if (Input.GetKey(KeyCode.D))
        {
            Quaternion q = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.right), 0.2f);
            transform.rotation = q;
            transform.Translate(Vector3.forward * Time.deltaTime * _speed);
            _isMove = true;

            return true;
        }

        return false;
    }

    private void OnClick(Define.MouseEvent evt)
    {
        // 카메라부터 Plane에 레이저 쏴서 처음 부딪히는 위치 찾기
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        /* 
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.nearClipPlane));
        //Vector3 dir = mousePos - Camera.main.transform.position ;
        dir.Normalize();
        */

        Debug.DrawRay(Camera.main.transform.position, ray.direction * 100, Color.red);
        // Vector..ㅋ (x, y, z)이지만 그 안엔 길이까지(+방향) 포함되어 있는 건가..?
        // 그게 아니고서야 어떻게 (x, y, z)에 이미 길이가 있는 건 물론이고
        // 30을 곱했다고 어떻게 더 길어질 수가 있는 거지...!!?ㅋㅋ
        // 신기하네.. 때론 포인트(특정 위치)가 되고 때론 방향을 포함한 길이가 되니.. 

        RaycastHit hit;
        LayerMask layerMask = LayerMask.GetMask("Plane");

        if (Physics.Raycast(ray, out hit, 100, layerMask))
        {            
            _rayHitPosition = hit.point;
        }
    }

    public void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _anim.SetBool("IsJump",true);
        }
    }

    public void BottonJump()
    {
        _anim.SetBool("IsJump", true);
    }

    public void JumpDown()
    {
        _anim.SetBool("IsJump", false);
    }

    public void ChangeSword(string name)
    {
        foreach(var pair in _swordTable)
        {
            pair.Value.SetActive(false);
        }

        _swordTable[name].SetActive(true);
        _swordTable[name].GetComponent<Collider>().enabled = false;
    }

    public void Attack()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            State = Define.State.ATTACK;
        }
    }

    public void AttackEnd()
    {
        State = Define.State.IDLE;
        _curSword.GetComponent<Collider>().enabled = false;
    }

    public void WeaponColTrigger()
    {
        _curSword.GetComponent<Collider>().enabled = true;
    }

    private void MouseMove()
    {
        // 캐릭터로부터 레이저 포인트 위치를 빼면 => 캐릭터부터 레이저 찍힌 위치까지의 방향
        _rayHitPosition.y = transform.position.y; // (이동할) y값은 캐릭터의 y값과 일치(당연히. 바닥이니까~)
        Vector3 derectionToHit = _rayHitPosition - transform.position; // 방향
        Vector3 dir = derectionToHit.normalized; // 방향으로의 단위 벡터 => 가까운 곳 찍든, 먼 곳 찍든 일정한 속도로 가기 위하여!!

        Vector3 orginPos = transform.position;
        orginPos += _capsuleCol.center;

        Debug.DrawRay(orginPos, dir * 1.2f, Color.red);
        if (Physics.Raycast(orginPos, dir, 1.2f, LayerMask.GetMask("Block")))
        {
            _isMove = false;
            return;
        }

        if (derectionToHit.magnitude < 0.1f) // magnitude <- 길이 
        {
            _isMove = false;
        }
        else
        {
            NavMeshAgent agent = GetComponent<NavMeshAgent>();
            agent.Move(dir * _speed * Time.deltaTime);
            //transform.position += dir * _speed * Time.deltaTime;
            _isMove = true;

            transform.rotation =
                Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dir), 5.0f * Time.deltaTime);
        }
    }

    protected override void UpdateMove()
    {
        base.UpdateMove();

        if (!OnKeyBoard())
            State = Define.State.IDLE;
    }

    protected override void UpdateIdle()
    {
        base.UpdateIdle();

        if (OnKeyBoard())
            State = Define.State.MOVE;
    }

    protected override void UpdateAttack()
    {
        base.UpdateAttack();
    }
}
