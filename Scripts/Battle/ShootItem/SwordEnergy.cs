using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SwordEnergy : ShootItem
{
    private Transform light;
    private Transform fire;
    private Transform dot;
    private float lightIntensity;
    private float particleSize;
    private float dotSpeed;
    private float startDamage;
    // Use this for initialization
    void Start()
    {
        GetComponent<Rigidbody>().AddForce(transform.forward.normalized * flyPower);
        light = transform.Find("Light");
        fire = transform.Find("MainFire01");
        dot = transform.Find("Dot");
        lightIntensity = light.GetComponent<Light>().intensity;
        particleSize = fire.GetComponent<ParticleSystem>().startSize;
        dotSpeed = dot.GetComponent<ParticleSystem>().startSpeed;
        startDamage = damage;
    }

    // Update is called once per frame
    void Update()
    {
        base.Update();
        if(time > deleteTime/2)
        {
            light.GetComponent<Light>().intensity = lightIntensity * ((deleteTime - time) / (deleteTime / 2.0f));
            fire.GetComponent<ParticleSystem>().startSize = ((deleteTime - time) / (deleteTime / 2.0f));
            dot.GetComponent<ParticleSystem>().startSpeed = dotSpeed * ((deleteTime - time) / (deleteTime / 2.0f));
            damage = startDamage * ((deleteTime - time) / (deleteTime / 2.0f));
        }

    }

    void OnTriggerEnter(Collider c)
    {
        if (c.tag == "Enemy")
        {
            c.gameObject.GetComponent<MonsterBehaviour>().DecreaseLife(damage);
        }
    }
}
