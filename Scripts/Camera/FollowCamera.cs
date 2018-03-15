using UnityEngine;
using System.Collections;



[RequireComponent(typeof(Camera))]
public class FollowCamera : CameraAbstract
{
    //摄像机追随速度
    [SerializeField]
    private float moveSpeed = 3;

    //摄像机转向速度
    [SerializeField]
    private float turnSpeed = 1;

    //摄像机旋转速度
    [SerializeField]
    private float rollSpeed = 0.2f;

    //是否跟随速度方向
    [SerializeField]
    public bool followVelocity = false;

    //是否跟随旋转
    [SerializeField]
    public bool followRoll = true;

    //是否跟随转向
    [SerializeField]
    public bool followTurn = true;

    //旋转转向限制
    [SerializeField]
    private float spinTurnLimit = 90;

    //跟随最低速度
    [SerializeField]
    private float targetVelocityLowerLimit = 4f;

    //转向速度
    [SerializeField]
    private float smoothTurnDelay = 0.2f;

    //抖动速度
    [SerializeField]
    private float shakeSpeed = 0.0f;

    //抖动幅度
    [SerializeField]
    private float shakeRange = 0.0f;

    //前一帧摄像机与目标旋转的度数差
    private float lastFlatAngle;

    //转向摄像机的总量
    private float currentTurnAmount;

    //转向所改变的速度量
    private float turnSpeedVelocityChange;

    //旋转上方
    private Vector3 rollUp = Vector3.up;

    // 一个摄像机环绕的点
    protected Transform center;

    //目标前一个点
    protected Vector3 lastTargetPosition;

    //初始的角度
    private Quaternion originalRotation;

    protected override void Start()
    {
        base.Start();

        center = transform.parent;
        originalRotation = transform.localRotation;
    }


    protected override void FollowTarget(float deltaTime)
    {
        if (!(deltaTime > 0) || targetTransform == null)
            return;

        transform.localRotation = originalRotation;

        Vector3 targetForward = targetTransform.forward;
        Vector3 targetUp = targetTransform.up;

        //跟随飞机的速度方向
        if (followVelocity)
        {
            if (targetRigidbody.velocity.magnitude > targetVelocityLowerLimit)
            {
                targetForward = targetRigidbody.velocity.normalized;
            }

            currentTurnAmount = Mathf.SmoothDamp(currentTurnAmount, 1, ref turnSpeedVelocityChange, smoothTurnDelay);
        }
        else
        {

            var currentFlatAngle = Mathf.Atan2(targetForward.x, targetForward.z) * Mathf.Rad2Deg;

            //当有限制的时候
            if (spinTurnLimit > 0)
            {
                float targetSpinSpeed = Mathf.Abs(Mathf.DeltaAngle(lastFlatAngle, currentFlatAngle)) / deltaTime;
                float desiredTurnAmount = Mathf.InverseLerp(spinTurnLimit, spinTurnLimit * 0.75f, targetSpinSpeed);
                float turnReactSpeed = (currentTurnAmount > desiredTurnAmount ? .1f : 1f);

                currentTurnAmount = Mathf.SmoothDamp(currentTurnAmount, desiredTurnAmount,
                                                        ref turnSpeedVelocityChange, turnReactSpeed);
            }
            else
            {
                currentTurnAmount = 1;
            }
            lastFlatAngle = currentFlatAngle;
        }



        //不跟随俯仰
        if (!followTurn)
        {
            targetForward.y = 0;
            if (targetForward.sqrMagnitude < float.Epsilon)
            {
                targetForward = center.forward;
            }
        }

        //不跟随旋转
        if (!followRoll)
            targetUp = Vector3.up;

        //改变摄像机旋转
        rollUp = rollSpeed > 0 ? Vector3.Slerp(rollUp, targetUp, rollSpeed * deltaTime) : Vector3.up;
        Quaternion rollRotation = Quaternion.LookRotation(targetForward, rollUp);
        if (followTurn)
            center.rotation = Quaternion.Lerp(center.rotation, rollRotation, turnSpeed * currentTurnAmount * deltaTime);

        //改变摄像机位置
        center.position = Vector3.Lerp(center.position, targetTransform.position, deltaTime * moveSpeed);



        //摄像机抖动
        float bx = (Mathf.PerlinNoise(0, Time.time * shakeSpeed) - 0.5f);
        float by = (Mathf.PerlinNoise(Time.time * shakeSpeed, 0)) - 0.5f;
        bx *= shakeRange;
        by *= shakeRange;

        //添加抖动
        transform.Rotate(bx, by, 0);

    }

    public float ShakeSpeed
    {
        get
        {
            return shakeSpeed;
        }
        set
        {
            if (value >= 0.0f)
                shakeSpeed = value;
            else
                shakeSpeed = 0.0f;
        }
    }

    public float ShakeRange
    {
        get
        {
            return shakeRange;
        }
        set
        {
            if (value >= 0.0f)
                shakeRange = value;
            else
                shakeRange = 0.0f;
        }
    }

    public float MoveSpeed
    {
        get
        {
            return moveSpeed;
        }
        set
        {
            if (value >= 0.0f)
                moveSpeed = value;
            else
                moveSpeed = 0.0f;
        }
    }

    public float TurnSpeed
    {
        get
        {
            return turnSpeed;
        }
        set
        {
            if (value >= 0.0f)
                turnSpeed = value;
            else
                turnSpeed = 0.0f;
        }
    }

    public float RollSpeed
    {
        get
        {
            return rollSpeed;
        }
        set
        {
            if (value >= 0.0f)
                rollSpeed = value;
            else
                rollSpeed = 0.0f;
        }
    }

}


