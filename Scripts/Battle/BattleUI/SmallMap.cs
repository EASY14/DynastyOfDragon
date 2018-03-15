using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class SmallMap : MonoBehaviour {

    public float StartSize = 5.0f;
    public float MaxSize = 10.0f;
    public float MinSize = 2.5f;
    public Camera mapCamera;
    public GameObject map;
    public GameObject playerIcon;
    public GameObject monsterIconParent;
    public GameObject monsterIcon;
    private Transform playerTransform;

    private static SmallMap _instance;

    private ArrayList iconList = new ArrayList();

    public static SmallMap Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindGameObjectWithTag(TagManager.smallmap).GetComponent<SmallMap>();
            }
            return _instance;
        }
    }

    private class MonsterData : Object
    {
        public GameObject Monster;
        public GameObject monsterIcon;
    }

	// Use this for initialization
	void Start () {
        playerTransform = GameObject.FindGameObjectWithTag(TagManager.player).transform;
        mapCamera.orthographicSize = StartSize;
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 cameraPos= new Vector3(playerTransform.position.x, playerTransform.position.y + 100, playerTransform.position.z);
        mapCamera.transform.position = cameraPos;

        UpdateMonsterIcon();
	}

    void UpdateMonsterIcon()
    {
        playerIcon.transform.rotation = Quaternion.Euler(new Vector3(0.0f, 0.0f, -playerTransform.eulerAngles.y));

        foreach(MonsterData m in iconList)
        {
            Vector3 toMonster = m.Monster.transform.position - playerTransform.position;
            float size = mapCamera.orthographicSize;
            Rect parentRect = monsterIconParent.GetComponent<RectTransform>().rect;
            m.monsterIcon.GetComponent<RectTransform>().anchoredPosition = new Vector2(
                (toMonster.x / size) * (parentRect.width * 0.5f),
                (toMonster.z / size) * (parentRect.height * 0.5f));
        }
    }

    public void AddMonsterIcon(GameObject mon)
    {
        if (mon == null)
            return;

        MonsterData data = new MonsterData();
        data.Monster = mon;
        data.monsterIcon = Instantiate(monsterIcon);
        data.monsterIcon.transform.SetParent(monsterIconParent.transform);
        iconList.Add(data);
    }

    public void RemoteMonsterIcon(GameObject mon)
    {
        if (mon == null)
            return;
        int index = 0;
        for (int i = 0; i < iconList.Count; i++)
        {
            if (((MonsterData)(iconList[i])).Monster == mon)
            {
                index = i;
                break;
            }
        }

        Destroy(((MonsterData)(iconList[index])).monsterIcon);
        iconList.RemoveAt(index);
        

    }

    public void IncreaseSize(float size)
    {
        mapCamera.orthographicSize += size;
        if (mapCamera.orthographicSize > MaxSize)
            mapCamera.orthographicSize = MaxSize;
    }

    public void DecreaseSize(float size)
    {
        mapCamera.orthographicSize -= size;
        if (mapCamera.orthographicSize < MinSize)
            mapCamera.orthographicSize = MinSize;
    }
}
