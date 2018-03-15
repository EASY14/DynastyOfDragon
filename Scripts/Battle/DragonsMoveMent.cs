using UnityEngine;
using System.Collections;
using System;
using UnityEngine.Events;
using UnityEngine.UI;
using DG.Tweening;
//using UnityEditor;




public class DragonsMoveMent : MonoBehaviour
{
    public AudioSource closeSound;
    public AudioSource distanceSound;

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

    //转身速度
    [SerializeField]
    public float rotateSpeed = 1;

    //攻击速度
    public float attackSpeed = 1;
    //导航
    private NavMeshAgent agent;

    public enum Position
    {
        ground,
        air,
        toground,
        toair

    }

    public Position currentPosition;
    //飛行高度
    public float airHeight = 10;

    //陆地時間
    public float landTime = 5;

    //空中時間
    public float airTime = 2;

    //行動時間
    private float moveTime = 0;

    //著陸位置
    private Vector3 landPosition;

    public float flySpeed = 1;

    public float flyUpDown = 1;

    private Vector3 originalPosition;

    //private Vector3 groundPosition;

    public GameObject FireBall;

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


    //是否检测到
    private bool isDetected;

    //
    public int cyrstalNumber = 300;

    public Animator myAnimator;

    [SerializeField]
    private float arriveDistance = 0.5f;

    [SerializeField]
    private GameObject ShootBallPos;

    [SerializeField]
    private float FlyPower = 1500;

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

    // Use this for initialization
    void Start()
    {
        myAnimator = GetComponent<Animator>();

        characterTransform = gameObject.transform;

        life = Maxlife;

        FindTarget();

        originalPosition = transform.position;

        //groundPosition = transform.position;

        agent = gameObject.GetComponent<NavMeshAgent>();
    }



    // Update is called once per frame
    void Update()
    {
        //Debug.Log(currentPosition + "-" + moveTime.ToString());
        float deltaTime = Time.deltaTime;

        if (!(deltaTime > 0))
            return;

        if (life <= 0.0f)
        {
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
                return;
            }
        }


        Vector3 toTarget = target.transform.position - characterTransform.position;
        toTarget.y = 0.0f;

        AnimatorStateInfo stateInfo = myAnimator.GetCurrentAnimatorStateInfo(0);

        if (stateInfo.IsTag("attack"))
            myAnimator.speed = attackSpeed;
        else
            myAnimator.speed = 1;

        changePosition();

