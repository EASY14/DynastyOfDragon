using UnityEngine;
using System.Collections;

public class Arrow : ShootItem{

    private bool isAble = true;
	// Use this for initialization
	void Start () {
        base.Start();
	}
	
	// Update is called once per frame
	void Update () {
        base.Update();

	}

    protected void OnTriggerEnter(Collider c)
    {
        if (c.tag == "Player" || c.tag == "Ground")
        {
            transform.GetComponent<Rigidbody>().velocity = Vector3.zero;
            transform.GetComponent<Rigidbody>().isKinematic = true;
            if (isAble)
            {
                GetComponent<CapsuleCollider>().enabled = false;
                GetComponent<AudioSource>().Play();
                if (c.tag == "Player")
                {
                    c.gameObject.GetComponent<CharacterBehaviour>().BeHurt(damage);
                    if (c.transform.FindChild("Bip001") != null)
                    {
                        transform.parent = c.transform.FindChild("Bip001").transform.FindChild("Bip001 Pelvis");
                    }
                }

                isAble = false;
            }
        }
    }
}
