using UnityEngine;
using System.Collections;
using DG.Tweening;

public class ArrowDeal : MonoBehaviour {
    private DOTweenAnimation myTweenAnimation;
    private Transform myTransform;
    private bool isAble = true;
    private float time = 0;
    public float deleteTime = 5.0f;
    public GameObject archer;
	// Use this for initialization
	void Start () {
        myTransform = this.transform;
        myTweenAnimation = GetComponent<DOTweenAnimation>();
	}
	
	// Update is called once per frame
	void Update () {
        time += Time.deltaTime;
        if(time>deleteTime)
        {
            myTweenAnimation.DOPlayForward();
        }
	}

    public void DeleteThis()
    {
        Destroy(this.gameObject);
    }

    void OnTriggerEnter(Collider c)
    {
        if(c.tag == "Player")
        {
            myTransform.GetComponent<Rigidbody>().velocity = Vector3.zero;
            myTransform.GetComponent<Rigidbody>().isKinematic = true;
            myTransform.parent = c.transform.FindChild("root");
            
            if (c.transform.FindChild("root") == null)
            {
                myTransform.parent = c.transform.FindChild("Root");
            }
            if(isAble)
            {
                if (GameControl.Instance.Player.GetComponent<PlayerControl>().IsSpellLock)
                {
                    return;
                }
                archer.GetComponent<MoveMent>().TouchPlayer();
                GameControl.Instance.Camera.GetComponent<BeHitEffect>().ShowBeHitEffect();
                isAble = false;
                transform.GetComponent<CapsuleCollider>().enabled = false;
            }
        }
    }
}
