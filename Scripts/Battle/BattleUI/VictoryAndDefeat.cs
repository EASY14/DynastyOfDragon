using UnityEngine;
using System.Collections;
using DG.Tweening;
using UnityEngine.UI;
public class VictoryAndDefeat : MonoBehaviour {

    public enum Result
    {
        None,
        Victory,
        Defeat
    }
    private static VictoryAndDefeat _instance;
    public Result result = Result.None;
    public static VictoryAndDefeat Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindGameObjectWithTag(TagManager.victoryanddefeat).GetComponent<VictoryAndDefeat>();
            }
            return _instance;
        }
    }

    private DOTweenAnimation VictoryDeal;
    private DOTweenAnimation DefeatDeal;

    public Text DefeatCyrstalNum;
    public Text VictoryCyrstalNum;

	// Use this for initialization
	void Start () {
        VictoryDeal = transform.FindChild("VictoryLayout").GetComponent<DOTweenAnimation>();
        DefeatDeal = transform.FindChild("DefeatLayout").GetComponent<DOTweenAnimation>();
	}
	
	// Update is called once per frame
	void Update () {

	}

    public void ShowVictory()
    {
        VictoryDeal.DOPlayForward();
        GameControl.Instance.Player.GetComponent<PlayerControl>().IsControl = false;
        result = Result.Victory;
        //PlayerData.CRYSTALNUM += int.Parse(VictoryCyrstalNum.text);

    }

    public void ShowDefeat()
    {
        DefeatDeal.DOPlayForward();
        GameControl.Instance.Player.GetComponent<PlayerControl>().IsControl = false;
        //PlayerData.CRYSTALNUM += int.Parse(DefeatCyrstalNum.text);
        result = Result.Defeat;
    }
}
