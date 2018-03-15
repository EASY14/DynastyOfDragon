using UnityEngine;
using System.Collections;

public class PlayerControl : MonoBehaviour
{
    private Animator myAnimator;
    //private AnimatorStateInfo stateInfo;
    private CharacterController charactercontroller;
    private Transform myTransform;

    private float f_MoveAndIdle = 0;
    public float lerpTime = 2;
    
    public float rotateSpeed = 2;
    private float AttackDistance = 5.0f;
    private float AttackAngle = 45.0f;
    private bool Stiff = false;
    public bool IsSpellLock = false;
    //private GameObject[] enemyTargets = null;

    private float MAXHP = 0;
    private float HP = 0;
    private float MS = 0;
    private float AS = 0;
    private float ATK = 0;
    private float POWERADD = 0;
    private float POWER = 0;

    private int Special = 0;
    private int MaxSpecial = 6;

    public bool IsControl = true;

    public GameObject ShootPosition;

    // Use this for initialization
    void Start()
    {
        myAnimator = this.GetComponent<Animator>();
        charactercontroller = this.GetComponent<CharacterController>();
        myTransform = this.transform;

        InitPlayer();

        //Vector3[] arg = { new Vector3(4, 2, 3), new Vector3(4, 3, 10), new Vector3(3, 5, 6),new Vector3(5,5,12) };
        //Vector3[] arg1 = { new Vector3(10, 2, 5) };
        //EnemyManager.Instance.AddKing("red", new Vector3(-6.74f, 4.98f, 12.5f),100 ,100, 10, arg);
        ////EnemyManager.Instance.AddKing("red", new Vector3(-6.74f, 4.98f, 12.5f), 100, 3, 10, arg);
        //EnemyManager.Instance.AddWarrior("red", new Vector3(1.733f, 0.65f, 2.0f), 50, 3, 100, arg1,2,2);

        //EnemyManager.Instance.AddMage("purple", new Vector3(-4.0f, 4.98f, 10.0f), 500, 10, 10, arg1);
        //EnemyManager.Instance.AddMage("greed", new Vector3(-6.0f, 4.98f, 10.0f), 500, 10, 10, arg1);
        //EnemyManager.Instance.AddMage("blue", new Vector3(-8.0f, 4.98f, 10.0f), 500, 10, 10, arg1);
        //EnemyManager.Instance.AddMage("purple", new Vector3(-4.0f, 4.98f, 10.0f), 500, 10, 10, arg1);
        //EnemyManager.Instance.AddMage("greed", new Vector3(-6.0f, 4.98f, 10.0f), 500, 10, 10, arg1);
        //EnemyManager.Instance.AddMage("blue", new Vector3(-8.0f, 4.98f, 10.0f), 500, 10, 10, arg1);
        //EnemyManager.Instance.AddMage("purple", new Vector3(-4.0f, 4.98f, 10.0f), 500, 10, 10, arg1);
        //EnemyManager.Instance.AddMage("greed", new Vector3(-6.0f, 4.98f, 10.0f), 500, 10, 10, arg1);
        //EnemyManager.Instance.AddMage("blue", new Vector3(-8.0f, 4.98f, 10.0f), 500, 10, 10, arg1);
        //EnemyManager.Instance.AddArcher("blue", new Vector3(-3.74f, 4.98f, 12.5f), new Vector3(3, 3, 10), 10, 2);
        //EnemyManager.Instance.AddArcher("greed", new Vector3(-6.74f, 4.98f, 15.5f), new Vector3(3, 3, 10), 10, 2);
    }

    void Update()
    {
        if (!IsControl)
        {
            return;
        }

        if (Special > 0)
        {
            AS = PlayerData.AS * 1.5f;
        }
        if (Special <= 0)
        {
            AS = PlayerData.AS;
        }

        if (POWER > 100)
        {
            POWER = 100;
        }

        MoveAndIdle();
        AttackAnimation();
    }

    void ToMoveAnimationSpeed()
    {
        myAnimator.speed = MS * 0.25f;
        if (myAnimator.speed > 1.5f)
        {
            myAnimator.speed = 1.5f;
        }
    }

    public void AddDoublePower()
    {
        POWER += 2 * POWERADD;
    }

    public void AddPowerByDrug()
    {
        ////////////////////////////////////////////////////////通过吃药增加气力值
    }

    public float GetHP()
    {
        return HP;
    }
    public float GetMaxHP()
    {
        return MAXHP;
    }
    public float GetMS()
    {
        return MS;
    }
    public float GetAS()
    {
        return AS;
    }
    public float GetATK()
    {
        return ATK;
    }

