using UnityEngine;
using System.Collections;

public class NPCItem : MonoBehaviour {

    private NPCsManager NPCsManager;
	// Use this for initialization
	void Start () 
    {
        NPCsManager = GameObject.Find("NPCs").GetComponent <NPCsManager>();
        
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    void OnTriggerStay(Collider c)
    {
        if (c.tag == "Player")
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                NPCsManager.NPCsStayfunction(this.gameObject);
            }
        }
    }

     void OnTriggerExit(Collider c)
    {
                NPCsManager.NPCsExitfunction(this.gameObject);
    }

     void OnTriggerEnter(Collider other)
     {
         if(this.GetComponent<AudioSource>()!=null)
         {
             if (this.GetComponent<AudioSource>().isPlaying)
             {
                 return;
             }
             this.GetComponent<AudioSource>().Play();
         }
     }

}
