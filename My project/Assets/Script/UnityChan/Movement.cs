using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        //Move();
        Move2();

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
        }
        if (Input.GetKey(KeyCode.S))
        {
            Quaternion q = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.back), 0.2f);
            transform.rotation = q;
            transform.Translate(Vector3.forward * Time.deltaTime * _speed);
        }
        if (Input.GetKey(KeyCode.A))
        {
            Quaternion q = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.left), 0.2f);
            transform.rotation = q;
            transform.Translate(Vector3.forward * Time.deltaTime * _speed);
        }
        if (Input.GetKey(KeyCode.D))
        {
            Quaternion q = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.right), 0.2f);
            transform.rotation = q;
            transform.Translate(Vector3.forward * Time.deltaTime * _speed);
        }
    }

    private void Move2()
    {
        // 카메라부터 Plane에 레이저 쏴서 처음 부딪히는 위치 찾기
        if (Input.GetMouseButton(1))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            /* 
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.nearClipPlane));
            //Vector3 dir = mousePos - Camera.main.transform.position ;
            dir.Normalize();
            */

            Debug.DrawRay(Camera.main.transform.position, ray.direction * 100, Color.red);
            // Vector..ㅋ 시팔 (x, y, z)이지만 그 안엔 길이까지(+방향) 포함되어 있는 건가..?
            // 그게 아니고서야 어떻게 (x, y, z)에 이미 길이가 있는 건 물론이고
            // 30을 곱했다고 어떻게 더 길어질 수가 있는 거지...!!?ㅋㅋ
            // 신기하네.. 때론 포인트(특정 위치)가 되고 때론 방향을 포함한 길이가 되니..

            RaycastHit hit;
            LayerMask layerMask = LayerMask.GetMask("Plane");

            if (Physics.Raycast(ray, out hit, 100, layerMask))
            {
                rayHitPosition = hit.point;
                Debug.Log(hit.transform.name);
            }
        }

        // 캐릭터로부터 레이저 포인트 위치를 빼면 => 캐릭터부터 레이저 찍힌 위치까지의 방향
        rayHitPosition.y = transform.position.y; // (이동할) y값은 캐릭터의 y값과 일치(당연히. 바닥이니까~)
        Vector3 derectionToHit = rayHitPosition - transform.position; // 방향
        Vector3 dir = derectionToHit.normalized; // 방향으로의 단위 벡터 => 가까운 곳 찍든, 먼 곳 찍든 일정한 속도로 가기 위하여!!

        if (derectionToHit.magnitude < 0.01f) // magnitude <- 길이 
        {
            _isMove = false;
            return;
        }

        transform.position += dir * _speed * Time.deltaTime;
        _isMove = true;

        transform.rotation =
            Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dir), 5.0f * Time.deltaTime);
    }

    private void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            anim.SetBool("IsJump",true);
        }
    }

    public void JumpDown()
    {
        anim.SetBool("IsJump", false);
    }
}
