using UnityEngine;
using System.Collections;
using DG.Tweening;
using UnityEngine.UI;
public class CameraControl : MonoBehaviour
{
    FollowCamera follow;

    public GameObject point0;
    public GameObject point1;
    public GameObject point2;
    public GameObject point3;
    public GameObject point4;

    public GameObject choose;
    public GameObject main;
    public GameObject end;
    public GameObject load;
    public GameObject fill;
    public GameObject tutorial;

    public GameObject text;
    public GameObject button;
    private bool isLoad;

    private Transform myT;

    //public GameObject selectPrince;
    //public GameObject selectAshe;
    //public GameObject selectMasterYi;

    public GameObject selectyi;

    private bool isChoosingHero;
    void Start()
    {

        follow = gameObject.GetComponent<FollowCamera>();
        follow.SetTarget(point0);
        isLoad = false;
        isChoosingHero = false;
    }

    void Update()
    {
        if (isLoad)
        {
            fill.GetComponent<Image>().fillAmount = AsyLoadScene.GetProgress();
            if (fill.GetComponent<Image>().fillAmount >= 1.0f)
            {
                text.GetComponent<Text>().text = "正在进入游戏";
                button.GetComponent<Button>().interactable = true;
                EnterNextScene();
            }
            else
            {
                text.GetComponent<Text>().text = "游戏正在载入";
                button.GetComponent<Button>().interactable = false;
            }
        }

        if (isChoosingHero)
            ClickHero();
    }

    public void EnterNextScene()
    {
        AsyLoadScene.EnterScene();
    }
    //显示主菜单
    public void ShowMenu()
    {
        follow.SetTarget(point1);
    }



    //开始游戏
    public void StartGame()
    {
        if (PlayerData.CharacterName == "")
            return;

        follow.SetTarget(point1);
        load.SetActive(true);
        load.GetComponent<DOTweenAnimation>().DOPlayForward();
        choose.GetComponent<DOTweenAnimation>().DOPlayBackwards();
        AsyLoadScene.sceneName = "MainCity";
        AsyLoadScene.LoadScene();
        isLoad = true;
        tutorial.SetActive(false);
    }

    // 选择英雄面板
    public void ChooseHero()
    {
        choose.SetActive(true);
        follow.SetTarget(point3);
        main.GetComponent<DOTweenAnimation>().DOPlayBackwards();
        choose.GetComponent<DOTweenAnimation>().DOPlayForward();
        isChoosingHero = true;

        tutorial.SetActive(true);
        tutorial.GetComponent<DOTweenAnimation>().DOPlayForward();
    }

    //public void StartBtn()
    //{
    //    PlayerData.CharacterName = "Jarvan";
    //    AllDataSava.Instance.LoadData("Jarvan");

    //    follow.SetTarget(point1);
    //    load.SetActive(true);
    //    load.GetComponent<DOTweenAnimation>().DOPlayForward();
    //    AsyLoadScene.sceneName = "Logo";
    //    AsyLoadScene.LoadScene();
    //    isLoad = true;
    //}

    //制作人面板
    public void Credits()
    {
        follow.SetTarget(point2);
    }

    //返回主菜单
    public void Back()
    {
        follow.SetTarget(point1);

        choose.GetComponent<DOTweenAnimation>().DOPlayBackwards();
        end.GetComponent<DOTweenAnimation>().DOPlayBackwards();
        main.GetComponent<DOTweenAnimation>().DOPlayForward();
        tutorial.GetComponent<DOTweenAnimation>().DOPlayBackwards();
        isChoosingHero = false;
    }

    //结束游戏面板
    public void EndGame()
    {
        follow.SetTarget(point4);
        end.SetActive(true);
        end.GetComponent<DOTweenAnimation>().DOPlayForward();
        main.GetComponent<DOTweenAnimation>().DOPlayBackwards();
    }

    //结束游戏
    public void End()
    {
        Application.Quit();
    }

    public void ClickHero()
    {
        if (Input.GetMouseButton(0))
        {

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 500))
            {
                if (hit.collider.gameObject != null)
                {
                    if (hit.collider.gameObject.tag == "Player")
                    {
                        if (hit.collider.gameObject.name == "yi")
                        {
                            selectyi.SetActive(true);
                            PlayerData.CharacterName = "Jarvan";
                            //Debug.Log("前："+PlayerData.POWERADD);
                            AllDataSava.Instance.LoadData("Jarvan");
                            //Debug.Log("后："+PlayerData.POWERADD);

                        }

                        //if (hit.collider.gameObject.name == "Jarvan")
                        //{
                        //    selectPrince.SetActive(true);
                        //    selectAshe.SetActive(false);
                        //    selectMasterYi.SetActive(false);
                        //    PlayerData.CharacterName = "Jarvan";
                        //    AllDataSava.Instance.LoadData("Jarvan");
                        //}
                        //if (hit.collider.gameObject.name == "Ashe")
                        //{
                        //    selectAshe.SetActive(true);
                        //    selectPrince.SetActive(false);
                        //    selectMasterYi.SetActive(false);
                        //    PlayerData.CharacterName = "Ashe";
                        //    AllDataSava.Instance.LoadData("Ashe");
                        //}
                        //if (hit.collider.gameObject.name == "MasterYi")
                        //{
                        //    selectAshe.SetActive(false);
                        //    selectPrince.SetActive(false);
                        //    selectMasterYi.SetActive(true);
                        //    PlayerData.CharacterName = "MasterYi";
                        //    AllDataSava.Instance.LoadData("MasterYi");
                        //}
                    }
                }
            }
        }
    }
}
