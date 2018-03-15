using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.IO;
using UnityEngine.UI;

public class SceneObjectInfo : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Excleset();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void Excleset()
    {
        //Debug.Log(Application.dataPath);
        //FileStream fileStream = File.Open(Application.dataPath + ("/ObjectInfo.xlsx"),FileMode.Open);

        //IExcelDataReader dataReader = ExcelReaderFactory.CreateOpenXmlReader(fileStream);

        //DataSet dataSet = dataReader.AsDataSet();//得到文当的信息

        //DataTableCollection tabelCollection = dataSet.Tables;//得到所有表格

        //DataTable dataTabel = tabelCollection[0];//得到单个表格

        //DataRowCollection rowCollection = dataTabel.Rows;

        //foreach (DataRow row in rowCollection) {

        //	for (int i = 0; i < 12; i++) {

        //	}
        //}

        //Debug.Log(rowCollection[2][1]);
    }
}
