using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;//Dict头文件
using DG.Tweening;
public class PlayEquipInfo : MonoBehaviour
{
    private TechniquePanel Techniquepanel;
    private BagManager Bagmanager;
    public GameObject takeOutEquipTips;
    public GameObject[] EquipGrilds;
    private List<string> EquipsName = new List<string>();//装备栏已装备的装备名
    public Dictionary<string, GameObject> PlayEquipDic = new Dictionary<string, GameObject>();


    //装备前的人物资料
    //private float preHp;
    //private float preAtk;
    //private float preAtkSpeed;
    //private float preMoveSp;
    //private float prePowAdd;

    public List<Button> btnList = new List<Button>();//当前面板的按钮list

    private AudioSource takeoutequipSound;
    
    //private ObjectInfoManager myObjectInfo;
    public string EquipIconName
    {
        get;
        set;
    }

    private static PlayEquipInfo _instance;
    public static PlayEquipInfo Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.Find("PlayEquipInfo").GetComponent<PlayEquipInfo>();

            }
            return _instance;
        }
    }

    // Use this for initialization
    void Start()
    {
        Init();
    }

    void OnDestroy()
    {
        SaveEquipData();
    }

    private void Init()
    {
        InitVar();

        InitEquipGrilds();
        LoadEquipData();
        add2BtnListbyName("Close_btn");
        InitBtnsDelegate();
        CulAllProperty(PlayerData.GRADE);
       //UpdatePlayerInfoByEquip(null, 0);
        takeoutequipSound = GetComponent<AudioSource>();

      
      
    }
    public void InitBtnsDelegate()
    {
        for (int i = 0; i < btnList.Count; i++)
        {
            Button btn = btnList[i];
            btn.onClick.AddListener(delegate() { this.onButtonClick(btn.name); });
        }
    }
    public void add2BtnListbyName(string btnName)
    {
        Button btn = transform.FindChild(btnName).GetComponent<Button>();
        btnList.Add(btn);
    }
    public void onButtonClick(string bN)
    {
        //Debug.Log(bN);
        switch (bN)
        {
            case "Close_btn":
                TweenBack();
                if (BagManager.Instance != null)
                    BagManager.Instance.TweenBack();
                if (ShoppingBagManager.Instance != null)
                    ShoppingBagManager.Instance.TweenBack();
                GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterBehaviour>().HideCursor();
                break;
            default:
                break;

        }
    }
    
    void Update()
    {

        //if (Input.GetKeyDown(KeyCode.K))
        //{
        //    GameObject.Find("changeScene").GetComponent<ChangeScene>().SceneName = "modeltest";
        //    GameObject.Find("changeScene").GetComponent<ChangeScene>().NextScene();
        //}

        if (Input.GetKeyDown(KeyCode.I))
        {
            if (GameObject.Find("BagSystem") != null)
            	GameObject.Find("BagSystem").GetComponent<Shop>().BagBtnOnClickShow();
            else
                TweenPlay();
            GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterBehaviour>().ShowCursor();
        } 
    }
    private void InitVar()
    {
        if (GameObject.Find("TechniqueUpgradePanel") != null)
            Techniquepanel = GameObject.Find("TechniqueUpgradePanel").GetComponent<TechniquePanel>();
        if (GameObject.Find("Bag") != null)
            Bagmanager = GameObject.Find("Bag").GetComponent<BagManager>();

        EquipGrilds = new GameObject[5];

    }


    public void ClickGoTechniqueUpgrade()
    {
        if (Techniquepanel != null)
        {
            Techniquepanel.TweenPlay();
          //  Debug.Log("show tech");
        }
        if (Bagmanager != null)
            Bagmanager.TweenBack();
    }
    public void TechniquepanelTweenBack()
    {
        if (Techniquepanel != null)
            Techniquepanel.TweenBack();
    }

     public void CulAllProperty(int PlayerGrade)
     {
         //Debug.Log(PlayerGrade);
         //Debug.Log(PlayerData.CurrentATK.ToString() + "," + PlayerData.CurrentAS.ToString() + "," + PlayerData.CurrentMS.ToString() + "," + PlayerData.CurrentPOWERADD.ToString());
         PlayerData.CurrentHP = CulPlayerUpgradeProperty(PlayerData.HP, 10.0f, PlayerGrade, 0);
         PlayerData.CurrentPOWERADD = CulPlayerUpgradeProperty(PlayerData.POWERADD, 10.0f, PlayerGrade, 0);
         PlayerData.CurrentMS = CulPlayerUpgradeProperty(PlayerData.MS, 2.0f, PlayerGrade, 0);
         PlayerData.CurrentATK = CulPlayerUpgradeProperty(PlayerData.ATK, 30.0f, PlayerGrade, 0);
         PlayerData.CurrentAS = CulPlayerUpgradeProperty(PlayerData.AS, 2.0f, PlayerGrade,0);
         for (int i = 0; i < EquipsName.Count; i++)
         {
             if (ObjectInfoManager.Instance.GetInfoByIconName(EquipsName[i]).Hp != 0)
             {
                 PlayerData.CurrentHP = CulPlayerUpgradeProperty(PlayerData.HP, 10.0f, PlayerGrade, ObjectInfoManager.Instance.GetInfoByIconName(EquipsName[i]).Hp);
             }
             if (ObjectInfoManager.Instance.GetInfoByIconName(EquipsName[i]).PowerSpeed != 0)
             {
                 PlayerData.CurrentPOWERADD = CulPlayerUpgradeProperty(PlayerData.POWERADD, 10.0f, PlayerGrade, ObjectInfoManager.Instance.GetInfoByIconName(EquipsName[i]).PowerSpeed);
             }
             if (ObjectInfoManager.Instance.GetInfoByIconName(EquipsName[i]).MoveSpeed != 0)
             {
                 PlayerData.CurrentMS = CulPlayerUpgradeProperty(PlayerData.MS, 2.0f, PlayerGrade, ObjectInfoManager.Instance.GetInfoByIconName(EquipsName[i]).MoveSpeed);
             }
             if (ObjectInfoManager.Instance.GetInfoByIconName(EquipsName[i]).AttackValue != 0)
             {
                 PlayerData.CurrentATK = CulPlayerUpgradeProperty(PlayerData.ATK, 30.0f, PlayerGrade, ObjectInfoManager.Instance.GetInfoByIconName(EquipsName[i]).AttackValue);

             }
             if (ObjectInfoManager.Instance.GetInfoByIconName(EquipsName[i]).AttackSpeed != 0)
             {
                 PlayerData.CurrentAS = CulPlayerUpgradeProperty(PlayerData.AS, 2.0f, PlayerGrade, ObjectInfoManager.Instance.GetInfoByIconName(EquipsName[i]).AttackSpeed);

             }
         }
         //if(Techniquepanel==null)
         //{
         //    Techniquepanel = GameObject.Find("TechniqueUpgradePanel").GetComponent<TechniquePanel>();
         //}
         //if(Techniquepanel!=null)
         //{
             //Techniquepanel.UpdatatoString(PlayerData.CurrentHP, PlayerData.CurrentATK, PlayerData.CurrentAS, PlayerData.CurrentMS, PlayerData.CurrentPOWERADD);
         //Debug.Log(PlayerData.CurrentATK.ToString() + "," + PlayerData.CurrentAS.ToString() + "," + PlayerData.CurrentMS.ToString() + "," + PlayerData.CurrentPOWERADD.ToString());
        
         BattleUIController.Instance.UpdateData(PlayerData.CurrentATK, PlayerData.CurrentAS, PlayerData.CurrentMS, PlayerData.CurrentPOWERADD);
         BattleUIController.Instance.HpChange(PlayerData.CurrentHP, PlayerData.CurrentHP);
         BattleUIController.Instance.UpdateExperience();
         GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterBehaviour>().SetData(PlayerData.CurrentHP, PlayerData.CurrentATK, PlayerData.CurrentAS, PlayerData.CurrentMS, PlayerData.CurrentPOWERADD);
         //}
         
     }

    //计算玩家升级属性
    public float CulPlayerUpgradeProperty(float baseData ,float perNum,int pG,float EquipProperty)
     {
         return baseData * (1 + (perNum / 300 * (pG - 1))) * (1 + EquipProperty *0.01f);
     }
      

    //保存装备栏数据
    public void SaveEquipData()
    {
        PlayerData.EQUIPNAMES.Clear();
        for (int i = 0; i < EquipGrilds.Length; i++)
        {
            if (EquipGrilds[i].transform.childCount == 0)
            {
                PlayerData.EQUIPNAMES.Add("null");
                continue;
            }
            PlayerData.EQUIPNAMES.Add(EquipGrilds[i].transform.GetChild(0).GetComponent<Image>().sprite.name);
        }
        
        //for(int j = 0;j<PlayerData.EQUIPNAMES.Count;j++)
        //{
        //    Debug.Log(PlayerData.EQUIPNAMES[j]);
        //}
    }

    //清理装备
    public void ClearEquipData()
    {
        for (int i = 0; i < EquipGrilds.Length; i++)
        {
            if (EquipGrilds[i].transform.childCount != 0)
            {
                GameObject EqGo=EquipGrilds[i].transform.GetChild(0).gameObject;
                string EquipIconSpriteName = EqGo.GetComponent<Image>().sprite.name;
                DestoryByName(EquipIconSpriteName);
            }
        }
        SaveEquipData();
    }

    //加载装备栏数据
    public void LoadEquipData()
    {
        PlayEquipDic.Clear();
        for (int i = 0; i < PlayerData.EQUIPNAMES.Count; i++)
        {
            if (PlayerData.EQUIPNAMES[i] == "null")
            {
                continue;
            }
            Object EquipIcon = Resources.Load("Prefabs/equipIcon");
            GameObject EquipIconGo = GameObject.Instantiate(EquipIcon, transform.position, Quaternion.identity) as GameObject;
            EquipIconGo.GetComponent<Image>().sprite = Resources.Load<Sprite>("shop/" + "equip/" + PlayerData.EQUIPNAMES[i]);
            //Debug.Log(PlayerData.EQUIPNAMES.Count);
            EquipIconGo.GetComponent<Transform>().SetParent(EquipGrilds[i].transform);
            EquipIconGo.transform.localScale = new Vector3(1, 1, 1);
            EquipIconGo.transform.localPosition = Vector3.zero;
            PlayEquipDic.Add(PlayerData.EQUIPNAMES[i], EquipIconGo);
            EquipsName.Add(PlayerData.EQUIPNAMES[i]);
        }
    }

    //初始化装备面板的格子
    private void InitEquipGrilds()
    {
        for (int i = 0; i < EquipGrilds.Length; i++)
        {
            int index = i + 1;
            string name = "equip" + index;
            GameObject go = transform.FindChild(name).gameObject;
            EquipGrilds[i] = go;
        }

    }

    //遍历将背包里的物体放入装备面板
    public bool PutInEquipInfo()
    {
        bool isPutIn = false;
        for (int i = 0; i < EquipGrilds.Length; i++)
        {
            if (EquipGrilds[i].transform.childCount == 0)
            {
                GameObject equipItem = null;

                if (i == 0 && FindTypeByName() == "Armor")
                {
                    equipItem = CreateItem();
                    equipItem.GetComponent<Transform>().SetParent(EquipGrilds[0].transform);
                    BagGrild.Instance.SetBagItemIconNum(EquipIconName, -1);
                    CulAllProperty(PlayerData.GRADE);
                    isPutIn = true;

                }
                if (i == 1 && FindTypeByName() == "shoes")
                {
                    equipItem = CreateItem();
                    equipItem.GetComponent<Transform>().SetParent(EquipGrilds[1].transform);
                    BagGrild.Instance.SetBagItemIconNum(EquipIconName, -1);
                    CulAllProperty(PlayerData.GRADE);
                    isPutIn = true;
                }
                if (i == 2 && FindTypeByName() == "weapon")
                {
                    equipItem = CreateItem();
                    equipItem.GetComponent<Transform>().SetParent(EquipGrilds[2].transform);
                    BagGrild.Instance.SetBagItemIconNum(EquipIconName, -1);
                    CulAllProperty(PlayerData.GRADE);
                    isPutIn = true;
                }
                if (i == 3 && FindTypeByName() == "bracer")
                {
                    equipItem = CreateItem();
                    equipItem.GetComponent<Transform>().SetParent(EquipGrilds[3].transform);
                    BagGrild.Instance.SetBagItemIconNum(EquipIconName, -1);
                    CulAllProperty(PlayerData.GRADE);
                    isPutIn = true;
                }
                if (i == 4 && FindTypeByName() == "necklace")
                {
                    equipItem = CreateItem();
                    equipItem.GetComponent<Transform>().SetParent(EquipGrilds[4].transform);
                    BagGrild.Instance.SetBagItemIconNum(EquipIconName, -1);
                    //UpdatePlayerInfoByEquip(EquipIconName, 1);
                    CulAllProperty(PlayerData.GRADE);
                    isPutIn = true;
                }
                if (equipItem != null)
                {
                    equipItem.transform.localScale = new Vector3(1, 1, 1);
                    equipItem.transform.localPosition = Vector3.zero;
                }

            }
        }
        return isPutIn;
    }

    //生成装备
    public GameObject CreateItem()
    {

        if (EquipIconName != null)
        {
            Object EquipIcon = Resources.Load("Prefabs/equipIcon");
            GameObject EquipIconGo = GameObject.Instantiate(EquipIcon, transform.position, Quaternion.identity) as GameObject;
            EquipIconGo.GetComponent<Image>().sprite = Resources.Load<Sprite>("shop/" + "equip/" + EquipIconName);

            PlayEquipDic.Add(EquipIconName, EquipIconGo);
            EquipsName.Add(EquipIconName);

            //Debug.Log(EquipIconName + "加入装备字典");
            return EquipIconGo;
        }
        return null;

    }

    //通过名字 查找 装备类型
    public string FindTypeByName()
    {
        ObjectInfo EquipInfo = ObjectInfoManager.Instance.GetInfoByIconName(EquipIconName);
        return EquipInfo.EquipType;
    }

    //通过名字 删除 装备物体
    public void DestoryByName(string PlayerEquipSpriteName)
    {
        GameObject Go = FindEquipitemByName(PlayerEquipSpriteName);

        PlayEquipDic.Remove(PlayerEquipSpriteName);
        EquipsName.Remove(PlayerEquipSpriteName);
        Destroy(Go);
        takeoutequipSound.Play();
        takeOutEquipTips.SetActive(true);
        takeOutEquipTips.GetComponent<DOTweenAnimation>().DORestart();
        takeOutEquipTips.GetComponent<DOTweenAnimation>().DOPlay();
       // Debug.Log(PlayerEquipSpriteName + "移出装备字典");
    }

    //通过名字 查找 装备物体
    public GameObject FindEquipitemByName(string PlayerEquipSpriteName)
    {
        GameObject Go = null;
        PlayEquipDic.TryGetValue(PlayerEquipSpriteName, out Go);

        return Go;
    }


    //面板入场动画
    public void TweenPlay()
    {
        transform.GetComponent<DOTweenAnimation>().DOPlayForward();


    }
    //面板出场动画
    public void TweenBack()
    {
        transform.GetComponent<DOTweenAnimation>().DOPlayBackwards();
    }

    public List<string> getEquipsName()
    {
        for (int i = 0; i < EquipsName.Count; i++)
        {
            Debug.Log(EquipsName[i]);
        }
        return EquipsName;
    }


}
