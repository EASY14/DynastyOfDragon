using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using DG.Tweening;
public class BattleUIController : MonoBehaviour {

    public Image HpImage;
    public Text HpNumText;
    public int HpNum
    {
        get;
        set;
    }

    public Image MpImage;
    public Text MpNumText;
    public int MpNum
    {
        get;
        set;
    }


    public Image InjuredImage;

    public Image hp;
    public Text hpText;
    public Image mp;
    public Text mpText;

    public Text levelText;
    public Text attackDamageText;
    public Text attackSpeedText;
    public Text moveSpeedText;
    public Text mpRateText;
    public Text currentCrystalText;
    public Image currentExperienceImage;
    public Image upgradeAvailableImage;

    public Text victoryGetCrystalText;
    public Text victoryGetExperience;
    public Text defeatedGetCrystalText;
    public Text defeatedGetExperience;

    public GameObject gamePause;
    public GameObject gameVictory;
    public GameObject gameDefeated;

    public float skillColdTime = 1.0f;
    public float medicationColdTime = 60.0f;

    public Image hpCold;
    public Image mpCold;
    public Image skill1Cold;
    public Image skill2Cold;
    public Image skill3Cold;

    private float hpColdTime = 0.0f;
    private float mpColdTime = 0.0f;
    private float skill1ColdTime = 0.0f;
    private float skill2ColdTime = 0.0f;
    private float skill3ColdTime = 0.0f;

    private ChangeScene changeScene;

    private static BattleUIController _instance;

    private int currentCrystal;
    private int getCryStal;

    private int currentExperience;
    private int maxExperience;
    private int getExperience;

