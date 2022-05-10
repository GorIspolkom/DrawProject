namespace Assets.Scrypts.GameData
{
    public class EnemyData
    {
        //расстояния до цели, чтобы защитать, что враг дошел
        public const float InaccuracyToTarget = 0.2f;
        //время, которое враг прячется
        public const float TimeInHideState = 2f;
        //скэйл символов над врагом
        public const float SymbolIconScale = 0.05f;
        //прозрачность символов, когда враг прячется
        public const float SymbolOnHideFadeTo = 0.2f;
        //время, которое символы затухают
        public const float TimeForHideSymbols = 0.5f;
        public const float ScaleSymbolByOrder = 1.5f;
    }
    public class PanelControllData
    {
        //время вывода панели в PanelAnim
        public const float TimePanelOutput = 1;
        //задержка вываода панели для победы/поражения
        public const float TimePanelSpawnDelay = 2.5f;
        //время затухания панели с информацией об уровне
        public const float TimeLvlInfoFade = 5;
        //кристалы для повторения уровней
        public const long DiamondForRepeatLevel = 5;
    }
    public class DrawParametrsData
    {
        public const float minScoreEnge = 0.8f;

    }
}
