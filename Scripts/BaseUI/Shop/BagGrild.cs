using UnityEngine;
using System.Collections;
using UnityEngine.UI;//Text 
using UnityEngine.SceneManagement;
using System.Collections.Generic;//Dict头文件
using DG.Tweening;
public class BagGrild : MonoBehaviour
{

    public GameObject[] bagItems;//背包格子
    private string bagItemIconPath;//装备物体的路径

    public Dictionary<string, GameObject> BagItemDic = new Dictionary<string, GameObject>();

    public GameObject putOnEquipTips;
    public GameObject NeedToTakeOutTips;
    public AudioSource putOnEquipSound;
    public AudioSource putOnDrugSound;

    public string clickIconName//获取在商店买了什么的名字传进来
    {
        set;
        get;
    }


    private static BagGrild _instance;
    public static BagGrild Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.Find("BagItemGroup").GetComponent<BagGrild>();
            }
            return _instance;
        }
    }

    void OnDestroy()
    {
        SaveBagData();
    }

    void Start()
    {
        bagItemIconPath = "Prefabs/bagIcon";
        clickIconName = null;
        LoadBagData();

    }

    // Update is called once per frame
    void Update()
    {

    }

    //生成物体
    private GameObject CreatebagItemIcon()
    {
        if (clickIconName != null)
        {
            if (FindBagitemByName(clickIconName) == null)
            {
                Object BagIcon = Resources.Load(bagItemIconPath);
                GameObject BagIconGo = GameObject.Instantiate(BagIcon, transform.position, Quaternion.identity) as GameObject;
                BagIconGo.GetComponent<Image>().sprite = Resources.Load<Sprite>("shop/" + "equip/" + clickIconName);


                BagItemDic.Add(clickIconName, BagIconGo);
                //Debug.Log(clickIconName+"加入背包字典");

                return BagIconGo;
            }
            else
            {
                SetBagItemIconNum(clickIconName, 1);
                //Debug.Log("物体加1");

            }


        }
        return null;


    }

    //设置物体的数目下标
    public void SetBagItemIconNum(string cName, int offset)
    {
        Text Gotext = FindBagitemByName(cName).transform.FindChild("Text").GetComponent<Text>();
        int Newnum = int.Parse(Gotext.text.ToString());
        Newnum += offset;
        if (Newnum <= 0)
        {
            DestorybagIconByName(cName);
        }
        else
        {
            Gotext.text = Newnum.ToString();
        }

    }


    //把购买的商品放进背包
    public void PutbagItemInGrild()
    {
        bool isfilled= true;
        for (int i = 0; i < bagItems.Length; i++)
        {
            if (bagItems[i].transform.childCount == 0)
            {
                isfilled = false;
                GameObject bagItemIcon = CreatebagItemIcon();
                if (bagItemIcon != null)
                {
                    bagItemIcon.GetComponent<Transform>().SetParent(bagItems[i].transform);
                    bagItemIcon.transform.localScale = new Vector3(1, 1, 1);
                    bagItemIcon.transform.localPosition = Vector3.zero;
                }
                return;
            }
        }
        if (isfilled)
            if (FindBagitemByName(clickIconName) != null)
                SetBagItemIconNum(clickIconName, 1);
    }
    public void ClearBag()
    {
        if(BagItemDic.Count!=0)
        {
            Debug.Log(BagItemDic.Count);
            Debug.Log(bagItems.Length);
            for (int i = 0; i < bagItems.Length; i++)
            {
                if (bagItems[i].transform.childCount != 0)
                {
                   GameObject bagIcon= bagItems[i].transform.GetChild(0).gameObject;
                   string bagIconSpriteName = bagIcon.GetComponent<Image>().sprite.name;
                   DestorybagIconByName(bagIconSpriteName);
                }
            }
        }
    }

    //保存背包数据
    public void SaveBagData()
    {
        PlayerData.BAGNAMES.Clear();
        PlayerData.BAGCOUNT.Clear();
        for (int i = 0; i < bagItems.Length; i++)
        {
            if (bagItems[i].transform.childCount == 0)
            {
                PlayerData.BAGNAMES.Add("null");
                PlayerData.BAGCOUNT.Add("null");
                continue;
            }
            PlayerData.BAGNAMES.Add(bagItems[i].transform.GetChild(0).GetComponent<Image>().sprite.name);
            PlayerData.BAGCOUNT.Add(bagItems[i].transform.GetChild(0).GetChild(0).GetComponent<Text>().text);
        }
    }

    //加载背包数据
    public void LoadBagData()
    {
        BagItemDic.Clear();
        for (int i = 0; i < PlayerData.BAGNAMES.Count; i++)
        {
            if (PlayerData.BAGNAMES[i] == "null")
            {
                continue;
            }
            Object BagIcon = Resources.Load(bagItemIconPath);
            GameObject BagIconGo = GameObject.Instantiate(BagIcon, bagItems[i].transform.position, Quaternion.identity) as GameObject;
            BagIconGo.GetComponent<Image>().sprite = Resources.Load<Sprite>("shop/" + "equip/" + PlayerData.BAGNAMES[i]);
            BagIconGo.transform.GetChild(0).GetComponent<Text>().text = PlayerData.BAGCOUNT[i];
            BagIconGo.GetComponent<Transform>().SetParent(bagItems[i].transform);
            BagIconGo.transform.localScale = new Vector3(1, 1, 1);
            BagIconGo.transform.localPosition = Vector3.zero;
            BagItemDic.Add(PlayerData.BAGNAMES[i], BagIconGo);
        }
    }

    //通过名字删除背包里的物体
    public void DestorybagIconByName(string bagitemIconName)
    {
        GameObject Go = FindBagitemByName(bagitemIconName);
        Destroy(Go);
        BagItemDic.Remove(bagitemIconName);
        //Debug.Log(bagitemIconName + "移出背包字典");

    }

    //通过名字查找背包里的物体
    public GameObject FindBagitemByName(string bagitemName)
    {
        GameObject Go = null;
        BagItemDic.TryGetValue(bagitemName, out Go);

        return Go;
    }

    public void ShowPutOnEquipTips()
    {
        putOnEquipTips.SetActive(true);
        putOnEquipTips.GetComponent<DOTweenAnimation>().DORestart();
        putOnEquipTips.GetComponent<DOTweenAnimation>().DOPlay();
    }

    public void ShowNeedToPutOnEquipTips()
    {
        NeedToTakeOutTips.SetActive(true);
        NeedToTakeOutTips.GetComponent<DOTweenAnimation>().DORestart();
        NeedToTakeOutTips.GetComponent<DOTweenAnimation>().DOPlay();
    }

    public void PlayDrugSound()
    {
        putOnDrugSound.Play();
    }

    public void PlayEquipSound()
    {
        putOnEquipSound.Play();
    }

}
