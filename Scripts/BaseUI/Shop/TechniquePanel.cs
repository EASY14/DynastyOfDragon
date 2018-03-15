using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using DG.Tweening;
public class TechniquePanel : SimplePanelManage {


    private List<Button> upgradeBtnList = new List<Button>();//技能升级按钮
    public List<int> ThreeTechGrade = new List<int>();      //技能的等级
    private List<Image> ThreeGradeInner = new List<Image>(); //技能的Inneer
    private List<int> ThreeGradeCrystal = new List<int>();   //升级所需钻石

    private Image head;
    public Sprite Jarvan;
    public Sprite Ashe;
    public Sprite MasterYi;
    //基础属性
    private Text hp;
    private Text atk;
    private Text atks;
    private Text ms;
    private Text poweradd;
   
    //经验
    private Image ImaExpInner;
    private Text TextExptext;
    private int maxExp;
    //private int test = 0;
    private Text TextExpUpgradetext;

    private AudioSource upgradeSound;

    private static TechniquePanel _instance;
    public static TechniquePanel Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.Find("TechniqueUpgradePanel").GetComponent<TechniquePanel>();

            }
            return _instance;
        }
    }

	// Use this for initialization
	void Start () {

        InitPropertyVar();
        InitVar();
        InitPlayerPropertyVar();

        InitUpgradeBtnList();
        InitBtnsDelegate();
        InitTechGradeInner();

        InitLists();

        PlayerProperToString(PlayerData.GRADE);
        LoadGradeAndExp();
        ZeroSetInfo();

        LoadTechGrade();

        upgradeSound = GetComponent<AudioSource>();
	}
    //初始化角色属性变量
    private void InitPlayerPropertyVar()
    {
        head = transform.FindChild("PlayerHead").FindChild("headIcon").GetComponent<Image>();
        switch (PlayerData.CharacterName)
        {
            case "Jarvan":
                head.sprite = Jarvan;
                break;
            case "Ashe":
                head.sprite = Ashe;
                break;
            case "MasterYi":
                head.sprite = MasterYi;
                break;
        }

    }
    //基础属性
    private void InitPropertyVar()
    {
        hp = transform.FindChild("HP").FindChild("Text").GetComponent<Text>();
        atk = transform.FindChild("ATK").FindChild("Text").GetComponent<Text>();
        atks = transform.FindChild("AS").FindChild("Text").GetComponent<Text>();
        ms = transform.FindChild("MS").FindChild("Text").GetComponent<Text>();
        poweradd = transform.FindChild("POWERADD").FindChild("Text").GetComponent<Text>();
    }

    public void PlayerProperToString(int PlayerGrade)
    {
        //Debug.Log("PlayerGrade : " + PlayerGrade.ToString());
        float ihp=PlayEquipInfo.Instance.CulPlayerUpgradeProperty(PlayerData.HP, 10.0f, PlayerGrade,0.0f);
        float ipoweradd = PlayEquipInfo.Instance.CulPlayerUpgradeProperty(PlayerData.POWERADD, 10.0f, PlayerGrade, 0.0f);
        float ims = PlayEquipInfo.Instance.CulPlayerUpgradeProperty(PlayerData.MS, 2.0f, PlayerGrade, 0.0f);
        float iatk = PlayEquipInfo.Instance.CulPlayerUpgradeProperty(PlayerData.ATK, 30.0f, PlayerGrade, 0.0f);
        float iatks = PlayEquipInfo.Instance.CulPlayerUpgradeProperty(PlayerData.AS, 2.0f, PlayerGrade, 0.0f);

        //Debug.Log(iatk.ToString() + "," + iatks.ToString() + "," + ims.ToString() + "," + ipoweradd.ToString());
        UpdatatoString(ihp, iatk, iatks, ims, ipoweradd);

        //BattleUIController.Instance.HpChange(ihp,ihp);
        //BattleUIController.Instance.UpdateData(iatk, iatks, ims, ipoweradd);

        PlayEquipInfo.Instance.CulAllProperty(PlayerGrade);
    }

    public void UpdatatoString(float ihp, float iatk, float iatks, float ims, float ipoweradd)
    {
        hp.text = string.Format("{0:N1}", ihp);
        atk.text = string.Format("{0:N2}", iatk);
        atks.text = string.Format("{0:N2}", iatks);
        ms.text = string.Format("{0:N2}", ims);
        poweradd.text = string.Format("{0:N2}", ipoweradd);
    }
    //该接口用于记录角色属性和 经验  等级 每当关闭游戏后在init()重新加载
    public void LoadGradeAndExp()
    {
        //Debug.Log(PlayerData.EXP);
        //Debug.Log(PlayerData.MaxEXP);
        
        TextExpUpgradetext.text = PlayerData.GRADE.ToString();
        UpdatePlayerInfoByUpgradBtn(PlayerData.GRADE);
        UpdateExp(PlayerData.EXP);

    }
    private void InitVar()
    {
        GameObject GoExp = transform.FindChild("EXP").gameObject;
        ImaExpInner = GoExp.transform.FindChild("exp_inner").GetComponent<Image>();
        TextExptext = GoExp.transform.FindChild("exp_text").GetComponent<Text>();
        TextExpUpgradetext = transform.FindChild("ExpUpgrade").FindChild("Text").GetComponent<Text>();

        maxExp = 100 * (PlayerData.GRADE + 1);
    }



    //保存技能等级
    private void SaveTechGrade()
    {
        PlayerData.TECHGRADE.Clear();
        for(int i=0;i<ThreeTechGrade.Count;i++)
        {
            PlayerData.TECHGRADE.Add(ThreeTechGrade[i]);
           // Debug.Log(ThreeTechGrade[i]);
        }
    }
    //加载技能等级
    public void LoadTechGrade()
    {
        ThreeTechGrade.Clear();
        ThreeGradeCrystal.Clear();
        if (ThreeTechGrade.Count==0)
        {
            InitLists();
        }
        if (PlayerData.TECHGRADE.Count == 0)
        {
            for (int i = 0; i < 3; i++)
            {
                PlayerData.TECHGRADE.Add(0);
            }
        }
        for (int grade = 0; grade < ThreeTechGrade.Count; grade++)
        {
            int Techgrade = PlayerData.TECHGRADE[grade];
            int index = grade + 1;//技能按钮的下标
            SetTechGradeAndInnerAndCrystalNumByTechGrade(index, Techgrade);
        }
        //for (int inner = 0; inner < 3;inner++ )
        //{
        //    ThreeGradeInner[inner].fillAmount = PlayerData.TECHGRADE[inner]/10.0f;
           
        //}
    }


    //数据归0
    public void TechInfoResetToZero()
    {
        ZeroSetInfo();
        SaveTechGrade();
        LoadTechGrade();
        
    }


    //初始化  【组件初始化】
    private void InitUpgradeBtnList()
    {
        Add2BtnListByName("tech1_upgrade");
        Add2BtnListByName("tech2_upgrade");
        Add2BtnListByName("tech3_upgrade");
        Add2BtnListByName("ExpUpgrade");
        Add2BtnListByName("Close_btn");
    }
    //通过名字添加按钮到按钮List    
    private void Add2BtnListByName(string btnName)
    {
        GameObject Go = transform.FindChild(btnName).gameObject;
        Button upgradeBtn = Go.GetComponent<Button>();
        upgradeBtnList.Add(upgradeBtn);
    }


    //技能级数内填充List  【组件初始化】
    private void InitTechGradeInner()
    {
        for (int index = 1; index <= 3; index++)
        {
            string innerName = "Tech" + index + "_inner";
            GameObject InnerGo = GameObject.Find(innerName);
            Image InnerImage = InnerGo.GetComponent<Image>();
            ThreeGradeInner.Add(InnerImage);
        }
    }

    private void InitLists()
    {
        for (int i = 0; i < 3; i++)
        {
            ThreeTechGrade.Add(0);
            ThreeGradeCrystal.Add(0);
        }
    }

     //技能级数&升级所需钻石0始化【数据初始化】
    private void ZeroSetInfo()
    {
        for (int i = 0; i < 3; i++)
        {
            int TechBtnindex = i + 1;//技能按钮的下标 Tech1 Tech2 Tech3
            SetTechGradeAndInnerAndCrystalNumByTechGrade(TechBtnindex, 0);
        }
    }
    //设置技能的级数 &&更改Inner &&更改钻石数
    private void SetTechGradeAndInnerAndCrystalNumByTechGrade(int index, int grade)
    {
       string tName="Tech" + index;
        GameObject GoTech = transform.FindChild(tName).gameObject;
        string strTech_text = tName + "_text";
        Text TextTech_text = GoTech.transform.FindChild(strTech_text).GetComponent<Text>();
        TextTech_text.text = grade.ToString() + "/10";

        
        ThreeGradeInner[index - 1].fillAmount = grade / 10.0f;
        ThreeTechGrade[index - 1] = grade;
        
        int money=5000*(grade+1);
        upgradeBtnList[index - 1].transform.FindChild("Text").GetComponent<Text>().text = money.ToString();
        ThreeGradeCrystal[index - 1] = money;
    }
  

    //按钮添加代理
	private void InitBtnsDelegate()
    {
        for (int i = 0; i < upgradeBtnList.Count; i++)
        {
            Button btn = upgradeBtnList[i];
            btn.onClick.AddListener(delegate() { this.onButtonClick(btn.name); });
        }
    }
    //技能升级按钮
    private void onButtonClick(string bt)
    {
        switch(bt)
        { 
            //升级按钮的名字
            case "tech1_upgrade"://btnid=1
                ClickUpgradeBtn(1);
                break;
            case "tech2_upgrade"://btnid=2
                ClickUpgradeBtn(2);
                break;
            case "tech3_upgrade"://btnid=3
                ClickUpgradeBtn(3);
                break;
            case "ExpUpgrade":
                ClickExpUpgrade();
                break;

            case "Close_btn":
                TweenBack();
                GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterBehaviour>().HideCursor();
                break;
            default:
                break;
        }
    }
    //角色等级升级
    private void ClickExpUpgrade()
    {

        if ((PlayerData.EXP >= PlayerData.MaxEXP) && PlayerData.GRADE < 31)
        {
            PlayerData.GRADE = PlayerData.GRADE + 1;
            TextExpUpgradetext.text = PlayerData.GRADE.ToString();
            BattleUIController.Instance.UpdateLevel();
            PlayerData.EXP -= PlayerData.MaxEXP;
            UpdatePlayerInfoByUpgradBtn(PlayerData.GRADE);
            UpdateExp(PlayerData.EXP);
            upgradeSound.Play();
        }
        /////////////////////////////
        //测试升级用
        //PlayerData.EXP += 100;
        //UpdateExp(PlayerData.EXP);
        /////////////////////////////
    }
    //更新经验条
    public void UpdateExp(int exp)
    {
        TextExptext.text = exp + "/" + maxExp;
        ImaExpInner.fillAmount = exp * 1.0f / maxExp;
    }
    //通过点击升级按钮更新玩家信息
    public void UpdatePlayerInfoByUpgradBtn(int PlayerGrade)
    {
        //Debug.Log(PlayerGrade);
        maxExp = 30 * (PlayerGrade+1);

        PlayEquipInfo.Instance.CulAllProperty(PlayerGrade);

        PlayerData.MaxEXP = maxExp;
        //Debug.Log(PlayerData.MaxEXP);
        PlayerProperToString(PlayerData.GRADE);
    }


    //获取升级需付钻石,by技能按钮id
    private int  GetPayMoneyByButtonTechId(int id)
    {
        Text t=upgradeBtnList[id - 1].transform.FindChild("Text").GetComponent<Text>();
        int shouldPay = int.Parse(t.text);
        return shouldPay;
    }

    //提升一个等级,by技能N的id
    private void UpgradeTechGradeTextByTechName(int id)
    {
        //string tName = "Tech" + id;
        ThreeTechGrade[id - 1] = ThreeTechGrade[id - 1]+1;
        //Debug.Log(ThreeTechGrade[id - 1]);
        SetTechGradeAndInnerAndCrystalNumByTechGrade(id, ThreeTechGrade[id - 1]);

    }
    //技能升级按钮
    private void ClickUpgradeBtn(int id)
    {
       int ExtraMoney= PlayerData.CRYSTALNUM-GetPayMoneyByButtonTechId(id);
       if (ThreeTechGrade[id - 1] < 10 && ExtraMoney>=0)
        {
            PlayerData.CRYSTALNUM -= GetPayMoneyByButtonTechId(id);
            UpgradeTechGradeTextByTechName(id);

            SaveTechGrade();

            upgradeSound.Play();
        }
    }


    ////面板入场动画
    //public void TweenPlay()
    //{
    //    transform.GetComponent<DOTweenAnimation>().DOPlayForward();


    //}
    ////面板出场动画
    //public void TweenBack()
    //{
    //    transform.GetComponent<DOTweenAnimation>().DOPlayBackwards();
    //}

}
