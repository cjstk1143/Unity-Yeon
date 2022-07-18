using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InputManager
{
    // Delegate : �븮��, �Լ� �����͸� ��� ���� �迭.(?)
    // �̷� ������ Delegate ���� ��.(?) => Listener Pattern
    public Action KeyAction = null;
    public Action<Define.MouseEvent> MouseAction = null;
                // ���� < > �̰� �Ű�������.

    bool _pressed = false;

    public void OnUpdate() // Update�� �ƴ� OnUpdate�� ������ �����ΰ� �ƴ� ���������� �پ Update�ؾ��ϱ� ����!
    {
        // UI�� ���콺�� �ö�������� ����
        if (EventSystem.current.IsPointerOverGameObject()) // ���콺�� ���� �� GameObject�� �ö���ֳ���?
            return; // =>Panel�� �� ��ũ��Ʈ�� ������.. EventSystem�� ����..

        if (Input.anyKey && KeyAction != null)
            KeyAction.Invoke(); // == KeyAction();�� ����ϴٳ� ���ٳ�~ ����Լ��� ȣ�����ִ� �ŷ�(..?)

        if (MouseAction != null)
        {
            if (Input.GetMouseButton(0)) //MouseAction�� 1���� ����, �Ű������� 1���� ��. == ��Ŭ��, ��Ŭ�� ���� ����
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
