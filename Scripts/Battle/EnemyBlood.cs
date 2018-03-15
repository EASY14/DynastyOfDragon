using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class EnemyBlood : MonoBehaviour {
    public GameObject enemy;
	// Use this for initialization
	void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {

        gameObject.transform.rotation = Quaternion.LookRotation(-Camera.main.transform.forward, Vector3.up);
	}
    public void bloodChange(float percentage)
    {
        if (percentage > 0)
        {
            gameObject.GetComponent<Scrollbar>().size = percentage; 
        }
        else
        {
            gameObject.SetActive(false);
        }

    }
}
