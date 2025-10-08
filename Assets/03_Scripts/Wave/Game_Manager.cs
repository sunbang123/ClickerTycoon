using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Game_Manager : MonoBehaviour
{
    [Header("Wave Set")]
    public TextMeshProUGUI t_wave;
    public int _waveNum = 1;

    [Header("Game Set")]
    public GameObject Winpanel;

    public static Game_Manager instance;

    void Start()
    {
        t_wave.text = $"Wave {_waveNum}";
        instance = this;
    }

    public void NextWave()
    {
        _waveNum++;
        t_wave.text = $"Wave {_waveNum}";
    }

    public void WinGame()
    {
        Debug.Log("You Win!");
        Winpanel.SetActive(true);
        Invoke("LoadScene", 1f);
    }

    public void LoadScene()
    {
        SceneManager.LoadScene("Lobby");
    }
}
