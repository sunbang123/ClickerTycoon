using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchToStart : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))     //마우스 클릭
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("Lobby");
        }
    }
}
