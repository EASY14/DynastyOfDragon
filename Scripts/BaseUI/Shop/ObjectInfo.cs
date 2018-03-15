using UnityEngine;
using System.Collections;

public class ObjectInfo
{
    //商品ID
    public int Id
    {
        get;
        set;
    }
    //商品中文名字 展示用
    public string Name
    {
        get;
        set;
    }
    //商品图标
    public string Icon
    {
        get;
        set;
    }
    //商品种类
    public string EquipType
    {
        get;
        set;
    }
    //商品出售价
    public int SoldPrice
    {
        get;
        set;
    }
    //商品购买价
    public int BuyPrice
    {
        get;
        set;
    }

    //生命值
    public int Hp
    {
        get;
        set;
    }

    //攻击力
    public int AttackValue
    {
        get;
        set;
    }

    //攻击速度
    public float AttackSpeed
    {
        get;
        set;
    }

    //集气速度
    public int PowerSpeed
    {
        get;
        set;
    }

    //移动速度
    public float MoveSpeed
    {
        get;
        set;
    }

    //气力值
    public float Power
    {
        get;
        set;
    }
}
