using UnityEngine;
using System.Collections;
public class AllDataSava : MonoBehaviour {
    private static int CRYSTALNUM = 3000;
    private static int GRADE = 1;
    private static int EXP = 0;
    private static int MAXEXP = 0;
    private static float HP = 200.0f;
    private static float ATK = 5.0f;
    private static float AS = 1.0f;
    private static float MS = 1.0f;
    private static float POWERADD = 3.0f;
    private static int LEVEL = 0;//当前解锁到的关卡

    private static AllDataSava _instance;
    public static AllDataSava Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindGameObjectWithTag("AllDataSava").GetComponent<AllDataSava>();

            }
            return _instance;
        }
    }
    void OnDestroy()
    {
        SaveData(PlayerData.CharacterName);
    }
    public void SaveData(string PlayerName)
    {
                PlayerPrefs.SetInt("Jarvan_LEVEL", PlayerData.CurrentLevelIndex);
                PlayerPrefs.SetInt("Jarvan_CRYSTALNUM", PlayerData.CRYSTALNUM);
                PlayerPrefs.SetInt("Jarvan_TECHGRADE", PlayerData.GRADE);
                PlayerPrefs.SetInt("Jarvan_EXP", PlayerData.EXP);
                PlayerPrefs.SetInt("Jarvan_MAXEXP", PlayerData.MaxEXP);
                PlayerPrefs.SetFloat("Jarvan_HP", PlayerData.HP);
                PlayerPrefs.SetFloat("Jarvan_ATK", PlayerData.ATK);
                PlayerPrefs.SetFloat("Jarvan_AS", PlayerData.AS);
                PlayerPrefs.SetFloat("Jarvan_MS", PlayerData.MS);
                PlayerPrefs.SetFloat("Jarvan_POWERADD", PlayerData.POWERADD);


                PlayerPrefs.SetInt("Jarvan_HPDRUG_NUMBER", PlayerData.CurrentHpdrugNum);
                PlayerPrefs.SetInt("Jarvan_MPDRUG_NUMBER", PlayerData.CurrentMpdrugNum);

                PlayerPrefs.SetInt("Jarvan_BAGNAMES_NUMBER", PlayerData.BAGNAMES.Count);
                PlayerPrefs.SetInt("Jarvan_BAGCOUNT_NUMBER", PlayerData.BAGCOUNT.Count);
                PlayerPrefs.SetInt("Jarvan_EQUIPNAMES_NUMBER", PlayerData.EQUIPNAMES.Count);

                PlayerPrefs.SetInt("Jarvan_TECHGRADE_NUMBER", PlayerData.TECHGRADE.Count);

                PlayerPrefs.SetInt("Jarvan_UNLOCKLEVEL_NUMBER", PlayerData.UNLOCKLEVEL.Count);

                //三个技能的等级
                for (int i = 0; i < PlayerData.TECHGRADE.Count; i++)
                {
                    PlayerPrefs.SetInt("Jarvan_TECHGRADE" + i, PlayerData.TECHGRADE[i]);
                }
                //背包里所有的bagIcon.spriteName
                for (int i = 0; i < PlayerData.BAGNAMES.Count; i++)
                {
                    PlayerPrefs.SetString("Jarvan_BAGNAMES" + i, PlayerData.BAGNAMES[i]);
                }
                //该bagIcon的个数
                for (int i = 0; i < PlayerData.BAGCOUNT.Count; i++)
                {
                    PlayerPrefs.SetString("Jarvan_BAGCOUNT" + i, PlayerData.BAGCOUNT[i]);
                }
                //装备栏的装备
                for (int i = 0; i < PlayerData.EQUIPNAMES.Count; i++)
                {
                    PlayerPrefs.SetString("Jarvan_EQUIPNAMES" + i, PlayerData.EQUIPNAMES[i]);
                }
                for(int i=0;i<PlayerData.UNLOCKLEVEL.Count;i++)
                {
                    PlayerPrefs.SetInt("Jarvan_UNLOCKLEVEL"+i, PlayerData.UNLOCKLEVEL[i]);
                }

        /*switch (PlayerName)
        {
            case "Jarvan":
                PlayerPrefs.SetInt("Jarvan_CRYSTALNUM", PlayerData.CRYSTALNUM);
                PlayerPrefs.SetInt("Jarvan_TECHGRADE", PlayerData.GRADE);
                PlayerPrefs.SetFloat("Jarvan_EXP", PlayerData.EXP);
                PlayerPrefs.SetFloat("Jarvan_HP", PlayerData.HP);
                PlayerPrefs.SetFloat("Jarvan_ATK", PlayerData.ATK);
                PlayerPrefs.SetFloat("Jarvan_AS", PlayerData.AS);
                PlayerPrefs.SetFloat("Jarvan_MS", PlayerData.MS);
                PlayerPrefs.SetFloat("Jarvan_POWERADD", PlayerData.POWERADD);

                
                PlayerPrefs.SetInt("Jarvan_BAGNAMES_NUMBER", PlayerData.BAGNAMES.Count);
                PlayerPrefs.SetInt("Jarvan_BAGCOUNT_NUMBER", PlayerData.BAGCOUNT.Count);
                PlayerPrefs.SetInt("Jarvan_EQUIPNAMES_NUMBER", PlayerData.EQUIPNAMES.Count);

                PlayerPrefs.SetInt("Jarvan_TECHGRADE_NUMBER", PlayerData.TECHGRADE.Count);

                for (int i = 0; i < PlayerData.TECHGRADE.Count; i++)
                {
                    PlayerPrefs.SetInt("Jarvan_TECHGRADE" + i, PlayerData.TECHGRADE[i]);
                }

                for (int i = 0; i < PlayerData.BAGNAMES.Count; i++)
                {
                    PlayerPrefs.SetString("Jarvan_BAGNAMES" + i, PlayerData.BAGNAMES[i]);
                }
                for (int i = 0; i < PlayerData.BAGCOUNT.Count; i++)
                {
                    PlayerPrefs.SetString("Jarvan_BAGCOUNT" + i, PlayerData.BAGCOUNT[i]);
                }
                for (int i = 0; i < PlayerData.EQUIPNAMES.Count; i++)
                {
                    PlayerPrefs.SetString("Jarvan_EQUIPNAMES" + i, PlayerData.EQUIPNAMES[i]);
                }
                break;
            case "Ashe":
                PlayerPrefs.SetInt("Ashe_CRYSTALNUM", PlayerData.CRYSTALNUM);
                PlayerPrefs.SetInt("Ashe_TECHGRADE", PlayerData.GRADE);
                PlayerPrefs.SetFloat("Ashe_EXP", PlayerData.EXP);
                PlayerPrefs.SetFloat("Ashe_HP", PlayerData.HP);
                PlayerPrefs.SetFloat("Ashe_ATK", PlayerData.ATK);
                PlayerPrefs.SetFloat("Ashe_AS", PlayerData.AS);
                PlayerPrefs.SetFloat("Ashe_MS", PlayerData.MS);
                PlayerPrefs.SetFloat("Ashe_POWERADD", PlayerData.POWERADD);
                PlayerPrefs.SetInt("Ashe_BAGNAMES_NUMBER", PlayerData.BAGNAMES.Count);
                PlayerPrefs.SetInt("Ashe_BAGCOUNT_NUMBER", PlayerData.BAGCOUNT.Count);
                PlayerPrefs.SetInt("Ashe_EQUIPNAMES_NUMBER", PlayerData.EQUIPNAMES.Count);

                PlayerPrefs.SetInt("Ashe_TECHGRADE_NUMBER", PlayerData.TECHGRADE.Count);

                for (int i = 0; i < PlayerData.TECHGRADE.Count; i++)
                {
                    PlayerPrefs.SetInt("Ashe_TECHGRADE" + i, PlayerData.TECHGRADE[i]);
                }

                for (int i = 0; i < PlayerData.BAGNAMES.Count; i++)
                {
                    PlayerPrefs.SetString("Ashe_BAGNAMES" + i, PlayerData.BAGNAMES[i]);
                }
                for (int i = 0; i < PlayerData.BAGCOUNT.Count; i++)
                {
                    PlayerPrefs.SetString("Ashe_BAGCOUNT" + i, PlayerData.BAGCOUNT[i]);
                }
                for (int i = 0; i < PlayerData.EQUIPNAMES.Count; i++)
                {
                    PlayerPrefs.SetString("Ashe_EQUIPNAMES" + i, PlayerData.EQUIPNAMES[i]);
                }
                break;
            case "MasterYi":
                PlayerPrefs.SetInt("MasterYi_CRYSTALNUM", PlayerData.CRYSTALNUM);
                PlayerPrefs.SetInt("MasterYi_TECHGRADE", PlayerData.GRADE);
                PlayerPrefs.SetFloat("MasterYi_EXP", PlayerData.EXP);
                PlayerPrefs.SetFloat("MasterYi_HP", PlayerData.HP);
                PlayerPrefs.SetFloat("MasterYi_ATK", PlayerData.ATK);
                PlayerPrefs.SetFloat("MasterYi_AS", PlayerData.AS);
                PlayerPrefs.SetFloat("MasterYi_MS", PlayerData.MS);
                PlayerPrefs.SetFloat("MasterYi_POWERADD", PlayerData.POWERADD);
                PlayerPrefs.SetInt("MasterYi_BAGNAMES_NUMBER", PlayerData.BAGNAMES.Count);
                PlayerPrefs.SetInt("MasterYi_BAGCOUNT_NUMBER", PlayerData.BAGCOUNT.Count);
                PlayerPrefs.SetInt("MasterYi_EQUIPNAMES_NUMBER", PlayerData.EQUIPNAMES.Count);

                PlayerPrefs.SetInt("MasterYi_TECHGRADE_NUMBER", PlayerData.TECHGRADE.Count);

                for (int i = 0; i < PlayerData.TECHGRADE.Count; i++)
                {
                    PlayerPrefs.SetInt("MasterYi_TECHGRADE" + i, PlayerData.TECHGRADE[i]);
                }

                for (int i = 0; i < PlayerData.BAGNAMES.Count; i++)
                {
                    PlayerPrefs.SetString("MasterYi_BAGNAMES" + i, PlayerData.BAGNAMES[i]);
                }
                for (int i = 0; i < PlayerData.BAGCOUNT.Count; i++)
                {
                    PlayerPrefs.SetString("MasterYi_BAGCOUNT" + i, PlayerData.BAGCOUNT[i]);
                }
                for (int i = 0; i < PlayerData.EQUIPNAMES.Count; i++)
                {
                    PlayerPrefs.SetString("MasterYi_EQUIPNAMES" + i, PlayerData.EQUIPNAMES[i]);
                }
                break;
        }*/
    }

    public void LoadData(string PlayerName)
    {
         PlayerData.CurrentLevelIndex = PlayerPrefs.GetInt("Jarvan_LEVEL", LEVEL);
         PlayerData.CRYSTALNUM = PlayerPrefs.GetInt("Jarvan_CRYSTALNUM", CRYSTALNUM);
                    PlayerData.GRADE = PlayerPrefs.GetInt("Jarvan_TECHGRADE", GRADE);
                    PlayerData.EXP = PlayerPrefs.GetInt("Jarvan_EXP", EXP);
                    PlayerData.MaxEXP = PlayerPrefs.GetInt("Jarvan_MAXEXP",MAXEXP);
                    PlayerData.HP = PlayerPrefs.GetFloat("Jarvan_HP", HP);
                    PlayerData.ATK = PlayerPrefs.GetFloat("Jarvan_ATK", ATK);
                    PlayerData.AS = PlayerPrefs.GetFloat("Jarvan_AS", AS);
                    PlayerData.MS = PlayerPrefs.GetFloat("Jarvan_MS", MS);
                    PlayerData.POWERADD = PlayerPrefs.GetFloat("Jarvan_POWERADD", POWERADD);
                    int bagnames_number = PlayerPrefs.GetInt("Jarvan_BAGNAMES_NUMBER");
                    int bagcount_number = PlayerPrefs.GetInt("Jarvan_BAGCOUNT_NUMBER");
                    int equipnames_number = PlayerPrefs.GetInt("Jarvan_EQUIPNAMES_NUMBER");
                    int techgrade_number = PlayerPrefs.GetInt("Jarvan_TECHGRADE_NUMBER");
                    int unlocklevel_number = PlayerPrefs.GetInt("Jarvan_UNLOCKLEVEL_NUMBER");

                    PlayerData.BAGNAMES.Clear();
                    PlayerData.BAGCOUNT.Clear();
                    PlayerData.EQUIPNAMES.Clear();
                    PlayerData.TECHGRADE.Clear();
                    PlayerData.UNLOCKLEVEL.Clear();

                    for (int i = 0; i < unlocklevel_number; i++)
                    {
                        PlayerData.UNLOCKLEVEL.Add(PlayerPrefs.GetInt("Jarvan_UNLOCKLEVEL" + i));
                    }
                    for (int i = 0; i < techgrade_number; i++)
                    {
                        PlayerData.TECHGRADE.Add(PlayerPrefs.GetInt("Jarvan_TECHGRADE" + i));
                    }

                    for (int i = 0; i < bagnames_number; i++)
                    {
                        PlayerData.BAGNAMES.Add(PlayerPrefs.GetString("Jarvan_BAGNAMES" + i));
                    }
                    for (int i = 0; i < bagcount_number; i++)
                    {
                        PlayerData.BAGCOUNT.Add(PlayerPrefs.GetString("Jarvan_BAGCOUNT" + i));
                    }
                    for (int i = 0; i < equipnames_number; i++)
                    {
                        PlayerData.EQUIPNAMES.Add(PlayerPrefs.GetString("Jarvan_EQUIPNAMES" + i));
                    }
                
        /*
        switch (PlayerName)
        {
            case "Jarvan":
                {
                    PlayerData.CRYSTALNUM = PlayerPrefs.GetInt("Jarvan_CRYSTALNUM", CRYSTALNUM);
                    PlayerData.GRADE = PlayerPrefs.GetInt("Jarvan_TECHGRADE", GRADE);
                    PlayerData.HP = PlayerPrefs.GetFloat("Jarvan_EXP", EXP);
                    PlayerData.HP = PlayerPrefs.GetFloat("Jarvan_HP", HP);
                    PlayerData.ATK = PlayerPrefs.GetFloat("Jarvan_ATK", ATK);
                    PlayerData.AS = PlayerPrefs.GetFloat("Jarvan_AS", AS);
                    PlayerData.MS = PlayerPrefs.GetFloat("Jarvan_MS", MS);
                    PlayerData.POWERADD = PlayerPrefs.GetInt("Jarvan_POWERADD", POWERADD);
                    int bagnames_number = PlayerPrefs.GetInt("Jarvan_BAGNAMES_NUMBER");
                    int bagcount_number = PlayerPrefs.GetInt("Jarvan_BAGCOUNT_NUMBER");
                    int equipnames_number = PlayerPrefs.GetInt("Jarvan_EQUIPNAMES_NUMBER");
                    PlayerData.BAGNAMES.Clear();
                    PlayerData.BAGCOUNT.Clear();
                    PlayerData.EQUIPNAMES.Clear();

                    int techgrade_number = PlayerPrefs.GetInt("Jarvan_TECHGRADE_NUMBER");
                    PlayerData.TECHGRADE.Clear();
                    for (int i = 0; i < techgrade_number; i++)
                    {
                        PlayerData.TECHGRADE.Add(PlayerPrefs.GetInt("Jarvan_TECHGRADE" + i));
                    }

                    for (int i = 0; i < bagnames_number; i++)
                    {
                        PlayerData.BAGNAMES.Add(PlayerPrefs.GetString("Jarvan_BAGNAMES" + i));
                    }
                    for (int i = 0; i < bagcount_number; i++)
                    {
                        PlayerData.BAGCOUNT.Add(PlayerPrefs.GetString("Jarvan_BAGCOUNT" + i));
                    }
                    for (int i = 0; i < equipnames_number; i++)
                    {
                        PlayerData.EQUIPNAMES.Add(PlayerPrefs.GetString("Jarvan_EQUIPNAMES" + i));
                    }
                }
                break;
            case "Ashe":
                {
                    PlayerData.CRYSTALNUM = PlayerPrefs.GetInt("Ashe_CRYSTALNUM", CRYSTALNUM);
                    PlayerData.GRADE = PlayerPrefs.GetInt("Ashe_TECHGRADE", GRADE);
                    PlayerData.HP = PlayerPrefs.GetFloat("Ashe_EXP", EXP);
                    PlayerData.HP = PlayerPrefs.GetFloat("Ashe_HP", HP);
                    PlayerData.ATK = PlayerPrefs.GetFloat("Ashe_ATK", ATK);
                    PlayerData.AS = PlayerPrefs.GetFloat("Ashe_AS", AS);
                    PlayerData.MS = PlayerPrefs.GetFloat("Ashe_MS", MS);
                    PlayerData.POWERADD = PlayerPrefs.GetInt("Ashe_POWERADD", POWERADD);
                    int bagnames_number = PlayerPrefs.GetInt("Ashe_BAGNAMES_NUMBER");
                    int bagcount_number = PlayerPrefs.GetInt("Ashe_BAGCOUNT_NUMBER");
                    int equipnames_number = PlayerPrefs.GetInt("Ashe_EQUIPNAMES_NUMBER");
                    PlayerData.BAGNAMES.Clear();
                    PlayerData.BAGCOUNT.Clear();
                    PlayerData.EQUIPNAMES.Clear();

                    int techgrade_number = PlayerPrefs.GetInt("Ashe_TECHGRADE_NUMBER");
                    PlayerData.TECHGRADE.Clear();
                    for (int i = 0; i < techgrade_number; i++)
                    {
                        PlayerData.TECHGRADE.Add(PlayerPrefs.GetInt("Ashe_TECHGRADE" + i));
                    }

                    for (int i = 0; i < bagnames_number; i++)
                    {
                        PlayerData.BAGNAMES.Add(PlayerPrefs.GetString("Ashe_BAGNAMES" + i));
                    }
                    for (int i = 0; i < bagcount_number; i++)
                    {
                        PlayerData.BAGCOUNT.Add(PlayerPrefs.GetString("Ashe_BAGCOUNT" + i));
                    }
                    for (int i = 0; i < equipnames_number; i++)
                    {
                        PlayerData.EQUIPNAMES.Add(PlayerPrefs.GetString("Ashe_EQUIPNAMES" + i));
                    }
                }
                break;
            case "MasterYi":
                {
                    PlayerData.CRYSTALNUM = PlayerPrefs.GetInt("MasterYi_CRYSTALNUM", CRYSTALNUM);
                    PlayerData.GRADE = PlayerPrefs.GetInt("MasterYi_TECHGRADE", GRADE);
                    PlayerData.HP = PlayerPrefs.GetFloat("MasterYi_EXP", EXP);
                    PlayerData.HP = PlayerPrefs.GetFloat("MasterYi_HP", HP);
                    PlayerData.ATK = PlayerPrefs.GetFloat("MasterYi_ATK", ATK);
                    PlayerData.AS = PlayerPrefs.GetFloat("MasterYi_AS", AS);
                    PlayerData.MS = PlayerPrefs.GetFloat("MasterYi_MS", MS);
                    PlayerData.POWERADD = PlayerPrefs.GetInt("MasterYi_POWERADD", POWERADD);
                    int bagnames_number = PlayerPrefs.GetInt("MasterYi_BAGNAMES_NUMBER");
                    int bagcount_number = PlayerPrefs.GetInt("MasterYi_BAGCOUNT_NUMBER");
                    int equipnames_number = PlayerPrefs.GetInt("MasterYi_EQUIPNAMES_NUMBER");
                    PlayerData.BAGNAMES.Clear();
                    PlayerData.BAGCOUNT.Clear();
                    PlayerData.EQUIPNAMES.Clear();

                    int techgrade_number = PlayerPrefs.GetInt("MasterYi_TECHGRADE_NUMBER");
                    PlayerData.TECHGRADE.Clear();
                    for (int i = 0; i < techgrade_number; i++)
                    {
                        PlayerData.TECHGRADE.Add(PlayerPrefs.GetInt("MasterYi_TECHGRADE" + i));
                    }

                    for (int i = 0; i < bagnames_number; i++)
                    {
                        PlayerData.BAGNAMES.Add(PlayerPrefs.GetString("MasterYi_BAGNAMES" + i));
                    }
                    for (int i = 0; i < bagcount_number; i++)
                    {
                        PlayerData.BAGCOUNT.Add(PlayerPrefs.GetString("MasterYi_BAGCOUNT" + i));
                    }
                    for (int i = 0; i < equipnames_number; i++)
                    {
                        PlayerData.EQUIPNAMES.Add(PlayerPrefs.GetString("MasterYi_EQUIPNAMES" + i));
                    }
                }
                break;
        }*/
    }

}
