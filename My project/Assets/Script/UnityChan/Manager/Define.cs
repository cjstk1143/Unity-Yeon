using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Define 
{
    public enum State
    {
        IDLE,
        MOVE,
        DEAD,
        JUMP,
        ATTACK
    }

    public enum MouseEvent
    {
        Press,
        Click
    }
}
