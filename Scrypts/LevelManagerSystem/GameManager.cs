using Assets.Scrypts.Enemy;
using Assets.Scrypts.Entity;
using Assets.Scrypts.InputModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniRx;
using UnityEngine;

namespace Assets.Scrypts.LevelManagerSystem
{
    public class GameManager : MonoBehaviour
    {
        private static GameManager _instance = null;
        public static GameManager Instance => _instance;

        [SerializeField] FortressController fortPrefab;
        [SerializeField] LevelEnemyManager enemyManager;

        private List<EnemyController> enemies;
        private List<FortressController> forts;
        public EnemyController[] Enemies => enemies.ToArray();
        public FortressController[] Forts => forts.ToArray();

        private void Start()
        {
            _instance = this;
            enemies = new List<EnemyController>();
            forts = new List<FortressController>();
            InputBehaviour.Subscribe(HeatEnemy);
        }
        public void SpawnForts()
        {
            CleanFortress();
            FortressController fort = Instantiate(fortPrefab, new Vector2(0, -2f), Quaternion.identity);
            forts.Add(fort);
        }
        public void RespawnForts()
        {
            CleanFortress();
            int size = LevelData.levelData.lvlStateOnStart.fortDatas.Length;
            for (int i = 0; i < size; i++)
            {
                FortressController fort = Instantiate(fortPrefab);
                LevelData.levelData.lvlStateOnStart.fortDatas[i].InitFortress(fort);
                forts.Add(fort);
            }
        }
        public void DestroyFortress(FortressController fort)
        {
            if (LevelData.levelData.enemyCount.Value > 0)
            {
                LevelData.levelData.fortressCount.Value--;
                PathManager.pathManager.DestroyFortress(fort.transform.position);
                forts.Remove(fort);
                Destroy(fort.gameObject);
            }
        }
        private void CleanFortress()
        {
            foreach (FortressController fortress in forts)
                Destroy(fortress.gameObject);
            forts.Clear();
        }
        public void AddEnemy(EnemyController enemy)
        {
            enemies.Add(enemy);
            enemyManager.AddEnemy(enemy);
        }
        private void HeatEnemy(string c)
        {
            foreach (EnemyController enemy in enemies)
                enemy.TakeDamage(c);
        }
        public void KillEnemy(EnemyController enemy)
        {
            if(LevelData.levelData.fortressCount.Value > 0)
                LevelData.levelData.enemyCount.Value--;
            SpawnLoot(enemy);
            enemyManager.OnDestroyEnemy(enemy);
        }
        public void DestroyEnemy(EnemyController enemy)
        {
            enemies.Remove(enemy);
            enemyManager.OnDestroyEnemy(enemy);
            Destroy(enemy.gameObject);
        }
        public void DestroyAllEnemies()
        {
            foreach (EnemyController enemy in enemies)
            {
                enemyManager.OnDestroyEnemy(enemy);
                Destroy(enemy.gameObject);
            }
            enemies.Clear();
        }
        private void SpawnLoot(EnemyController enemy)
        {
            //определяем дроп
            Dictionary<ValueType, Loot> onSpawnLoot = new Dictionary<ValueType, Loot>();
            Loot[] loots = enemy.Loots;
            foreach (Loot loot in loots)
            {
                float randomValue = UnityEngine.Random.value;
                float offset = loot.propability / 2;
                if (randomValue > 0.5f - offset && randomValue < 0.5f + offset)
                {
                    ValueType valueType = loot.lootPrefab.valutType;
                    if (onSpawnLoot.ContainsKey(valueType))
                        onSpawnLoot[valueType] += loot;
                    else
                        onSpawnLoot.Add(valueType, loot);
                }
            }
            //спавним дроп
            foreach (Loot loot in onSpawnLoot.Values)
                loot.Spawn(enemy.transform.position);
        }
        private void OnDestroy()
        {
            _instance = GameObject.FindObjectOfType<GameManager>();
            InputBehaviour.UnSubscribe(HeatEnemy);
        }
    }
}
