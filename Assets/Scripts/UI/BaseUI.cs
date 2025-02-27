using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class BaseUI : MonoBehaviour // UI 공통 로직, UI는 특정 UIState 하나를 담당하며, 그 상태일 때만 활성화
{
    protected UIManager uiManager;

    public virtual void Init(UIManager uiManager) // uiManager의 참조를 받아둠.
    {
        this.uiManager = uiManager;
    }
    
    protected abstract UIState GetUIState(); 
    public virtual void SetActive(UIState state)
    {
        this.gameObject.SetActive(GetUIState() == state);
    }
    
}