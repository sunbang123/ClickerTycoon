using System.Collections.Generic;
using UnityEngine;

public enum GameData // ���� ������ �ڷ���
{
    Player_Level,
    Wave_Level,
    Gold,
}

public class PlayerData
{
    public Dictionary<GameData, int> battleData = new Dictionary<GameData, int>()
    {
        { GameData.Player_Level, 1 },
        { GameData.Wave_Level, 1 },
        { GameData.Gold, 1000 },
    };

    public int GetData(GameData config)
    {
        return battleData[config];
    }

    public void SetData(GameData config, int value)
    {
        battleData[config] = value;
    }
}
