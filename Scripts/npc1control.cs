using UnityEngine;
using System.Collections;

public class npc1control : MonoBehaviour {

    private float npcaction;
    private Animator animator;
    private AnimatorStateInfo info;

	// Use this for initialization
	void Start () {
        animator = this.GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
        info = animator.GetCurrentAnimatorStateInfo(0);
        if (info.IsTag("actionTime"))
        {
            
            playit();
        }
        else
        {
            animator.SetFloat("action", 0);
        }
	}

    private void playit()
    {
        npcaction = animator.GetFloat("action");
        npcaction = Mathf.Lerp(npcaction, 1.0f, Time.deltaTime);
  
        animator.SetFloat("action", npcaction);

    }

}
