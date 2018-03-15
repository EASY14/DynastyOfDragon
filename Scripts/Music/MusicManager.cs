using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MusicManager : MonoBehaviour {
    public List<AudioSource> AudioList = new List<AudioSource>();
    public string BGM;
    public string BossBGM;
    public bool IsBGM = false;
    private bool IsBGMStrat = false;
	// Use this for initialization
	void Start () {
        
    }
	
	// Update is called once per frame
	void Update () {
        if(IsBGMStrat == false)
        {
            BGMPlay(BGM);
            IsBGMStrat = true;
        }
    }

    //添加音乐资源并加入组件
    public bool AudioAdd(GameObject gameobject,string path,string name)
    {
        if(gameobject.transform.FindChild("music") == null)
        {
            GameObject music = new GameObject();
            music.name = "music";
            music.transform.parent = gameobject.transform;
            music.transform.localPosition = new Vector3(0, 0, 0);
        }
        else if(Resources.Load(path, typeof(AudioClip)) == null)
        {
            Debug.Log(path + " withoutmuisc");
            return false;
        }
        GameObject newgame = new GameObject();
        newgame.name = name;
        newgame.transform.parent = gameobject.transform.FindChild("music").transform;
        newgame.transform.localPosition = new Vector3(0, 0, 0);
        newgame.AddComponent<AudioSource>();
        newgame.GetComponent<AudioSource>().clip = Resources.Load(path, typeof(AudioClip)) as AudioClip;
        AudioList.Add(newgame.GetComponent<AudioSource>());
        return true;
    }

    //public bool AudioPlay(string audios)
    //{
    //    if (AudioList.Count <= 0)
    //    {
    //        return false;
    //    }
    //    foreach(AudioSource audio in AudioList)
    //    {
    //        if(audio.clip != null && audio.clip.name == audios)
    //        {
    //            audio.Play();
    //            return true;
    //        }
    //    }
    //    return false;
    //}

    //全音乐停止
    void AudioAllStop()
    {
        foreach (AudioSource audio in AudioList)
        {
            if (audio.clip != null)
            {
                audio.Stop();
            }
        }
    }

    public void BGMPlay(string name)
    {
        if(IsBGM)
        {
            foreach (Transform audio in GameObject.FindGameObjectWithTag("MainCamera").transform.FindChild("music"))
            {
                if (audio.gameObject.GetComponent<AudioSource>().clip.name == name)
                {
                    audio.gameObject.GetComponent<AudioSource>().Play();
                    audio.gameObject.GetComponent<AudioSource>().loop = true;
                }
                else
                {
                    audio.gameObject.GetComponent<AudioSource>().Stop();
                    audio.gameObject.GetComponent<AudioSource>().loop = false;
                }
            }
        }
    }

    public void BGMStop(string name)
    {
        if (IsBGM)
        {
            foreach (AudioSource audio in AudioList)
            {
                Debug.Log(audio.clip.name);
                if (audio.clip.name == name)
                {
                    Debug.Log(audio.clip.name);
                    audio.Stop();
                }
            }
        }
    }

    public void AudioAllVolume(float volume)
    {
        foreach (AudioSource audio in AudioList)
        {
            if (audio.clip != null)
            {
                audio.volume = volume;
            }
        }
    }

    public void BGMVolume(float volume)
    {
        foreach (AudioSource audio in AudioList)
        {
            if (audio.clip.name == name)
            {
                audio.volume = volume;
            }
        }
    }
}
