using UnityEngine;
using System.Collections;
using System;
using UnityEngine.Events;
using UnityEngine.UI;
using DG.Tweening;
//using UnityEditor;

[Serializable]
public class CharacterEvent : UnityEvent<GameObject>
{

}



[RequireComponent(typeof(NavMeshAgent))]
public class MoveMent : MonoBehaviour
{

    //提示
    [SerializeField]
    private bool showInfo = true;

    //侦测颜色
    [SerializeField]
    private Color detectColor = Color.blue;

    //攻击颜色
    [SerializeField]
    private Color attackColor = Color.red;

    //tag
    //[SerializeField]
    //private string targetTag = "Player";

    //攻击目标
    [SerializeField]
    private GameObject target;

    //最大生命
    [SerializeField]
    public float Maxlife;

    //生命
    [HideInInspector]
    public float life;

    public bool IsDeath = false;

    //检测范围
    [SerializeField]
    private float detectRange = 0;

    private float predetectRange = 0;

    //转身速度
    [SerializeField]
    public float rotateSpeed = 1;

    //攻击速度
    public float attackSpeed = 1;

    //
    public int cyrstalNumber = 10;

    //技能列表
    public Vector3[] Range_Angle_Damage;

    //当前技能
    [SerializeField]
    public int curentSkill = 0;

    [SerializeField]
    //站岗时候的事件
    public CharacterEvent OnIdle;

    [SerializeField]
    //追踪时候的事件
    public CharacterEvent OnTrace;

    [SerializeField]
    //攻击开始时候的事件
    public CharacterEvent OnBeginAttack;

    [SerializeField]
    //攻击结束时候的事件
    public CharacterEvent OnEngAttack;

    [SerializeField]
    //接触时候的事件
    public CharacterEvent OnTouch;

    [SerializeField]
    //死亡时候的事件
    public CharacterEvent OnDie;

    //怪物
    private Transform characterTransform;

    //导航
    private NavMeshAgent agent;

    //是否检测到
    private bool isDetected;


    private Animator myAnimator;





    // Use this for initialization
    void Start()
    {
        myAnimator = GetComponent<Animator>();

        characterTransform = gameObject.transform;
        agent = gameObject.GetComponent<NavMeshAgent>();

        life = Maxlife;

        FindTarget();
    }



    // Update is called once per frame
    void Update()
    {

        float deltaTime = Time.deltaTime;

        if (!(deltaTime > 0))
            return;

        if (life <= 0.0f)
        {
            if (LevelControl.Instance.IsBossLevel && EnemyManager.Instance.EnemyAliveNumber == 1)
            {
                EnemyManager.Instance.SlowCamera();
            }
            if (IsDeath)
            {

                return;
            }
            //播放死亡动画
            DeadAnimation();
            return;
        }


        //找不到目标则寻找目标
        if (target == null)
        {
            FindTarget();
            if (target == null)
            {
                agent.Stop();
                return;
            }
        }

        //进行寻路
        Vector3 toTarget = target.transform.position - characterTransform.position;
        toTarget.y = 0.0f;

        AnimatorStateInfo stateInfo = myAnimator.GetCurrentAnimatorStateInfo(0);

        if (stateInfo.IsTag("attack"))
            myAnimator.speed = attackSpeed;
        else
            myAnimator.speed = 1;

        if (toTarget.magnitude > detectRange)
        {
            //正在站岗
            IdleAnimation();
        }
        else
        {

            if (stateInfo.IsTag("attack") || stateInfo.IsTag("damage")) //攻击和受伤时不位移和旋转
            {
                StopMove();
            }
            else if (toTarget.magnitude > Range_Angle_Damage[curentSkill].x) //追踪时位移和旋转
            {
                MoveTo(target.transform.position);
                RotateTo(toTarget);
                RunAnimation();
            }
            else if (toTarget.magnitude <= Range_Angle_Damage[curentSkill].x) //瞄准的时候只旋转
            {
                StopMove();
                RotateTo(toTarget);
                float angle = GetAngle(toTarget);
                if (angle <= Range_Angle_Damage[curentSkill].y / 0.5f)
                    AttackAnimation();
            }

        }
    }

    //初始化怪物数据
    public void InitEnemy(float maxlife, float rotatespeed, float detectRange, Vector3[] attack_range_angle_damage,float attackspeed,float movespeed,int cyrstalnumber)
    {
        this.Maxlife = maxlife;
        life = this.Maxlife;
        this.rotateSpeed = rotatespeed;
        this.detectRange = detectRange;
        this.predetectRange = detectRange;
        Range_Angle_Damage = attack_range_angle_damage;
        this.attackSpeed = attackspeed;
        this.moveSpeed = movespeed;
        this.cyrstalNumber = cyrstalnumber;
    }

    public Animator GetAnimator()
    {
        return myAnimator;
    }

