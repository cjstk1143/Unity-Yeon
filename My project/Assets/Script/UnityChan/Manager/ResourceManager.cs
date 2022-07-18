using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : MonoBehaviour
{
    // 역할
    // 1. Prefabs을 이용해서 Instantiate함수로 Clone을 만들 수 있었다.
    // 2. Instantiate으로 만든 Clone들을 어디서 만들었는지 추적하기가 쉽지 않다.

    // Template / Generic
    // where는 함수 뒤에 조건식(반환 형식에 대한 조건)
    public T Load<T>(string path) where T : Object
    {
        return Resources.Load<T>(path);
    }

    public GameObject Instantiate(string path, Transform parent = null)
    {
        GameObject prefab = Load<GameObject>($"Prefabs/{path}");
        if (prefab == null)
        {
            Debug.LogError($"Can't Find {path}");
            return prefab;
        }

        return Object.Instantiate(prefab, parent);
    }

    public void Destroy(GameObject go)
    {
        if (go != null)
            Object.Destroy(go);

        return;
    }
}
