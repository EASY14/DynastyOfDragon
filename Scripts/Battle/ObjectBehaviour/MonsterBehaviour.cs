using UnityEngine;
using System.Collections;
using System;
using UnityEngine.Events;


[Serializable]
public class MonsterBloodChange : UnityEvent<float> { }

public enum MovetionState
{
    IDLE,
    CHASE,
    ATTACK,
    DEAD
}

[RequireComponent(typeof(NavMeshAgent))]
public class MonsterBehaviour : Behaviour {

    [SerializeField]
    //最大生命值
    protected float maxhp = 200.0f;

    //当前生命值
    protected float currenthp = 0.0f;

    [SerializeField]
    //侦察范围
    protected float detectRange = 5.0f;

    [SerializeField]
    //攻击速度
    protected float attackSpeed = 1.0f;

    [SerializeField]
    //移动速度
    protected float moveSpeed = 1.0f;

    [SerializeField]
    //攻击伤害
    protected float attackDamage = 5.0f;

    [SerializeField]
    //可获得经验
    protected int experience = 100;

    [SerializeField]
    //可获得水晶
    protected int cyrstal = 100;

    [SerializeField]
    private GameObject dieEffect;

    [SerializeField]
    private GameObject showEffect;

    [SerializeField]
    // 血量改变 当前血量的百分比[0.0f,1.0f]
    protected MonsterBloodChange bloodChangeEvent;

    [SerializeField]
    // 显示技能提示 范围 角度
    protected ShowSkillTipsEvent showSkillTipsEvent;

    [SerializeField]
    // 隐藏技能提示 
    protected HideSkillTipsEvent hideSkillTipsEvent;

    [SerializeField]
    private bool isShowBloodChange = false;
    [SerializeField]
    private bool isShowSkillTips = false;
    [SerializeField]
    private bool isHideSkillTips = false;
    [SerializeField]
    private bool isShowRange = false;

    [SerializeField]
    private int currentSkillID = 0;

    [SerializeField]
    private SkillData[] skills;

    //动画控制类
    protected MonsterAnimationController monAnimationCon;

    //导航
    protected NavMeshAgent agent;

    //是否检测到玩家
    protected bool isDetectePlayer;
    
    //玩家对象
    protected GameObject player;

    protected Vector3 prePlayerPosition;

    protected float deleteTime = 0.0f;
    protected float reactTime = 0.0f;

    protected MovetionState state;
    // Use this for initialization
    protected void Start()
    {
        base.Start();
        
        agent = GetComponent<NavMeshAgent>();
        if (agent == null)
            Debug.LogError("need Component NavMeshAgent");

        monAnimationCon = animationCon as MonsterAnimationController;
        if (monAnimationCon == null)
            Debug.LogError("need Component MonsterAnimationController");

        monAnimationCon.SetAttackSpeed(attackSpeed);
        monAnimationCon.SetMoveSpeed(moveSpeed);
        agent.speed = moveSpeed;

        currenthp = maxhp;

        state = MovetionState.IDLE;

        currentSkillID = UnityEngine.Random.Range(0, skills.Length);

        bloodChangeEvent.Invoke(currenthp/maxhp);

        SmallMap.Instance.AddMonsterIcon(gameObject);

        if (showEffect != null)
            Instantiate(showEffect, transform.position, transform.rotation); 
    }

    // Update is called once per frame
    protected void Update()
    {
        if (Input.GetKey(KeyCode.H))
            DecreaseLife(10);
        AnimationControl();
    }

    protected void FixedUpdate()
    {
        MoveControl();
    }


    protected virtual void MoveControl()
    {
        float deltaTime = Time.deltaTime;

        if (!(deltaTime > 0))
            return;

        if (currenthp <= 0.0f)
        {
                
            deleteTime += Time.deltaTime;
            if (deleteTime >= GameControl.Instance.EnemyDeleteTime)
            {
                if (dieEffect != null)
                    Instantiate(dieEffect, transform.position, transform.rotation); 
                Destroy(this.gameObject);
            }

            hideSkillTipsEvent.Invoke();
            return;
        }

        if (reactTime < GameControl.Instance.EnemyReactTime)
        {
            reactTime += Time.deltaTime;
            return;
        }

        //找不到目标则寻找目标
        if (player == null)
        {
            FindTarget();
            if (player == null)
            {
                agent.Stop();
                return;
            }
        }

        //进行寻路
        Vector3 toTarget = player.transform.position - transform.position;
        toTarget.y = 0.0f;
        if (toTarget.magnitude > detectRange && !isDetectePlayer)
        {
            //正在站岗
            agent.Stop();
            state = MovetionState.IDLE;
        }
        else
        {
            if(!isDetectePlayer)
            {
                isDetectePlayer = true;
                ContestEnemy();
            }

            if (monAnimationCon.IsAttacking() || monAnimationCon.IsInjured() || state == MovetionState.DEAD) //攻击和受伤时不位移和旋转
            {
                agent.Stop();
            }
            else if (toTarget.magnitude > skills[currentSkillID].range) //追踪时位移和旋转
            {
                MoveTo(player.transform.position);
                RotateTo(toTarget);
                state = MovetionState.CHASE;
            }
            else if (toTarget.magnitude <= skills[currentSkillID].range) //瞄准的时候只旋转
            {
                agent.Stop();
                float angle = GetAngle(toTarget);
                if (angle <= skills[currentSkillID].angle * 0.3f)
                {
                    state = MovetionState.ATTACK;
                }
                else
                {
                    RotateTo(toTarget);
                    state = MovetionState.CHASE;
                }
            }

        }
    }

