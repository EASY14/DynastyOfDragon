using UnityEngine;
using System.Collections;
using UnityEngine.Events;
using System;
using UnityStandardAssets.Cameras;
using DG.Tweening;
[Serializable]
public class BloodTipEvent : UnityEvent<float, float> { }

[Serializable]
public class EnergyTipEvent : UnityEvent<float, float> { }

[Serializable]
public class UpdatePlayerData : UnityEvent<float, float, float, float> { }

[Serializable]
public class ShowSkillTipsEvent : UnityEvent<float, float> { }
[Serializable]
public class HideSkillTipsEvent : UnityEvent { }

[Serializable]
public struct ChaSkillData
{
    public float damageFactor;
    public int mp;
    public float angle;
    public float range;
    public GameObject sound;
    public GameObject shootItem;
    public GameObject shootPosition;
}

public class CharacterBehaviour : Behaviour
{
    [SerializeField]
    //最大生命值
    private float maxhp = 200.0f;

    //当前生命值
    private float currenthp = 0.0f;

    [SerializeField]
    //最大气力值
    private float maxmp = 100.0f;

    //当前气力值
    private float currentmp = 0.0f;

    [SerializeField]
    //mp增加率
    private float mpRate = 5.0f;

    [SerializeField]
    //攻击速度
    private float attackSpeed = 1.0f;

    [SerializeField]
    //移动速度
    private float moveSpeed = 1.0f;

    [SerializeField]
    //攻击伤害
    private float attackDamage = 5.0f;

    [SerializeField]
    private GameObject showEffect;
    [SerializeField]
    private GameObject resumeHPEffect;
    [SerializeField]
    private GameObject resumeMPEffect;

    [SerializeField]
    // 血量改变 
    private BloodTipEvent bloodChangeEvent;

    [SerializeField]
    private bool isShowBloodChange = false;

    [SerializeField]
    // 气量改变 
    private EnergyTipEvent energyChangeEvent;

    [SerializeField]
    private bool isShowEnergyChange = false;
    //玩家数据更新
    [SerializeField]
    private UpdatePlayerData updatePlayData;

    [SerializeField]
    private bool isShowUpdatePlayerData = false;
     
    //动画控制类
    private CharacterAnimationController chaAnimationCon;

    //角色控制类
    private CharacterController characterCon;

    [SerializeField]
    private bool isShowRange = false;

    [SerializeField]
    private int currentSkillID = 0;

    [SerializeField]
    private ChaSkillData[] skills;

    //摄像机
    private Transform cameraTransform;
    private float turnAmount;
    private float forwardAmount;

    private bool isControl = true;

    private bool isShowMouse = false;

    private bool isAttacked = false;
    private bool isShowEffect = false;
    // Use this for initialization
    private void Start()
    {
        base.Start();

        cameraTransform = Camera.main.transform;

        characterCon = GetComponent<CharacterController>();
        if (characterCon == null)
            Debug.LogError("need Component CharacterController");

        chaAnimationCon = animationCon as CharacterAnimationController;
        if (chaAnimationCon == null)
            Debug.LogError("need Component CharacterAnimationController");

        chaAnimationCon.SetAttackSpeed(attackSpeed);
        chaAnimationCon.SetMoveSpeed(moveSpeed);

        maxhp = PlayerData.CurrentHP;
        mpRate = PlayerData.CurrentPOWERADD;
        attackDamage = PlayerData.CurrentATK;
        attackSpeed = PlayerData.CurrentAS;
        moveSpeed = PlayerData.CurrentMS;
        currenthp = maxhp;
        currentmp = maxmp;

        isControl = true;
        if (PlayerData.TECHGRADE.Count == 3)
        {
            skills[1].damageFactor *= (PlayerData.TECHGRADE[0] * 0.05f + 1.0f);
            skills[2].damageFactor *= (PlayerData.TECHGRADE[1] * 0.05f + 1.0f);
            skills[3].damageFactor *= (PlayerData.TECHGRADE[2] * 0.05f + 1.0f);
        }

        bloodChangeEvent.Invoke(maxhp,currenthp);
        energyChangeEvent.Invoke(maxmp,currentmp);
        //Debug.Log(PlayerData.CurrentATK.ToString() + "," + PlayerData.CurrentAS.ToString() + "," + PlayerData.CurrentMS.ToString() + "," + PlayerData.CurrentPOWERADD.ToString());

        updatePlayData.Invoke(attackDamage,attackSpeed,moveSpeed,mpRate);
        if (showEffect != null)
            Instantiate(showEffect, transform.position, transform.rotation); 
    }

    // Update is called once per frame
    private void Update()
    {
        //得到方向键
        float v = Input.GetAxis("Vertical");
        float h = Input.GetAxis("Horizontal");

        //设置动画
        AnimationControl(v, h);

    }

