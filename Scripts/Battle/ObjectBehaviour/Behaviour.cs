using UnityEngine;
using System.Collections;
using System;


[Serializable]
public struct SkillData
{
    public float damageFactor;
    public float angle;
    public float range;
    public GameObject sound;
    public GameObject shootItem;
    public GameObject shootPosition;
}

public class Behaviour : MonoBehaviour {

    //动画控制类
    protected AnimationController animationCon;

	// Use this for initialization
	protected void Start () {
        animationCon = GetComponent<AnimationController>();

        if (animationCon == null)
            Debug.LogError("need Component CharacterAnimationController");
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
