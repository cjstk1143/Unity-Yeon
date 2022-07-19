using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;

public class Movement : MonoBehaviour
{
    [SerializeField]
    private float _speed = 5.0f;
    Vector3 _rayHitPosition = Vector3.zero;

    private bool _isMove = false;
    private CapsuleCollider _capsuleCol;
    enum State
    {
        IS_MOVE,
        IS_JUMP,
        IS_DEAD,
        IS_SKILL
    }

    private float _ratio = 0.0f;
    private Animator _anim;

    void Start()
    {
        InputManager input = Managers.Input;
        input.KeyAction -= OnKeyBoard; // ���� ���� �ڵ�
        input.KeyAction += OnKeyBoard;
        input.MouseAction -= OnClick;
        input.MouseAction += OnClick;
        input.KeyAction -= Attack;
        input.KeyAction += Attack;
        input.KeyAction -= Jump;
        input.KeyAction += Jump;

        _anim = GetComponent<Animator>();
        _capsuleCol = GetComponent<CapsuleCollider>();

        _rayHitPosition = transform.position;
    }

    void MouseMove()
    {
        // ĳ���ͷκ��� ������ ����Ʈ ��ġ�� ���� => ĳ���ͺ��� ������ ���� ��ġ������ ����
        _rayHitPosition.y = transform.position.y; // (�̵���) y���� ĳ������ y���� ��ġ(�翬��. �ٴ��̴ϱ�~)
        Vector3 derectionToHit = _rayHitPosition - transform.position; // ����
        Vector3 dir = derectionToHit.normalized; // ���������� ���� ���� => ����� �� ���, �� �� ��� ������ �ӵ��� ���� ���Ͽ�!!

        Vector3 orginPos = transform.position;
        orginPos += _capsuleCol.center;

        Debug.DrawRay(orginPos, dir * 1.2f, Color.red);
        if (Physics.Raycast(orginPos, dir, 1.2f, LayerMask.GetMask("Block")))
        {
            _isMove = false;
            return;
        }

        if (derectionToHit.magnitude < 0.1f) // magnitude <- ���� 
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

    void Update()
    {
        MouseMove();

        if (_isMove)
        {
            _anim.SetFloat("Speed", _speed);
        }
        else
        {
            _anim.SetFloat("Speed", 0);
        }

        Jump();
    }

    private void OnKeyBoard()
    {
        if (Input.GetKey(KeyCode.W))
        {
            Quaternion q = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.forward), 0.2f);
            transform.rotation = q;
            transform.Translate(Vector3.forward * Time.deltaTime * _speed);
            _isMove = true;
        }
        if (Input.GetKey(KeyCode.S))
        {
            Quaternion q = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.back), 0.2f);
            transform.rotation = q;
            transform.Translate(Vector3.forward * Time.deltaTime * _speed);
            _isMove = true;
        }
        if (Input.GetKey(KeyCode.A))
        {
            Quaternion q = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.left), 0.2f);
            transform.rotation = q;
            transform.Translate(Vector3.forward * Time.deltaTime * _speed);
            _isMove = true;
        }
        if (Input.GetKey(KeyCode.D))
        {
            Quaternion q = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.right), 0.2f);
            transform.rotation = q;
            transform.Translate(Vector3.forward * Time.deltaTime * _speed);
            _isMove = true;
        }

        //if (Input.anyKey)
        //    _isMove = false;
    }

    private void OnClick(Define.MouseEvent evt)
    {
        // ī�޶���� Plane�� ������ ���� ó�� �ε����� ��ġ ã��
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        /* 
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.nearClipPlane));
        //Vector3 dir = mousePos - Camera.main.transform.position ;
        dir.Normalize();
        */

        Debug.DrawRay(Camera.main.transform.position, ray.direction * 100, Color.red);
        // Vector..�� (x, y, z)������ �� �ȿ� ���̱���(+����) ���ԵǾ� �ִ� �ǰ�..?
        // �װ� �ƴϰ��� ��� (x, y, z)�� �̹� ���̰� �ִ� �� �����̰�
        // 30�� ���ߴٰ� ��� �� ����� ���� �ִ� ����...!!?����
        // �ű��ϳ�.. ���� ����Ʈ(Ư�� ��ġ)�� �ǰ� ���� ������ ������ ���̰� �Ǵ�.. 

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

    public void Attack()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            _anim.SetBool("IsAttack", true);
        }
    }

    public void AttackEnd()
    {
        _anim.SetBool("IsAttack", false);
    }
}
