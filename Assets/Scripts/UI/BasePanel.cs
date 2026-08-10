using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasePanel : MonoBehaviour
{

    public CanvasGroup _cg;

    public void Init()
    {
        _cg = GetComponent<CanvasGroup>();
    }
    
    //打开
    public void OpenPanel()
    {
        _cg.alpha = 1;
        _cg.interactable = true;
        _cg.blocksRaycasts = true;
        
        //切换指针为ui
        GameManager.Instance.SetUICursor();
    }
    
    //关闭
    public void ClosePanel()
    {
        _cg.alpha = 0;
        _cg.interactable = false;
        _cg.blocksRaycasts = false;
        
        
        GameManager.Instance.SetShootCursor();
    }

    
}