    public void SetRangeAttack()
    {
        curentSkill = UnityEngine.Random.Range(0, Range_Angle_Damage.Length);
        myAnimator.SetFloat("attackidex", curentSkill);
    }
    //减少生命
    public void DecreaseLife(float damage)
    {
        if (IsDeath || this.life <= 0)
            return;
        this.life -= damage;
        foreach(GameObject mm in EnemyManager.Instance.GetAllEnemys())
        {
            if (mm == null)
                continue;
            if (mm.GetComponent<MoveMent>() != null)
            {
                mm.GetComponent<MoveMent>().detectRange = 100.0f;
            }


        }
        myAnimator.SetFloat("damage", UnityEngine.Random.Range(0, 2));
        myAnimator.SetTrigger("bedamage");
    }

    //检测是否击中玩家(帧函数)
    public void DetectTouchPlayer()
    {
        transform.GetComponent<music>().Play("attack");
        
        Vector3 toTarget = target.transform.position - characterTransform.position;
        toTarget.y = 0.0f;
        float angle = GetAngle(toTarget);
        if ((angle <= Range_Angle_Damage[curentSkill].y / 0.5f) && toTarget.magnitude <= Range_Angle_Damage[curentSkill].x)
        {
            TouchPlayer();
        }

    }

    //攻击开始(帧函数)
    public void BeginAttack()
    {
        OnBeginAttack.Invoke(gameObject);
    }

    //攻击结束(帧函数)
    public void EndAttack()
    {
        OnEngAttack.Invoke(gameObject);
    }


    //击中玩家
    public void TouchPlayer()
    {
        if (GameControl.Instance.Player.GetComponent<PlayerControl>().IsSpellLock)
        {
            return;
        }
        GameControl.Instance.Camera.GetComponent<BeHitEffect>().ShowBeHitEffect();
        target.GetComponent<PlayerControl>().BeHurt((int)Range_Angle_Damage[curentSkill].z);
        OnTouch.Invoke(gameObject);
    }

    //寻找玩家
    public void FindTarget()
    {
        target = GameControl.Instance.Player.gameObject;
        characterTransform.LookAt(target.transform);
    }

    public GameObject GetTarget()
    {
        return target;
    }

    void OnDrawGizmos()
    {
        if (!showInfo)
            return;

        //检测范围
        Gizmos.color = detectColor;
        Gizmos.DrawWireSphere(gameObject.transform.position, detectRange);

        //攻击范围
        Gizmos.color = attackColor;
        Gizmos.DrawWireSphere(gameObject.transform.position, Range_Angle_Damage[curentSkill].x);



    }

    private float GetAngle(Vector3 toTarget)
    {
        toTarget.y = 0.0f;
        return Vector3.Angle(characterTransform.forward, toTarget.normalized);
    }

    private void MoveTo(Vector3 des)
    {
        agent.SetDestination(des);
        agent.Resume();
    }

    private void RotateTo(Vector3 toTarget)
    {
        toTarget.y = 0.0f;
        characterTransform.rotation = Quaternion.Slerp(characterTransform.rotation, Quaternion.LookRotation(toTarget, Vector3.up), Time.deltaTime * rotateSpeed);
    }

    private void StopMove()
    {
        agent.Stop();
    }

    public void StopFollow()
    {
        detectRange = 0;
    }

    public void BeginFollow()
    {
        detectRange = predetectRange;
    }

    private void IdleAnimation()
    {
        myAnimator.SetBool("run", false);
        OnIdle.Invoke(gameObject);
        agent.Stop();
    }

    private void RunAnimation()
    {
        myAnimator.SetBool("run", true);
        OnTrace.Invoke(gameObject);
    }

    private void AttackAnimation()
    {
        agent.Stop();
        myAnimator.SetBool("run", false);
        myAnimator.SetTrigger("attack");
    }

    private void DeadAnimation()
    {
        
        agent.Stop();
        myAnimator.Play("death");
        transform.FindChild("Canvas").gameObject.SetActive(false);
        OnDie.Invoke(gameObject);
       
        transform.GetComponent<CapsuleCollider>().enabled = false;
        if (GameControl.Instance.Player.GetComponent<PlayerControl>().IsSpellLock == true)
        {
            GameControl.Instance.Player.GetComponent<PlayerControl>().BeginAttack();
        }
    }

    public void EnemyAliveNumberDecrease()
    {
        int cyrstalnumber = (int)(cyrstalNumber * UnityEngine.Random.Range(0.5f, 1.5f));
        PlayerData.CRYSTALNUM += cyrstalnumber;
        GameObject.Find("PlayerUI").GetComponent<PlayerUIdata>().AddBattleCyrstal(cyrstalnumber);
        GameControl.Instance.Player.GetComponent<PlayerControl>().AddDoublePower();
        EnemyManager.Instance.EnemyDecrease();
        IsDeath = true;
    }

    public float moveSpeed
    {
        get
        {
            return gameObject.GetComponent<NavMeshAgent>().speed;
        }

        set
        {
            gameObject.GetComponent<NavMeshAgent>().speed = value;
        }
    }

}


