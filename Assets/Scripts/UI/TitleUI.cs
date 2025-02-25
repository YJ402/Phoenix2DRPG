using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class TitleUI : BaseUI
{
    Button startButton;
    
    protected override UIState GetUIState()
    {
        return UIState.Title;
    }
    
    public override void Init(UIManager uiManager)
    {
        base.Init(uiManager);
        
        startButton = transform.Find("StartButton").GetComponent<Button>();
        
        startButton.onClick.AddListener(OnStartButtonClicked);
    }
    
    // 버튼 클릭 시 호출되는 메서드
    private void OnStartButtonClicked()
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