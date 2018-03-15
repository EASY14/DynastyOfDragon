using UnityEngine;
using System.Collections;

public class CharacterAnimationController : AnimationController
{

    private float runDampTime = 0.1f;
    private float directionDampTime = 0.1f;
    private float horizontal = 0.0f;
    private float vertical = 0.0f;
    private float moveSpeed = 1.0f;
    private float attackSpeed = 1.0f;
    private int normalAttackDir = 0;
    private bool isWalk = false;

    private void Start()
    {
        base.Start();
    }

    private void Update()
    {

    }

    public void SetVertical(float v)
    {
        vertical = v;
    }

    public void SetHorizontal(float h)
    {
        horizontal = h;
    }

    public void SetMoveSpeed(float ms)
    {
        moveSpeed = ms;
    }

    public void SetAttackSpeed(float ats)
    {
        attackSpeed = ats;
    }

    public void Run()
    {
        isWalk = false;
    }

    public void Walk()
    {
        isWalk = true;
    }

    public void SetNormalAttackDir(int dir)
    {
        normalAttackDir = dir;
    }

    public override void Move()
    {
        if (myAnimator == null)
        {
            GetAnimator();
            if (myAnimator == null)
                return;
        }
        if (!isWalk)
        {
            myAnimator.SetFloat("vertical", vertical + ((moveSpeed - 1) * vertical), runDampTime, Time.deltaTime);
            myAnimator.SetFloat("horizontal", horizontal, runDampTime, Time.deltaTime);  
        }
        else
        {
            myAnimator.SetFloat("vertical", vertical/2, runDampTime, Time.deltaTime);
            myAnimator.SetFloat("horizontal", horizontal, runDampTime, Time.deltaTime);  
        }
    }


    public override void Attack(int attackType)
    {
        if (myAnimator == null)
        {
            GetAnimator();
            if (myAnimator == null)
                return;
        }
        if (IsAttacking() || IsSkill3())
            return;

        myAnimator.SetFloat("attackSpeed", attackSpeed);
        switch(attackType)
        {
            case 0:
                myAnimator.SetFloat("normalAttackDir", normalAttackDir);
                myAnimator.SetTrigger("normalAttack");
                break;
            case 1:
                myAnimator.SetTrigger("skillAttack1");
                break;

            case 2:
                myAnimator.SetTrigger("skillAttack2");
                break;

            case 3:
                myAnimator.SetTrigger("skillAttack3");
                break;
        }
    }

    public override void Injured()
    {
        if (myAnimator == null)
        {
            GetAnimator();
            if (myAnimator == null)
                return;
        }
        myAnimator.SetTrigger("injured");
    }

    public override void Die()
    {
        if (myAnimator == null)
        {
            GetAnimator();
            if (myAnimator == null)
                return;
        }
        myAnimator.SetTrigger("die");
    }

    public override bool IsInjured()
    {
        if (myAnimator == null)
        {
            GetAnimator();
            if (myAnimator == null)
                return false;
        }
        if (myAnimator.GetCurrentAnimatorStateInfo(1).IsTag("injured"))
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
        if (myAnimator.GetCurrentAnimatorStateInfo(0).IsTag("move"))
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

    public bool IsSkill3()
    {
        if (myAnimator == null)
        {
            GetAnimator();
            if (myAnimator == null)
                return false;
        }
        if (myAnimator.GetCurrentAnimatorStateInfo(0).IsTag("skill3"))
            return true;
        else
            return false;
    }
}
