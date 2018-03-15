using UnityEngine;
using System.Collections;

using UnityEngine.EventSystems;
using UnityEngine.UI;
public class BagItemIcon : MonoBehaviour,IPointerDownHandler,IDragHandler,IPointerUpHandler{

    private SoldPanelManager soldPanelManager;
    private Transform ExParent=null;//该商品的前父级
    private string EquipSpriteName;//该商品的图标名字
    private int EquipNum;//商品总件数

    void Start()
    {
        EquipSpriteName = this.GetComponent<Image>().sprite.name;
        EquipNum = int.Parse(this.transform.FindChild("Text").GetComponent<Text>().text);
        
        
        soldPanelManager = GameObject.Find("SoldPanel").GetComponent<SoldPanelManager>();
    }


    public void OnPointerDown(PointerEventData eventData)
    {
        EquipSpriteName = this.GetComponent<Image>().sprite.name;
        EquipNum = int.Parse(this.transform.FindChild("Text").GetComponent<Text>().text);

        if(eventData.button==PointerEventData.InputButton.Right)
        {
            
            ObjectInfo EquipInfo = ObjectInfoManager.Instance.GetInfoByIconName(EquipSpriteName);
            ExParent = this.transform.parent;
            ReBack();
            if (EquipInfo.EquipType != "drug")
            {
                PlayEquipInfo.Instance.EquipIconName = EquipSpriteName;

                if (PlayEquipInfo.Instance.PutInEquipInfo())
                {
                    BagGrild.Instance.PlayEquipSound();
                    BagGrild.Instance.ShowPutOnEquipTips();
                }
                else
                    BagGrild.Instance.ShowNeedToPutOnEquipTips();
            }

            if (EquipInfo.EquipType == "drug")
            {
                BagGrild.Instance.PlayDrugSound();
                if (EquipInfo.Power != 0)
                {
                    
                    BattleUIController.Instance.PutMpHpInBattleUI(null, 0, EquipSpriteName, EquipNum);
                    int delectEquipNum = -1 * EquipNum;
                    BagGrild.Instance.SetBagItemIconNum(EquipSpriteName, delectEquipNum);
                }
                if (EquipInfo.Hp != 0)
                {
                    BattleUIController.Instance.PutMpHpInBattleUI(EquipSpriteName, EquipNum, null, 0);
                    int delectEquipNum = -1 * EquipNum;
                    BagGrild.Instance.SetBagItemIconNum(EquipSpriteName, delectEquipNum);
                }
            }

            
                  
        }
        //左击 将装备的父级改为Bag
        if(eventData.button==PointerEventData.InputButton.Left)
        {
            ExParent = this.transform.parent;
            this.transform.SetParent(GameObject.Find("Bag").transform);
            this.GetComponent<CanvasGroup>().blocksRaycasts = false;
           
        }

    }

    public void OnDrag(PointerEventData eventData)
    {
        //左击拖拽
       if(eventData.button==PointerEventData.InputButton.Left)
       {
           this.transform.position = Input.mousePosition;
       }
    }

    public void OnPointerUp(PointerEventData eventData)
    {

        ReBack();
        if (eventData.pointerCurrentRaycast.gameObject == null)
        {
            soldPanelManager.TweenPlay();
            soldPanelManager.ItemNum = EquipNum;
            soldPanelManager.ItemName = EquipSpriteName;
        }
       
    }

    //回到原位
    public void ReBack()
    {
        this.transform.SetParent(ExParent);
        this.transform.localPosition = Vector3.zero;
        this.GetComponent<CanvasGroup>().blocksRaycasts = true;
    }
}
