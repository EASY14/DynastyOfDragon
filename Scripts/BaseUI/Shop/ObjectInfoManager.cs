//using UnityEngine;
//using System.Collections;

//using System.Data;
//using Excel;
//using System.IO;

//using System.Collections.Generic;

//public class ObjectInfoManager : MonoBehaviour {

//    private static ObjectInfoManager _instance;
//    public Dictionary<int, ObjectInfo> ObjectDic = new Dictionary<int, ObjectInfo>();
//    public Dictionary<string,int>IconToIdDic=new Dictionary<string,int>();

//    //excel里商品的个数
//    private int totalRows = 18;
//    public static ObjectInfoManager Instance
//    {
//        get
//        {
//            if(_instance==null)
//            {
//                _instance = GameObject.FindGameObjectWithTag("GameController").GetComponent<ObjectInfoManager>();

//            }
//            return _instance;
//        }
//    }

//	// Use this for initialization
//	void Awake () {
//        GetExcelInfo();
//	}

//	// Update is called once per frame
//	void Update () {

//	}

//    //读取excel里的商品，记录到字典
//    public void GetExcelInfo()
//    {
//         FileStream fileStream = File.Open(Application.dataPath+("/EquipInfo.xlsx"),FileMode.Open);
//         IExcelDataReader dataReader = ExcelReaderFactory.CreateOpenXmlReader(fileStream);

//        //得到文档信息
//        DataSet dataSet = dataReader.AsDataSet();

//        //得到所有表格
//        DataTableCollection tableCollection = dataSet.Tables;

//        //得到第0个表格
//        DataTable dataTable=tableCollection[0];

//        //得到表格行列数据
//        DataRowCollection rowCollection = dataTable.Rows;


//        for (int i = 0; i < totalRows; i++)
//        {
//            ObjectInfo info = new ObjectInfo();
//            info.Id = int.Parse(rowCollection[i + 1][0].ToString());
//            info.Name = rowCollection[i + 1][1].ToString();
//            info.Icon = rowCollection[i + 1][2].ToString();
//            info.EquipType = rowCollection[i + 1][3].ToString();
//            info.SoldPrice = int.Parse(rowCollection[i + 1][4].ToString());
//            info.BuyPrice = int.Parse(rowCollection[i + 1][5].ToString());
//            info.Hp = int.Parse(rowCollection[i + 1][6].ToString());
//            info.AttackValue = int.Parse(rowCollection[i + 1][7].ToString());
//            info.AttackSpeed = float.Parse(rowCollection[i + 1][8].ToString());
//            info.PowerSpeed = int.Parse(rowCollection[i + 1][9].ToString());
//            info.MoveSpeed = float.Parse(rowCollection[i + 1][10].ToString());

//            //  Debug.Log("set"+"Id:" + info.Id + "Icon:" + info.Icon);

//            ObjectDic.Add(info.Id,info);
//            IconToIdDic.Add(info.Icon,info.Id);

//            //for (int j = 0; j < ObjectDic.Count; j++)
//            //{
//            //    // Debug.Log("set" + "Id:" + info.Id + "Icon:" + info.Icon);
//            //    Debug.Log("get:" + "Icon:" + ObjectDic[j + 1].Icon);
//            //}

//        }


//    }

//    //通过商品id获取商品数据
//    public ObjectInfo GetInfoById(int id)
//    {
//        ObjectInfo newInfo = new ObjectInfo();
//        ObjectDic.TryGetValue(id, out newInfo);
//        return newInfo;
//    }

//    public ObjectInfo GetInfoByIconName(string icon)
//    {
//        int id = -1;
//        IconToIdDic.TryGetValue(icon,out id);
//        return GetInfoById(id);
//    }
//}

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;

using System;

public class ObjectInfoManager : MonoBehaviour
{

    private static ObjectInfoManager _instance;
    public Dictionary<int, ObjectInfo> ObjectDic = new Dictionary<int, ObjectInfo>();
    public Dictionary<string, int> IconToIdDic = new Dictionary<string, int>();

    public object[,] rowCollection;

