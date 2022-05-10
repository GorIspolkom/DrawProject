using Assets.Scrypts.LevelManagerSystem;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using Assets.Scrypts.GameData;


namespace Assets.Scrypts.UI
{
    public class LvlInfo : MonoBehaviour
    {
        public void OutputLvlInfo()
        {
            Text lvlType = transform.GetChild(0).GetComponent<Text>();
            Text lvl = transform.GetChild(1).GetComponent<Text>();

            switch (LevelData.levelData.levelType)
            {
                case LevelType.Normal:
                    lvlType.text = "";
                    break;
                case LevelType.Bonus:
                    lvlType.text = "Bonus";
                    break;
                case LevelType.Boss:
                    lvlType.text = "Boss";
                    break;
            }

            lvl.text = "LEVEL " + LevelData.levelData.currentLvl;

            AnimatFade();
        }
        public void AnimatFade()
        {
            Text[] texts = transform.GetComponentsInChildren<Text>();
            foreach (Text text in texts)
            {
                text.color = new Color(text.color.r, text.color.g, text.color.b, 1);
                text.DOFade(0, PanelControllData.TimeLvlInfoFade);
            }
        }
        public void Destruct()
        {
            for (int i = 0; i < 3; i++)
            {
                Text text = transform.GetChild(i).GetComponent<Text>();
                text.DOKill();
            }
        }
    }
}