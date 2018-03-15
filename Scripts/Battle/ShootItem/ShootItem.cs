using UnityEngine;
using System.Collections;

public class ShootItem : MonoBehaviour {

    [HideInInspector]
    public float damage;
    [HideInInspector]
    public Vector3 forceForward;

    public float flyPower;
    public float deleteTime = 5.0f;

    protected float time = 0;



	// Use this for initialization
	protected void Start () {
        GetComponent<Rigidbody>().AddForce(forceForward.normalized * flyPower);
	}
	
	// Update is called once per frame
    protected void Update()
    {
        time += Time.deltaTime;
        if (time > deleteTime)
        {
            Destroy(this.gameObject);
        }
	}

    //protected void OnTriggerEnter(Collider c)
    //{

    //}
}
