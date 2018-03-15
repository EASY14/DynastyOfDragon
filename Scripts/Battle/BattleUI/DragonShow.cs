using UnityEngine;
using System.Collections;
using DG.Tweening;
public class DragonShow : MonoBehaviour 
{
    private AudioSource DragonFire;
    //private Transform myTransform;
    private Animator myAnimator;
    public DOTweenAnimation Shockanimation;


    public GameObject Spark01;
    public GameObject Swirl01;
    public GameObject Fire01;
	// Use this for initialization
	void Start () 
    {
        DragonFire = GetComponent<AudioSource>();
        //myTransform = this.transform;
        myAnimator = GetComponent<Animator>();
        StopEmit();
	}
	
	// Update is called once per frame
	void Update () 
    {

	}
    public void EndFly()
    {
        myAnimator.SetBool("idle",true);
    }

    public void Shock()
    {
        DragonFire.Play();
        StartEmit();
        Shockanimation.DOPlayForward();
    }

    public void ShutUp()
    {
        StopEmit();
    }
    public void ResetShock()
    {
        Shockanimation.DORestart();
        Shockanimation.DOPause();
    }

    void StartEmit()
    {
        Spark01.GetComponent<EllipsoidParticleEmitter>().emit = true;
        Swirl01.GetComponent<EllipsoidParticleEmitter>().emit = true;
        Fire01.GetComponent<EllipsoidParticleEmitter>().emit = true;
    }

    void StopEmit()
    {
        Spark01.GetComponent<EllipsoidParticleEmitter>().emit = false;
        Swirl01.GetComponent<EllipsoidParticleEmitter>().emit = false;
        Fire01.GetComponent<EllipsoidParticleEmitter>().emit = false;
    }
}
