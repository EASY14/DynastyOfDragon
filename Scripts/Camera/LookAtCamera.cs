using UnityEngine;
using System.Collections;



[RequireComponent(typeof(Camera))]
public class LookAtCamera : CameraAbstract
{
        
    //角度的范围
    [SerializeField]
    private Vector2 rotationRange = new Vector2(90.0f, 360.0f);

    //追踪的延迟
    [Range(0.0f, 10.0f)]
    [SerializeField]
    private float traceDelay = 0.0f;

    //抖动速度
    [SerializeField]
    private float shakeSpeed = 0.0f;

    //抖动幅度
    [SerializeField]
    private float shakeRange = 0.0f;

    //对准的预测位置
    [SerializeField]
    private float predictPosition = 0.0f;

    //当前的角度
    private Vector3 currentTraceAngle;

    //当前的速度
    private Vector3 currentTraceVelocity;

    //初始的角度
    private Quaternion originalRotation;


    protected override void Start()
    {
        base.Start();

        originalRotation = transform.localRotation;
        currentTraceAngle = new Vector3();
    }


    protected override void FollowTarget(float deltaTime)
    {
        if (!(deltaTime > 0) || targetTransform == null)
            return;

        transform.localRotation = originalRotation;

        //计算左右角度
        Vector3 localTarget = transform.InverseTransformPoint(targetTransform.position);
        float yAngle = Mathf.Atan2(localTarget.x, localTarget.z) * Mathf.Rad2Deg;
        yAngle = Mathf.Clamp(yAngle, -rotationRange.y * 0.5f, rotationRange.y * 0.5f);

        //计算俯仰角度
        Transform temTransform = transform;
        temTransform.localRotation = originalRotation * Quaternion.Euler(0, yAngle, 0);
        localTarget = temTransform.InverseTransformPoint(targetTransform.position);
        float xAngle = Mathf.Atan2(localTarget.y, localTarget.z) * Mathf.Rad2Deg;
        xAngle = Mathf.Clamp(xAngle, -rotationRange.x * 0.5f, rotationRange.x * 0.5f);

        //得到旋转的向量
        Vector3 targetAngles = new Vector3(currentTraceAngle.x + Mathf.DeltaAngle(currentTraceAngle.x, xAngle),
            currentTraceAngle.y + Mathf.DeltaAngle(currentTraceAngle.y, yAngle));

        //平滑过渡
        currentTraceAngle = Vector3.SmoothDamp(currentTraceAngle, targetAngles, ref currentTraceVelocity, traceDelay);

        //摄像机对准目标
        transform.localRotation = originalRotation * Quaternion.Euler(-currentTraceAngle.x, currentTraceAngle.y, 0);

        //摄像机抖动
        float bx = (Mathf.PerlinNoise(0, Time.time * shakeSpeed) - 0.5f);
        float by = (Mathf.PerlinNoise(Time.time * shakeSpeed, 0)) - 0.5f;
        bx *= shakeRange;
        by *= shakeRange;

        //预测路线
        float tx = -predictPosition * currentTraceVelocity.x;
        float ty = predictPosition * currentTraceVelocity.y;

        //添加抖动和预测
        transform.Rotate(tx + bx, ty + by, 0);

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

}  

