using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MainCityData : MonoBehaviour {
    private Text cyrstalnum;
    
    // Use this for initialization
    void Start () {
        cyrstalnum = transform.FindChild("Data").FindChild("Crystal").FindChild("Text").GetComponent<Text>();
       
    }
	
	// Update is called once per frame
	void Update () {
        cyrstalnum.text = "x" + PlayerData.CRYSTALNUM.ToString();
    }
}
