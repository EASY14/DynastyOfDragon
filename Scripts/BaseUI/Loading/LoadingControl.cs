using UnityEngine;
using System.Collections;
using DG.Tweening;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class LoadingControl : MonoBehaviour
{
    public GameObject load;
    public GameObject fill;

    public GameObject text;

    private Color originalColor;

    public GameObject[] background;
	void Start () 
    {
        //background[Random.Range(0, 4)].SetActive(true);

        if (AsyLoadScene.sceneName == "MainCity")
        {
            background[5].SetActive(true);
        }
        else if (AsyLoadScene.sceneName == "MainMenu")
        {
            background[4].SetActive(true);
        }
        else
        {
            int index = PlayerData.CurrentLevelIndex % 4;
            if (index == -1)
            {
                background[0].SetActive(true);
            }
            else
            {
                background[index].SetActive(true);
            } 
        }
        
        AsyLoadScene.LoadScene();
	}
	
	void Update () 
    {
        fill.GetComponent<Image>().fillAmount = AsyLoadScene.GetProgress();
        if (fill.GetComponent<Image>().fillAmount >= 1.0f)
        {
            text.GetComponent<Text>().text = "正在进入游戏";
            fill.GetComponent<Button>().interactable = true;

            EnterNextScene();
        }
        else
        {
            text.GetComponent<Text>().text = "游戏正在载入";
            fill.GetComponent<Button>().interactable = false;
        }
	}

    public void EnterNextScene()
    {
        AsyLoadScene.EnterScene();
    }
}
