using System.Collections.Generic;
using UnityEngine;

public enum GameData
{
    Player_Level,
    Wave_Level,
    Gold,
    ClickPower,
    ClickLevel,
    ClickCost,
    AutoGoldA_Level,
    AutoGoldA_Cost,
    AutoGoldA_Amount,
    AutoGoldB_Level,
    AutoGoldB_Cost,
    AutoGoldB_Amount
}

public class PlayerData
{
    public Dictionary<GameData, int> battleData = new Dictionary<GameData, int>()
    {
        { GameData.Player_Level, 1 },
        { GameData.Wave_Level, 1 },
        { GameData.Gold, 1000 },
        { GameData.ClickPower, 1 },
        { GameData.ClickLevel, 1 },
        { GameData.ClickCost, 10 },
        { GameData.AutoGoldA_Level, 0 },
        { GameData.AutoGoldA_Cost, 100 },
        { GameData.AutoGoldA_Amount, 1 },
        { GameData.AutoGoldB_Level, 0 },
        { GameData.AutoGoldB_Cost, 500 },
        { GameData.AutoGoldB_Amount, 10 }
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
