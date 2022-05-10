using Assets.Scrypts.Enemy;
using Assets.Scrypts.Entity;
using Assets.Scrypts.GameData;
using Assets.Scrypts.InputModule;
using Assets.Scrypts.UI;
using System;
using UniRx;
using UnityEngine;

namespace Assets.Scrypts.LevelManagerSystem
{
    class LevelManager : MonoBehaviour
    {
        [SerializeField] FortressController fortressPrefab;
        [SerializeField] PlayerAnimatorController playerAnimator;
        [SerializeField] LvlInfo lvlInfoOtputter;
        [SerializeField] Spawner spawner;
        [SerializeField] DrawInput drawInput;
        private LevelPreset levelPreset;

        private LevelPreset NextLevel =>
            Resources.Load<LevelPreset>($"{Paths.LEVEL_PRESETS}Level{LevelData.levelData.currentLvl + 1}");
        private LevelPreset FirstLevel =>
            Resources.Load<LevelPreset>($"{Paths.LEVEL_PRESETS}Level1");

        private void Awake()
        {
            //грузим сохранение
            Profile.LoadData();
            //создаем данные уровня
            LevelData.InitData();

            InputBehaviour.Subscribe((string _) => playerAnimator.SetAttack());
            //подписка на победу и поражение
            LevelData.levelData.fortressCount.Where(x => x == 0).Subscribe(OnLose);
            LevelData.levelData.enemyCount.Where(x => x == 0).Subscribe(OnWin);
        }
        //загрузка следующего уровня
        public void LoadNextLevel()
        {
            levelPreset = NextLevel;
            LevelData.levelData.currentLvl++;
            //проверка на существование следующего уровня
            if (levelPreset == null)
            {
                LevelData.levelData.currentLvl = 1;
                levelPreset = FirstLevel;
            }
            LoadLevel();
        }
        public void LoadFirstLevel()
        {
            LevelData.levelData.currentLvl = 1;

            GameManager.Instance.SpawnForts();
            levelPreset = FirstLevel;

            LoadLevel();
            LevelData.levelData.SetStartValutes();
        }
        //перезапуск прошлого состояния уровня
        public void ReloadLevel()
        {
            GameManager.Instance.RespawnForts();
            LevelData.levelData.ResetData();
            levelPreset = Resources.Load<LevelPreset>($"{Paths.LEVEL_PRESETS}Level{LevelData.levelData.currentLvl}");
            LoadLevel();
        }
        private void LoadLevel()
        {
            DeserializeLevel();
            Vector2[] points = levelPreset.SpawnShelters();
            PathManager.pathManager.CreateNodeList(points);
            LevelData.levelData.InitLevelData(levelPreset);
            drawInput.InitSymbolTrainingAssets();

            lvlInfoOtputter.OutputLvlInfo();
            spawner.InitUnitInfos(levelPreset.unitsInfos);
        }
        //уничтожение укрытий и врагов
        private void DeserializeLevel()
        {
            playerAnimator.Reset();

            GameObject shelters = GameObject.Find("Shelters");
            Destroy(shelters);
            GameManager.Instance.DestroyAllEnemies();

            //прячем панель с инфоормацией об уровне
            lvlInfoOtputter.Destruct();
        }
        private void OnLose(int count)
        {
            if(count == 0)
            {
                playerAnimator.SetLose();
                levelPreset = FirstLevel;
                drawInput.Clear();
                Debug.Log("You Lose!");
            }
        }
        private void OnWin(int count)
        {
            if (count == 0)
            {
                playerAnimator.SetVictory();
                Profile.UpdateLevel(LevelData.levelData.currentLvl);
                Profile.SaveData();
                drawInput.Clear();
                Debug.Log("You Win!");
            }
        }
        private void OnDestroy()
        {
            Profile.SaveData();
        }
    }
}
