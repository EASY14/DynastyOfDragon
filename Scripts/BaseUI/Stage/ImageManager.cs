using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class ImageManager : MonoBehaviour 
{
    public GameObject StoryText1;
    public GameObject StoryText2;
    public GameObject StoryText3;

    private string story1 = "恶龙现世，亡灵天灾   庙小妖风大，池浅王八多   一群人千里送人头。";

    private string story2 = "人们为了去寻龙而走上了充满战斗和希望的道路。";

    private string story3 = "战士与龙的战斗永世不止，开启了一场大屠龙时代。 ";

    public GameObject one;
    public GameObject two;
    public GameObject three;

    public GameObject BG;
	// Use this for initialization
	void Start () 
    {
        StoryText1.GetComponent<DOTweenAnimation>().endValueString = story1;
        ShowText(StoryText1);
        StoryText2.GetComponent<DOTweenAnimation>().endValueString = story2;
        //ShowText(StoryText2);
        StoryText3.GetComponent<DOTweenAnimation>().endValueString = story3;
        //ShowText(StoryText3);

        StoryText1.SetActive(true);
        StoryText2.SetActive(false);
        StoryText3.SetActive(false);

        one.SetActive(false);
        two.SetActive(false);
        three.SetActive(false);

        BG.SetActive(true);
	}
	
	// Update is called once per frame
	void Update () 
    {
	    if(Input.GetKeyDown(KeyCode.Escape))
        {
            EndThree();
        }
	}

    public void EndOne()
    {
        one.SetActive(false);
        BG.SetActive(true);
        StoryText2.SetActive(true);
        ShowText(StoryText2);
    }

    public void EndTwo()
    {
        two.SetActive(false);
        BG.SetActive(true);
        StoryText3.SetActive(true);
        ShowText(StoryText3);
    }

    public void EndThree()
    {
        BG.SetActive(true);
        three.SetActive(false);
        GameObject.Find("changeScene").GetComponent<ChangeScene>().NextScene();
    }

    public void ShowText(GameObject story1)
    {
        story1.GetComponent<DOTweenAnimation>().tween.Rewind();
        story1.GetComponent<DOTweenAnimation>().tween.Kill();
        if (story1.GetComponent<DOTweenAnimation>().isValid)
        {
            story1.GetComponent<DOTweenAnimation>().CreateTween();
            story1.GetComponent<DOTweenAnimation>().tween.Play();
        }
    }

    public void ToOne()
    {
        BG.SetActive(false);
        one.SetActive(true);
        StoryText1.SetActive(false);
    }

    public void ToTwo()
    {
        BG.SetActive(false);
        two.SetActive(true);
        StoryText2.SetActive(false);
    }

    public void ToThree()
    {
        BG.SetActive(false);
        three.SetActive(true);
        StoryText3.SetActive(false);
        
    }
}

