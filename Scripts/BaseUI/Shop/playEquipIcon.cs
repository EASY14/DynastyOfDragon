using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class playEquipIcon : MonoBehaviour, IPointerDownHandler
{

    public void OnPointerDown(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            string PlayerEquipSpriteName = this.GetComponent<Image>().sprite.name;
            BagGrild.Instance.clickIconName = PlayerEquipSpriteName;
            BagGrild.Instance.PutbagItemInGrild();
          //  PlayEquipInfo.Instance.UpdatePlayerInfoByEquip(PlayerEquipSpriteName,-1);
            PlayEquipInfo.Instance.DestoryByName(PlayerEquipSpriteName);
            PlayEquipInfo.Instance.CulAllProperty(PlayerData.GRADE);
        }
    }
}