    public float GetPower()
    {
        return POWER;
    }

    public float GetMaxPower()
    {
        return 100.0f;
    }

    public Animator GetAnimator()
    {
        return myAnimator;
    }

    public float GetPowerRate()
    {
        return POWER/100;
    }

    public float GetHpRate()
    {
        return HP / MAXHP;
    }

    void InitPlayer()
    {
        this.HP = PlayerData.HP;
        this.MAXHP = PlayerData.HP;
        this.ATK = PlayerData.ATK;
        this.AS = PlayerData.AS;
        this.MS = PlayerData.MS;
        this.POWERADD = PlayerData.POWERADD;
        ToMoveAnimationSpeed();
    }

    public void BeHurt(int damage)
    {
        if(IsSpellLock)
        {
            return;
        }
        HP -= damage;
        if (HP <= 0)
        {
            HP = 0;
            if (IsControl)
            {
                myAnimator.SetTrigger("Death");
                IsControl = false;
            }
        }
    }

    public void ShootArrow()
    {
        GameObject nearestenemy = EnemyManager.Instance.GetNearestEnemy(myTransform.gameObject);
        if (nearestenemy == null)
        {
            return;
        }

        Object arrow = Resources.Load("Character/Main_Arrow");

        GameObject g = Instantiate(arrow, ShootPosition.transform.position, transform.rotation) as GameObject;

        Vector3 totarget = nearestenemy.transform.position + new Vector3(0, 0.7f, 0) - ShootPosition.transform.position;

        g.GetComponent<Rigidbody>().AddForce(totarget.normalized * 2000);
        g.transform.Rotate(new Vector3(0.0f, 90.0f, -90.0f));
        //Vector3 toTarget = nearestenemy.transform.forward;
        //g.GetComponent<Rigidbody>().AddForce(toTarget.normalized * FlyPower);
        //g.GetComponent<ArrowDeal>().archer = gameObj;
    }

    public void DeathDeal()
    {

        GameObject.Find("VictoryAndDefeat").GetComponent<VictoryAndDefeat>().ShowDefeat();
    }


    void MoveAndIdle()
    {
        //AnimatorStateInfo stateInfo = myAnimator.GetCurrentAnimatorStateInfo(0);
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        //Vector3 dir = new Vector3(h, 0, v);
        if (Mathf.Abs(h) > 0.01f || Mathf.Abs(v) > 0.01f)
        {
            f_MoveAndIdle = Mathf.Lerp(f_MoveAndIdle, 1, lerpTime * Time.deltaTime);
            myAnimator.SetFloat("MoveAndIdle", f_MoveAndIdle);
            if (!Stiff&&!IsSpellLock)
            {
                //charactercontroller.SimpleMove(myTransform.forward * MS *1.2f * f_MoveAndIdle);
                //Quaternion rotation = Quaternion.LookRotation(dir, myTransform.up);
                //myTransform.rotation = Quaternion.Lerp(myTransform.rotation, rotation, rotateTime * Time.deltaTime);


                if (v > 0)
                {
                    charactercontroller.SimpleMove(myTransform.forward * MS * 1.2f * f_MoveAndIdle * v);
                    myTransform.Rotate(new Vector3(0.0f, rotateSpeed * h, 0.0f));
                    myAnimator.SetFloat("MoveAndIdle", f_MoveAndIdle);
                }
                else if (v < 0)
                {
                    charactercontroller.SimpleMove(myTransform.forward * MS * 1.2f * f_MoveAndIdle * v * 0.5f);
                    myTransform.Rotate(new Vector3(0.0f, rotateSpeed * h, 0.0f));
                    myAnimator.SetFloat("MoveAndIdle", f_MoveAndIdle);
                    //myAnimator.speed = -1.0f;
                }

            }
        }
        else
        {
            f_MoveAndIdle = Mathf.Lerp(f_MoveAndIdle, 0, lerpTime * Time.deltaTime);
            myAnimator.SetFloat("MoveAndIdle", f_MoveAndIdle);
        }
    }

    public void PlaySound(string name)
    {
        transform.GetComponent<music>().Play(name);
    }
    public void ClearStiff()
    {
        Stiff = false;
        ToMoveAnimationSpeed();
    }

