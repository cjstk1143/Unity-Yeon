using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Managers : MonoBehaviour
{
    static Managers _instance;
    
    public static Managers Instance { get { Init(); return _instance; } }  // �׳� �����ϰ� �д� �뵵�θ� �ҷ����� ����. �� get��. class ���� �Լ� rg~?

    InputManager _input = new InputManager();

    public static InputManager Input { get { return Instance._input; } } 

    private void Start()
    {
        Init();
    }

    private void Update()
    {
        _input.OnUpdate();
    }

    public static void Init()
    {
        if (_instance == null)
        {
            GameObject go = GameObject.Find("@Managers");
            if (go == null)
            {
                go = new GameObject { name = "@Managers"}; // go = new GameObject(); go.name = "@Managers"; �̰͵� ��
                go.AddComponent<Managers>();
            }

            // ���� �ٲ� �Ȼ������
            DontDestroyOnLoad(go);
            _instance = go.GetComponent<Managers>();
        }
    }
}