using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackButton : MonoBehaviour
{
    
    public void Btn_BackMainScene() {
        SceneManager.LoadScene("SampleScene");

    }

}
