using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class InfoPanelManager : MonoBehaviour {

    private Text ObjTotalText;
    
	// Use this for initialization
	void Start () {
        InitVar();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    private void InitVar()
    {
        ObjTotalText = transform.FindChild("Infos").GetComponent<Text>();
    }

    public void setText(ObjectInfo info)
    {
        switch(info.EquipType)
        {
            case "Armor":
                ObjTotalText.text = info.Name + "\n"
                + "生命值："+info.Hp + "%\n"
                + "出售价格：" + info.SoldPrice + "\n"
                + "购买价格：" + info.BuyPrice;
                break;
            case "shoes":
                ObjTotalText.text = info.Name + "\n"
                + "移动速度：" + info.MoveSpeed + "%\n"
                + "出售价格：" + info.SoldPrice + "\n"
                + "购买价格：" + info.BuyPrice;
                break;
            case "weapon":
                ObjTotalText.text = info.Name + "\n"
                + "攻击力：" + info.AttackValue + "%\n"
                + "出售价格：" + info.SoldPrice + "\n"
                + "购买价格：" + info.BuyPrice;
                break;
            case "bracer":
                ObjTotalText.text = info.Name + "\n"
                + "攻击速度：" + info.AttackSpeed + "%\n"
                + "出售价格：" + info.SoldPrice + "\n"
                + "购买价格：" + info.BuyPrice;
                break;
            case "necklace":
                ObjTotalText.text = info.Name + "\n"
                + "集气速度：" + info.PowerSpeed + "%\n"
                + "出售价格：" + info.SoldPrice + "\n"
                + "购买价格：" + info.BuyPrice;
                break;
            case "drug":
                ObjTotalText.text = info.Name + "\n"
                + "气力恢复：" + info.Power + "\n"
                + "生命恢复：" + info.Hp + "\n"
                + "出售价格：" + info.SoldPrice + "\n"
                + "购买价格：" + info.BuyPrice;
                break;
        }
        
        
    }
    //public void SetDefaultText()
    //{
    //    int randText=Random.Range(1,3);
    //    switch(randText)
    //    {
    //        case 1:
    //            ObjTotalText.text = "走过路过，不要错过咧                                    ——店小二";
    //            break;

    //        case 2:
    //            ObjTotalText.text = "装备不是你想买，想买就给买                                    ——店小二";
    //            break;

    //        case 3:
    //            ObjTotalText.text = "极品装备，看rp出售                                    ——店小二";
    //            break; 
    //        default:
    //            break;
    //    }
    //}
}
