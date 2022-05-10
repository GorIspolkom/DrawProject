using UnityEngine;

namespace Assets.Scrypts.LevelManagerSystem
{
    [CreateAssetMenu(fileName = "SymbolPreset", menuName = "GamePresets/SymbolList", order = 1)]
    class PlayableSymbolList : ScriptableObject
    {
        public string[] symbols;
    }
}