    private void FixedUpdate()
    {
        // read inputs
        float v = Input.GetAxis("Vertical");
        float h = Input.GetAxis("Horizontal");

        //玩家移动
        MoveControl(v, h);
    }

    //动画控制函数
    private void AnimationControl(float v, float h)
    {

        if (chaAnimationCon.IsAttacking() || chaAnimationCon.IsSkill3() || currenthp < 0)
            return;
        chaAnimationCon.SetAttackSpeed(attackSpeed);
        chaAnimationCon.SetHorizontal(h);
        chaAnimationCon.SetVertical(v);

        if (Input.GetKeyUp(KeyCode.Tab))
        {
            isShowMouse = !isShowMouse;
            if(!isShowMouse)
                cameraTransform.parent.parent.GetComponent<FreeLookCam>().OnLockCursor();
            else
                cameraTransform.parent.parent.GetComponent<FreeLookCam>().OnShowCursor();
        }
        if (isShowMouse)
        {
            chaAnimationCon.SetHorizontal(0);
            chaAnimationCon.SetVertical(0);
            chaAnimationCon.Move();
            return;
        }

        BattleUIController battleUIController = BattleUIController.Instance;

        if (!Input.GetKey(KeyCode.LeftShift))
            chaAnimationCon.Walk();
        else
            chaAnimationCon.Run();

        chaAnimationCon.Move();

        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            currentSkillID = 0;
            chaAnimationCon.SetNormalAttackDir(0);
            chaAnimationCon.Attack(0);
        }
        if (Input.GetKeyUp(KeyCode.Mouse1))
        {
            currentSkillID = 0;
            chaAnimationCon.SetNormalAttackDir(1);
            chaAnimationCon.Attack(0);
        }
        if (Input.GetKeyUp(KeyCode.Q))
        {
            if ((currentmp - 20) < 0)
                return;

            if (!battleUIController.UseSkill(1))
                return;
            currentmp -= 20;
            currentSkillID = 1;
            chaAnimationCon.Attack(1);
            energyChangeEvent.Invoke(maxmp, currentmp);
        }
        if (Input.GetKeyUp(KeyCode.E))
        {
            if ((currentmp - 20) < 0)
                return;
            if (!battleUIController.UseSkill(2))
                return;
            currentmp -= 20;
            currentSkillID = 2;
            chaAnimationCon.Attack(2);
            energyChangeEvent.Invoke(maxmp, currentmp);
        }
        if (Input.GetKeyUp(KeyCode.R))
        {
            if ((currentmp - 100) < 0)
                return;
            if (!battleUIController.UseSkill(3))
                return;
            currentmp -= 100;
            currentSkillID = 3;
            chaAnimationCon.Attack(3);
            energyChangeEvent.Invoke(maxmp, currentmp);
        }

        if (Input.GetKeyUp(KeyCode.Alpha1))
        {
            if (!battleUIController.UseHPMedication())
                return;
            currenthp = ((currenthp + battleUIController.GetAddHp()) > maxhp ? maxhp : (currenthp + battleUIController.GetAddHp()));
            bloodChangeEvent.Invoke(maxhp, currenthp);
            if (resumeHPEffect != null)
                (Instantiate(resumeHPEffect, transform.position, transform.rotation) as GameObject).transform.SetParent(transform); 
        }

