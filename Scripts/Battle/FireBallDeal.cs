using UnityEngine;
using System.Collections;
using DG.Tweening;

public class FireBallDeal : MonoBehaviour
{
    public GameObject dragon;
    public GameObject explosion;
    private float time = 0;
    public float deleteTime = 5.0f;
    private bool isTouched = false;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if (time > deleteTime)
        {
            Destroy(this.gameObject);
        }
    }

    public void DeleteThis()
    {
        Destroy(this.gameObject);
    }

    void OnCollisionEnter(Collision c)
    {
        if (c == null)
            return;
        if (isTouched)
            return;
        //Debug.Log(c.collider.gameObject.name);
        isTouched = true;
        if (c.collider.tag == "Player")
        {
            dragon.GetComponent<DragonsMoveMent>().TouchPlayer();
            
        }
        GameObject game = null;
        game = Instantiate(explosion, gameObject.transform.position, transform.rotation) as GameObject;
        game.GetComponent<Explosion>().dragon = dragon;
        ParticleSystem[] p = transform.GetComponentsInChildren<ParticleSystem>();
        foreach (ParticleSystem v in p)
        {
            v.Stop();
        }
        transform.GetComponentInChildren<Light>().enabled = false;;
    }
}
