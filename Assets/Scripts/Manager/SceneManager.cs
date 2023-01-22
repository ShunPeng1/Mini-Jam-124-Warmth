using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityUtilities;

public class SceneManager : SingletonMonoBehaviour<SceneManager>
{
    
    
    public void RestartScene(){
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
    }

    public void GetNextScene(){
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex+1);
    }

    public void GetHomeScene(){
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }
}
