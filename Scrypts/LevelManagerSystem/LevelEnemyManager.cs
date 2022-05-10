using Assets.Scrypts.Enemy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scrypts.LevelManagerSystem
{
    public enum RespawnArea
    {
        Left,
        Right,
        Top
    }
    class LevelEnemyManager : MonoBehaviour
    {
        [SerializeField] Transform pointerPrefab;

        private List<Transform> enemies;
        private List<Transform> enemiesOutSide;
        private List<Transform> pointers;

        private Vector2 leftBottom, rightTop;

        private void Start()
        {
            enemies = new List<Transform>();
            enemiesOutSide = new List<Transform>();
            pointers = new List<Transform>();

            leftBottom = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
            rightTop = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));
        }
        private void Update()
        {
            //обновляет позиции и состояния поинтеров
            for (int i = 0; i < enemiesOutSide.Count; i++)
            {
                Vector2 position = enemiesOutSide[i].position;
                if (!(position.x > rightTop.x || position.x < leftBottom.x || position.y > rightTop.y))
                {
                    Destroy(pointers[i].gameObject);
                    enemies.Add(enemiesOutSide[i]);
                    enemiesOutSide.RemoveAt(i);
                    pointers.RemoveAt(i);
                }
                else
                {
                    CalcPointerPosition(pointers[i], position);
                }
            }
            //проверяет позицию врагов на внутри/вне экрана
            for (int i = 0; i < enemies.Count; i++)
            {
                Vector2 position = enemies[i].position;
                if (position.x > rightTop.x || position.x < leftBottom.x || position.y > rightTop.y)
                {
                    pointers.Add(Instantiate(pointerPrefab, transform));
                    CalcPointerPosition(pointers.Last(), position);
                    enemiesOutSide.Add(enemies[i]);
                    enemies.RemoveAt(i);
                }
            }
        }
        private void CalcPointerPosition(Transform pointer, Vector2 position)
        {
            //считаем позицию
            Vector2 newPosition;
            newPosition.x = Mathf.Clamp(position.x, leftBottom.x, rightTop.x);
            newPosition.y = Mathf.Clamp(position.y, leftBottom.y, rightTop.y);
            pointer.position = newPosition;

            //считаем угол
            float angle = Vector2.Angle(newPosition, Vector2.down);
            if (position.x < 0)
                angle *= -1;
            Quaternion target = Quaternion.Euler(0, 0, angle);
            pointer.rotation = target;
        }
        public void AddEnemy(EnemyController enemy)
        {
            enemies.Add(enemy.transform);
        }
        public void OnDestroyEnemy(EnemyController enemy)
        {
            Transform enemyTransform = enemy.transform;
            if (enemies.Contains(enemyTransform))
                enemies.Remove(enemyTransform);
            else
            {
                int index = enemiesOutSide.IndexOf(enemyTransform);
                if(index > -1)
                {
                    Destroy(pointers[index].gameObject);
                    pointers.RemoveAt(index);
                    enemiesOutSide.RemoveAt(index);
                }
            }
        }
    }
}
