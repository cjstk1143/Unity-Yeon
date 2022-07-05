using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Managers : MonoBehaviour
{
    static Managers _instance;
    
    public static Managers Instance { get { Init(); return _instance; } }  // 그냥 안전하게 읽는 용도로만 불러오는 거임. 이 get이. class 안의 함수 rg~?

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
                go = new GameObject { name = "@Managers"}; // go = new GameObject(); go.name = "@Managers"; 이것도 됨
                go.AddComponent<Managers>();
            }

            // 씬이 바뀌어도 안사라지게
            DontDestroyOnLoad(go);
            _instance = go.GetComponent<Managers>();
        }
    }
}