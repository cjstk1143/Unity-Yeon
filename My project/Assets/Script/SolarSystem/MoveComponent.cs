using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveComponent : MonoBehaviour
{
    [SerializeField]
    private float _speed = 30.0f;

    private float _angleX = 0.0f;
    private float _angleY = 0.0f;
    private float _angleZ = 0.0f;

    //private float _xDistance = 0.0f;
    //private float _yDistance = 0.0f;
    //private float _zDistance = 0.0f;

    void Start()
    {
        transform.position = Vector3.zero;
    }

    void Update()
    {
        // w
        if (Input.GetKey(KeyCode.W))
        {
            transform.position += Vector3.forward * Time.deltaTime * _speed;
            //_zDistance += Time.deltaTime * _speed;
            //transform.Translate(Vector3.forward * _zDistance);
        }

        // s
        if (Input.GetKey(KeyCode.S))
        {
            transform.position -= Vector3.forward * Time.deltaTime * _speed;
            //_zDistance -= Time.deltaTime * _speed;
            //transform.Translate(Vector3.forward * _zDistance);
        }

        // a
        if (Input.GetKey(KeyCode.A))
        {
            transform.position -= Vector3.right * Time.deltaTime * _speed;
            //_xDistance -= Time.deltaTime * _speed;
            //transform.Translate(Vector3.right * _xDistance);
        }

        // d
        if (Input.GetKey(KeyCode.D))
        {
            transform.position += Vector3.right * Time.deltaTime * _speed;
            //_xDistance += Time.deltaTime * _speed;
            //transform.Translate(Vector3.right * _xDistance); ---> ...�̰Ť� ������ ��ư ���� ���峭 �Լ�(?)��
            // ��ġ�� ���ϰ� ����..? ��� ����� �׷� �� ����.
        }


        // Scale
        // o
        if (Input.GetKey(KeyCode.O))
            transform.localScale += Vector3.one * Time.deltaTime * _speed;

        // p
        if (Input.GetKey(KeyCode.P))
            transform.localScale -= Vector3.one * Time.deltaTime * _speed;


        // Rotation
        _angleX += Time.deltaTime * _speed;
        _angleY += Time.deltaTime * _speed;
        _angleZ += Time.deltaTime * _speed;

        // ������ ���� �������� Quaternion ������ ����Ѵٴ´뤻 ã�ƺ���
        // X��
        if (Input.GetKey(KeyCode.Z))
            transform.eulerAngles += new Vector3(1, 0, 0) * Time.deltaTime * _speed;
        // Y��
        if (Input.GetKey(KeyCode.X))
            transform.eulerAngles += new Vector3(0, 1, 0) * Time.deltaTime * _speed;
        // Z��
        if (Input.GetKey(KeyCode.C))
            transform.eulerAngles += new Vector3(0, 0, 1) * Time.deltaTime * _speed;
    }
}
