using UnityEngine;
using System.Collections;


[RequireComponent(typeof(Camera))]
public abstract class CameraAbstract : MonoBehaviour
{
         
    //更新的类型
    public enum UpdateType
    {
        FixedUpdate,//固定更新
        LateUpdate,//最后更新
        ManualUpdate,//人为更新
    }

    //目标
    [SerializeField]
    private GameObject CurrentTarget;

    //更新方式
    [SerializeField]
    private UpdateType CurrentUpdateType;

    //目标的转换
    protected Transform targetTransform;
    //目标的刚体
    protected Rigidbody targetRigidbody;

    // Use this for initialization
    protected virtual void Start()
    {
        if (CurrentTarget == null)
        {
            FindAndTargetPlayer();
        }

        targetTransform = CurrentTarget.transform;
        targetRigidbody = CurrentTarget.GetComponent<Rigidbody>();
    }

    //固定更新
    private void FixedUpdate()
    {

        if (CurrentTarget == null || !CurrentTarget.gameObject.activeSelf)
        {
            FindAndTargetPlayer();
        }
        if (CurrentUpdateType == UpdateType.FixedUpdate)
        {
            FollowTarget(Time.deltaTime);
        }
    }

    //最后更新
    private void LateUpdate()
    {
        if (CurrentTarget == null || !CurrentTarget.gameObject.activeSelf)
        {
            FindAndTargetPlayer();
        }
        if (CurrentUpdateType == UpdateType.LateUpdate)
        {
            FollowTarget(Time.deltaTime);
        }
    }

    //手动更新
    public void ManualUpdate()
    {
        if (CurrentTarget == null || !CurrentTarget.gameObject.activeSelf)
        {
            FindAndTargetPlayer();
        }
        if (CurrentUpdateType == UpdateType.ManualUpdate)
        {
            FollowTarget(Time.deltaTime);
        }
    }

    protected abstract void FollowTarget(float deltaTime);


    //寻找目标
    public void FindAndTargetPlayer()
    {
        GameObject targetObj = GameObject.FindGameObjectWithTag("Player");
        if (targetObj)
        {
            Target = targetObj;

            targetTransform = CurrentTarget.transform;
            targetRigidbody = CurrentTarget.GetComponent<Rigidbody>();
        }
    }

    public virtual void SetTarget(GameObject newTarget)
    {
        CurrentTarget = newTarget;
        targetTransform = CurrentTarget.transform;
        targetRigidbody = CurrentTarget.GetComponent<Rigidbody>();
            
    }
    //目标接口
    public GameObject Target
    {
        get
        {
            return CurrentTarget;
        }
        set
        {
            SetTarget(value);
        }
    }


    //获得方式的接口
    public UpdateType updateType
    {
        get
        {
            return CurrentUpdateType;
        }
    }

} 


