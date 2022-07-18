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
        // Update에서 프레임마다 호출하는 게 아니라
        // 일정부분 실행시키고, 다음 Tick에서 이어서 실행시킨다.
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
        Debug.Log("마우스 버튼이 눌렸습니다!");
        _score++;
        _tmpText.text = $"Score : {_score}";
    }
    
    // 게임 시작하고 0.5초 뒤에 실행되는 함수를 만들고 싶다.
    IEnumerator CO_UnityCoroutineTest(float waitTime)
    {
        Debug.Log("폭탄 터짐");
        yield return new WaitForSeconds(waitTime);  
        Debug.Log("폭탄 소리!!!");
        yield break;
        // return 번호 상관없이 위에서 아래로 실행 됨.
    }
}
