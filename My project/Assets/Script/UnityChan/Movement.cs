using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Movement : MonoBehaviour
{
    [SerializeField]
    private float _speed = 5.0f;
    Vector3 rayHitPosition = Vector3.zero;

    private bool _isMove = false;

    enum State
    {
        IS_MOVE,
        IS_JUMP,
        IS_DEAD,
        IS_SKILL
    }

    private float _ratio = 0.0f;
    private Animator anim;

    void Start()
    {
        InputManager input = Managers.Input;
        input.KeyAction -= Move; // ���� ���� �ڵ�
        input.KeyAction += Move;
        input.MouseAction -= Move2;
        input.MouseAction += Move2;

        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if(_isMove)
        {
            anim.SetFloat("Speed", _speed);
        }
        else
        {
            anim.SetFloat("Speed", 0);
        }

        Jump();
    }

    private void Move()
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

    private void Move2(Define.MouseEvent evt)
    {
        // UI�� ���콺�� �ö�������� ����
        if (EventSystem.current.IsPointerOverGameObject()) // ���콺�� ���� �� GameObject�� �ö���ֳ���?
            return; // =>Panel�� �� ��ũ��Ʈ�� ������.. EventSystem�� ����..

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
            rayHitPosition = hit.point;
            Debug.Log(hit.transform.name);
        }

        // ĳ���ͷκ��� ������ ����Ʈ ��ġ�� ���� => ĳ���ͺ��� ������ ���� ��ġ������ ����
        rayHitPosition.y = transform.position.y; // (�̵���) y���� ĳ������ y���� ��ġ(�翬��. �ٴ��̴ϱ�~)
        Vector3 derectionToHit = rayHitPosition - transform.position; // ����
        Vector3 dir = derectionToHit.normalized; // ���������� ���� ���� => ����� �� ���, �� �� ��� ������ �ӵ��� ���� ���Ͽ�!!

        if (derectionToHit.magnitude < 0.01f) // magnitude <- ���� 
        {
            _isMove = false;
            return;
        }

        transform.position += dir * _speed * Time.deltaTime;
        _isMove = true;

        transform.rotation =
            Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dir), 5.0f * Time.deltaTime);
    }

    public void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            anim.SetBool("IsJump",true);
        }
    }

    public void BottonJump()
    {
        anim.SetBool("IsJump", true);
    }

    public void JumpDown()
    {
        anim.SetBool("IsJump", false);
    }
}
