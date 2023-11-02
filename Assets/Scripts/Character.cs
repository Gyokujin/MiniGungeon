using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField]
    private float maxHP = 3;
    private float hp;

    public void Init()
    {
        hp = maxHP;
    }

    public bool Hit(float damage)
    {
        hp -= damage;

        if (hp < 0)
        {
            hp = 0;
        }

        return hp > 0;
    }
}