        if (currentPosition == Position.air)
        {
            if (toTarget.magnitude > detectRange)
            {
                Debug.Log(0);
                //正在站岗
                FlyAnimation();
            }
            else
            {

                if (stateInfo.IsTag("attack") || stateInfo.IsTag("damage")) //攻击和受伤时不位移和旋转
                {
                    return;
                }
                else if (toTarget.magnitude > Range_Angle_Damage[curentSkill].x) //追踪时位移和旋转
                {

                    FlyTo(target.transform.position);
                    RotateTo(toTarget);
                    IdleAnimation();
                    FlyAnimation();
                }
                else if (toTarget.magnitude <= Range_Angle_Damage[curentSkill].x) //瞄准的时候只旋转
                {
                    RotateTo(toTarget);
                    float angle = GetAngle(toTarget);
                    if (angle <= Range_Angle_Damage[curentSkill].y / 0.5f)
                        AttackAnimation();
                    else
                    {
                        IdleAnimation(); 
                        FlyAnimation();
                    }
                }

            } 
        }
        else if(currentPosition == Position.ground)
        {
            if (stateInfo.IsTag("attack") || stateInfo.IsTag("damage")) //攻击和受伤时不位移和旋转
            {
                return;
            }else if (toTarget.magnitude > Range_Angle_Damage[curentSkill].x) //追踪时位移和旋转
            {
                MoveTo(target.transform.position);
                RotateTo(toTarget);
                WalkAnimation();
            }
            else if (toTarget.magnitude <= Range_Angle_Damage[curentSkill].x) //瞄准的时候只旋转
            {
                StopMove();
                RotateTo(toTarget);
                float angle = GetAngle(toTarget);
                if (angle <= Range_Angle_Damage[curentSkill].y / 0.5f)
                {
                    IdleAnimation();
                    AttackAnimation();
                }
                else
                    WalkAnimation();
            }else
                IdleAnimation();

        }
        else if(currentPosition == Position.toair)
        {
            MoveToAir(gameObject.transform.position);
            IdleAnimation();
            FlyAnimation();
        }
        else if (currentPosition == Position.toground)
        {
            MoveToGround(landPosition);
            IdleAnimation();
            FlyAnimation();
        }
    }


    private void changePosition()
    {
        switch (currentPosition)
        {
            case Position.air:
                {
                    curentSkill = 0;
                    moveTime += Time.deltaTime;
                    if (moveTime >= airTime)
                    {
                        GetComponent<SphereCollider>().enabled = true;
                        landPosition = target.transform.position;
                        landPosition.y = originalPosition.y;
                        currentPosition = Position.toground;
                        moveTime = 0;
                    }

                }
                break;
            case Position.ground:
                {
                    curentSkill = 1;
                    moveTime += Time.deltaTime;
                    if(moveTime >= landTime)
                    {
                        GetComponent<SphereCollider>().enabled = false;
                        agent.enabled = false;
                        currentPosition = Position.toair;
                        moveTime = 0;
                    }

                }
                break;

            default:
                break;
        }
    }

    private void MoveToGround(Vector3 vec)
    {
        Vector3 moveDirection = (vec - transform.position).normalized;
        transform.position += (moveDirection * flySpeed * Time.deltaTime * flyUpDown);
        if ((transform.position - vec).magnitude < arriveDistance)
        {
            agent.enabled = true;
            currentPosition = Position.ground;
            //groundPosition = transform.position;
        }
    }

    private void MoveToAir(Vector3 vec)
    {
        AnimatorStateInfo stateInfo = myAnimator.GetCurrentAnimatorStateInfo(0);
        if (stateInfo.IsTag("attack"))
            return;
        vec.y = airHeight + originalPosition.y;
        Vector3 moveDirection = (vec - transform.position).normalized;
        if ((transform.position - vec).magnitude < arriveDistance)
        {
            currentPosition = Position.air;
        }

        transform.position += (moveDirection * flySpeed * Time.deltaTime * flyUpDown);
    }

    private void GoAway(Vector3 toTarget)
    {
        toTarget.y = target.transform.position.y + airHeight;
        Vector3 moveDirection = (toTarget - transform.position).normalized;
        moveDirection.x *= -1;
        moveDirection.y *= -1;
        transform.position += (moveDirection * flySpeed * Time.deltaTime);

    }

    //初始化怪物数据
    public void InitEnemy(float maxlife, float rotatespeed, float detectRange, Vector3[] attack_range_angle_damage, float attackspeed, float movespeed,int Cyrstalnum)
    {
        this.Maxlife = maxlife;
        life = this.Maxlife;
        this.rotateSpeed = rotatespeed;
        this.detectRange = detectRange;
        Range_Angle_Damage = attack_range_angle_damage;
        this.attackSpeed = attackspeed;
        this.flySpeed = movespeed;
        this.cyrstalNumber = Cyrstalnum;
    }

    //减少生命
    public void DecreaseLife(float damage)
    {
        if (currentPosition != Position.ground)
            return;

        this.life -= damage;
        if (life <= 0.0f)
        {
            if (IsDeath)
            {
                return;
            }
            //播放死亡动画
            DeadAnimation();
            return;
        }

        myAnimator.SetFloat("damage", UnityEngine.Random.Range(0, 2));
        myAnimator.SetTrigger("bedamage");
    }

    //检测是否击中玩家(帧函数)
    public void DetectTouchPlayer()
    {
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

    private void FlyTo(Vector3 des)
    {
        des.y = target.transform.position.y + airHeight;
        Vector3 moveDirection = (des - transform.position).normalized;
        transform.position += (moveDirection * flySpeed * Time.deltaTime);

    }

    private void MoveTo(Vector3 des)
    {
        agent.SetDestination(des);
        agent.Resume();
    }

    private void StopMove()
    {
        agent.Stop();
    }

    private void RotateTo(Vector3 toTarget)
    {
        toTarget.y = 0.0f;
        characterTransform.rotation = Quaternion.Slerp(characterTransform.rotation, Quaternion.LookRotation(toTarget, Vector3.up), Time.deltaTime * rotateSpeed);
    }


    private void IdleAnimation()
    {
        myAnimator.SetBool("fly", false);
        myAnimator.SetBool("walk", false);
        OnIdle.Invoke(gameObject);
    }

    private void FlyAnimation()
    {
        myAnimator.SetBool("fly", true);
        OnTrace.Invoke(gameObject);
    }

    private void WalkAnimation()
    {
        myAnimator.SetBool("walk", true);
        OnTrace.Invoke(gameObject);
    }



    private void AttackAnimation()
    {
        myAnimator.SetBool("fly", false);
        myAnimator.SetTrigger("toattack");
        switch (currentPosition)
        {
            case Position.ground:
                myAnimator.SetFloat("attack", 0.0f);
                break;
            case Position.air:
                myAnimator.SetFloat("attack", 1.0f);
                break;
            case Position.toground:
                break;
            default:
                break;
        }
    }

    private void DeadAnimation()
    {
        myAnimator.SetTrigger("death");
        IsDeath = true;
        OnDie.Invoke(gameObject);
    }

    private void ShootFireBall()
    {
        Vector3 toTarget = target.transform.position - (ShootBallPos.transform.position);
        GameObject g = Instantiate(FireBall, (ShootBallPos.transform.position), transform.rotation) as GameObject;
        g.GetComponent<Rigidbody>().AddForce(toTarget.normalized * FlyPower);
        g.GetComponent<FireBallDeal>().dragon = gameObject;
        distanceSound.Play();
    }

    public void EnemyAliveNumberDecrease()
    {
        int cyrstalnumber = (int)(cyrstalNumber * UnityEngine.Random.Range(0.5f, 1.5f));
        PlayerData.CRYSTALNUM += cyrstalnumber;
        GameObject.Find("PlayerUI").GetComponent<PlayerUIdata>().AddBattleCyrstal(cyrstalnumber);
        GameControl.Instance.Player.GetComponent<PlayerControl>().AddDoublePower();
        EnemyManager.Instance.EnemyDecrease();
    }
    
}


