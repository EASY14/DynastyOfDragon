using UnityEngine;
using System.Collections;

public class SkillShowControl : MonoBehaviour {

    private SectorMesh skillTips;
    private MeshRenderer mesh;
	// Use this for initialization
	void Start () {
        skillTips = GetComponent<SectorMesh>();
        skillTips.segments = 20;
        mesh = GetComponent<MeshRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void ShowSKillTips(float range ,float angle)
    {
        if (skillTips == null || mesh == null)
            return;
        
        skillTips.enabled = true;
        mesh.enabled = true;
        skillTips.radius = range*2;
        skillTips.angleDegree = angle;
    }

    public void HideSKillTips()
    {
        if (skillTips == null || mesh == null)
            return;
        skillTips.enabled = false;
        mesh.enabled = false;
    }
}
