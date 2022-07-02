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
        //+알다시피 속도를 0으로 해야 자전속도와 공전속도가 같아.
        //아무래도 local을 써야.. ㅎ 아 몰라 로컬와 월드의 차이는. 로컬은 기준이 부모인거야. 그건 rg? 
    }
}