        if (Input.GetKeyUp(KeyCode.Alpha2))
        {
            if (!battleUIController.UseMPMedication())
                return;
            currentmp = ((currentmp + battleUIController.GetAddMp()) > maxmp ? maxmp : (currentmp + battleUIController.GetAddMp()));
            energyChangeEvent.Invoke(maxmp,currentmp);
            if (resumeMPEffect != null)
                (Instantiate(resumeMPEffect, transform.position, transform.rotation) as GameObject).transform.SetParent(transform); 
        }


        
    }


    private void MoveControl(float v, float h)
    {
        if (chaAnimationCon.IsAttacking() || currenthp <= 0)
            return;
        if (isShowMouse)
            return;

        Vector3 move;
        Vector3 camForward;

        if (cameraTransform != null)
        {
            camForward = Vector3.Scale(cameraTransform.forward, new Vector3(1, 0, 1)).normalized;
            move = Mathf.Abs(v) * camForward + h * cameraTransform.right;
        }
        else
            move = Mathf.Abs(v) * Vector3.forward + h * Vector3.right;

        if (move.magnitude > 1f) move.Normalize();
        move = transform.InverseTransformDirection(move);
        move = Vector3.ProjectOnPlane(move, Vector3.up);

        //移动
        if (!Input.GetKey(KeyCode.LeftShift))
            characterCon.SimpleMove(gameObject.transform.forward * 0.8f * v);
        else
            characterCon.SimpleMove(gameObject.transform.forward * moveSpeed * v);

        //旋转
        turnAmount = Mathf.Atan2(move.x, move.z);
        forwardAmount = move.z;
        float turnSpeed = Mathf.Lerp(180, 360, forwardAmount);
        transform.Rotate(0, turnAmount * turnSpeed * Time.deltaTime, 0);
    }

    public void BeHurt(float damage)
    {
        currenthp -= damage;
        bloodChangeEvent.Invoke(maxhp,currenthp);
        if (currenthp <= 0)
        {
            currenthp = 0;
            if (isControl)
            {
                BattleManager.Instance.GameDefeated();
                chaAnimationCon.Die();
                characterCon.enabled = false;
                isControl = false;
            }
        }
        else
        {
            chaAnimationCon.Injured();
            BattleUIController.Instance.Injured();
        }
    }

    void OnDrawGizmos()
    {
        if (!isShowRange)
            return;

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

    /// <summary>
    /// 以下是帧函数
    /// </summary>
    public void CloseAttack()
    {
        if (isAttacked)
            return;
        GameObject allEnemy = BattleManager.Instance.GetCurrentLevelEnemy();
        if (allEnemy!=null)
        {
            Transform trans = allEnemy.transform;
            for (int i = 0; i < trans.childCount; i++)
            {
                Vector3 toTarget = trans.GetChild(i).position - transform.position;
                toTarget.y = 0.0f;
                float angle = GetAngle(toTarget);
                if ((angle <= skills[currentSkillID].angle * 0.5f) && toTarget.magnitude <= skills[currentSkillID].range)
                {
                    trans.GetChild(i).GetComponent<MonsterBehaviour>().DecreaseLife(attackDamage * skills[currentSkillID].damageFactor);
                }
            } 
        }
        else
            return;
        isAttacked = true;
    }

    public void NormalCloseAttack()
    {
        if (isAttacked)
            return;
        GameObject allEnemy = BattleManager.Instance.GetCurrentLevelEnemy();
        if (allEnemy != null)
        {
            Transform trans = allEnemy.transform;
            for (int i = 0; i < trans.childCount; i++)
            {
                Vector3 toTarget = trans.GetChild(i).position - transform.position;
                toTarget.y = 0.0f;
                float angle = GetAngle(toTarget);
                if ((angle <= skills[currentSkillID].angle * 0.5f) && toTarget.magnitude <= skills[currentSkillID].range)
                {
                    if (trans.GetChild(i).GetComponent<MonsterBehaviour>().DecreaseLife(attackDamage * skills[currentSkillID].damageFactor))
                    {
                        currentmp = ((currentmp + mpRate) > maxmp ? maxmp : (currentmp + mpRate));
                        energyChangeEvent.Invoke(maxmp, currentmp);
                    }
                }
            }
        }

        isAttacked = true;
    }

    public void ResetAttack()
    {
        isAttacked = false;
    }

    public void ResetEffect()
    {
        isShowEffect = false;
    }

    public void PlaySkillSound()
    {
        if (skills[currentSkillID].sound.GetComponent<AudioSource>() != null)
        {
            skills[currentSkillID].sound.GetComponent<AudioSource>().Play();
        }
    }

    public void DistantAttack()
    {
        if (isAttacked)
            return;
        GameObject shootItem = Instantiate(skills[currentSkillID].shootItem, skills[currentSkillID].shootPosition.transform.position, transform.rotation) as GameObject;
        shootItem.GetComponent<ShootItem>().forceForward = transform.forward;
        shootItem.GetComponent<ShootItem>().damage = attackDamage * skills[currentSkillID].damageFactor;
        isAttacked = true;
    }

    public void ShowEffect()
    {
        if (isShowEffect)
            return;
        GameObject shootItem = Instantiate(skills[currentSkillID].shootItem, skills[currentSkillID].shootPosition.transform.position, transform.rotation) as GameObject;
        shootItem.GetComponent<ShootItem>().forceForward = transform.forward;
        shootItem.GetComponent<ShootItem>().damage = attackDamage * skills[currentSkillID].damageFactor;
        isShowEffect = true;
    }

    public void ShowCursor()
    {
        isShowMouse = true;
        cameraTransform.parent.parent.GetComponent<FreeLookCam>().OnShowCursor();
    }

    public void HideCursor()
    {
        isShowMouse = false;
        cameraTransform.parent.parent.GetComponent<FreeLookCam>().OnLockCursor();
    }

    public void SetData(float maxHp, float attackdamage, float attackspeed, float movespeed, float mprate)
    {
        maxhp = maxHp;
        currenthp = maxhp;
        attackDamage = attackdamage;
        attackSpeed = attackspeed;
        moveSpeed = movespeed;
        mpRate = mprate;
    }

}
