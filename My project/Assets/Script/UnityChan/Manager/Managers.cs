using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Managers : MonoBehaviour
{
    static Managers _instance;
    
    public static Managers Instance { get { Init(); return _instance; } }  // �׳� �����ϰ� �д� �뵵�θ� �ҷ����� ����. �� get��. class ���� �Լ� rg~?

    InputManager _input = new InputManager();
    ResourceManager _resource = new ResourceManager();
    EffectManager _effectManager;

    public static InputManager Input { get { return Instance._input; } }
    public static ResourceManager Resource { get { return Instance._resource; } }
    public static EffectManager Effect { get { return Instance._effectManager; } }

    private void Start()
    {
        Init();

        GameObject UI_Manager = GameObject.Find("UI_Manager");
        if (UI_Manager == null)
            UI_Manager = Resource.Instantiate("UnityChan/UI/UI_Manager");

        UI_Manager.transform.SetParent(gameObject.transform);

        _effectManager = GameObject.Find("EffectManager").GetComponent<EffectManager>();
        if (_effectManager == null)
            Debug.LogError("Effect Manager�� �߰����ּ���.");
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