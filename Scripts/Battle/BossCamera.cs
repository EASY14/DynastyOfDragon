using UnityEngine;
using System.Collections;

public class BossCamera : MonoBehaviour {

    public float moveSpeed;
    public float rotateSpeed;
    public Transform stopPosition;
    public Transform lookAtTarget;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 toTarget = lookAtTarget.position - stopPosition.position;
        transform.position = Vector3.Lerp(transform.position, stopPosition.position, Time.deltaTime * moveSpeed);
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(toTarget.normalized, Vector3.up), Time.deltaTime * rotateSpeed);
	}
}
