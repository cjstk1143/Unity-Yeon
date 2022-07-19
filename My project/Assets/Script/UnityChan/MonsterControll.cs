using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterControll : BaseControll
{
    public GameObject _hpBar;

    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "PlayerWeapon")
        {
            gameObject.SetActive(false);
        }
    }
}
