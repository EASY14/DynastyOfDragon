using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerData
{
    public static string CharacterName = "";

    public static string INITHpSpriteName = "BattleUIdrug";
    public static string INITMpSpriteName = "BattleUIdrug";
    public static int INITHpdrugNum = 0;//初始HP药数量
    public static int INITMpdrugNum = 0;//初始MP药数量
    public static int AddHp = 0;
    public static float AddMp = 0;

    public static int INITCRYSTALNUM = 3000;//钻石

    //public static string HEADSPRITE =  "m";
    public static int GRADE = 1;//玩家等级
    public static int CRYSTALNUM = 3000;//钻石  这里改人民币玩家版
    public static float HP = 200.0f;//生命
    public static float ATK = 5.0f;//攻击力
    public static float AS = 1.0f;//攻击速度
    public static float MS = 1.0f;//移动速度
    public static float POWERADD = 3.0f;//集气速度
    public static int EXP = 0;//玩家在某个等级的当前经验
    public static int MaxEXP = 200;//玩家在某个等级的最大经验

 
    public static List<int> TECHGRADE = new List<int>();//三种技能的等级
    
    public static List<string> BAGNAMES = new List<string>();
    public static List<string>BAGCOUNT = new List<string>();
    public static List<string> EQUIPNAMES = new List<string>();

    public static List<int> UNLOCKLEVEL = new List<int>();//关卡解锁情况

    public static string CurrentHpSpriteName = "BattleUIdrug";
    public static string CurrentMpSpriteName = "BattleUIdrug";
    public static int CurrentHpdrugNum = 0;
    public static int CurrentMpdrugNum = 0;

    public static int CurrentLevelIndex = 0;


    public static float CurrentHP = 200.0f;//当前生命
    public static float CurrentATK = 5.0f;//当前攻击力
    public static float CurrentAS = 1.0f;//当前攻击速度
    public static float CurrentMS = 1.0f;//当前移动速度
    public static float CurrentPOWERADD = 3.0f;//当前集气速度

    //public static float CurrentHP = 1600.0f;//当前生命
    //public static float CurrentATK = 100.0f;//当前攻击力
    //public static float CurrentAS = 1.8f;//当前攻击速度
    //public static float CurrentMS = 1.8f;//当前移动速度
    //public static float CurrentPOWERADD = 18.0f;//当前集气速度
}