    //excel里商品的个数
    private int totalRows = 36;
    public static ObjectInfoManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindGameObjectWithTag("ObjectInfo").GetComponent<ObjectInfoManager>();

            }
            return _instance;
        }
    }

    // Use this for initialization
    void Awake()
    {
        GetExcelInfo();

    }

    void Start()
    {
        //GetExcelInfo();
    }

    // Update is called once per frame
    void Update()
    {
    }

    void InitInfo()
    {
        //rowCollection = new object[19, 11]{
        //{"编号ID","名字Name）"  ,"图标Icon" ,"种类 EquipType","卖出价","购买价","生命值","攻击力","攻击速度","集气速度","移动速度"},
        //{"11","上衣（红）"      ,"Armor_1" ,"Armor"          ,"100"    ,"120"  ,"200"   ,"0"     ,"0"       ,"0"       ,"0"},
        //{"12","上衣（黑）"      ,"Armor_2" ,"Armor"          ,"101"    ,"200"  ,"300"   ,"0"     ,"0"       ,"0"       ,"0"},
        //{"13","上衣（紫）"      ,"Armor_3" ,"Armor"          ,"102"    ,"350"  ,"400"   ,"0"     ,"0"       ,"0"       ,"0"},
        //{"14","上衣（黑红）"    ,"Armor_4" ,"Armor"          ,"103"    ,"400"  ,"500"   ,"0"     ,"0"       ,"0"       ,"0"},
        //{"15","上衣（紫黄）"    ,"Armor_5" ,"Armor"          ,"104"    ,"450"  ,"600"   ,"0"     ,"0"       ,"0"       ,"0"},
        //{"16","上衣（红黑）"    ,"Armor_6" ,"Armor"          ,"105"    ,"999"  ,"1000"  ,"0"     ,"0"       ,"0"       ,"0"},
        //{"21","鞋子（紫黄）"    ,"shoes_1" ,"shoes"          ,"75"     ,"60"   ,"0"     ,"0"     ,"0"       ,"0"       ,"1.0"},
        //{"22","鞋子（黑灰）"    ,"shoes_2" ,"shoes"          ,"76"     ,"80"   ,"0"     ,"0"     ,"0"       ,"0"       ,"1.5"},
        //{"23","鞋子（紫黄）"    ,"shoes_3" ,"shoes"          ,"77"     ,"120"  ,"0"     ,"0"     ,"0"       ,"0"       ,"2.0"},
        //{"24","鞋子（黑黄）"    ,"shoes_4" ,"shoes"          ,"78"     ,"200"  ,"0"     ,"0"     ,"0"       ,"0"       ,"2.5"},
        //{"25","鞋子（蓝黑银）"  ,"shoes_5" ,"shoes"          ,"79"     ,"230"  ,"0"     ,"0"     ,"0"       ,"0"       ,"3.0"},
        //{"26","鞋子（紫黑黄）"  ,"shoes_6" ,"shoes"          ,"80"     ,"666"  ,"0"     ,"0"     ,"0"       ,"0"       ,"4.0"},
        //{"31","武器（深紫黑剑）","weapon_1","weapon"         ,"120"    ,"120"  ,"0"     ,"5"     ,"0.2"     ,"2"       ,"0"},
        //{"32","武器（紫黑银健）","weapon_2","weapon"         ,"121"    ,"200"  ,"0"     ,"7"     ,"0.3"     ,"4"       ,"0"},
        //{"33","武器（红黑银剑）","weapon_3","weapon"         ,"122"    ,"300"  ,"0"     ,"10"    ,"0.4"     ,"5"       ,"0"},
        //{"34","武器（深红黑剑）","weapon_4","weapon"         ,"123"    ,"450"  ,"0"     ,"20"    ,"0.5"     ,"7"       ,"0"},
        //{"35","武器（银黑红剑）","weapon_5","weapon"         ,"124"    ,"600"  ,"0"     ,"32"    ,"0.6"     ,"8"       ,"0"},
        //{"36","武器（黄紫项链）","weapon_6","weapon"         ,"125"    ,"999"  ,"0"     ,"70"    ,"1.0"     ,"15"      ,"0"},
        //};
        rowCollection = new object[37, 12]{
        {"编号ID","名字Name）"  ,"图标Icon" ,"种类 ","卖出价","购买价" ,"生命值%","攻击力%","攻击速度%","集气速度%","移动速度%","气力值"},
        {"11","上衣（1级）"     ,"Armor_1" ,"Armor" ,"1000"  ,"2000"   ,"25"     ,"0"      ,"0"        ,"0"        ,"0"        ,"0"},
        {"12","上衣（2级）"     ,"Armor_2" ,"Armor" ,"2000"  ,"4000"   ,"50"     ,"0"      ,"0"        ,"0"        ,"0"        ,"0"},
        {"13","上衣（3级）"     ,"Armor_3" ,"Armor" ,"5000"  ,"10000"  ,"100"    ,"0"      ,"0"        ,"0"        ,"0"        ,"0"},
        {"14","上衣（4级）"     ,"Armor_4" ,"Armor" ,"10000" ,"20000"  ,"150"    ,"0"      ,"0"        ,"0"        ,"0"        ,"0"},
        {"15","上衣（5级）"     ,"Armor_5" ,"Armor" ,"25000" ,"50000"  ,"200"    ,"0"      ,"0"        ,"0"        ,"0"        ,"0"},
        {"16","上衣（6级）"     ,"Armor_6" ,"Armor" ,"50000" ,"100000" ,"300"    ,"0"      ,"0"        ,"0"        ,"0"        ,"0"},
      
        {"21","鞋子（1级）"    ,"shoes_1" ,"shoes"  ,"1000"  ,"2000"   ,"0"      ,"0"      ,"0"        ,"0"        ,"5.0"      ,"0"},
        {"22","鞋子（2级）"    ,"shoes_2" ,"shoes"  ,"2000"  ,"4000"   ,"0"      ,"0"      ,"0"        ,"0"        ,"10.0"     ,"0"},
        {"23","鞋子（3级）"    ,"shoes_3" ,"shoes"  ,"5000"  ,"10000"  ,"0"      ,"0"      ,"0"        ,"0"        ,"20.0"     ,"0"},
        {"24","鞋子（4级）"    ,"shoes_4" ,"shoes"  ,"10000" ,"20000"  ,"0"      ,"0"      ,"0"        ,"0"        ,"30.5"     ,"0"},
        {"25","鞋子（5级）"    ,"shoes_5" ,"shoes"  ,"25000" ,"50000"  ,"0"      ,"0"      ,"0"        ,"0"        ,"40.0"     ,"0"},
        {"26","鞋子（6级）"    ,"shoes_6" ,"shoes"  ,"50000" ,"100000" ,"0"      ,"0"      ,"0"        ,"0"        ,"50.0"     ,"0"},
     
        {"31","武器（1级剑）"  ,"weapon_1","weapon" ,"1000"  ,"2000"   ,"0"     ,"25"      ,"0"        ,"0"        ,"0"        ,"0"},
        {"32","武器（2级剑）"  ,"weapon_2","weapon" ,"2000"  ,"4000"   ,"0"     ,"50"     ,"0"        ,"0"        ,"0"        ,"0"},
        {"33","武器（3级剑）"  ,"weapon_3","weapon" ,"5000"  ,"10000"  ,"0"     ,"100"     ,"0"        ,"0"        ,"0"        ,"0"},
        {"34","武器（4级剑）"  ,"weapon_4","weapon" ,"10000" ,"20000"  ,"0"     ,"200"     ,"0"        ,"0"        ,"0"        ,"0"},
        {"35","武器（5级剑）"  ,"weapon_5","weapon" ,"25000" ,"50000"  ,"0"     ,"300"     ,"0"        ,"0"        ,"0"        ,"0"},
        {"36","武器（6级项剑）","weapon_6","weapon" ,"50000" ,"100000" ,"0"     ,"400"     ,"0"        ,"0"        ,"0"        ,"0"},
       //19 
       
        {"41","护腕（1级）"    ,"bracer_1","bracer"  ,"1000"  ,"2000"   ,"0"     ,"0"      ,"50"        ,"0"       ,"0"        ,"0"},
        {"42","护腕（2级）"   ,"bracer_2","bracer"  ,"2000"  ,"4000"    ,"0"     ,"0"      ,"10"       ,"0"       ,"0"        ,"0"},
        {"43","护腕（3级）"   ,"bracer_3","bracer"  ,"5000"  ,"10000"   ,"0"     ,"0"      ,"20"       ,"0"       ,"0"        ,"0"},
        {"44","护腕（4级）"   ,"bracer_4","bracer"  ,"10000" ,"20000"   ,"0"     ,"0"      ,"30"       ,"0"       ,"0"        ,"0"},
        {"45","护腕（5级）"   ,"bracer_5","bracer"  ,"25000" ,"50000"   ,"0"     ,"0"      ,"40"       ,"0"       ,"0"        ,"0"},
        {"46","护腕（6级）"   ,"bracer_6","bracer"  ,"50000" ,"100000"  ,"0"     ,"0"      ,"50"       ,"0"       ,"0"        ,"0"},
      //25

        {"51","项链（1级）"   ,"necklace_1","necklace","1000" ,"2000"   ,"0"     ,"0"      ,"0"         ,"10"      ,"0"        ,"0"},
        {"52","项链（2级）"   ,"necklace_2","necklace","2000" ,"4000"   ,"0"     ,"0"      ,"0"         ,"25"      ,"0"        ,"0"},
        {"53","项链（3级）"   ,"necklace_3","necklace","5000" ,"10000"  ,"0"     ,"0"      ,"0"         ,"50"      ,"0"        ,"0"},
        {"54","项链（4级）"   ,"necklace_4","necklace","10000","20000"  ,"0"     ,"0"      ,"0"         ,"100"     ,"0"        ,"0"},
        {"55","项链（5级）"   ,"necklace_5","necklace","25000","50000"  ,"0"     ,"0"      ,"0"         ,"150"     ,"0"        ,"0"},
        {"56","项链（6级）"   ,"necklace_6","necklace","50000","100000" ,"0"     ,"0"      ,"0"         ,"200"     ,"0"        ,"0"},
      //31
      //{"编号ID","名字Name）"  ,"图标Icon" ,"种类 ","卖出价","购买价" ,"生命值","攻击力%","攻击速度%","集气速度%","移动速度%","气力值"},//生命值和气力值直接加
      //恢复当前生命的气力值
        {"61","药物（气力药小）"   ,"drug_1","drug","1000" ,"2000"      ,"0"     ,"0"      ,"0"         ,"0"       ,"0"        ,"20"},
        {"62","药物（气力药中）"   ,"drug_2","drug","2000" ,"4000"      ,"0"     ,"0"      ,"0"         ,"0"       ,"0"        ,"60"},
        {"63","药物（气力药大）"   ,"drug_3","drug","5000" ,"10000"     ,"0"     ,"0"      ,"0"         ,"0"       ,"0"        ,"100"},
     //恢复当前气力的生命值
        {"64","药物（生命药小）"   ,"drug_4","drug","500","2000"     ,"100"    ,"0"      ,"0"         ,"0"       ,"0"        ,"0"},
        {"65","药物（生命药中）"   ,"drug_5","drug","2000","4000"    ,"400"    ,"0"      ,"0"         ,"0"       ,"0"        ,"0"},
        {"66","药物（生命药大）"   ,"drug_6","drug","5000","10000"    ,"800"    ,"0"      ,"0"         ,"0"       ,"0"        ,"0"},
      //39
        };
    }

    //读取excel里的商品，记录到字典
    public void GetExcelInfo()
    {
        /*FileStream fileStream = File.Open(Application.dataPath + ("/ObjectInfo.xlsx"), FileMode.Open, FileAccess.Read);
        IExcelDataReader dataReader = ExcelReaderFactory.CreateOpenXmlReader(fileStream);

        //得到文档信息
        DataSet dataSet = dataReader.AsDataSet();

        //得到所有表格
        DataTableCollection tableCollection = dataSet.Tables;

        //得到第0个表格
        DataTable dataTable = tableCollection[0];

        //得到表格行列数据
        DataRowCollection rowCollection = dataTable.Rows;*/

        InitInfo();

        for (int i = 0; i < totalRows; i++)
        {
            ObjectInfo info = new ObjectInfo();
            info.Id = int.Parse(rowCollection[i + 1, 0].ToString());
            info.Name = rowCollection[i + 1, 1].ToString();

            info.Icon = rowCollection[i + 1, 2].ToString();
            info.EquipType = rowCollection[i + 1, 3].ToString();

            info.SoldPrice = int.Parse(rowCollection[i + 1, 4].ToString());
            info.BuyPrice = int.Parse(rowCollection[i + 1, 5].ToString());

            info.Hp = int.Parse(rowCollection[i + 1,6].ToString());
            info.AttackValue = int.Parse(rowCollection[i + 1,7].ToString());
            info.AttackSpeed = float.Parse(rowCollection[i + 1,8].ToString());
            info.PowerSpeed = int.Parse(rowCollection[i + 1,9].ToString());
            info.MoveSpeed = float.Parse(rowCollection[i + 1,10].ToString());
            info.Power = float.Parse(rowCollection[i + 1, 11].ToString());
            /* ObjectInfo info = new ObjectInfo();
             info.Id = int.Parse(rowCollection[i + 1][0].ToString());

             info.Name = rowCollection[i + 1][1].ToString();
             info.Icon = rowCollection[i + 1][2].ToString();
             info.EquipType = rowCollection[i + 1][3].ToString();

             info.SoldPrice = int.Parse(rowCollection[i + 1][4].ToString());
             info.BuyPrice = int.Parse(rowCollection[i + 1][5].ToString());*/



            ObjectDic.Add(info.Id, info);
            IconToIdDic.Add(info.Icon, info.Id);

            //for (int j = 0; j < ObjectDic.Count; j++)
            //{
            //    // Debug.Log("set" + "Id:" + info.Id + "Icon:" + info.Icon);
            //    Debug.Log("get:" + "Icon:" + ObjectDic[j + 1].Icon);
            //}
        }


    }


    //通过商品id获取商品数据
    public ObjectInfo GetInfoById(int id)
    {
        ObjectInfo newInfo = new ObjectInfo();
        ObjectDic.TryGetValue(id, out newInfo);
        return newInfo;
    }
    //通过商品图标获取商品数据
    public ObjectInfo GetInfoByIconName(string icon)
    {
        int id = -1;
        IconToIdDic.TryGetValue(icon, out id);
        return GetInfoById(id);
    }
    


}

