using UnityEngine;
using System.Collections;

public class mageBall : ShootItem {

    private ParticleSystem myPS;
    private bool isAble = true;
	// Use this for initialization
	void Start () {
        base.Start();
        myPS = GetComponent<ParticleSystem>();
        transform.Find("Ball").GetComponent<ParticleSystem>().Play();
        transform.Find("Point light").GetComponent<ParticleSystem>().Play();
	}
	
	// Update is called once per frame
	void Update () {
        base.Update();

	}

    void OnTriggerEnter(Collider c)
    {
        if (isAble && c.tag == "Player")
        {
            myPS.Play();
            transform.FindChild("Ball").gameObject.SetActive(false);
            transform.FindChild("Point light").gameObject.SetActive(false);
            c.gameObject.GetComponent<CharacterBehaviour>().BeHurt(damage);
            isAble = false;
            GetComponent<SphereCollider>().enabled = false;
            GetComponent<AudioSource>().Play();
        }
        
    }
}
