using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using DG.Tweening;
public class SoldPanelManager : MonoBehaviour {

    private Button sold;
    private Button cancel;
    public List<Button> SoldButtonList = new List<Button>();

    public InputField InputText;//输入数字的文本框
    private int InputNum;//输入的出售数目

    public int ItemNum;//商品总数目
    public string ItemName;//商品名

    public BagGrild bagGrild;

    private AudioSource saleSound;

	// Use this for initialization
	void Start () 
    {
        InitBtn();
        BtnsDelegate();
	}
    private void InitBtn()
    {
        sold = transform.FindChild("soldBtn").gameObject.GetComponent<Button>();
        cancel = transform.FindChild("cancelBtn").gameObject.GetComponent<Button>();
        SoldButtonList.Add(sold);
        SoldButtonList.Add(cancel);

        InputText = transform.FindChild("InputField").GetComponent<InputField>();
        
        bagGrild = GameObject.Find("Bag").transform.FindChild("BagItemGroup").GetComponent<BagGrild>();

        saleSound = GetComponent<AudioSource>();
    }
    private void BtnsDelegate()
    {
        for (int i = 0; i < SoldButtonList.Count; i++)
        {
            Button btn = SoldButtonList[i];
            btn.onClick.AddListener(delegate() { this.onSoldPanelClick(btn.name); });

        }
    }
	

    public void onSoldPanelClick(string btnN)
    {
        switch (btnN)
        {
            case "soldBtn":
                
                OnClickSold();
                TweenBack();
                break;
            case "cancelBtn":
                TweenBack();
                break;
            default: 
                break;
        }
    }


    private void OnClickSold()
    {
        if(InputText.text==""||ItemName=="")
        {
            return;
        }

        InputNum = int.Parse(InputText.text);

        //Debug.Log(InputNum);

        if(InputNum>=ItemNum)
        {
            InputNum = ItemNum;
             
        }
        if (InputNum < 0)
        {
            InputNum = 1;
        }
        InputText.text = InputNum.ToString();
        //InputNum=InputNum >= ItemNum ? ItemNum : 1;
        //Debug.Log(PlayerData.CRYSTALNUM);
        //Debug.Log(GetSoldPrice());
        PlayerData.CRYSTALNUM += GetSoldPrice();
       // Debug.Log(PlayerData.CRYSTALNUM);
        saleSound.Play();
        //bagGrild.DestorybagIconByName(ItemName);
        bagGrild.SetBagItemIconNum(ItemName, -InputNum);
        ItemName = "";
    }
    //得到出售后得到的钱
    private int  GetSoldPrice()
    {
        int soldPrice = 0;
        soldPrice = InputNum*ObjectInfoManager.Instance.GetInfoByIconName(ItemName).SoldPrice;
        return soldPrice;
    }


    public void TweenPlay()
    {
        InputText.text = "";
        transform.GetComponent<DOTweenAnimation>().DOPlayForward();
    }
    public void TweenBack()
    {
        
        transform.GetComponent<DOTweenAnimation>().DOPlayBackwards();
    }
}
