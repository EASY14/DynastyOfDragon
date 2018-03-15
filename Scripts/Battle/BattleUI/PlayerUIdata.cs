using UnityEngine;
using System.Collections;
using DG.Tweening;
using UnityEngine.UI;

public class PlayerUIdata : MonoBehaviour {
    // Use this for initialization
    private Image lifepanel;
    private Text life_number;

    private Image power_inner;
    private Text power_number;

    private Text cyrstaltxt;
    private int cyrstalnum;

    private Text attackPowerText;
    private Text attackSpeedText;
    private Text moveSpeedText;

    private Image head;
    public Sprite Jarvan;
    public Sprite Ashe;
    public Sprite MasterYi;

    public GameObject ESC;
    void Start () {
        lifepanel = transform.FindChild("life").FindChild("lifepanel").GetComponent<Image>();
        life_number = transform.FindChild("life").FindChild("life_number").GetComponent<Text>();

        power_inner = transform.FindChild("power").FindChild("power_inner").GetComponent<Image>();
        power_number = transform.FindChild("power").FindChild("power_number").GetComponent<Text>();

        attackPowerText = transform.FindChild("AttackPower").FindChild("AttackPowerText").GetComponent<Text>();
        attackSpeedText = transform.FindChild("AttackSpeed").FindChild("AttackSpeedText").GetComponent<Text>();
        moveSpeedText = transform.FindChild("MoveSpeed").FindChild("MoveSpeedText").GetComponent<Text>();

        cyrstaltxt = transform.FindChild("Cyrstal").FindChild("cyrstalnum").GetComponent<Text>();
        cyrstalnum = 0;
        head = transform.FindChild("Head").FindChild("head").GetComponent<Image>();
        
        switch (PlayerData.CharacterName)
        {
            case "Jarvan":
                head.sprite = Jarvan;
                break;
            case "Ashe":
                head.sprite = Ashe;
                break;
            case "MasterYi":
                head.sprite = MasterYi;
                break;
        }
    }

	
	// Update is called once per frame
	void Update () {
        PlayerControl pc = GameControl.Instance.Player.GetComponent<PlayerControl>();
        lifepanel.fillAmount = pc.GetHpRate();
        life_number.text = ((int)(pc.GetHP())).ToString() + "/" + ((int)(pc.GetMaxHP())).ToString();
        power_inner.fillAmount = pc.GetPowerRate();
        power_number.text = ((int)(pc.GetPower())).ToString() + "/" + ((int)(pc.GetMaxPower())).ToString();

        attackPowerText.text = pc.GetATK().ToString();
        attackSpeedText.text = pc.GetAS().ToString();
        moveSpeedText.text = pc.GetMS().ToString();
        cyrstaltxt.text = "x"+cyrstalnum.ToString();

        if (Input.GetKeyUp(KeyCode.Escape))
        {
             OnButtonESC();
        }
    }
    
    public void OnButtonESC()
    {
        if (VictoryAndDefeat.Instance.result != VictoryAndDefeat.Result.None)
            return;
        if (Mathf.Abs(Time.timeScale - 0.0f) < 0.1f)
        {
            Time.timeScale = 1.0f;
            ESC.transform.FindChild("PauseLayout").GetComponent<DOTweenAnimation>().DOPlayBackwards();
        }
        else if (Mathf.Abs(Time.timeScale - 1.0f) < 0.1f)
        {
            ESC.transform.FindChild("PauseLayout").GetComponent<DOTweenAnimation>().DOPlayForward();
        }
        ESC.transform.FindChild("Button").GetComponent<CanvasGroup>().blocksRaycasts = !ESC.transform.FindChild("Button").GetComponent<CanvasGroup>().blocksRaycasts;
    }

    public void GamePause()
    {
       Time.timeScale = 0.0f;
    }

    public void AddBattleCyrstal(int num)
    {
        cyrstalnum += num;
        VictoryAndDefeat.Instance.DefeatCyrstalNum.text = cyrstalnum.ToString();
        VictoryAndDefeat.Instance.VictoryCyrstalNum.text = cyrstalnum.ToString();
    }
}
