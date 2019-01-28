using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

[Serializable]
public class Spell : IUseable, IMoveable, IDescribable
{
    [SerializeField]
    private string name;
    [SerializeField]
    private string nameTitel;
    [SerializeField]
    private int damage;
    [SerializeField]
    private Sprite icon;
    [SerializeField]
    private float speed;
    [SerializeField]
    private float castTime;
    [SerializeField]
    private GameObject spellPrefab;
    [SerializeField]
    private string element;
    [SerializeField]
    private Color barColor;

    public string MyName
    {
        get
        {
            return name;
        }
    }
    public int MyDamage
    {
        get
        {
            return damage;
        }
    }
    public Sprite MyIcon
    {
        get
        {
            return icon;
        }
    }
    public float MySpeed
    {
        get
        {
            return speed;
        }
    }
    public float MyCastTime
    {
        get
        {
            return castTime;
        }
    }
    public GameObject MySpellPrefab
    {
        get
        {
            return spellPrefab;
        }
    }
    public Color MyBarColor
    {
        get
        {
            return barColor;
        }
    }

    public string GetDescription()
    {
        return string.Format("{0}\n<color=#d7d4ae>Действие:</color> На цель \n<color=#d7d4ae>Подготовка: </color>{1}<color=#d7d4ae>сек.\nЭффект: Наносит </color><color=#dac644>{2}</color> <color=#d7d4ae>ед. урона цели.\nСтихия: </color>{3}", nameTitel, castTime, damage, element);
    }

    public void Use()
    {
        Player.MyInstance.CasteSpell(MyName);
    }
}

