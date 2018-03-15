using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using DG.Tweening;
using System;
//GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterBehaviour>().ShowCursor();

public class Shop : MonoBehaviour {

    public List<Button>buttonList=new List<Button>();

    private TechniquePanel Techniquepanel;
    
    private ShoppingBagManager ShoppingBagManager;

    private BagManager BagManager;

    private PlayEquipInfo PlayEquipInfo;


    private ChangeScene myChangeScene;

    public PlayEquipInfo playequipinfo;

    
    // Use this for initialization
    void Start () {

        InitVars();
        InitBtnsDelegate();

	}


	// Update is called once per frame
	void Update () {
        
    }

    /*初始化所有private变量*/
    private void InitVars()
    {
        
        Techniquepanel = transform.FindChild("TechniqueUpgradePanel").GetComponent<TechniquePanel>();
        ShoppingBagManager = transform.FindChild("ShoppingBag").GetComponent<ShoppingBagManager>();
        BagManager = transform.FindChild("Bag").GetComponent<BagManager>();
        PlayEquipInfo = transform.FindChild("PlayEquipInfo").GetComponent<PlayEquipInfo>();
        myChangeScene = GameObject.Find("changeScene").GetComponent<ChangeScene>();
    }

    /*按钮点击事件代理*/
    private void InitBtnsDelegate()
    {
        for (int i = 0; i < buttonList.Count; i++)
        {
            Button btn = buttonList[i];
            btn.onClick.AddListener(delegate() { this.onButtonClick(btn.name); });
            

            //slider.onValueChanged.AddListener(delegate (float l){ fun(l); });
        }
    }
    
    //public void fun(float a)
    //{

    //}

    public void onButtonClick(string b)
    {
        switch(b)
        {
            case "Shop_btn":
                ShoppingBagBtnOnClickShow();
                PlayEquipInfo.Instance.SaveEquipData();
                BagGrild.Instance.SaveBagData();
                AllDataSava.Instance.SaveData(PlayerData.CharacterName);
               
                break;

            case "MainScene_btn":
                PlayEquipInfo.Instance.SaveEquipData();
                BagGrild.Instance.SaveBagData();
                AllDataSava.Instance.SaveData(PlayerData.CharacterName);
                myChangeScene.NextScene("MainMenu");
                break;

            case "Bag_btn":
                BagBtnOnClickShow();
                PlayEquipInfo.Instance.SaveEquipData();
                BagGrild.Instance.SaveBagData();
                AllDataSava.Instance.SaveData(PlayerData.CharacterName);
                break;
            case "Battle_btn":
                BattleBtnOnClickShow();
                PlayEquipInfo.Instance.SaveEquipData();
                BagGrild.Instance.SaveBagData();
                AllDataSava.Instance.SaveData(PlayerData.CharacterName);
                break;
            default:
                //myChangeScene.SceneName = b;
                //myChangeScene.NextScene();
                break;
        }
        
        

    }

    
    public void ShoppingBagBtnOnClickShow()
    {
        PlayEquipInfo.TweenBack();
        ShoppingBagManager.TweenPlay();
        BagManager.TweenPlay();
        MapUIManager.Instance.TweenBack();
    }
    public void AllHide()
    {
        ShoppingBagManager.TweenBack();
        BagManager.TweenBack();
        PlayEquipInfo.TweenBack();
        MapUIManager.Instance.TweenBack();
        //GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterBehaviour>().ShowCursor();
    }

    /*按下bag的回调函数*/
   public void BagBtnOnClickShow()
    {
        ShoppingBagManager.TweenBack();
        Techniquepanel.TweenBack();
        PlayEquipInfo.TweenPlay();
        BagManager.TweenPlay();
        MapUIManager.Instance.TweenBack();
    }

    public void BattleBtnOnClickShow()
    {
        ShoppingBagManager.TweenBack();
        PlayEquipInfo.TweenBack();
        BagManager.TweenBack();
        MapUIManager.Instance.TweenPlay();
    }

}
