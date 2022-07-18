using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : MonoBehaviour
{
    // ����
    // 1. Prefabs�� �̿��ؼ� Instantiate�Լ��� Clone�� ���� �� �־���.
    // 2. Instantiate���� ���� Clone���� ��� ��������� �����ϱⰡ ���� �ʴ�.

    // Template / Generic
    // where�� �Լ� �ڿ� ���ǽ�(��ȯ ���Ŀ� ���� ����)
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
