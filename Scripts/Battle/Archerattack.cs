using UnityEngine;
using System.Collections;

public class Archerattack : MonoBehaviour {
    public GameObject arrowPrefab;
    public GameObject ShootPosition;
    public float FlyPower;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

    }

    public void ShootArrow(GameObject gameObj)
    {
        GameObject g = Instantiate(arrowPrefab, ShootPosition.transform.position, transform.rotation)as GameObject;

        Vector3 toTarget = gameObj.transform.forward;
        g.GetComponent<Rigidbody>().AddForce(toTarget.normalized * FlyPower);
        g.GetComponent<ArrowDeal>().archer = gameObj;
    }
}
