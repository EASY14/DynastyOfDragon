using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ResetPanelManager :SimplePanelManage
{
    
    private TechniquePanel TechniquePanel;
    private BagGrild BagGrild;
	// Use this for initialization
	void Start () {
        InitVar();
	}
    
    private void InitVar()
    {
        GameObject BagSystem=GameObject.Find("BagSystem");
        TechniquePanel = BagSystem.transform.FindChild("TechniqueUpgradePanel").GetComponent<TechniquePanel>();
        BagGrild = BagSystem.transform.FindChild("Bag").transform.FindChild("BagItemGroup").GetComponent <BagGrild>();

        add2BtnListbyName("Yes_btn");
        add2BtnListbyName("No_btn");
        InitBtnsDelegate();
    }
	
	// Update is called once per frame
	void Update () {
	
	}
    public override void onButtonClick(string bN)
    {
        switch(bN)
        {
            case "Yes_btn":
                TweenBack();
                BagGrild.Instance.ClearBag();
                BagGrild.Instance.SaveBagData();

                PlayEquipInfo.Instance.ClearEquipData();
                PlayerData.GRADE = 1;
                PlayEquipInfo.Instance.CulAllProperty(PlayerData.GRADE);

                BattleUIController.Instance.ResetDateHpMp();

                MapUIManager.Instance.ClearMapData();

                PlayerData.CRYSTALNUM = PlayerData.INITCRYSTALNUM;
                
                
                TechniquePanel.Instance.TechInfoResetToZero();
                TechniquePanel.Instance.LoadGradeAndExp();
                PlayerData.CurrentHP = PlayerData.HP;
                PlayerData.CurrentATK = PlayerData.ATK;
                PlayerData.CurrentAS = PlayerData.AS;
                PlayerData.CurrentMS = PlayerData.MS;
                PlayerData.CurrentPOWERADD = PlayerData.POWERADD;
                TechniquePanel.Instance.UpdatatoString(PlayerData.CurrentHP, PlayerData.CurrentATK, PlayerData.CurrentAS, PlayerData.CurrentMS, PlayerData.CurrentPOWERADD);

                AllDataSava.Instance.SaveData(PlayerData.CharacterName);
                AllDataSava.Instance.LoadData("Jarvan");
                Debug.Log(PlayerData.POWERADD);


                GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterBehaviour>().HideCursor();
                break;
            case "No_btn":
                TweenBack();
                GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterBehaviour>().HideCursor();
                break;
            default:
                break;

        }
    }

}
