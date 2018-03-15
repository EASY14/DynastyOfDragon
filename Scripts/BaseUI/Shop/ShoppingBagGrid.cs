using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ShoppingBagGrid : MonoBehaviour
{

    private ObjectInfo info;

    private Image icon;//商品
    private Button itembg_btn;
    public string type;//格子类型
    private int randId;//随机Id

    static int armorID = 11;
    static int shoesID = 21;
    static int weaponID = 31;
    static int bracerID = 41;
    static int necklaceID = 51;
    static int drugID = 61;


    private Text CrystalText;

    void Start()
    {

        randId = 0;

        icon = transform.FindChild("Iconbg").transform.FindChild("GrildIcon").GetComponent<Image>();
        itembg_btn = transform.FindChild("Itembg").GetComponent<Button>();

        itembg_btn.onClick.AddListener(BuyBtnOnClick);

        CrystalText = transform.FindChild("Itembg").transform.FindChild("Text").GetComponent<Text>();

        
        SetItem(type);
        
    }


    //动态加载商店里的物品
    void SetItem(string ty)
    {
        randId = JudgeType(ty);
        info = ObjectInfoManager.Instance.GetInfoById(randId);

        randId = Id2IconIndex(randId);
        
        icon.sprite = Resources.Load<Sprite>("shop/" + "equip/" + info.EquipType + "_" + randId);
        icon.transform.localScale = new Vector3(0.8f, 0.8f, 1.4f);

        ShowSoldPriceString();

        
    }

    //显示出售价格
    void ShowSoldPriceString()
    {
        CrystalText.text = info.BuyPrice.ToString();
        CrystalText.fontStyle = FontStyle.Bold;
        CrystalText.fontSize = 16;
    }

    //根据格子类型定随机值范围
    int JudgeType(string typ)
    {
        switch (typ)
        {
            case "Armor":
                if (armorID >= 17)
                    armorID = 11;
                randId = armorID;
                armorID++;
                break;
            case "shoes":
                if (shoesID >= 27)
                    shoesID = 21;
                randId = shoesID;
                shoesID++;
                break;
            case "weapon":
                if (weaponID >= 37)
                    weaponID = 31;
                randId = weaponID;
                weaponID++;
                break;
            case "bracer":
                if (bracerID >= 47)
                    bracerID = 41;
                randId = bracerID;
                bracerID++;
                break;
            case "necklace":
                if (necklaceID >= 57)
                    necklaceID = 51;
                randId = necklaceID;
                necklaceID++;
                break;
            case "drug":
                if (drugID >= 67)
                    drugID = 61;
                randId = drugID;
                drugID++;
                break;
            default:
                randId = 11;
                break;

        }
        return randId;
    }

  
    //Id切换为图片名字的下标
    private int Id2IconIndex(int id)
    {
        //if (id > 60)
        //{
        //    id -= 60;
        //}
        //else if (id > 50)
        //{
        //    id -= 50;
        //}
        //else if (id > 40)
        //{
        //    id -= 40;
        //}
        //else if (id > 30)
        //{
        //    id -= 30;
        //}
        //else if (id > 20)
        //{
        //    id -= 20;
        //}
        //else if (id > 10)
        //{
        //    id -= 10;
        //}
        //return id;

        for (int indexExtra = 60; indexExtra >= 10; )
        {
            if (id > indexExtra)
            {
                id -= indexExtra;
                //Debug.Log("id:"+id);
                break;
            }
            indexExtra -= 10;

        }
        return id;
    }

    //点击商品，获取商品名字，遍历背包
    void BuyBtnOnClick()
    {
        //if (info.BuyPrice > PlayerData.CRYSTALNUM)
        //{
        //    return;
        //}
        //PlayerData.CRYSTALNUM -= info.BuyPrice;
        //BagGrild.Instance.clickIconName = GetIconName();
        //BagGrild.Instance.PutbagItemInGrild();

        if (ShoppingBagManager.Instance.Itembg_Go == null)
        {
            GameObject Itembg = itembg_btn.gameObject;

            ShoppingBagManager.Instance.Itembg_Go = Itembg;
            ShoppingBagManager.Instance.Itembg_FirstSprite = Itembg.GetComponent<Image>().sprite;
            ShoppingBagManager.Instance.IconName = GetIconName();
            ShoppingBagManager.Instance.BuyObjectInfo = info;

            Itembg.GetComponent<Image>().sprite = itembg_btn.spriteState.pressedSprite;

        
        }
        else
        {
            ShoppingBagManager.Instance.ResetItembg();

            GameObject Itembg = itembg_btn.gameObject;

            ShoppingBagManager.Instance.Itembg_Go = Itembg;
            ShoppingBagManager.Instance.Itembg_FirstSprite = Itembg.GetComponent<Image>().sprite;
            ShoppingBagManager.Instance.IconName = GetIconName();
            ShoppingBagManager.Instance.BuyObjectInfo = info;

            Itembg.GetComponent<Image>().sprite = itembg_btn.spriteState.pressedSprite;
        }

        
        
        
    }

    //获取商品名字
    string GetIconName()
    {
        return icon.GetComponent<Image>().sprite.name;
    }


}

