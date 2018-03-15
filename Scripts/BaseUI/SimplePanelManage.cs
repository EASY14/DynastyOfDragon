using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using DG.Tweening;
public class SimplePanelManage : MonoBehaviour {

    public List<Button> btnList;

    //初始化btnList
    public void add2BtnListbyName(string btnName)
    {
        Button btn = transform.FindChild(btnName).GetComponent<Button>();
        btnList.Add(btn);
    }
    //初始化btn代理
    public void InitBtnsDelegate()
    {
        for (int i = 0; i < btnList.Count; i++)
        {
            Button btn = btnList[i];
            btn.onClick.AddListener(delegate() { this.onButtonClick(btn.name); });
        }
    }

    //onclick事件
    public virtual void onButtonClick(string bN)
    {

    }

    public void TweenPlay()
    {
        if (transform.GetComponent<DOTweenAnimation>()!=null)
        {
            transform.GetComponent<DOTweenAnimation>().DOPlayForward();
        }
    }

    public void TweenBack()
    {
        if (transform.GetComponent<DOTweenAnimation>() != null)
        {
            transform.GetComponent<DOTweenAnimation>().DOPlayBackwards();
            PlayEquipInfo.Instance.SaveEquipData();
            MapUIManager.Instance.SaveunLockData();
            BagGrild.Instance.SaveBagData();
            AllDataSava.Instance.SaveData(PlayerData.CharacterName);
        }
    }
}
