using UnityEngine;
using System.Collections;

public class AsheArrowDeal : MonoBehaviour {

    private Transform myTransform;
    private bool isAble = true;
    private float time = 0;
    public float deleteTime = 5.0f;
	// Use this for initialization
	void Start () {
        //enemy.GetComponent<MoveMent>().DecreaseLife(ATK * damagepoint);
        myTransform = this.transform;
	}
	
	// Update is called once per frame
	void Update () {
        time += Time.deltaTime;
        if(time>deleteTime)
        {
            Destroy(this.gameObject);
        }
	}

    void OnTriggerEnter(Collider c)
    {
        if (c.tag == "Enemy")
        {
            myTransform.GetComponent<Rigidbody>().velocity = Vector3.zero;
            myTransform.GetComponent<Rigidbody>().isKinematic = true;
            myTransform.parent = c.transform.FindChild("Character1_Reference");
            if (isAble)
            {
                GameControl.Instance.Player.GetComponent<PlayerControl>().ArrowAttackCheck(1.5f);
                isAble = false;
                transform.GetComponent<CapsuleCollider>().enabled = false;

            }
        }
    }

}
