using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; //씬 관리 인클루드

public class StartManager : MonoBehaviour
{
    public void GameStart()
    {
        SceneManager.LoadScene("GameScene");
        Time.timeScale = 1;
    }

    public void gameExit()
    {
        Application.Quit();
    }

    // Start is called before the first frame update
    void Start()
    {
        Screen.SetResolution(1920, 1200, true); //풀스크린이 가능하게
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
