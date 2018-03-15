using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using DG.Tweening;
public class DragonBloodUI : MonoBehaviour {

    public Text num;

    public Image blood;

    private GameObject dragon;

    private bool isShow;

	// Use this for initialization
	void Start () {
        isShow = false;
	}
	
	// Update is called once per frame
	void Update () {
	    if(!isShow)
        {
            dragon = EnemyManager.Instance.GetDragon();
            if (dragon != null)
            {
                GetComponent<DOTweenAnimation>().DOPlayForward();
                isShow = true;
            }
        }
        else
        {
            if (dragon == null)
                return;
            float a = dragon.GetComponent<DragonsMoveMent>().life/dragon.GetComponent<DragonsMoveMent>().Maxlife;
            if (a < 0.0f)
                a = 0;
            blood.fillAmount = a;
            num.text = (a*100).ToString() + "%";
        }
	}
}
