using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterStat : BaseStat
{
    void Start()
    {
        _maxHp = 100;
        _curHp = _maxHp;
    }
}
