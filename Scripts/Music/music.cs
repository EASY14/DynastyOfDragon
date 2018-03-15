using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class music : MonoBehaviour {
    public string path;
    public List<string> musicname = new List<string>();
    private MusicManager mm;
	// Use this for initialization
    void Start()
    {
        mm = GameObject.FindGameObjectWithTag("music").GetComponent<MusicManager>();
        AddAllMusic();
    }
        
    //通过查找名字播放音乐
    public void Play(string m)
    {
        foreach (string n in musicname)
        {
            if (n == m)
            {
                transform.FindChild("music").FindChild(m).GetComponent<AudioSource>().Play();
            }
        }
    }

    public void Stop(string m)
    {
        foreach (string n in musicname)
        {
            if (n == m)
            {
                transform.FindChild("music").FindChild(m).GetComponent<AudioSource>().Stop();
            }
        }
    }

    //加载所有该角色的音乐
    public void AddAllMusic()
    {
        foreach (string n in musicname)
        {
            if (path != "" && n != "")
            {
                mm.AudioAdd(this.gameObject, path + "/" + n,n);
            }
            else if (n != "")
            {
                mm.AudioAdd(this.gameObject, n,n);
            }
        }
    }
	
	// Update is called once per frame
}
