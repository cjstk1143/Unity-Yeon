using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStat : BaseStat
{
    void Start()
    {
        _maxHp = 300;
        _curHp = _maxHp;
    }
}