    protected virtual void AnimationControl()
    {
        if (monAnimationCon.IsInjured() || monAnimationCon.IsAttacking() || state == MovetionState.DEAD)
            return;
        switch (state)
        {
            case MovetionState.IDLE:
                monAnimationCon.SetIsIdle(true);
                monAnimationCon.Move();
                break;

            case MovetionState.CHASE:
                monAnimationCon.SetIsIdle(false);
                monAnimationCon.Move();
                break;

            case MovetionState.ATTACK:
                	monAnimationCon.Attack(currentSkillID);
                break;

            case MovetionState.DEAD:
                break;

            default:
                break;
        }
    }


    public virtual bool DecreaseLife(float damage)
    {
        if (currenthp <= 0)
            return false;
        currenthp -= damage;
        bloodChangeEvent.Invoke(currenthp / maxhp);
        if (currenthp <= 0 && state != MovetionState.DEAD)
        {
            SmallMap.Instance.RemoteMonsterIcon(gameObject);
            agent.Stop();
            state = MovetionState.DEAD;
            monAnimationCon.Die();
            GetComponent<CapsuleCollider>().enabled = false;
            GetComponent<NavMeshAgent>().enabled = false;
            GetReward();
            return true;
        }
        else if (currenthp <= 0 && state == MovetionState.DEAD)
            return false;
        currentSkillID = UnityEngine.Random.Range(0, skills.Length);
        hideSkillTipsEvent.Invoke();
        monAnimationCon.Injured();
        if (!isDetectePlayer)
        {
            isDetectePlayer = true;
            ContestEnemy();
        }
        return true;
        
    }

    protected void FindTarget()
    {
        player = GameControl.Instance.Player.gameObject;
        transform.LookAt(player.transform);
    }

    public void HurtPlayer()
    {
        player.GetComponent<CharacterBehaviour>().BeHurt(skills[currentSkillID].damageFactor*attackDamage);
    }

    protected void GetReward()
    {
        BattleUIController.Instance.GetCrystal(cyrstal);
        BattleUIController.Instance.GetExperience(experience);
    }

    void OnDrawGizmos()
    {
        if (!isShowRange)
            return;

        //检测范围
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(gameObject.transform.position, detectRange);

        //攻击范围
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(gameObject.transform.position, skills[currentSkillID].range);



    }

    protected float GetAngle(Vector3 toTarget)
    {
        Vector3 f = transform.forward;
        f.y = 0.0f;
        toTarget.y = 0.0f;
        return Vector3.Angle(f, toTarget.normalized);
    }

    protected void MoveTo(Vector3 des)
    {
        agent.SetDestination(des);
        agent.Resume();
    }

    protected void RotateTo(Vector3 toTarget)
    {
        toTarget.y = 0.0f;
        if(Vector3.Angle(transform.forward,toTarget)<5.0f)
            transform.rotation = Quaternion.LookRotation(toTarget, Vector3.up);
        else
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(toTarget, Vector3.up), Time.deltaTime * moveSpeed * 10);
    }

    public void FindPlayer()
    {
        isDetectePlayer = true;
    }

    public void ContestEnemy()
    {
        GameObject allEnemy = BattleManager.Instance.GetCurrentLevelEnemy();
        if (allEnemy != null)
        {
            Transform trans = allEnemy.transform;
            for (int i = 0; i < trans.childCount; i++)
                trans.GetChild(i).GetComponent<MonsterBehaviour>().FindPlayer();
        }
        else
            return;
    }

    /// <summary>
    /// 以下是帧函数
    /// </summary>
    public void CloseAttack()
    {
        Vector3 toTarget = player.transform.position - transform.position;
        toTarget.y = 0.0f;
        float angle = GetAngle(toTarget);
        if ((angle <= skills[currentSkillID].angle * 0.5f) && toTarget.magnitude <= skills[currentSkillID].range)
        {
            HurtPlayer();
        }

    }

    public void DistantAttack(int id)
    {
        if (prePlayerPosition == null)
            return;

        GameObject shootItem = Instantiate(skills[id].shootItem, skills[id].shootPosition.transform.position, transform.rotation) as GameObject;
        Vector3 toTarget = prePlayerPosition - transform.position;
        shootItem.GetComponent<ShootItem>().forceForward = toTarget;
        shootItem.GetComponent<ShootItem>().damage = attackDamage * skills[id].damageFactor;
    }

    public void PlaySkillSound(int id)
    {
        if (skills[id].sound.GetComponent<AudioSource>() != null)
        {
            skills[id].sound.GetComponent<AudioSource>().Play();
        }
    }

    public void BeginAttack(int id)
    {
        prePlayerPosition = player.transform.position;
        showSkillTipsEvent.Invoke(skills[id].range, skills[id].angle);
    }

    public void EndAttack()
    {
        currentSkillID = UnityEngine.Random.Range(0, skills.Length);
        monAnimationCon.ResumeSpeed();
        hideSkillTipsEvent.Invoke();
    }

    public MovetionState GetState()
    {
        return state;
    }
}
