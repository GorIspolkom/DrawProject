using Assets.Scrypts.Entity;
using Assets.Scrypts.GameData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniRx;
using UnityEngine;

namespace Assets.Scrypts.LevelManagerSystem
{
    public enum LevelType
    {
        Normal,
        Bonus,
        Boss
    }
    public enum ValutType
    {
        Coin,
        Crystal
    }
    struct FortressData
    {
        public float hp;
        public Vector2 position;
        public float regeneratPerSecond;
        public FortressData(FortressController fort)
        {
            hp = fort.CurHp;
            Debug.Log(fort.CurHp);
            position = fort.transform.position;
            regeneratPerSecond = fort.RegenPerSecond;
        }
        public void InitFortress(FortressController fort)
        {
            fort.CurHp = hp;
            fort.transform.position = position;
            fort.RegenPerSecond = regeneratPerSecond;
        }
    }
    //данные для перезагрузки уровня
    struct LvlStateOnStart
    {
        public FortressData[] fortDatas;
        public LvlStateOnStart(FortressController[] forts)
        {
            fortDatas = new FortressData[forts.Length];
            for (int i = 0; i < forts.Length; i++)
                fortDatas[i] = new FortressData(forts[i]);
        }
    }
    class LevelData
    {
        private static LevelData _levelData;
        public static LevelData levelData
        {
            get
            {
                if (_levelData == null)
                    _levelData = new LevelData();
                return _levelData;
            }
            set
            {
                _levelData = value;
            }
        }

        //текущий уровень
        public int currentLvl;
        //символы уровня
        public string[] symbols;
        //спрайты символов
        private Dictionary<string, Sprite> symbolSprites;
        //тип уровня
        public LevelType levelType;
        //ресурсы за уровень
        public readonly LongReactiveProperty[] lvlValutes;
        //количество врагов и фортов
        public readonly IntReactiveProperty enemyCount, fortressCount;
        //стартовые значения при запуске игры
        public long[] startValuts;
        public long LvlCoins { get => lvlValutes[(int)ValutType.Coin].Value; set => lvlValutes[(int)ValutType.Coin].SetValueAndForceNotify(value); }
        public long LvlCrystals { get => lvlValutes[(int)ValutType.Crystal].Value; set => lvlValutes[(int)ValutType.Crystal].SetValueAndForceNotify(value); }
        //данные на старт уровня
        public LvlStateOnStart lvlStateOnStart;

        public Sprite GetSpriteOf(string symbolName) =>
            symbolSprites[symbolName];

        public void AddValute(ValutType valutType, long add) =>
            lvlValutes[(int)valutType].Value += add;
        public long GetValut(ValutType valutType)
        {
            return lvlValutes[(int)valutType].Value;
        }
        public void SubscribeOnValut(ValutType valutType, Action<long> onChange) =>
            lvlValutes[(int)valutType].Subscribe(onChange);

        private LevelData()
        {
            levelType = LevelType.Normal;
            currentLvl = 1;
            symbols = new string[] { "a" };

            symbolSprites = new Dictionary<string, Sprite>();
            Sprite[] sprites = Resources.LoadAll<Sprite>(Paths.SYMBOL_SPRITES);
            foreach (Sprite sprite in sprites)
                symbolSprites.Add(sprite.name.ToLower(), sprite);

            lvlValutes = new LongReactiveProperty[2];
            foreach (ValutType valutType in Enum.GetValues(typeof(ValutType)))
                lvlValutes[(int)valutType] = new LongReactiveProperty(0);

            enemyCount = new IntReactiveProperty(1);
            fortressCount = new IntReactiveProperty(1);
        }
        //устанавливает значение ресурсов на начало игры
        public void SetStartValutes()
        {
            startValuts = new long[2];
            foreach (ValutType valutType in Enum.GetValues(typeof(ValutType)))
                startValuts[(int)valutType] = Profile.profileData.GetValut(valutType);
        }
        //инициализирует данные об уровне
        public void InitLevelData(LevelPreset levelPreset)
        {
            levelType = levelPreset.levelType;

            symbols = levelPreset.Symbols.Clone() as string[];
            for (int i = 0; i < symbols.Length; i++)
                symbols[i] = symbols[i].ToLower();

            foreach (ValutType valutType in Enum.GetValues(typeof(ValutType)))
                lvlValutes[(int)valutType].Value = 0;

            lvlStateOnStart = new LvlStateOnStart(GameManager.Instance.Forts);

            enemyCount.Value = levelPreset.unitsInfos.Length;
            fortressCount.Value = PathManager.pathManager.GetFortress().Length;
        }
        public void ResetData()
        {
            foreach (ValutType valutType in Enum.GetValues(typeof(ValutType)))
                lvlValutes[(int)valutType].Value = 0;
        }
        //пересоздает уровень
        public static void InitData() =>
                levelData = new LevelData();
    }
}
