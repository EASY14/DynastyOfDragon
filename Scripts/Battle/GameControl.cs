using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class GameControl : MonoBehaviour {

    public float EnemyDeleteTime = 5.0f;
    public float EnemyReactTime = 1.5f;
	private static GameControl _instance;

	public static GameControl Instance{
		get{ 
			if(_instance==null){
				_instance = GameObject.FindGameObjectWithTag (TagManager.game).GetComponent<GameControl> ();
			}
			return _instance;
		}
	}

	private GameControl(){
		
	}

	private Transform player;

	public Transform Player{
		get{ 
			if(player==null){
				player = GameObject.FindGameObjectWithTag (TagManager.player).transform;
			}
			return player;
		}
	}

    private Transform mycamera;
    
    public Transform Camera
    {
        get
        {
            if(mycamera == null)
            {
                mycamera = GameObject.FindGameObjectWithTag(TagManager.camera).transform;
            }
            return mycamera;
        }
    } 

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
        
	}

}
