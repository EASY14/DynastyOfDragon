using UnityEngine;
using System.Collections;

public class NPCsManager : MonoBehaviour {

    private Shop Shop;
    private PlayEquipInfo PlayEquipInfo;
    private ResetPanelManager ResetPanelManager;

	// Use this for initialization
    void Start()
    {
        PlayEquipInfo = GameObject.Find("BagSystem").transform.FindChild("PlayEquipInfo").GetComponent<PlayEquipInfo>();
        Shop = GameObject.Find("BagSystem").GetComponent<Shop>();
        ResetPanelManager = GameObject.Find("BagSystem").transform.FindChild("ResetPanel").GetComponent<ResetPanelManager>();
    }

   public void NPCsStayfunction(GameObject currentnpc)
    {
        //Debug.Log(currentnpc.name);
       switch(currentnpc.name)
       {
           case "Shop_NPC":
               Shop.ShoppingBagBtnOnClickShow();
               GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterBehaviour>().ShowCursor();
                
               break;
           case "Choice_NPC":
               Shop.BattleBtnOnClickShow();
               GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterBehaviour>().ShowCursor();
               break;
           case "Tech_NPC":
               PlayEquipInfo.ClickGoTechniqueUpgrade();
               //Debug.Log("tech");
               GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterBehaviour>().ShowCursor();
               break;
           case "ResetPlayer_NPC":
               ResetPanelManager.TweenPlay();
               GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterBehaviour>().ShowCursor();

               
               break;
           case "Bag_NPC":
               Shop.BagBtnOnClickShow();
               GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterBehaviour>().ShowCursor();
               break;
           
       }
    }

   public void NPCsExitfunction(GameObject currentnpc)
   {
       Shop.AllHide();
       PlayEquipInfo.TechniquepanelTweenBack();
       ResetPanelManager.TweenBack();
   }


	
}
