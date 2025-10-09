using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Game_Manager : MonoBehaviour
{
    [Header("Wave Set")]
    public TextMeshProUGUI t_wave;
    public int _waveNum;

    [Header("Game Set")]
    public GameObject Winpanel;

    [Header("Player Set")]
    public int _playerLv;

    public static Game_Manager instance;

    private void Awake()
    {
        _waveNum = DataManager.instance.playerData.GetData(GameData.Wave_Level);
        _playerLv = DataManager.instance.playerData.GetData(GameData.Player_Level);
    }

    void Start()
    {
        t_wave.text = $"Wave {_waveNum}";
        instance = this;
    }

    public void NextWave()
    {
        DataManager.instance.playerData.SetData(GameData.Wave_Level, ++_waveNum);
    }
    public void LevelUp()
    {
        DataManager.instance.playerData.SetData(GameData.Player_Level, ++_playerLv);
        Debug.Log($"Player Level Up! New Level: {_playerLv}");
    }

    public void WinGame()
    {
        Debug.Log("You Win!");
        LevelUp();
        NextWave();
        Winpanel.SetActive(true);
        Invoke("LoadScene", 3f);
    }

    public void LoadScene()
    {
        SceneManager.LoadScene("Lobby");
    }
}
