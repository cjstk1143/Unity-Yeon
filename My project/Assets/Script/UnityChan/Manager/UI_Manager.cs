using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_Manager : MonoBehaviour
{
    public TMP_Text _tmpText;
    private IEnumerator _co; 

    int _score = 0;

    // Coroutine
    // c++ : 21
    // c#

    void ComplexFunction()
    {
        // Update���� �����Ӹ��� ȣ���ϴ� �� �ƴ϶�
        // �����κ� �����Ű��, ���� Tick���� �̾ �����Ų��.
        // => Coroutine

        for (int i = 0; i < 10000; i++)
        {
            if (i == 1000)
                break;
        }
    }

    private void Start()
    {
        _co = CO_UnityCoroutineTest(0.5f);

        if (_co != null)
            StartCoroutine(_co);
    }

    private void Update()
    {
        ComplexFunction();
    }

    public void OnMouseClick()
    {
        Debug.Log("���콺 ��ư�� ���Ƚ��ϴ�!");
        _score++;
        _tmpText.text = $"Score : {_score}";
    }
    
    // ���� �����ϰ� 0.5�� �ڿ� ����Ǵ� �Լ��� ����� �ʹ�.
    IEnumerator CO_UnityCoroutineTest(float waitTime)
    {
        Debug.Log("��ź ����");
        yield return new WaitForSeconds(waitTime);  
        Debug.Log("��ź �Ҹ�!!!");
        yield break;
        // return ��ȣ ������� ������ �Ʒ��� ���� ��.
    }
}
