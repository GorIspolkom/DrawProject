using Assets.Scrypts.Entity;
using Assets.Scrypts.GameData;
using Assets.Scrypts.LevelManagerSystem;
using UniRx;
using UnityEngine;

namespace Assets.Scrypts.Enemy
{
    enum WalkType
    {
        Linear,
        Hidden
    }
    public class WalkEnemy : EnemyController
    {
        [SerializeField] WalkType walkType;
        [SerializeField] protected float velocity;
        private Vector2 spawnPoint;
        private Vector2[] points;
        private int curPoint;

        private void Start()
        {
            spawnPoint = _transform.position;
            LevelData.levelData.fortressCount.ObserveEveryValueChanged(x => x.Value)
                .Subscribe((int count) =>
                {
                    if (count > 0)
                    {
                        switch (walkType)
                        {
                            case WalkType.Linear:
                                points = new Vector2[1];
                                points[0] = PathManager.pathManager.ClosestFortress(transform.position);
                                break;
                            case WalkType.Hidden:
                                points = PathManager.pathManager.SearchPath(transform.position);
                                break;
                        }
                        if (!(State is AttackState))
                        {
                            State = new WalkToTargetState(anim, _transform, points[0], velocity);
                            State.Subscribe(() => curPoint++);
                            curPoint = 0;
                        }
                    }
                    else
                        EnemyWin();
                })
                .AddTo(this);
        }
        private void EnemyWin()
        {
            anim.SetBool(EnemyStateNames.SELEBRATE, true);
            HpStruct.SwitchHide();
            State = new WalkToTargetState(anim, transform, spawnPoint, velocity * 1.5f);
            State.Subscribe(() => GameManager.Instance.DestroyEnemy(this));
        }
        protected override void StateMachine()
        {
            //проверка жизней
            if (HpStruct.isAlive)
            {
                //состояние когда лимит атак врага достигнут
                //уходит с наворованным
                if (AttackInfo.attackCount == 0)// && !HpStruct.isHide)
                {
                    EnemyWin();
                    if (PathManager.pathManager.GetFortress().Length > 0)
                        //уменьшаем счетчик врагов
                        LevelData.levelData.enemyCount.Value--;
                }
                //Если путь ломанный, то прячется
                else if (anim.GetBool("isMove"))
                    State = new HideState(anim, HpStruct, EnemyData.TimeInHideState);
                //если цель не достигнута
                else if (points.Length > curPoint)
                {
                    State = new WalkToTargetState(anim, transform, points[curPoint], velocity);
                    State.Subscribe(() => curPoint++);
                }
                //если что то идет не так, то уходит и удаляется
                else
                {
                    EnemyWin();
                    LevelData.levelData.enemyCount.Value--;
                }
            }
            else
            {
                State = new WalkToTargetState(anim, _transform, spawnPoint, velocity * 2);
                State.Subscribe(() => GameManager.Instance.DestroyEnemy(this));
            }
        }
        //тригер на атаку владения
        private void OnTriggerEnter2D(Collider2D collider)
        {
            FortressController fortress = collider.GetComponent<FortressController>();
            if (fortress)
            {
                State = new AttackState(anim, fortress, AttackInfo);
                curPoint = points.Length;
            }
        }
    }
}
