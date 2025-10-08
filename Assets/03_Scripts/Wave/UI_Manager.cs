using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UI_Manager : MonoBehaviour
{
    public TextMeshProUGUI t_wave;
    public int _waveNum = 1;
    void Start()
    {
        t_wave.text = $"Wave {_waveNum}";
    }
}
