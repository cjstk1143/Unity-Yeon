using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotation : MonoBehaviour
{
    [SerializeField]
    private float _speed = 30.0f;
    private float _angleY = 0.0f;

    void Start()
    {
        
    }

    void Update()
    {
        _angleY += _speed * Time.deltaTime;

        transform.localRotation = Quaternion.Euler(new Vector3(0, _angleY, 0));
        //+�˴ٽ��� �ӵ��� 0���� �ؾ� �����ӵ��� �����ӵ��� ����.
        //�ƹ����� local�� ���.. �� �� ���� ���ÿ� ������ ���̴�. ������ ������ �θ��ΰž�. �װ� rg? 
    }
}
