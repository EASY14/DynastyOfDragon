using UnityEngine;
using System.Collections;


public class FocusOnTarget : CameraAbstract
{
    //调整时间
    [SerializeField]
    private float fovAdjustTime = 1;  
        
    //放大倍数
    [SerializeField]
    private float zoomAmountMultiplier = 2;      

    //边缘效果
    [SerializeField]
    private bool includeEffectsInSize = false;   

    private float boundSize;
    private float fovAdjustVelocity;
    private Camera cam;
    private Transform lastTarget;

    protected override void Start()
    {
        base.Start();
        boundSize = MaxBoundsExtent(targetTransform, includeEffectsInSize);
        cam = GetComponentInChildren<Camera>();
    }


    protected override void FollowTarget(float deltaTime)
    {

        // calculate the correct field of view to fit the bounds size at the current distance
        float dist = (targetTransform.position - transform.position).magnitude;
        float requiredFOV = Mathf.Atan2(boundSize, dist) * Mathf.Rad2Deg * zoomAmountMultiplier;

        cam.fieldOfView = Mathf.SmoothDamp(cam.fieldOfView, requiredFOV, ref fovAdjustVelocity, fovAdjustTime);
    }


    public override void SetTarget(GameObject newTransform)
    {
        base.SetTarget(newTransform);
        boundSize = MaxBoundsExtent(newTransform.transform, includeEffectsInSize);
    }


    public static float MaxBoundsExtent(Transform obj, bool includeEffects)
    {
        // get the maximum bounds extent of object, including all child renderers,
        // but excluding particles and trails, for FOV zooming effect.

        var renderers = obj.GetComponentsInChildren<Renderer>();

        Bounds bounds = new Bounds();
        bool initBounds = false;
        foreach (Renderer r in renderers)
        {
            if (!((r is TrailRenderer) || (r is ParticleRenderer) || (r is ParticleSystemRenderer)))
            {
                if (!initBounds)
                {
                    initBounds = true;
                    bounds = r.bounds;
                }
                else
                {
                    bounds.Encapsulate(r.bounds);
                }
            }
        }
        float max = Mathf.Max(bounds.extents.x, bounds.extents.y, bounds.extents.z);
        return max;
    }

    public float FovAdjustTime
    {
        get 
        { 
            return fovAdjustTime; 
        }
        set
        {
            if (value >= 0.0f)
                fovAdjustTime = value;
            else
                fovAdjustTime = 0.0f;
        }
    }

    public float ZoomAmountMultiplier
    {
        get
        {
            return zoomAmountMultiplier;
        }
        set
        {
            if (value >= 0.0f)
                zoomAmountMultiplier = value;
            else
                zoomAmountMultiplier = 0.0f;
        }
    }
}
