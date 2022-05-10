using Assets.Scrypts.LevelManagerSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scrypts.GameData
{
    //данные игрока
    [Serializable]
    struct ProfileData
    {
        public long coin, crystal;
        public int maxLvl;

        public ProfileData(long coin, long crystal, int maxLvl)
        {
            this.coin = coin;
            this.crystal = crystal;
            this.maxLvl = maxLvl;
        }
        public long GetValut(ValutType valutType)
        {
            switch (valutType)
            {
                case ValutType.Coin:
                    return coin;
                case ValutType.Crystal:
                    return crystal;
            }
            return 0;
        }
    }

    //класс с хранением, сохранением и обработкой данных игрока
    static class Profile
    {
        private static ProfileData _profileData;
        public static ProfileData profileData { get => _profileData; }
        public static void AddValut(long value, ValutType valutType)
        {
            switch (valutType)
            {
                case ValutType.Coin:
                    _profileData.coin += value;
                    break;
                case ValutType.Crystal:
                    _profileData.crystal += value;
                    break;
            }
        }
        public static void UpdateLevel(int lvl)
        {
            if (_profileData.maxLvl < lvl)
                _profileData.maxLvl = lvl;
        }
        public static void SaveData()
        {
            PlayerPrefs.SetString("Coins", _profileData.coin.ToString());
            PlayerPrefs.SetString("Crystals", _profileData.crystal.ToString());
            PlayerPrefs.SetInt("BestScore", _profileData.maxLvl);
            PlayerPrefs.Save();
        }
        public static void LoadData()
        {
            if (PlayerPrefs.HasKey("Coins"))
            {
                long coins = long.Parse(PlayerPrefs.GetString("Coins"));
                long crystals = long.Parse(PlayerPrefs.GetString("Crystals"));
                int maxLvl = PlayerPrefs.GetInt("BestScore");
                _profileData = new ProfileData(coins, crystals, maxLvl);
            }
            else
                _profileData = new ProfileData(0, 10, 0);
        }

        public static void Reset()
        {
            _profileData = new ProfileData(0, 10, 0);
            SaveData();
        }
    }
}
