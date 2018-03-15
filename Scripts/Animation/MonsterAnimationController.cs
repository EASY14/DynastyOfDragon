using UnityEngine;
using System.Collections;

public class MonsterAnimationController : AnimationController {

    private float runDampTime = 0.1f;
    private float directionDampTime = 0.1f;
    private float moveSpeed = 1.0f;
    private float attackSpeed = 1.0f;
    private bool isIdle = true;

    private void Start()
    {
        base.Start();
    }

    private void Update()
    {

    }

    public void SetMoveSpeed(float ms)
    {
        moveSpeed = ms;
    }

    public void SetAttackSpeed(float ats)
    {
        attackSpeed = ats;
    }

    public void ResumeSpeed()
    {
        if (myAnimator == null)
        {
            GetAnimator();
            if (myAnimator == null)
                return;
        }
        myAnimator.speed = 1.0f;
    }

    public void SetIsIdle(bool b)
    {
        isIdle = b;
    }

    public override void Move()
    {
        if (myAnimator == null)
        {
            GetAnimator();
            if (myAnimator == null)
                return;
        }
        myAnimator.speed = moveSpeed;
        if (isIdle)
            myAnimator.SetBool("run", false);
        else
            myAnimator.SetBool("run", true);
    }


    public override void Attack(int attackType)
    {
        if (myAnimator == null)
        {
            GetAnimator();
            if (myAnimator == null)
                return;
        }
        if (IsInjured())
            return;
        myAnimator.SetBool("run", false);
        myAnimator.speed = attackSpeed;
        myAnimator.SetFloat("attackidex", attackType);
        myAnimator.SetTrigger("attack");
    }

    public override void Injured()
    {
        if (myAnimator == null)
        {
            GetAnimator();
            if (myAnimator == null)
                return;
        }
        myAnimator.speed = 1.0f;
        myAnimator.SetTrigger("bedamage");
    }

    public override void Die()
    {
        if (myAnimator == null)
        {
            GetAnimator();
            if (myAnimator == null)
                return;
        }
        myAnimator.speed = 1.0f;
        myAnimator.SetFloat("damage", UnityEngine.Random.Range(0, 2));
        myAnimator.SetTrigger("death");
        myAnimator.ResetTrigger("bedamage");
    }

    public override bool IsInjured()
    {
        if (myAnimator == null)
        {
            GetAnimator();
            if (myAnimator == null)
                return false;
        }
        if (myAnimator.GetCurrentAnimatorStateInfo(0).IsTag("damage"))
            return true;
        else
            return false;
    }

    public override bool IsMoving()
    {
        if (myAnimator == null)
        {
            GetAnimator();
            if (myAnimator == null)
                return false;
        }
        if (myAnimator.GetCurrentAnimatorStateInfo(0).IsTag("run"))
            return true;
        else
            return false;
    }

    public override bool IsAttacking()
    {
        if (myAnimator == null)
        {
            GetAnimator();
            if (myAnimator == null)
                return false;
        }
        if (myAnimator.GetCurrentAnimatorStateInfo(0).IsTag("attack"))
            return true;
        else
            return false;
    }

    public override bool IsDieing()
    {
        if (myAnimator == null)
        {
            GetAnimator();
            if (myAnimator == null)
                return false;
        }
        if (myAnimator.GetCurrentAnimatorStateInfo(0).IsTag("death"))
            return true;
        else
            return false;
    }


}
