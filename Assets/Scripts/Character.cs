using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField]
    private float maxHP = 3;
    [SerializeField]
    private RectTransform hpGauage;
    private float hp;
    private float hpMaxWidth;

    void Start()
    {
        hp = maxHP;

        if (hpGauage != null)
        {
            hpMaxWidth = hpGauage.sizeDelta.x;
        }
    }

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

        if (hpGauage != null)
        {
            hpGauage.sizeDelta = new Vector2(hp / maxHP * hpMaxWidth, hpGauage.sizeDelta.y);
        }

        return hp > 0;
    }
}