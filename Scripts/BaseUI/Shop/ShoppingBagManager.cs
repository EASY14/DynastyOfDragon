using UnityEngine;
using System.Collections;
using DG.Tweening;
using UnityEngine.UI;
public class ShoppingBagManager : SimplePanelManage
{
    public ObjectInfo BuyObjectInfo//获取在商店买了什么的名字传进来
    {
        set;
        get;
    }
    public GameObject Itembg_Go
    {
        set;
        get;
    }
    public Sprite Itembg_FirstSprite//商品 Grild bg的需要即将恢复成的sprite
    {
        set;
        get;
    }
    public string IconName
    {
        set;
        get;
    }

    private BagManager BagManager;

    private static ShoppingBagManager _instance;
    public static ShoppingBagManager Instance
    {
        get
        {
            if(_instance==null)
            {
                GameObject obj;
                obj = GameObject.Find("ShoppingBag");
                if (obj == null) return null;
                _instance = obj.GetComponent<ShoppingBagManager>();
            }
            return _instance;
        }
    }

    private AudioSource purchaseSound;
    void Start()
    {
        InitVar();
    }
  
    private void InitVar()
    {
        add2BtnListbyName("Down_btn");
        add2BtnListbyName("Up_btn");
        add2BtnListbyName("Close_btn");
        add2BtnListbyName("Buy_btn");
        add2BtnListbyName("Cancel_btn");

        InitBtnsDelegate();

        purchaseSound = GetComponent<AudioSource>();

        //BagManager = GameObject.Find(ShopSystemPathsTag.BagPanel).GetComponent<BagManager>();
    }

    public override void onButtonClick(string bN)
    {
        switch (bN)
        {
            case "Buy_btn":
                //Debug.Log(PlayerData.CRYSTALNUM);
                if (Itembg_Go!=null)
                {
                    if (BuyObjectInfo.BuyPrice > PlayerData.CRYSTALNUM)
                    {
                        return;
                    }
                    PlayerData.CRYSTALNUM -= BuyObjectInfo.BuyPrice;
                    //Debug.Log(BuyObjectInfo.BuyPrice);
                    //Debug.Log(PlayerData.CRYSTALNUM);

                    BagGrild.Instance.clickIconName = IconName;
                    BagGrild.Instance.PutbagItemInGrild();
                    purchaseSound.Play();
                }
                
                break;

            case "Cancel_btn":
                ResetItembg();
                break;

            case "Close_btn":
                TweenBack();
                BagManager.TweenBack();
                GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterBehaviour>().HideCursor();
                break;

            case "Up_btn":
                transform.FindChild("mask").GetComponent<ShoppingBar>().GoUP();
                break;

            case "Down_btn":
                transform.FindChild("mask").GetComponent<ShoppingBar>().GoDOWN();
                break;

            default:
                break;

        }
    }

    //点击高光效果重置到最初状态
    public void ResetItembg()
    {
        if (Itembg_Go != null)
        {
            Image Itembg_Ima = Itembg_Go.GetComponent<Image>();
            Itembg_Ima.sprite = Itembg_FirstSprite;
            Itembg_Go = null;
        }
    }
}