    void AttackAnimation()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            NomalAttackAnimation();
        }

        if (Input.GetKeyDown(KeyCode.K))
        {
            SkillAttackAnimation(1);
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            SkillAttackAnimation(2);
        }
        if(Input.GetKeyDown(KeyCode.R))
        {
            SkillAttackAnimation(3);
        }

    }

    public void NomalAttackAnimation()
    {
        //AnimatorStateInfo stateInfo = myAnimator.GetCurrentAnimatorStateInfo(0);
        int index = Random.Range(0, 2);
        if (!Stiff)
        {
            myAnimator.SetFloat("Attack_Idex", index);
            myAnimator.SetTrigger("Attack");
            if (Special > 0)
            {
                Special--;
            }
        }
            
    }

    public void AddSpecial()
    {
        Special += 6;
        if(Special > MaxSpecial)
        {
            Special = MaxSpecial;
        }
    }
    public void ClearSpellLock()
    {
        IsSpellLock = false;
    }

    public void SkillAttackAnimation(int index)
    {
        //AnimatorStateInfo stateInfo = myAnimator.GetCurrentAnimatorStateInfo(0);
        if (!Stiff)
        {
            if(index == 3)
            {
                if(POWER<100)
                {
                    return;
                }
                POWER = 0;
                IsSpellLock = true;
            }
            if(index == 1)
            {
                if(Special > 0)
                {
                    Special--;
                }
            }
            myAnimator.SetTrigger("Spell_" + index);
        }
    }

    public void MasterYiSpellMove()
    {
        if(IsSpellLock)
        {
            myTransform.GetComponent<Rigidbody>().AddForce(transform.forward * 700);

            myTransform.GetComponent<CharacterController>().enabled = false;
        }
        
    }

    public void StopSpellMove()
    {
        myTransform.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
        myTransform.GetComponent<CharacterController>().enabled = true;
        //myTransform.GetComponent<CharacterController>().velocity = new Vector3(0, 0, 0);
    }

    public void ArrowAttackCheck(float damagepoint)
    {
        GameObject enemy = EnemyManager.Instance.GetNearestEnemy(myTransform.gameObject);
        if (enemy.GetComponent<MoveMent>() != null)
        {
            enemy.GetComponent<MoveMent>().DecreaseLife(ATK * damagepoint);
            if (!enemy.GetComponent<MoveMent>().IsDeath && !IsSpellLock)
            {
                POWER += POWERADD;

            } 
        }

        if (enemy.GetComponent<DragonsMoveMent>() != null)
        {
            enemy.GetComponent<DragonsMoveMent>().DecreaseLife(ATK * damagepoint);
            if (!enemy.GetComponent<DragonsMoveMent>().IsDeath && !IsSpellLock)
            {
                POWER += POWERADD;

            }
        }
    }

    public void AttackCheck(float damagepoint)
    {
        foreach (GameObject enemy in EnemyManager.Instance.GetAllEnemys())
        {
            Vector3 toTarget = enemy.transform.localPosition - myTransform.localPosition;
            if (toTarget.magnitude < AttackDistance)
            {
                toTarget.y = 0.0f;
                if(Vector3.Angle(myTransform.forward,toTarget.normalized)<AttackAngle)
                {
                    

                    if (enemy.GetComponent<MoveMent>() != null)
                    {
                        enemy.GetComponent<MoveMent>().DecreaseLife(ATK * damagepoint);
                        enemy.GetComponent<MoveMent>().SetRangeAttack();
                        if (enemy.GetComponent<MoveMent>().life > 0.0f && !IsSpellLock)
                        {
                            POWER += POWERADD;
                            if (POWER > 100)
                            {
                                POWER = 100;
                            }
                        }
                    }

                    if (enemy.GetComponent<DragonsMoveMent>() != null)
                    {
                        enemy.GetComponent<DragonsMoveMent>().DecreaseLife(ATK * damagepoint);
                        if (enemy.GetComponent<DragonsMoveMent>().life > 0.0f && !IsSpellLock)
                        {
                            POWER += POWERADD;
                            if (POWER > 100)
                            {
                                POWER = 100;
                            }
                        }
                    }
                }
            }
        }
    }

    public void EnterStiff()
    {
        Stiff = true;
    }

    public void BeginAttack()
    {
        Stiff = true;
        //if(Special>0)
        //{
        //    AS = PlayerData.AS * 1.5f;
        //}
        //if(Special<=0)
        //{
        //    AS = PlayerData.AS;
        //}
        myAnimator.speed = AS;
        GameObject nearestenemy = EnemyManager.Instance.GetNearestEnemy(myTransform.gameObject);
        if(nearestenemy == null)
        {
            return;
        }
        Vector3 pe = nearestenemy.transform.localPosition - myTransform.localPosition;
        Vector3 rota = new Vector3(pe.x, 0, pe.z);
        Quaternion rotation = Quaternion.LookRotation(rota.normalized, myTransform.up);
        myTransform.rotation = rotation;
    }
}
