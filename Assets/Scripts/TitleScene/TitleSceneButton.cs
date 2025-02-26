using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleSceneButton : MonoBehaviour
{
    public void OnStartButtonClick()
    {
        SceneManager.LoadScene("LobbyScene");
    }

    public void OnExitButtonClick()
    {

    }
}
