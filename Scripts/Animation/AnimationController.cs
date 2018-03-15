using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Animator))]
public abstract class AnimationController : MonoBehaviour 
{
    
    protected Animator myAnimator;

	// Use this for initialization
	protected void Start () {
        myAnimator = gameObject.GetComponent<Animator>();
	}

    protected Animator GetAnimator()
    {
        if (myAnimator == null)
            return gameObject.GetComponent<Animator>();
        else
            return myAnimator;
    }

    public abstract void Attack(int attackType);

    public abstract void Injured();

    public abstract void Die();

    public abstract void Move();

    public abstract bool IsInjured();

    public abstract bool IsMoving();

    public abstract bool IsAttacking();

    public abstract bool IsDieing();
}
