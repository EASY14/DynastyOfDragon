using UnityEngine;
using System.Collections;

public class PlayerAnimatorControl : MonoBehaviour {

    private CharacterController charaContro;
    private Animator animator;

    private float lerpmove = 0;
    private float lerpSkillAll = 0;
    private float lerpSkill12 = 0;
    private bool b_secStop = false;
    private bool b_canMove = false;
	// Use this for initialization
	void Start () {
        charaContro = this.GetComponent<CharacterController>();
        animator = this.GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
        Move();
        if(b_secStop)
        {
          //  Debug.Log(b_secStop);
            ShowSkillSecondStep();
        }
	}

    public void setCanMove()
    {
        b_canMove = true;
    }
    private void Move()
    {
        if(b_canMove)
        {
            lerpmove = Mathf.Lerp(lerpmove, 1, 0.05f + Time.deltaTime * Time.deltaTime);

            animator.SetFloat("walk", lerpmove);
        }
        
        
    }

    public void ShowSkillFirstStep()
    {
        animator.SetBool("skillBool", true);
        lerpSkillAll = 0;
        animator.SetFloat("skillAll", lerpSkillAll);
        lerpSkill12 = 0;
        animator.SetFloat("skill12", lerpSkill12);

        Invoke("setShowSkillSecondStep", 3.85f);

        
    }
    public void playsound()
    {
        AudioSource auds=this.GetComponent<AudioSource>();
        if(auds!=null)
        {
            auds.Play();
        }
        else
        {
            return;
        }
    }
    public void setShowSkillSecondStep()
    {
        b_secStop = true;
       
    }
    public void ShowSkillSecondStep()
    {
        //lerpSkillAll = Mathf.Lerp(lerpSkillAll, 1, Time.deltaTime);
        //if (lerpSkillAll >0.9)
        //{
          //  Debug.Log("开始");
            lerpSkillAll = 1;
            animator.SetFloat("skillAll", lerpSkillAll);
            lerpSkill12 = 1;
            animator.SetFloat("skill12", lerpSkill12);
            Invoke("stopShowSkillSecondStep", 1.50f);
        //}
        
    }

    public void stopShowSkillSecondStep()
    {
        b_secStop = false;
        animator.SetBool("skillBool", false);

    }
    
    public void PlaySkillSound()
    {

    }
    public void NormalCloseAttack()
    {

    }
    public void ResetAttack()
    {

    }
    public void DistantAttack()
    {

    }
    public void CloseAttack()
    {

    }
    public void ShowEffect()
    {

    }
}
