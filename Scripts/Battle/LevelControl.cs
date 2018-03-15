using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
public class LevelControl : MonoBehaviour {
    public List<PointController> listpointcontroller = new List<PointController>();
    public bool GameStop = false;
    public int StartLevel = 0;
    public int LastLevel = 0;
    public int BossLevel = 3;
    public GameObject bossposition;
    public bool IsBossCamare = false;
    public float bosstime = 2.0f;
    public bool IsBossLevel = false;
	// Use this for initialization

    private static LevelControl _instance;

    public static LevelControl Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindGameObjectWithTag(TagManager.levelcontrol).GetComponent<LevelControl>();
            }
            return _instance;
        }
    }

	void Start () {
        if(bossposition != null)
        {
            IsBossCamare = true;
        }
	}
	
	// Update is called once per frame
	void Update () {

        LevelClear();
        GameWin();
        LevelRun();
        
        
	}
    void LevelRun()
    {
        if (EnemyManager.Instance.GetAllEnemys().Count == 0 &&  GameStop != true)
        {
            StartLevel++;
            LevelSet(StartLevel);
            
        }
        else if (StartLevel == BossLevel && bossposition != null && IsBossCamare)
        {
            IsBossLevel = true;
            BossCameraFollow();
            GameObject.FindGameObjectWithTag("music").GetComponent<MusicManager>().BGMPlay(GameObject.FindGameObjectWithTag("music").GetComponent<MusicManager>().BossBGM);
        }
    }



    void LevelSet(int lv)
    {
        foreach(PointController pc in listpointcontroller)
        {
            if(pc.Level == lv)
            {
                pc.LevelEmeny();
            }
        }
    }

    void LevelClear()
    {
        if(EnemyManager.Instance.EnemyAliveNumber == 0)
        {
            EnemyManager.Instance.RemoveEnemy();
        }
        //EnemyManager.Instance.GetAllEnemys().Clear();
    }

    void GameWin()
    {
        if (StartLevel > LastLevel+1)
        {
            GameStop = true;
        }
    }

    void GameOver()
    {
    }

    void BossCameraFollow()
    {
        Vector3 forward = GameControl.Instance.Camera.TransformDirection(Vector3.forward);

        Vector3 toOther = bossposition.transform.position - GameControl.Instance.Camera.position;
        if (Vector3.Distance(bossposition.transform.position, GameControl.Instance.Camera.position) < 0.1f && Mathf.Acos(Vector3.Dot(forward, toOther)) < 5.0f)
        {
            //transform.do
            EnemyManager.Instance.BeginMove();
            GameControl.Instance.Camera.FindChild("Main Camera").GetComponent<FollowCamera>().followTurn = true;
            GameControl.Instance.Camera.FindChild("Main Camera").GetComponent<FollowCamera>().followRoll = true;
            GameControl.Instance.Camera.FindChild("Main Camera").GetComponent<FollowCamera>().SetTarget(GameControl.Instance.Player.gameObject);
            IsBossCamare = false;
        }
        else
        {
            EnemyManager.Instance.StopMove();
            GameControl.Instance.Camera.FindChild("Main Camera").GetComponent<FollowCamera>().followTurn = false;
            GameControl.Instance.Camera.FindChild("Main Camera").GetComponent<FollowCamera>().followRoll = false;
            GameControl.Instance.Camera.FindChild("Main Camera").GetComponent<FollowCamera>().SetTarget(bossposition);
        }
    }
}
