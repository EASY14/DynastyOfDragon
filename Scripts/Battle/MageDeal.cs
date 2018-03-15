using UnityEngine;
using System.Collections;

public class MageDeal : MonoBehaviour {
    private Transform myTransform;
    private ParticleSystem myPS;
    private float time = 0;
    public float deleteTime = 10.0f;
    private bool isAble = true;
    public GameObject Mage;
    // Use this for initialization
    void Start()
    {
        myTransform = this.transform;
        myPS = GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if (time > deleteTime)
        {
            if (this.gameObject != null)
            {
                Destroy(this.gameObject);
            }
        }
    }
    void OnTriggerEnter(Collider c)
    {
        if (c.tag == "Player" || c.tag == "Ground")
        {
            myTransform.FindChild("Ball").gameObject.SetActive(false);
            myTransform.FindChild("Point light").gameObject.SetActive(false);
            myPS.Play();
            if (isAble&&c.tag == "Player")
            {
                if (GameControl.Instance.Player.GetComponent<PlayerControl>().IsSpellLock)
                {
                    return;
                }
                Mage.GetComponent<MoveMent>().TouchPlayer();
                GameControl.Instance.Camera.GetComponent<BeHitEffect>().ShowBeHitEffect();
                isAble = false;
            }
        }
    }
}
