using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class MapModeItem : MonoBehaviour {

    public bool isLock;//关卡是否锁上

    public Sprite NormalSprite;//正常显示
    public Sprite LockSprite;  //锁上显示

    public Image CurrentSprite;//当前图片
	void Awake () {
        InitVar();
	}
	
    private void InitVar()
    {
        NormalSprite = this.GetComponent<Image>().sprite;
        LockSprite = this.GetComponent<Button>().spriteState.disabledSprite;

        CurrentSprite = this.GetComponent<Image>();
        CurrentSprite.sprite = NormalSprite;

    }

    public void ShowT2LockOrF2Normal(bool isL)
    {
        //Debug.Log(this.name);
        //Debug.Log(NormalSprite);
        //Debug.Log(LockSprite);
        //Debug.Log(isL);
        if (LockSprite!=null)
        {
            if (isL)//true
            {
                //Debug.Log("锁上");
                CurrentSprite.sprite = LockSprite;
            }
        }
        if(NormalSprite!=null)
        {
            if (!isL)//false
            {
                //Debug.Log("解锁");
                CurrentSprite.sprite = NormalSprite;
            }
        }
        
    }

}
