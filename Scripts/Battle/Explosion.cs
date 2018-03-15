using UnityEngine;
using System.Collections;

public class Explosion : MonoBehaviour {

    public GameObject dragon;
    private float time = 0;
    public float deleteTime = 5.0f;
    private bool isTouchedPlayer = false;
    // Use this for initialization
    void Start()
    {
        isTouchedPlayer = false;
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        GetComponent<Light>().intensity -= Time.deltaTime*3;
        if (time > deleteTime)
        {
            Destroy(this.gameObject);
        }
    }

    public void DeleteThis()
    {
        Destroy(this.gameObject);
    }

    void OnTriggerEnter(Collider c)
    {
        if (c.tag == "Player" && !isTouchedPlayer)
        {
            isTouchedPlayer = true;
            dragon.GetComponent<DragonsMoveMent>().TouchPlayer();
            GameControl.Instance.Camera.GetComponent<BeHitEffect>().StrongHitEffect();
        }
    }

}
