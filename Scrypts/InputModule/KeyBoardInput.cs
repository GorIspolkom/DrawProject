using Assets.Scrypts.LevelManagerSystem;
using System.Linq;
using UnityEngine;

namespace Assets.Scrypts.InputModule
{
    public class KeyBoardInput : InputBehaviour
    {
        protected override string InputSymbol()
        {
            return Input.inputString.ToString();
        }
        void Update()
        {
            if (Input.anyKeyDown)
                if(LevelData.levelData.symbols.Contains(Input.inputString.ToLower()))
                    OnSymbolInput();
        }
    }
}
