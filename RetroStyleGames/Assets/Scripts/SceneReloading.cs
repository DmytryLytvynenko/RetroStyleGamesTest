using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneReloading : MonoBehaviour
{
    public void LoadSn(int sceneNumber)
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(sceneNumber);
    }
}
