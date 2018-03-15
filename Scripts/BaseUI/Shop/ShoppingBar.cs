using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Collections.Generic;

public class ShoppingBar : MonoBehaviour,IEndDragHandler,IBeginDragHandler{

    private ScrollRect rect;

    private float[] BarRange = new float[] { 0f, 0.194f, 0.386f, 0.579f, 0.772f, 0.964f };
    private int index;
    private float rectIndex;
    private bool isLerp;

    public float lerpTime = 3.0f;

    //private Toggle Togglebtn1;
    //private Toggle Togglebtn2;
    //private Toggle Togglebtn3;
    //private Toggle Togglebtn4;
    //private Toggle Togglebtn5;
    //private Toggle Togglebtn6;

    public List<Toggle> ToggleList;
    private int TogglebtnMax=6;
	// Use this for initialization
	void Start () {
        InitVar();
	}

    void InitVar()
    {
        rect = GetComponent<ScrollRect>();
        index = 0;
        rectIndex = 0.0f;

        isLerp = false;

        //Togglebtn1 = GameObject.Find("Toggle1").GetComponent<Toggle>();
        //Togglebtn2 = GameObject.Find("Toggle2").GetComponent<Toggle>();
        //Togglebtn3 = GameObject.Find("Toggle3").GetComponent<Toggle>();
        //Togglebtn4 = GameObject.Find("Toggle4").GetComponent<Toggle>();
        //Togglebtn5 = GameObject.Find("Toggle5").GetComponent<Toggle>();
        //Togglebtn6 = GameObject.Find("Toggle6").GetComponent<Toggle>();

        //Togglebtn1.onValueChanged.AddListener(Toggle1OnClick);
        //Togglebtn2.onValueChanged.AddListener(Toggle2OnClick);
        //Togglebtn3.onValueChanged.AddListener(Toggle3OnClick);
        //Togglebtn4.onValueChanged.AddListener(Toggle4OnClick);
        //Togglebtn5.onValueChanged.AddListener(Toggle5OnClick);
        //Togglebtn6.onValueChanged.AddListener(Toggle6OnClick);
        for (int t = 1; t <= TogglebtnMax;t++ )
        {
            Toggle Tog = GameObject.Find("Toggle"+t).GetComponent<Toggle>();
            ToggleList.Add(Tog);
        }
        for (int i = 0; i < ToggleList.Count; i++)
        {
            Toggle Tog = ToggleList[i];
            int TogNameIndex = i + 1;
            Tog.onValueChanged.AddListener(delegate(bool isOn) { this.myToggleClick(Tog.name, isOn); });

        }
    }
	
	// Update is called once per frame
	void Update () {
        if (isLerp)
        {
            rect.horizontalNormalizedPosition = Mathf.Lerp(rect.horizontalNormalizedPosition, rectIndex, lerpTime * Time.deltaTime);
        }
	}

    public void OnEndDrag(PointerEventData eventData)
    {
        isLerp = true;
        ChangePage();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        isLerp = false;
    }


    private void setTogglesIsOn(int index)
    {
        //if (index == 5)
        //{
        //    Togglebtn6.isOn = true;
        //}
        //if (index == 4)
        //{
        //    Togglebtn5.isOn = true;
        //}
        //if (index == 3)
        //{
        //    Togglebtn4.isOn = true;
        //}
        //if(index==2)
        //{
        //    Togglebtn3.isOn = true;
        //}
        //if (index == 1)
        //{
        //    Togglebtn2.isOn = true;
        //}
        //if (index == 0)
        //{
        //    Togglebtn1.isOn = true;
        //}
        ToggleList[index].isOn = true;
    }
    
        
    public void myToggleClick(string TogName, bool isOn)
    {
        int TNindex = GetTogNameIndexByTogName(TogName);//1-6
        //Debug.Log(TogName);
        if (isOn)
        {
            rectIndex = BarRange[TNindex-1];
            isLerp = true;
        }
    }
    //通过Toggle名字获取名字后标
    public int GetTogNameIndexByTogName(string TogName)
    {
        int index = 0;
        switch (TogName)
        {
            case "Toggle1":
                index = 1;
                break;
            case "Toggle2":
                index = 2;
                break;
            case "Toggle3":
                index = 3;
                break;
            case "Toggle4":
                index = 4;
                break;
            case "Toggle5":
                index = 5;
                break;
            case "Toggle6":
                index = 6;
                break;
            default:
                break;
        }
        return index;
    }
    
    public void GoUP()
    {
        if (index > 0)
        {
        	index -= 1;
            ToggleList[index].isOn = true;
            //myToggleClick("Toggle" + index.ToString(), true);
        }

    }


    public void GoDOWN()
    {
        if (index < 5)
        {
            index += 1;
            ToggleList[index].isOn = true;
            //myToggleClick("Toggle" + index.ToString(), true);
        }
    }

    public void ChangePage()
    {
        float rectX = rect.horizontalNormalizedPosition;
        float Leftoffset = Mathf.Abs(BarRange[index] - rectX);

        for (int i = 0; i < BarRange.Length; i++)
        {
            float Rightoffset = Mathf.Abs(BarRange[i] - rectX);
            if (Rightoffset < Leftoffset)
            {
                index = i;
                Leftoffset = Rightoffset;
                break;
            }
        }
        rectIndex = BarRange[index];
        setTogglesIsOn(index);
    }

    //public void Toggle1OnClick(bool isOn)
    //{
    //    if (isOn)
    //    {
    //        rectIndex = BarRange[0];
    //        isLerp = true;
    //    }
    //}
    //public void Toggle2OnClick(bool isOn)
    //{
    //    if (isOn)
    //    {
    //        rectIndex = BarRange[1];
    //        isLerp = true;
    //    }
    //}
    //public void Toggle3OnClick(bool isOn)
    //{
    //    if (isOn)
    //    {
    //        rectIndex = BarRange[2];
    //        isLerp = true;
    //    }
    //}
    //public void Toggle4OnClick(bool isOn)
    //{
    //    if (isOn)
    //    {
    //        rectIndex = BarRange[3];
    //        isLerp = true;
    //    }
    //}
    //public void Toggle5OnClick(bool isOn)
    //{
    //    if (isOn)
    //    {
    //        rectIndex = BarRange[4];
    //        isLerp = true;
    //    }
    //}
    //public void Toggle6OnClick(bool isOn)
    //{
    //    if (isOn)
    //    {
    //        rectIndex = BarRange[5];
    //        isLerp = true;
    //    }
    //}
}
