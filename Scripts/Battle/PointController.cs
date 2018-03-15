//using UnityEngine;
//using System.Collections;
//using System.Text.RegularExpressions;
//using Excel;
//using System.IO;
//using System.Data;
//using UnityEngine.UI;

//public class PointController : MonoBehaviour {
//    public int Level = 1;

//    private Transform mytransform;

//    private FileStream fileStream;

//    private IExcelDataReader dataReader;

//    private DataSet dataSet;

//    private DataTableCollection tabelCollection;

//    //private DataTable dataTabel;
//    // Use this for initialization
//    void Start () {
//        mytransform = this.transform;

//        fileStream = File.Open(Application.dataPath + ("/Resources/ObjectInfo.xlsx"), FileMode.Open,FileAccess.Read);

//        dataReader = ExcelReaderFactory.CreateOpenXmlReader(fileStream);

//        dataSet = dataReader.AsDataSet();//得到文当的信息

//        tabelCollection = dataSet.Tables;//得到所有表格


//        //dataTabel = tabelCollection[0];//得到单个表格
//    }
	
//    // Update is called once per frame
//    void Update () {
//        //if(Input.GetKeyDown(KeyCode.H))
//        //{
//        //    LevelEmeny();
//        //}
//    }

//    //控制每一关每一场景的怪物出现
//    public void LevelEmeny()
//    {
//        foreach(Transform T in this.transform)
//        {
//            string[] sArray = T.name.Split(new char[1]{'_'});

//            DataTable dataTabel = tabelCollection[0];

//            DataRowCollection rowCollection = dataTabel.Rows;

//            float life = 0;

//            float rotatespeed = 0;

//            float detectRange = 0;

//            Vector3[] attack_range_angle_damage = new Vector3[0];

//            float attackspeed = 1.0f;
//            float movespeed = 2.0f;
//            foreach (DataRow row in rowCollection)
//            {
//                //Debug.Log(row[0]);
//                //Debug.Log(T.name);
//                if(row[0].ToString() == T.name)
//                {
//                    //Debug.Log(row[1]);
//                    life = float.Parse(row[1].ToString());

//                    detectRange = float.Parse(row[2].ToString());

//                    rotatespeed = float.Parse(row[3].ToString());

//                    attack_range_angle_damage = new Vector3[int.Parse(row[4].ToString())];
//                    for (int i = 0; i < int.Parse(row[4].ToString()); i++)
//                    {
//                        attack_range_angle_damage[i] = new Vector3(float.Parse(row[5 + i].ToString()), float.Parse(row[6 + i].ToString()), int.Parse(row[7 + i].ToString()));
//                    }
//                }

//            }

//            switch(sArray[0])
//            {
//                case "Archer": EnemyManager.Instance.AddArcher(sArray[1],T.position,life,rotatespeed,detectRange,attack_range_angle_damage,attackspeed,movespeed,10);
//                    break;
//                case "Grunt": EnemyManager.Instance.AddGrunt(sArray[1], T.position, life, rotatespeed, detectRange, attack_range_angle_damage, attackspeed, movespeed,10);
//                    break;
//                case "King": EnemyManager.Instance.AddKing(sArray[1], T.position, life, rotatespeed, detectRange, attack_range_angle_damage, attackspeed, movespeed,10);
//                    break;
//                case "mage": EnemyManager.Instance.AddMage(sArray[1], T.position, life, rotatespeed, detectRange, attack_range_angle_damage, attackspeed, movespeed,10);
//                    break;
//                case "Warrior": EnemyManager.Instance.AddWarrior(sArray[1], T.position, life, rotatespeed, detectRange, attack_range_angle_damage, attackspeed, movespeed,10);
//                    break;
//            }
//        }
//    }
//}

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.IO;

using System;

public class PointController : MonoBehaviour
{
    public int Level = 1;

    private Transform mytransform;


    // Use this for initialization

    public List<List<string>> data = new List<List<string>>();

    void Awake()
    {

        Read(Application.dataPath + "/Resources/datatext.txt");
    }

    // Update is called once per frame
    void Update()
    {

    }





    public void Read(string Path)
    {
        FileStream aFile = new FileStream(@"" + Path, FileMode.Open, FileAccess.Read);
        StreamReader sr = new StreamReader(aFile);
        string t_sLine;
        while ((t_sLine = sr.ReadLine()) != null)
        {
            string[] sArray = t_sLine.Split(new char[1] { ' ' });
            List<string> data1 = new List<string>();
            foreach (string s in sArray)
            {
                data1.Add(s); // 将每一行的内容存入数组链表容器中
                //Debug.Log(s);
            }
            data.Add(data1);
        }
        sr.Close();
        sr.Dispose();
    }



