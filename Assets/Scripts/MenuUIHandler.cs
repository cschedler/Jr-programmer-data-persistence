using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class MenuUIHandler : MonoBehaviour
{
    public TextMeshProUGUI userInput;
    
    public void StartGame()
    {
        SceneManager.LoadScene(1);
        StandingController.standingInstance.userStanding.userName = userInput.text;
        StandingController.standingInstance.SaveStanding();
    }

    public void Exit()
    {
#if UNITY_EDITOR
            EditorApplication.ExitPlaymode();
#else
        Application.Quit(); // original code to quit Unity player
#endif
    }

}
