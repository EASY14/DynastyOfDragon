using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MapUIManager : SimplePanelManage {

    private Toggle[] ToggleGroup;
    private GameObject[] PanelMaps;
    private GameObject[] StoryTexts;
         
    private int MaxMap=4;

    public MapModeItem[] Levels;//E1 E2 E3 E4 M1 M2 M3 M4 D1 D2 D3 D4
   
    public int MaxLevels = 8;
    public int[] unLockLevels;//解锁为1 锁上为0


    private static MapUIManager _instance;
    public static MapUIManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.Find("MapUI").GetComponent<MapUIManager>();

            }
            return _instance;
        }
    }

	void Start () {
        InitVar();
        DefaultState();

        DefaultLockData();

       
        LoadunLockData();
        SaveunLockData();
        UpdateLevelsLock();

	}
    private void InitVar()
    {
       
        ToggleGroup = new Toggle[MaxMap];
        for (int i = 0; i < MaxMap; i++)
        {
            int index=i+1;
            Toggle t = transform.FindChild("MapGroup").FindChild("Map" + index).GetComponent<Toggle>();

            t.onValueChanged.AddListener(delegate(bool bisOn) { this.onToggleClick(bisOn,t.name); });

            ToggleGroup[i] = t;
        }


        PanelMaps = new GameObject[MaxMap];
        StoryTexts = new GameObject[MaxMap];
        for (int i = 0; i < MaxMap; i++)
        {
            int index = i + 1;
            GameObject Pm = transform.FindChild("PanelMap" + index).gameObject;
            PanelMaps[i] = Pm;
            GameObject Pt = Pm.transform.Find("story").gameObject;
            StoryTexts[i] = Pt;
        }

        unLockLevels = new int[MaxLevels];
        SetLevel1Story();
       
    }


    //保存关卡解锁数据
    public void SaveunLockData()
    {
        PlayerData.UNLOCKLEVEL.Clear();
        for (int i = 0; i < MaxLevels; i++)
        {
            PlayerData.UNLOCKLEVEL.Add(unLockLevels[i]);
        }
    }
    //加载关卡解锁数据
    public void LoadunLockData()
    {
        if (PlayerData.UNLOCKLEVEL.Count == MaxLevels)
        {
            for (int i = 0; i < MaxLevels; i++)
            {
                unLockLevels[i] = PlayerData.UNLOCKLEVEL[i];
            }
        }
       
    }

    public void ClearMapData()
    {
        DefaultLockData();
        UpdateLevelsLock();
        SaveunLockData();
    }

    //默认第一关开，其他关卡全部锁上
    public void DefaultLockData()
    {
        unLockLevels[0] = 1;
        for (int i = 1; i < MaxLevels; i++)
        {
            unLockLevels[i] = 0;
            
        }
    }

    ////解锁某一关
    //public void SetunLockLevel(int levelIndex)//下标0-7 
    //{
    //    unLockLevels[levelIndex] = 1;
    //    UpdateLevelsLock();
    //    SaveunLockData();
    //}

    //保存
    public void UpdateLevelsLock()
    {
        for (int i = 0; i < MaxLevels; i++)
        {
            //Debug.Log("man:"+unLockLevels[i]);
            if(unLockLevels[i]==1)//解锁
            {
                Levels[i].isLock = false;
            }
            if(unLockLevels[i]==0)
            {
                Levels[i].isLock = true;
            }
            Levels[i].ShowT2LockOrF2Normal(Levels[i].isLock);
            //Debug.Log("level" + i + ":" + Levels[i].isLock+"锁");
        }
    }


    //默认只显示第一关的面板
    private void DefaultState()
    {
        hidePanelMap();
        PanelMaps[0].SetActive(true);
    }

    private void hidePanelMap()
    {
        for (int i = 0; i < MaxMap; i++)
        {
            PanelMaps[i].SetActive(false);
        }
    }

    private void onToggleClick(bool bIsOn,string togName)
    {
        if(bIsOn)
        {
            hidePanelMap();
            switch (togName)
            {
                case "Map1":
                    PanelMaps[0].SetActive(true);
                    SetLevel1Story();
                    break;
                case "Map2":
                    PanelMaps[1].SetActive(true);
                    SetLevel2Story();
                    break;
                case "Map3":
                    PanelMaps[2].SetActive(true);
                    SetLevel3Story();
                    break;
                case "Map4":
                    PanelMaps[3].SetActive(true);
                    SetLevel4Story();
                    break;
                default:
                    break;
            }
        }
    }
	
    private void SetLevel1Story()
    {
        if(PlayerData.CurrentLevelIndex/4==0)
        {
            StoryTexts[0].GetComponent<Text>().text = "地形：狂风呼啸的草原  骷髅把草原占领后 提刀骷髅成为大军 长矛得以发展 草原从此被长矛骷髅带领。 屠龙者长途跋涉， 面对狂风如棉针的草原 该何去何从？ 向来胜者为王败者为寇          ......";
        }
        if (PlayerData.CurrentLevelIndex / 4 == 1)
        {
            StoryTexts[0].GetComponent<Text>().text = "地形：记忆草原  我说过要让时光倒流  被这群无知的长矛、提刀  还有弓箭手处处阻碍      你在哪。         我回到记忆里寻找            你一定还在等我。            今年的春节                            我想跟你吃个年夜饭                ....";
        }
        
    }
    private void SetLevel2Story()
    {
        if (PlayerData.CurrentLevelIndex / 4 == 0)
        {
            StoryTexts[1].GetComponent<Text>().text = "地形：冰川雪地  圣诞节来临之际 长矛倡议捕猎庆祝 提刀骷髅决定角逐领导 雪地哀嚎遍野 屠龙者长途跋涉， 面对猎杀狂欢 谁才是谁的猎物？ 谁才是雪野的主宰                   ......";
        }
        if (PlayerData.CurrentLevelIndex / 4 == 1)
        {
            StoryTexts[1].GetComponent<Text>().text = "地形：无尽极地  还是同样的配方  还是同样的小啰啰   执着大概就是我想看看你正脸       穿过这片极地                   我就快要找到我的记忆        变强是拯救我们的唯一道路。            今年的圣诞                           烟花不是用血放的 真好                                 ....";
        }
       
    }
    private void SetLevel3Story()
    {
        if (PlayerData.CurrentLevelIndex / 4 == 0)
        {
            StoryTexts[2].GetComponent<Text>().text = "地形：沙漠风暴  长期缺水精瘦的骷髅 茹毛饮血的提刀  百发百中的弓箭手 听说有人造访磨刀霍霍 屠龙者长途跋涉， 穿越沙漠？不，活着  前缘未了，岂敢中途放弃  拯救？或者埋没？                           ......";
        }
        if (PlayerData.CurrentLevelIndex / 4 == 1)
        {
            StoryTexts[2].GetComponent<Text>().text = "地形：寂寞沙漠   前路越来越难走 也正说明我快走到尽头   而我体力也快达到极限       这片荒漠有种魔力想留住我                   我不能屈服        我要再续前缘。            我不是世界的主宰               我也能继续前行                                 ....";
        }
       
    }
    private void SetLevel4Story()
    {
        if (PlayerData.CurrentLevelIndex / 4 == 0)
        {
            StoryTexts[3].GetComponent<Text>().text = "地形：浓血岩浆  死伤无数的岩浆地带 散发着让人呕吐的味道  无论是长矛、提刀  还是弓箭手来杠我  我不在乎，我要时间倒流 我要知道真相 这个历历在目的背影            是谁？别怕....                  我生来拯救你                                 ....";
        }
        if (PlayerData.CurrentLevelIndex / 4 == 1)
        {
            StoryTexts[3].GetComponent<Text>().text = "地形：迷情沙漠         变成龙的你      是我必须战胜的对象            造物弄人         我竟然生为屠龙者                    相爱相杀 才是我们最终的结局？         等我                                             ....";
        }
        
    }


    public void Leve1Easy(string name)
    {
        if (unLockLevels[0] == 0)
        {
            return;
        }
        //Time.timeScale = 1.0f;
        AsyLoadScene.sceneName = name;
        SceneManager.LoadScene("Loading");
        PlayerData.CurrentLevelIndex = 0;
    }
    public void Leve2Easy(string name)
    {
        if (unLockLevels[1] == 0)
        {
            return;
        }
        //Time.timeScale = 1.0f;
        AsyLoadScene.sceneName = name;
        SceneManager.LoadScene("Loading");
        PlayerData.CurrentLevelIndex = 1;
    }
    public void Leve3Easy(string name)
    {
        if (unLockLevels[2] == 0)
        {
            return;
        }
        //Time.timeScale = 1.0f;
        AsyLoadScene.sceneName = name;
        SceneManager.LoadScene("Loading");
        PlayerData.CurrentLevelIndex = 2;
    }
    public void Leve4Easy(string name)
    {
        if (unLockLevels[3] == 0)
        {
            return;
        }
        //Time.timeScale = 1.0f;
        AsyLoadScene.sceneName = name;
        SceneManager.LoadScene("Loading");
        PlayerData.CurrentLevelIndex = 3;
    }


    public void Leve1Medium(string name)
    {
        if (unLockLevels[4] == 0)
        {
            return;
        }
        //Time.timeScale = 1.0f;
        AsyLoadScene.sceneName = name;
        SceneManager.LoadScene("Loading");
        PlayerData.CurrentLevelIndex = 4;
    }
    public void Leve2Medium(string name)
    {
        if (unLockLevels[5] == 0)
        {
            return;
        }
        //Time.timeScale = 1.0f;
        AsyLoadScene.sceneName = name;
        SceneManager.LoadScene("Loading");
        PlayerData.CurrentLevelIndex = 5;
    }
    public void Leve3Medium(string name)
    {
        if (unLockLevels[6] == 0)
        {
            return;
        }
        //Time.timeScale = 1.0f;
        AsyLoadScene.sceneName = name;
        SceneManager.LoadScene("Loading");
        PlayerData.CurrentLevelIndex = 6;
    }
    public void Leve4Medium(string name)
    {
        if (unLockLevels[7] == 0)
        {
            return;
        }
        //Time.timeScale = 1.0f;
        AsyLoadScene.sceneName = name;
        SceneManager.LoadScene("Loading");
        PlayerData.CurrentLevelIndex = 7;
    }
}
