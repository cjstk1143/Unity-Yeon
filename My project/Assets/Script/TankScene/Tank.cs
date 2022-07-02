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
            Debug.Log("Ball을 못 찾았습니다.");
        if (_muzzle == null)
            Debug.Log("Muzzle 못 찾았습니다.");
        if (_head == null)
            Debug.Log("Head 못 찾았습니다.");

        _ballList = GameObject.Find("ObjectPool").GetComponent<ObjectPool>().GetList();
        
        if (GameObject.Find("ObjectPool") == null)
            Debug.Log("ObjectPool를 못찾음");

        if (_ballList.Count == 0)
            Debug.Log("_ballList를 못찾음");

        _camera = GameObject.Find("Main Camera").GetComponent<Transform>();
        if (_camera == null)
            Debug.Log("_camera를 못찾음");

        _virtualCameraTransform = transform.Find("CameraPos");
        if (_virtualCameraTransform == null)
            Debug.Log("_virtualCameraTransform를 못찾음");
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
            //        // TransformDirection 이건 현재, 위에 _head를 기준으로 한 방향을 말하는 거임.
            //        // 그 방향을 world 방향으로 변환해서 가져온 것. = 이유는 모르나 변환을 무조건 해야하나봐. 흠.
            //        // _ball.GetComponent<TankBall>().SetDirection(Vector3.forward);
            //        // 이건 딴 기준 볼것도 없이 딱 한번에 world 기준으로 정해진 한 방향!만 가리킴.

            //        return;
            //    }
            //}  이걸 줄이면 짜잔~ 아래를 보라구~ 'c# 람다'라나 뭐라나~
            GameObject ball = _ballList.Find(ball => ball.activeSelf == false);
            if (ball != null)
            {
                ball.SetActive(true);
                ball.transform.position = _muzzle.position;
                ball.GetComponent<TankBall>().SetDirection(_head.transform.TransformDirection(Vector3.forward));
                //_ball.transform.position = _muzzle.transform.localPosition;
                //이렇게 하면 world(0, 0, 0.5)로 가게 되는데 그 이유가 중요함.
                //Muzzle의 위치가 Barrel기준으로(0, 0, 0.5)임.
                //Barrel의(0, 0, 0)은 Head기준으로(0,0,1)임.
                //Head의(0, 0, 0)은 Body(0,0,0)이고 Body는 Tank(0, 0, 0)를 기준으로 함
                //그리고 Tank는!!! world를 기준으로 한다 이말씀!!!즉->world(0, 0, 0.5)로 가게 되는 거임.
                //쉽게 말해서 local은 무조건 부모기준!
                //world는..world가 기준이긴 한데. 무조건 부모로만 좁히는 게 아니라 최대한 자식끝까지 넓혀서 본다고 해야하나..ㅋ
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
            // Vector..ㅋ 시팔 (x, y, z)이지만 그 안엔 길이까지(+방향) 포함되어 있는 건가..?
            // 그게 아니고서야 어떻게 (x, y, z)에 이미 길이가 있는 건 물론이고 30을 곱했다고 어떻게 더 길어질 수가 있는 거지...!!?ㅋㅋ
            // 신기하네.. 때론 포인트(특정 위치)가 되고 때론 방향을 포함한 길이가 되니..

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

            Vector3 derectionToHit = rayHitPosition - transform.position; // 방향
            derectionToHit.Normalize(); // 방향으로의 단위 벡터 = 속도를 늦추려는건지 맞추려는 건지.
            transform.position += derectionToHit * _speed * Time.deltaTime;

            transform.rotation = 
                Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(derectionToHit), 5.0f * Time.deltaTime);
        }
    }
}
