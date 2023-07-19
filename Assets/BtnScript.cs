using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BtnScript : MonoBehaviour
{
    public void playBtn(){
        SceneManager.LoadScene("Room", LoadSceneMode.Single);
    }

    public void quit(){
        Application.Quit();
    }

    public void playAgainBtn(){
        SceneManager.LoadScene("Entry", LoadSceneMode.Single);
    }
}
