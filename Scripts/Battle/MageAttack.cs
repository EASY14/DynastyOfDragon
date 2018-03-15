using UnityEngine;
using System.Collections;

public class MageAttack : MonoBehaviour
{
    public GameObject magePrefab;
    public GameObject ShootPosition;
    public float FlyPower;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ShootMage(GameObject gameObj)
    {
        GameObject g = Instantiate(magePrefab, ShootPosition.transform.position, transform.rotation) as GameObject;

        Vector3 toTarget = gameObj.transform.forward;
        g.GetComponent<Rigidbody>().AddForce(toTarget.normalized * FlyPower);
        g.GetComponent<MageDeal>().Mage = gameObj;
    }
}
