using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityStandardAssets.Cameras;
public class BattleManager : MonoBehaviour
{
    public GameObject GameBGM;
    public GameObject BossBGM;
    public GameObject VictoryBGM;
    public GameObject DefeatedBGM;

    public float changeLevelTime = 0.0f;
    private float changeTime = 0.0f;

    public Transform bossCameraStopPos;
    public Transform bossCameraLookAtTarget;
    public float lookBossTime = 0.0f;
    private float bossTime = 0.0f;

    public MonsterBehaviour boss;
    public float slowMotion = 0.0f;
    private float slowTime = 0.0f;

    public GameObject[] AllEnemy;

    private bool GameStop = false;
    public int currentLevel = 0;
    // Use this for initialization

    private static BattleManager _instance;

    public static BattleManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindGameObjectWithTag(TagManager.battlemanager).GetComponent<BattleManager>();
            }
            return _instance;
        }
    }

    void Start()
    {
        changeTime = 0.0f;
        if (GameBGM != null)
            GameBGM.GetComponent<AudioSource>().Play(); 
    }

    // Update is called once per frame
    void Update()
    {
        LevelRun();
    }
    void LevelRun()
    {
        if (currentLevel == AllEnemy.Length)
            return;

        ShowEnemy();

        BossLevel();

    }



    void ShowEnemy()
    {
        if (currentLevel < AllEnemy.Length && currentLevel >= 0)
        {
            if (!AllEnemy[currentLevel].activeInHierarchy)
            {
                changeTime += Time.deltaTime;
                if (changeTime > changeLevelTime)
                {
                    changeTime = 0.0f;
                    AllEnemy[currentLevel].SetActive(true);
                }
            }
            else if (AllEnemy[currentLevel].transform.childCount == 0)
            {
                changeTime += Time.deltaTime;
                if (changeTime > changeLevelTime)
                {
                    changeTime = 0.0f;
                    currentLevel++;
                    if (currentLevel < AllEnemy.Length)
                        AllEnemy[currentLevel].SetActive(true);
                }
            }
        }
    }

    void BossLevel()
    {
        if (currentLevel == (AllEnemy.Length - 1))
        {
            bossTime += Time.deltaTime;
            if (bossTime < lookBossTime)
                BossCameraFollow();
            else
                ResumeCamera();

            if (GameBGM != null && GameBGM.GetComponent<AudioSource>().isPlaying)
                GameBGM.GetComponent<AudioSource>().Stop();
            if (BossBGM != null && !BossBGM.GetComponent<AudioSource>().isPlaying)
                BossBGM.GetComponent<AudioSource>().Play();
        }
        else if (currentLevel == AllEnemy.Length)
        {
            GameVictory();
        }

        SlowCam();

    }

    void SlowCam()
    {
        if (boss!=null)
        {
            if (boss.GetState() == MovetionState.DEAD)
            {
                slowTime += Time.deltaTime / Time.timeScale;
                if (slowTime > slowMotion)
                {
                    Time.timeScale = 1.0f;
                }
                else
                {
                    Time.timeScale = 0.1f;
                }
            } 
        }
    }
    void BossCameraFollow()
    {
        Transform camera =  Camera.main.transform;
        camera.parent.parent.GetComponent<FreeLookCam>().enabled = false;
        camera.parent.parent.GetComponent<ProtectCameraFromWallClip>().enabled = false;
        camera.parent.parent.GetComponent<BossCamera>().enabled = true;
        camera.parent.parent.GetComponent<BossCamera>().stopPosition = bossCameraStopPos;
        camera.parent.parent.GetComponent<BossCamera>().lookAtTarget = bossCameraLookAtTarget;
        GameObject.FindGameObjectWithTag(TagManager.game).GetComponent<GameControl>().EnemyReactTime = lookBossTime;
    }

    void ResumeCamera()
    {
        Transform camera = Camera.main.transform;
        camera.parent.parent.GetComponent<FreeLookCam>().enabled = true;
        camera.parent.parent.GetComponent<ProtectCameraFromWallClip>().enabled = true;
        camera.parent.parent.GetComponent<BossCamera>().enabled = false;
    }

    public GameObject GetCurrentLevelEnemy()
    {
        if (AllEnemy.Length > 0 && currentLevel < AllEnemy.Length)
            return AllEnemy[currentLevel]; 
        else
            return null;
    }

    public bool IsBattleFinish()
    {
        return (currentLevel == AllEnemy.Length) ? true : false;
    }

    public void GameVictory()
    {
        BattleUIController.Instance.GameVictory();
        GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterBehaviour>().ShowCursor();
        if (GameBGM != null)
            GameBGM.GetComponent<AudioSource>().Stop();
        if (BossBGM != null)
            BossBGM.GetComponent<AudioSource>().Stop();
        if (VictoryBGM != null)
                VictoryBGM.GetComponent<AudioSource>().Play();
    }

    public void GameDefeated()
    {
        BattleUIController.Instance.GameDefeated();
        GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterBehaviour>().ShowCursor();
        if (GameBGM != null)
            GameBGM.GetComponent<AudioSource>().Stop();
        if (BossBGM != null)
            BossBGM.GetComponent<AudioSource>().Stop();
        if (DefeatedBGM != null)
                DefeatedBGM.GetComponent<AudioSource>().Play();
    }

}
