using UnityEngine;
using System.Collections;
using DG.Tweening;
public class BeHitEffect : MonoBehaviour {
    private Transform myTransform;

	// Use this for initialization
	void Start () {
        myTransform = this.transform;
	}
	
	// Update is called once per frame
	void Update () {

	}
    
    public void ShowBeHitEffect()
    {
        myTransform.DOShakePosition(0.2f, 0.2f, 50, 90);
    }

    public void StrongHitEffect()
    {
        myTransform.DOShakePosition(0.2f, 3.0f, 100, 90);
    }
}