    //控制每一关每一场景的怪物出现
    public void LevelEmeny()
    {
        //foreach(Transform T in this.transform)
        //{
        //    string[] sArray = T.name.Split(new char[1]{'_'});

        //    DataTable dataTabel = tabelCollection[0];
        //    DataRowCollection rowCollection = dataTabel.Rows;
        //    float life = 0;

        //    float rotatespeed = 0;

        //    float detectRange = 0;

        //    Vector3[] attack_range_angle_damage = new Vector3[0];

        //    float attackspeed = 1.0f;
        //    float movespeed = 2.0f;
        //    foreach (DataRow row in rowCollection)
        //    {
        //        //Debug.Log(row[0]);
        //        //Debug.Log(T.name);
        //        if(row[0].ToString() == T.name)
        //        {
        //            //Debug.Log(row[1]);
        //            life = float.Parse(row[1].ToString());

        //            detectRange = float.Parse(row[2].ToString());

        //            rotatespeed = float.Parse(row[3].ToString());

        //            attack_range_angle_damage = new Vector3[int.Parse(row[4].ToString())];
        //            for (int i = 0; i < int.Parse(row[4].ToString()); i++)
        //            {
        //                attack_range_angle_damage[i] = new Vector3(float.Parse(row[5 + i].ToString()), float.Parse(row[6 + i].ToString()), int.Parse(row[7 + i].ToString()));
        //            }
        //        }

        //    }

        //    switch(sArray[0])
        //    {
        //        case "Archer": EnemyManager.Instance.AddArcher(sArray[1],T.position,life,rotatespeed,detectRange,attack_range_angle_damage,attackspeed,movespeed);
        //            break;
        //        case "Grunt": EnemyManager.Instance.AddGrunt(sArray[1], T.position, life, rotatespeed, detectRange, attack_range_angle_damage, attackspeed, movespeed);
        //            break;
        //        case "King": EnemyManager.Instance.AddKing(sArray[1], T.position, life, rotatespeed, detectRange, attack_range_angle_damage, attackspeed, movespeed);
        //            break;
        //        case "mage": EnemyManager.Instance.AddMage(sArray[1], T.position, life, rotatespeed, detectRange, attack_range_angle_damage, attackspeed, movespeed);
        //            break;
        //        case "Warrior": EnemyManager.Instance.AddWarrior(sArray[1], T.position, life, rotatespeed, detectRange, attack_range_angle_damage, attackspeed, movespeed);
        //            break;
        //    }
        //}

        foreach (Transform T in this.transform)
        {
            string[] sArray = T.name.Split(new char[1] { '_' });
            float life = 0;

            float rotatespeed = 0;

            float detectRange = 0;

            Vector3[] attack_range_angle_damage = new Vector3[0];

            float attackspeed = 0;
            float movespeed = 0;
            int diamond = 0;
            foreach (List<string> row in data)
            {
                if (row[0].ToString() == T.name)
                {
                    life = float.Parse(row[1].ToString());

                    detectRange = float.Parse(row[2].ToString());

                    rotatespeed = float.Parse(row[3].ToString());

                    movespeed = float.Parse(row[4].ToString());

                    diamond = int.Parse(row[5].ToString());

                    attackspeed = float.Parse(row[6].ToString());

                    attack_range_angle_damage = new Vector3[int.Parse(row[7].ToString())];
                    for (int i = 0; i < int.Parse(row[7].ToString()); i++)
                    {
                        attack_range_angle_damage[i] = new Vector3(float.Parse(row[8 + 3 * i].ToString()), float.Parse(row[9 + 3 * i].ToString()), int.Parse(row[10 + 3 * i].ToString()));
                    }
                }

            }

            switch (sArray[0])
            {
                case "Archer": EnemyManager.Instance.AddArcher(sArray[1], T.position, life, rotatespeed, detectRange, attack_range_angle_damage, attackspeed, movespeed, diamond);
                    break;
                case "Grunt": EnemyManager.Instance.AddGrunt(sArray[1], T.position, life, rotatespeed, detectRange, attack_range_angle_damage, attackspeed, movespeed, diamond);
                    break;
                case "King": EnemyManager.Instance.AddKing(sArray[1], T.position, life, rotatespeed, detectRange, attack_range_angle_damage, attackspeed, movespeed, diamond);
                    break;
                case "mage": EnemyManager.Instance.AddMage(sArray[1], T.position, life, rotatespeed, detectRange, attack_range_angle_damage, attackspeed, movespeed, diamond);
                    break;
                case "Warrior": EnemyManager.Instance.AddWarrior(sArray[1], T.position, life, rotatespeed, detectRange, attack_range_angle_damage, attackspeed, movespeed, diamond);
                    break;
                case "Dragon": EnemyManager.Instance.AddDragon(sArray[0], T.position, life, rotatespeed, detectRange, attack_range_angle_damage, attackspeed, movespeed, diamond);
                    break;
            }
        }
    }
}

