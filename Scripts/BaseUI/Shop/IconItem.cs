using UnityEngine;
using System.Collections;

using UnityEngine.EventSystems;
using UnityEngine.UI;
using System;

public class IconItem : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler,IPointerDownHandler
{
    private InfoPanelManager infoPanelManager;
    private GameObject InfoPanel;
    private RectTransform canvas;
    private ObjectInfo info;

	// Use this for initialization
	void Start () {

        if (GameObject.Find("BagSystem") != null)
        {
        	canvas = GameObject.Find("BagSystem").GetComponent<RectTransform>();
        }

        InfoPanel = GameObject.Find("InfoPanel");
        if (InfoPanel != null)
        {
            infoPanelManager = InfoPanel.GetComponent<InfoPanelManager>();
        }
        //else
        //{
        //    Debug.Log("infoPanel为空");
        //}
	}

    //设置鼠标进入时装备资料面板的位置
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (canvas == null) return;
        string IconName = this.GetComponent<Image>().sprite.name;
        info=ObjectInfoManager.Instance.GetInfoByIconName(IconName);
        infoPanelManager.setText(info);
        // InfoPanel.transform.localPosition = Input.mousePosition;

        Vector2 mousp = Input.mousePosition;
        Vector2 newp = new Vector2();
        RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas, mousp, eventData.enterEventCamera, out newp);

        InfoPanel.transform.localPosition = newp+new Vector2(200,0);

    }

    //设置鼠标离开时装备资料面板的位置
    public void OnPointerExit(PointerEventData eventData)
    {
        if (InfoPanel == null) return;
        InfoPanel.transform.localPosition = new Vector3(0,600,0);
    }
    
    public void OnPointerDown(PointerEventData eventData)
    {
        if (InfoPanel == null) return;
        InfoPanel.transform.localPosition = new Vector3(1600, 900, 0);
    }
}
