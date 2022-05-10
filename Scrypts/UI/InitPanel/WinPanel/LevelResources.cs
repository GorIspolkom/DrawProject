using Assets.Scrypts.LevelManagerSystem;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scrypts.UI
{
    public class LevelResources : MonoBehaviour
    {
        [SerializeField] ValutType valut;
        void Start()
        {
            Text text = GetComponent<Text>();
            text.text = LevelData.levelData.GetValut(valut).ToString();
        }
    }
}