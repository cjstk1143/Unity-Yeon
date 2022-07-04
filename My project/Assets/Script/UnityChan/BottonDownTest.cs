using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BottonDownTest : MonoBehaviour
{
    public Text _UItext;
    int _score = 0;

    private void Start()
    {
    }

    public void OnMouseClick()
    {
        Debug.Log("마우스 버튼이 눌렸습니다!");
        _score++;
        _UItext.text = $"Score : {_score}";
    }
}
