using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InputManager
{
    // Delegate : 대리자, 함수 포인터를 담는 동적 배열.(?)
    // 이런 식으로 Delegate 쓰는 것.(?) => Listener Pattern
    public Action KeyAction = null;
    public Action<Define.MouseEvent> MouseAction = null;
                // 위에 < > 이거 매개변수임.

    bool _pressed = false;

    public void OnUpdate() // Update가 아닌 OnUpdate이 이유는 스스로가 아닌 누군가에게 붙어서 Update해야하기 때문!
    {
        // UI에 마우스가 올라와있으면 리턴
        if (EventSystem.current.IsPointerOverGameObject()) // 마우스가 지금 내 GameObject에 올라와있나요?
            return; // =>Panel에 이 스크립트는 없지만.. EventSystem은 있지..

        if (Input.anyKey && KeyAction != null)
            KeyAction.Invoke(); // == KeyAction();와 비슷하다나 같다나~ 멤버함수를 호출해주는 거래(..?)

        if (MouseAction != null)
        {
            if (Input.GetMouseButton(0)) //MouseAction의 1번이 실행, 매개변수로 1번이 들어감. == 우클릭, 좌클릭 맞음 ㅇㅇ
            {
                MouseAction.Invoke(Define.MouseEvent.Press);
                _pressed = true;
            }
            else
            {
                if (_pressed)
                    MouseAction.Invoke(Define.MouseEvent.Click);
                _pressed = false;
            }
        }
    }
}