    public static BattleUIController Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindGameObjectWithTag(TagManager.battleuicontrol).GetComponent<BattleUIController>();
            }
            return _instance;
        }
    }

	// Use this for initialization
	void Start () {
        changeScene = GetComponent<ChangeScene>();

        getCryStal = 0;
        getExperience = 0;

        //初始化等级
        UpdateLevel();
        
        //初始化经验
        UpdateExperience();

        currentCrystalText.text = PlayerData.CRYSTALNUM.ToString();
        currentCrystal = PlayerData.CRYSTALNUM;

        LoadDateHpMp();

	}
	
	// Update is called once per frame
	void Update () {
        UpdateCold();
	}

    public void UpdateLevel()
    {
        levelText.text = PlayerData.GRADE.ToString();
    }
    private void UpdateCold()
    {
        if(hpCold.fillAmount  > 0)
        {
            hpColdTime += Time.deltaTime;
            hpCold.fillAmount = (1-hpColdTime/medicationColdTime);
        }
        if (mpCold.fillAmount > 0)
        {
            mpColdTime += Time.deltaTime;
            mpCold.fillAmount = (1 - mpColdTime / medicationColdTime);
        }
        if (skill1Cold.fillAmount > 0)
        {
            skill1ColdTime += Time.deltaTime;
            skill1Cold.fillAmount = (1 - skill1ColdTime / skillColdTime);
        }
        if (skill2Cold.fillAmount > 0)
        {
            skill2ColdTime += Time.deltaTime;
            skill2Cold.fillAmount = (1 - skill2ColdTime / skillColdTime);
        }
        if (skill3Cold.fillAmount > 0)
        {
            skill3ColdTime += Time.deltaTime;
            skill3Cold.fillAmount = (1 - skill3ColdTime / skillColdTime);
        }
    }

    public bool UseHPMedication()
    {
        if (hpCold.fillAmount > 0)
        {
            return false;
        }
        else
        {
            if (HpNum <= 0)
                return false;
            HpNum -= 1;
            hpCold.fillAmount = 1.0f;
            hpColdTime = 0.0f;
            SetHpMpToString();
            SaveHpMpData();
            return true;
        }
    }

    public bool UseMPMedication()
    {
        if (mpCold.fillAmount > 0)
        {
            return false;
        }
        else
        {
            if (MpNum <= 0)
                return false;
            MpNum -= 1;
            mpCold.fillAmount = 1.0f;
            mpColdTime = 0.0f;
            SetHpMpToString();
            SaveHpMpData();
            return true;
        }
    }

    public bool UseSkill(int ID)
    {
        switch(ID)
        {
            case 1:
                if (skill1Cold.fillAmount > 0)
                {
                    return false;
                }
                else
                {
                    skill1Cold.fillAmount = 1.0f;
                    skill1ColdTime = 0.0f;
                    return true;
                }

            case 2:
                if (skill2Cold.fillAmount > 0)
                {
                    return false;
                }
                else
                {
                    skill2Cold.fillAmount = 1.0f;
                    skill2ColdTime = 0.0f;
                    return true;
                }

            case 3:
                if (skill3Cold.fillAmount > 0)
                {
                    return false;
                }
                else
                {
                    skill3Cold.fillAmount = 1.0f;
                    skill3ColdTime = 0.0f;
                    return true;
                }

            default:
                return false;
        }
    }



    public void HpChange(float maxHp,float currentHp)
    {
        hp.fillAmount = currentHp / maxHp;
        hpText.text = string.Format("{0:N1}", currentHp >= 0 ? currentHp : 0) + "/" + string.Format("{0:N1}", maxHp);
    }

    public void MpChange(float maxMp, float currentMp)
    {
        mp.fillAmount = currentMp / maxMp;
        mpText.text = string.Format("{0:N0}", currentMp >= 0 ? currentMp : 0) + "/" + string.Format("{0:N0}", maxMp);
    }

    public void UpdateData(float attackdamage, float attackspeed, float movespeed, float mprate)
    {
        //Debug.Log(attackdamage.ToString()+","+attackspeed.ToString()+","+movespeed.ToString()+","+mprate.ToString());
        attackDamageText.text = string.Format("{0:N2}", attackdamage);
        attackSpeedText.text = string.Format("{0:N2}", attackspeed);
        moveSpeedText.text = string.Format("{0:N2}", movespeed);
        mpRateText.text = string.Format("{0:N2}", mprate);
      
    }

    public void GetCrystal(int crystal)
    {
        getCryStal += crystal;
        currentCrystal += crystal;
        currentCrystalText.text = currentCrystal.ToString();
    }

    public void GetExperience(int exp)
    {
        getExperience += exp;
        currentExperience += exp;
        currentExperienceImage.fillAmount = currentExperience / (float)maxExperience;
        if (currentExperience >= maxExperience && maxExperience > 0)
            upgradeAvailableImage.gameObject.SetActive(true);
        else if (currentExperience < maxExperience)
            upgradeAvailableImage.gameObject.SetActive(false);
    }

    public void UpdateExperience()
    {
        currentExperience = PlayerData.EXP;
        maxExperience = PlayerData.MaxEXP;
        currentExperienceImage.fillAmount = currentExperience / (float)maxExperience;
        if (currentExperience >= maxExperience && maxExperience > 0)
            upgradeAvailableImage.gameObject.SetActive(true);
        else if(currentExperience < maxExperience)
            upgradeAvailableImage.gameObject.SetActive(false);
    }

    public void GameVictory()
    {
        if (gameVictory!=null)
        {

            gameVictory.SetActive(true);
            gameVictory.GetComponent<DOTweenAnimation>().DOPlay();
            victoryGetCrystalText.text = getCryStal.ToString();
            victoryGetExperience.text = getExperience.ToString();


            BattleUIController.Instance.unLockNextLevel();
        }
       
        BattleFinish();
    }

    public void GameDefeated()
    {
        if (gameDefeated!=null)
        {

            gameDefeated.SetActive(true);
            gameDefeated.GetComponent<DOTweenAnimation>().DOPlay();
            defeatedGetCrystalText.text = getCryStal.ToString();
            defeatedGetExperience.text = getExperience.ToString();
        }
        
        BattleFinish();
    }

    public void GamePause()
    {
        Time.timeScale = 0.0f;
        gamePause.SetActive(true);
    }

    public void GameResume()
    {
        Time.timeScale = 1.0f;
        gamePause.SetActive(false);
    }

    public void GameReplay()
    {
        changeScene.ResetScene();
    }

    public void GameQuit()
    {
        BattleFinish();
        changeScene.NextScene("MainCity");
    }

    private void BattleFinish()
    {
        PlayerData.EXP = currentExperience;
        PlayerData.CRYSTALNUM = currentCrystal;
    }

    public void Injured()
    {
        InjuredImage.color = new Color(1.0f, 0.0f, 0.0f, 25.0f/255.0f);
        InjuredImage.GetComponent<DOTweenAnimation>().DOPlay();
        InjuredImage.GetComponent<DOTweenAnimation>().DORestart();

    }

    //-----------------------------------------------------------------------//
    public void ResetDateHpMp()
    {
        SetHpMpNumandSprite(PlayerData.INITHpSpriteName, PlayerData.INITHpdrugNum, PlayerData.INITMpSpriteName, PlayerData.INITMpdrugNum);

        PlayerData.CurrentHpSpriteName = PlayerData.INITHpSpriteName;
        PlayerData.CurrentHpdrugNum = PlayerData.INITHpdrugNum;

        PlayerData.CurrentMpSpriteName = PlayerData.INITMpSpriteName;
        PlayerData.CurrentMpdrugNum = PlayerData.INITMpdrugNum;

        HpNum = PlayerData.CurrentHpdrugNum;
        MpNum = PlayerData.CurrentMpdrugNum;

        SetHpMpToString();
    }

    public void LoadDateHpMp()
    {
        SetHpMpNumandSprite(PlayerData.CurrentHpSpriteName, PlayerData.CurrentHpdrugNum, PlayerData.CurrentMpSpriteName, PlayerData.CurrentMpdrugNum);
        SetHpMpToString();


    }
    private void SetHpMpNumandSprite(string Hpsprite, int Hpnum, string Mpsprite, int Mpnum)
    {
        HpImage.sprite = Resources.Load<Sprite>("shop/" + "equip/" + Hpsprite);
        HpNum = Hpnum;

        MpImage.sprite = Resources.Load<Sprite>("shop/" + "equip/" + Mpsprite);
        MpNum = Mpnum;

        SaveHpMpData();
    }
    public void SaveHpMpData()
    {
        PlayerData.CurrentHpdrugNum = HpNum;
        PlayerData.CurrentMpdrugNum = MpNum;
        if (HpImage.sprite != null)
        {
            PlayerData.CurrentHpSpriteName = HpImage.sprite.name;
        }
        if (MpImage.sprite != null)
        {
            PlayerData.CurrentMpSpriteName = MpImage.sprite.name;
        }

    }
    public void SetHpMpToString()
    {
        if (HpImage.sprite != null)
        {
            if (HpImage.sprite.name == PlayerData.INITHpSpriteName)
            {
                HpNumText.text = "";
            }
            else
            {
                HpNumText.text = HpNum.ToString();
            }
        }
        

        if(MpImage.sprite!=null)
        {
            if (MpImage.sprite.name == PlayerData.INITMpSpriteName)
            {
                MpNumText.text = "";
            }
            else
            {
                MpNumText.text = MpNum.ToString();
            }
        }
        
        SaveHpMpData();
    }

    public void PutMpHpInBattleUI(string HpsN, int Hpn, string MpsN, int Mpn)//HpSpriteName HpSpriteNum   MpSpriteName MpSpriteNum 
    {
        
        if(HpsN!=null)
        {          
            if (HpImage.sprite.name==HpsN)//需加的药和原来的药品栏里的一样
            {
                HpNum += Hpn; 
            }
            if (HpImage.sprite.name != HpsN)//需加的药和原来的药品栏里的不一样  
            {
                if (HpImage.sprite.name != PlayerData.INITHpSpriteName)//当前药品栏有药
                {
                    BagGrild.Instance.clickIconName = HpImage.sprite.name;
                    BagGrild.Instance.PutbagItemInGrild();
                    BagGrild.Instance.SetBagItemIconNum(HpImage.sprite.name, HpNum-1);
                }

                HpImage.sprite = Resources.Load<Sprite>("shop/" + "equip/" + HpsN);
                HpNum = Hpn;
            }
            SetHpMpToString();

            PlayerData.CurrentHpSpriteName = HpsN;
            PlayerData.CurrentHpdrugNum = HpNum;
        }

        if (MpsN != null)
        {
            if (MpImage.sprite.name==MpsN)
            {
                MpNum += Mpn; 
            }
            if(MpImage.sprite.name!=MpsN)
            {
                if(MpImage.sprite.name!=PlayerData.INITMpSpriteName)
                {
                    BagGrild.Instance.clickIconName = MpImage.sprite.name;
                    BagGrild.Instance.PutbagItemInGrild();
                    BagGrild.Instance.SetBagItemIconNum(MpImage.sprite.name, MpNum - 1);
                }
                MpImage.sprite = Resources.Load<Sprite>("shop/" + "equip/" + MpsN);
                MpNum = Mpn;
            }
            SetHpMpToString();

            PlayerData.CurrentMpSpriteName = MpsN;
            PlayerData.CurrentMpdrugNum = MpNum;
        }
    }

    public int GetAddHp()//获取当前药品栏 Hp药的hp值
    {
        if (PlayerData.CurrentHpSpriteName != PlayerData.INITHpSpriteName)
        {
            ObjectInfo DrugInfo = ObjectInfoManager.Instance.GetInfoByIconName(PlayerData.CurrentHpSpriteName);
            PlayerData.AddHp = DrugInfo.Hp;

        }
        return PlayerData.AddHp;
    }
    public float GetAddMp()//获取当前药品栏 Mp药的mp值
    {
        if (PlayerData.CurrentMpSpriteName != PlayerData.INITMpSpriteName)
        {
            ObjectInfo DrugInfo = ObjectInfoManager.Instance.GetInfoByIconName(PlayerData.CurrentMpSpriteName);
            PlayerData.AddMp = DrugInfo.Power;
        }
        return PlayerData.AddMp;
    }
    
    //------------------------------------------------------------------------//
    public void unLockNextLevel()
    {
        Debug.Log(11111111111111);
        if (PlayerData.UNLOCKLEVEL[PlayerData.CurrentLevelIndex + 1] == 0)
        {
            PlayerData.UNLOCKLEVEL[PlayerData.CurrentLevelIndex + 1] = 1;
        }
    }
}
