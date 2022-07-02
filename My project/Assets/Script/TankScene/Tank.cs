using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tank : MonoBehaviour
{
    public List<GameObject> _ballList;
    private GameObject _ball;
    private Transform _muzzle;
    private Transform _head;

    private Transform _camera;
    private Transform _virtualCameraTransform;

    [SerializeField]
    private float _speed = 5.0f;

    private void Awake()
    { 
    }
    private void OnEnable()
    {
    }

    void Start()
    {
        _ball = Resources.Load<GameObject>($"Prefabs/Ball");
        _muzzle = transform.Find("Body/Head/Barrel/Muzzle");
        _head = transform.Find("Body/Head");

        if (_ball == null)
            Debug.Log("Ball�� �� ã�ҽ��ϴ�.");
        if (_muzzle == null)
            Debug.Log("Muzzle �� ã�ҽ��ϴ�.");
        if (_head == null)
            Debug.Log("Head �� ã�ҽ��ϴ�.");

        _ballList = GameObject.Find("ObjectPool").GetComponent<ObjectPool>().GetList();
        
        if (GameObject.Find("ObjectPool") == null)
            Debug.Log("ObjectPool�� ��ã��");

        if (_ballList.Count == 0)
            Debug.Log("_ballList�� ��ã��");

        _camera = GameObject.Find("Main Camera").GetComponent<Transform>();
        if (_camera == null)
            Debug.Log("_camera�� ��ã��");

        _virtualCameraTransform = transform.Find("CameraPos");
        if (_virtualCameraTransform == null)
            Debug.Log("_virtualCameraTransform�� ��ã��");
    }

    void Update()
    {
        Move();
        Fire();
        MoveCamera();
        RayCast();
        Move2();
    }

    private void Move()
    {
        // w
        if (Input.GetKey(KeyCode.W))
        {
            //transform.rotation = Quaternion.LookRotation(Vector3.forward);
            //transform.position += Vector3.forward * Time.deltaTime * _speed;
            Quaternion q = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.forward), 0.2f);
            transform.rotation = q;
            transform.Translate(Vector3.forward * Time.deltaTime * _speed);
        }

        // s
        if (Input.GetKey(KeyCode.S))
        {
            //transform.rotation = Quaternion.LookRotation(Vector3.back);
            //transform.position += Vector3.back * Time.deltaTime * _speed;
            Quaternion q = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.back), 0.2f);
            transform.rotation = q;
            transform.Translate(Vector3.forward * Time.deltaTime * _speed);
        }

        // a
        if (Input.GetKey(KeyCode.A))
        {
            //transform.rotation = Quaternion.LookRotation(Vector3.left);
            //transform.position += Vector3.left * Time.deltaTime * _speed;
            Quaternion q = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.left), 0.2f);
            transform.rotation = q;
            transform.Translate(Vector3.forward * Time.deltaTime * _speed);
        }

        // d
        if (Input.GetKey(KeyCode.D))
        {
            //transform.rotation = Quaternion.LookRotation(Vector3.right);
            //transform.position += Vector3.right * Time.deltaTime * _speed;
            Quaternion q = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.right), 0.2f);
            transform.rotation = q;
            transform.Translate(Vector3.forward * Time.deltaTime * _speed);
        }
    }

    private void Fire()
    {
        if (Input.GetMouseButtonUp(0))
        {
            //for (int i = 0; i < 30; i++)
            //{
            //    if (_ballList[i].activeSelf == false)
            //    {
            //        _ballList[i].transform.position = _muzzle.position;
            //        _ballList[i].GetComponent<TankBall>().SetDirection(_head.transform.TransformDirection(Vector3.forward));
            //        _ballList[i].SetActive(true);
            //        // TransformDirection �̰� ����, ���� _head�� �������� �� ������ ���ϴ� ����.
            //        // �� ������ world �������� ��ȯ�ؼ� ������ ��. = ������ �𸣳� ��ȯ�� ������ �ؾ��ϳ���. ��.
            //        // _ball.GetComponent<TankBall>().SetDirection(Vector3.forward);
            //        // �̰� �� ���� ���͵� ���� �� �ѹ��� world �������� ������ �� ����!�� ����Ŵ.

            //        return;
            //    }
            //}  �̰� ���̸� ¥��~ �Ʒ��� ����~ 'c# ����'�� ����~
            GameObject ball = _ballList.Find(ball => ball.activeSelf == false);
            if (ball != null)
            {
                ball.SetActive(true);
                ball.transform.position = _muzzle.position;
                ball.GetComponent<TankBall>().SetDirection(_head.transform.TransformDirection(Vector3.forward));
                //_ball.transform.position = _muzzle.transform.localPosition;
                //�̷��� �ϸ� world(0, 0, 0.5)�� ���� �Ǵµ� �� ������ �߿���.
                //Muzzle�� ��ġ�� Barrel��������(0, 0, 0.5)��.
                //Barrel��(0, 0, 0)�� Head��������(0,0,1)��.
                //Head��(0, 0, 0)�� Body(0,0,0)�̰� Body�� Tank(0, 0, 0)�� �������� ��
                //�׸��� Tank��!!! world�� �������� �Ѵ� �̸���!!!��->world(0, 0, 0.5)�� ���� �Ǵ� ����.
                //���� ���ؼ� local�� ������ �θ����!
                //world��..world�� �����̱� �ѵ�. ������ �θ�θ� ������ �� �ƴ϶� �ִ��� �ڽĳ����� ������ ���ٰ� �ؾ��ϳ�..��
            }
        }
    }

    private void MoveCamera()
    {
        //_camera.position = _virtualCameraTransform.position;
        //_camera.rotation = _virtualCameraTransform.rotation;
    }

    private void RayCast()
    {
        Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 10, Color.red);

        //RaycastHit hit;
        //LayerMask layerMask = LayerMask.GetMask("Tank") | LayerMask.GetMask("Plane");
        //if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward) * 10, out hit, 10, layerMask))
        //    Debug.Log(hit.transform.name);
    }

    private void Move2()
    {
        if (Input.GetMouseButton(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            /* 
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.nearClipPlane));
            //Vector3 dir = mousePos - Camera.main.transform.position ;
            dir.Normalize();
            */

            Debug.DrawRay(Camera.main.transform.position, ray.direction * 100, Color.red);
            // Vector..�� ���� (x, y, z)������ �� �ȿ� ���̱���(+����) ���ԵǾ� �ִ� �ǰ�..?
            // �װ� �ƴϰ��� ��� (x, y, z)�� �̹� ���̰� �ִ� �� �����̰� 30�� ���ߴٰ� ��� �� ����� ���� �ִ� ����...!!?����
            // �ű��ϳ�.. ���� ����Ʈ(Ư�� ��ġ)�� �ǰ� ���� ������ ������ ���̰� �Ǵ�..

            RaycastHit hit;
            Vector3 rayHitPosition = new Vector3();
            LayerMask layerMask = LayerMask.GetMask("Plane");

            if (Physics.Raycast(ray, out hit, 100, layerMask))
            {
                rayHitPosition = hit.point;
                Debug.Log(hit.transform.name);
            }
            rayHitPosition.y = transform.position.y;
            //Debug.Log(rayHitPosition);

            Vector3 derectionToHit = rayHitPosition - transform.position; // ����
            derectionToHit.Normalize(); // ���������� ���� ���� = �ӵ��� ���߷��°��� ���߷��� ����.
            transform.position += derectionToHit * _speed * Time.deltaTime;

            transform.rotation = 
                Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(derectionToHit), 5.0f * Time.deltaTime);
        }
    }
}
