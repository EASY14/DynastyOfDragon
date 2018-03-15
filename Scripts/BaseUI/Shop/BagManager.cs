using UnityEngine;
using System.Collections;
using DG.Tweening;
using UnityEngine.UI;
public class BagManager : SimplePanelManage {

    private Text cyrstalnum2; 
    private static BagManager _instance;
    public static BagManager Instance
    {
        get
        {
            if (_instance == null)
            {
                GameObject obj;
                obj = GameObject.Find("Bag");
                if (obj == null) 
                    return null;
                _instance = obj.GetComponent<BagManager>();
            }
            return _instance;
        }
    }


    void Start()
    {
        InitVar();
        cyrstalnum2 = transform.FindChild("Crystal").GetComponent<Text>();
    }
    void Update()
    {
        cyrstalnum2.text = "x" + PlayerData.CRYSTALNUM.ToString();
    }


    private void InitVar()
    {
        add2BtnListbyName("Close_btn");
        InitBtnsDelegate();
    }

    public override void onButtonClick(string bN)
    {
        switch (bN)
        {
            case "Close_btn":
                TweenBack();
                PlayEquipInfo.Instance.TweenBack();
                ShoppingBagManager.Instance.TweenBack();
                GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterBehaviour>().HideCursor();
                break;
            default:
                break;

        }
    }
}
