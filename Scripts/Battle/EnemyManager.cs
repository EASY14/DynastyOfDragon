using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyManager : MonoBehaviour {
    private List<GameObject> AllEnemys = new List<GameObject>();
    //private List<GameObject> GruntEnemys = new List<GameObject>();
    //private List<GameObject> ArcherEnemys = new List<GameObject>();
    //private List<GameObject> MageEnemys = new List<GameObject>();
    //private List<GameObject> KingEnemys = new List<GameObject>();
    //private List<GameObject> WarriorEnemys = new List<GameObject>();
    private List<GameObject> DragonEnemys = new List<GameObject>();

    private static EnemyManager _instance;

    public int EnemyAliveNumber = 0;

    public static EnemyManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindGameObjectWithTag(TagManager.enemymanager).GetComponent<EnemyManager>();
            }
            return _instance;
        }
    }
    // Use this for initialization
    void Start () {
	}
	
	// Update is called once per frame
	void Update () {

	}

    public void AddGrunt(string colorname, Vector3 pos, float maxlife, float rotatespeed,float detectRange, Vector3[] attack_range_angle_damage, float attackspeed, float movespeed,int cyrstalnumber)
    {
        GameObject enemy = Instantiate(Resources.Load("Prefabs/Monster/Grunt_" + colorname), pos, Quaternion.identity) as GameObject;
        enemy.GetComponent<MoveMent>().InitEnemy(maxlife, rotatespeed, detectRange, attack_range_angle_damage,attackspeed,movespeed, cyrstalnumber);
        if (enemy == null)
        {
            Debug.LogError("Grunt资源加载失败");
            return;
        }
        AllEnemys.Add(enemy);
        //GruntEnemys.Add(enemy);
        EnemyAliveNumber++;
    }

    public void AddArcher(string colorname, Vector3 pos, float maxlife, float rotatespeed, float detectRange, Vector3[] attack_range_angle_damage, float attackspeed, float movespeed, int cyrstalnumber)
    {
        GameObject enemy = Instantiate(Resources.Load("Prefabs/Monster/Archer_" + colorname), pos, Quaternion.identity) as GameObject;
        enemy.GetComponent<MoveMent>().InitEnemy(maxlife, rotatespeed, detectRange, attack_range_angle_damage, attackspeed, movespeed, cyrstalnumber);
        if (enemy == null)
        {
            Debug.LogError("Archer资源加载失败");
            return;
        }
        AllEnemys.Add(enemy);
        //ArcherEnemys.Add(enemy);
        EnemyAliveNumber++;
    }

    public void AddMage(string colorname, Vector3 pos, float maxlife, float rotatespeed, float detectRange, Vector3[] attack_range_angle_damage, float attackspeed, float movespeed, int cyrstalnumber)
    {
        GameObject enemy = Instantiate(Resources.Load("Prefabs/Monster/Mage_" + colorname), pos, Quaternion.identity) as GameObject;
        enemy.GetComponent<MoveMent>().InitEnemy(maxlife, rotatespeed, detectRange, attack_range_angle_damage, attackspeed, movespeed, cyrstalnumber);
        if (enemy == null)
        {
            Debug.LogError("Mage资源加载失败");
            return;
        }
        AllEnemys.Add(enemy);
        //MageEnemys.Add(enemy);
        EnemyAliveNumber++;
    }

    public void AddKing(string colorname, Vector3 pos, float maxlife, float rotatespeed, float detectRange, Vector3[] attack_range_angle_damage, float attackspeed, float movespeed, int cyrstalnumber)
    {
        GameObject enemy = Instantiate(Resources.Load("Prefabs/Monster/King_" + colorname), pos, Quaternion.identity) as GameObject;
        enemy.GetComponent<MoveMent>().InitEnemy(maxlife, rotatespeed, detectRange, attack_range_angle_damage, attackspeed, movespeed, cyrstalnumber);
        if (enemy == null)
        {
            Debug.LogError("King资源加载失败");
            return;
        }
        AllEnemys.Add(enemy);
       //KingEnemys.Add(enemy);
        EnemyAliveNumber++;
    }

    public void AddWarrior(string colorname, Vector3 pos, float maxlife, float rotatespeed, float detectRange, Vector3[] attack_range_angle_damage, float attackspeed, float movespeed, int cyrstalnumber)
    {
        GameObject enemy = Instantiate(Resources.Load("Prefabs/Monster/Warrior_" + colorname), pos, Quaternion.identity) as GameObject;
        enemy.GetComponent<MoveMent>().InitEnemy(maxlife, rotatespeed, detectRange, attack_range_angle_damage, attackspeed, movespeed, cyrstalnumber);
        if (enemy == null)
        {
            Debug.LogError("Warrior资源加载失败");
            return;
        }
        AllEnemys.Add(enemy);
        //WarriorEnemys.Add(enemy);
        EnemyAliveNumber++;
    }

    public List<GameObject> GetAllEnemys()
    {
        return AllEnemys;
    }

    public GameObject GetDragon()
    {
        if (DragonEnemys.Count == 0)
            return null;
        return DragonEnemys[0];
    }

    public GameObject GetNearestEnemy(GameObject player)
    {
        GameObject nearestenemy = null; 
        float Newdistance = 1000.0f;
        foreach(GameObject enemy in AllEnemys)
        {
            if (enemy == null)
                continue;
            if (enemy.GetComponent<MoveMent>() != null)
            {
                if (enemy.GetComponent<MoveMent>().IsDeath)
                    continue; 
            }

            if (enemy.GetComponent<DragonsMoveMent>() != null)
            {
                if (enemy.GetComponent<DragonsMoveMent>().IsDeath)
                    continue;
            }

            float distance = (enemy.transform.localPosition - player.transform.localPosition).sqrMagnitude;
            if(distance<=Newdistance)
            {
                Newdistance = distance;
                nearestenemy = enemy;
            }
        }
        return nearestenemy;
    }

    public void RemoveEnemy()
    {
        foreach(GameObject ae in AllEnemys)
        {
            Destroy(ae);
        }


        AllEnemys.Clear();
    }

    public void StopMove()
    {
        foreach(GameObject ae in AllEnemys)
        {

            if (ae.GetComponent<MoveMent>() != null)
            {
                ae.GetComponent<MoveMent>().StopFollow();
            }

        }
    }
    public void BeginMove()
    {
        foreach (GameObject ae in AllEnemys)
        {
            if (ae.GetComponent<MoveMent>() != null)
            {
                ae.GetComponent<MoveMent>().BeginFollow();
            }
        }
    }

    public void SlowCamera()
    {
        foreach (GameObject ae in AllEnemys)
        {
            if (ae.GetComponent<MoveMent>() != null)
            {
                ae.GetComponent<MoveMent>().GetAnimator().speed = 0.2f;
            }
            if (ae.GetComponent<DragonsMoveMent>() != null)
            {
                ae.GetComponent<DragonsMoveMent>().myAnimator.speed = 0.2f;
            }
        }
        GameControl.Instance.Player.GetComponent<PlayerControl>().GetAnimator().speed = 0.2f;
    }

    public void EnemyDecrease()
    {
        EnemyAliveNumber--;
        if(EnemyAliveNumber == 0&&LevelControl.Instance.IsBossLevel)
        {
            VictoryAndDefeat.Instance.ShowVictory();
        }
    }

    public void AddDragon(string colorname, Vector3 pos, float maxlife, float rotatespeed, float detectRange, Vector3[] attack_range_angle_damage, float attackspeed, float movespeed, int cyrstalnumber)
    {
        GameObject enemy = Instantiate(Resources.Load("Prefabs/Monster/BattleDragonBoss"), pos, Quaternion.identity) as GameObject;
        //enemy.GetComponent<DragonsMoveMent>().InitEnemy(maxlife, rotatespeed, detectRange, attack_range_angle_damage, attackspeed, movespeed, cyrstalnumber);
        if (enemy == null)
        {
            Debug.LogError("Dragon资源加载失败");
            return;
        }
        AllEnemys.Add(enemy);
        //WarriorEnemys.Add(enemy);
        DragonEnemys.Add(enemy);
        EnemyAliveNumber++;
        //AllEnemys.Add(dragon);
        //EnemyAliveNumber++;
    }

    public void SaveData()
    {
        AllDataSava.Instance.SaveData(PlayerData.CharacterName);
    }
}
