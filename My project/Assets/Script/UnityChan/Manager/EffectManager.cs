using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectManager : MonoBehaviour
{
    Dictionary<string, List<GameObject>> _effectTable;
    int _poolCount = 30;

    private void Start()
    {
        _effectTable = new Dictionary<string, List<GameObject>>();

        List<GameObject> temp = new List<GameObject>();

        for (int i = 0; i < _poolCount; i++)
        {
            GameObject go = Managers.Resource.Instantiate("Hits/Hit_04", transform);

            if (go == null)
                Debug.LogError("Hit Effect 못찾음");

            go.SetActive(false);
            temp.Add(go);
        }

        _effectTable["Attack"] = temp;
    }

    public void PlayEffect(string name, Vector3 pos)
    {
        if (!_effectTable.ContainsKey(name)) // _effectTable에 변수 name이라는 얘가 있으면 true
            Debug.LogError(name + "을 못찾음");

        foreach (GameObject go in _effectTable[name])
        {
            if (go.activeSelf == false)
            {
                go.SetActive(true);
                go.transform.position = pos;
                ParticleSystem particle = go.GetComponent<ParticleSystem>();
                particle.Play();
                break;
            }
        }
    }
}
