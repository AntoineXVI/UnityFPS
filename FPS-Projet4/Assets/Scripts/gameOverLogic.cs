using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class gameOverLogic : MonoBehaviour
{
    
    public void OverUI()
    {
        SceneManager.LoadScene("GameOver");
    }
}