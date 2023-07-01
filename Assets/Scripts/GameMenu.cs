using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMenu : MonoBehaviour
{
    public void ContinueGame(){
        this.gameObject.SetActive(false);
    }

    public void ExitGame(){
        Application.Quit();
    }

    private void OnEnable() {
        Time.timeScale = 0;
    }

    private void OnDisable() {
        Time.timeScale = 1;
    }
}
