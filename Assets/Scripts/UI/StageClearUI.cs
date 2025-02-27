using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StageClearUI : BaseUI
{
    [Header("Overlay UIs")]
    [SerializeField] private GameObject stageClearUI;
    
    Button exitButton;
    
    protected override UIState GetUIState()
    {
        return UIState.StageClear;
    }

    public override void Init(UIManager uiManager)
    {
        base.Init(uiManager);
        
        exitButton = transform.GetComponentInChildren<Button>();
        
        exitButton.onClick.AddListener(OnExitButtonClicked);
    }
    
    // 버튼 클릭 시 호출되는 메서드
    private void OnExitButtonClicked()
    {
        Debug.Log("OnStartButtonClicked");
        SceneManager.LoadScene("LobbyScene");
    }

    private void Update()
    {
        // 옵션: 스페이스바를 눌러서 씬 이동할 수도 있음
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene("LobbyScene");
        }
    }
    
}   
