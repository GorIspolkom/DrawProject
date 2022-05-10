using Assets.Scrypts.LevelManagerSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scrypts.Enemy
{
    class Spawner : MonoBehaviour
    {
        [SerializeField] float depth;
        private List<UnitInfos> units;

        private Vector2 leftBottom, rightTop;
        void Start()
        {
            units = new List<UnitInfos>();
            rightTop = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));
            leftBottom = Camera.main.ViewportToWorldPoint(new Vector2(0, 0.5f)); 
        }
        public void InitUnitInfos(UnitInfos[] enemyPrefabs)
        {
            UnitInfos[] unitsArray = (UnitInfos[])enemyPrefabs.Clone();
            int size = unitsArray.Length;
            for (int i = 0; i < size; i++)
            {
                for (int j = i; j < size; j++)
                    if (unitsArray[i].respawnTimeout > unitsArray[j].respawnTimeout)
                    {
                        UnitInfos unit = unitsArray[i];
                        unitsArray[i] = unitsArray[j];
                        unitsArray[j] = unit;
                    }
                unitsArray[i].respawnTimeout += Time.time;
            }
            units = new List<UnitInfos>(unitsArray);
        }
        private void Update()
        {
            if (units.Count > 0)
                if (units[0].respawnTimeout < Time.time)
                    while (units[0].respawnTimeout < Time.time)
                    {
                        SpawnEnemy(units[0]);
                        units.RemoveAt(0);
                        if (units.Count == 0)
                            break;
                    }
        }
        private void SpawnEnemy(UnitInfos unit)
        {
            Vector2 position = Vector2.zero;
            switch (unit.respawnArea)
            {
                case RespawnArea.Top:
                    position.y = UnityEngine.Random.Range(0, depth) + rightTop.y;
                    position.x = UnityEngine.Random.Range(leftBottom.x, rightTop.x);
                    break;
                case RespawnArea.Left:
                    position.y = UnityEngine.Random.Range(leftBottom.y, rightTop.y);
                    position.x = leftBottom.x - UnityEngine.Random.Range(0, depth);
                    break;
                case RespawnArea.Right:
                    position.y = UnityEngine.Random.Range(leftBottom.y, rightTop.y);
                    position.x = rightTop.x + UnityEngine.Random.Range(0, depth);
                    break;
            }
            GameManager.Instance.AddEnemy(Instantiate(unit.enemyPrefab, position, Quaternion.identity, transform));
        }
    }
}
