using Assets.Scrypts.Entity;
using Assets.Scrypts.GameData;
using Assets.Scrypts.InputModule;
using Assets.Scrypts.LevelManagerSystem;
using System;
using System.Collections.Generic;
using UnityEngine;  

namespace Assets.Scrypts.Enemy
{
    public abstract class EnemyController : MonoBehaviour
    {
        [SerializeField] protected Animator anim;
        [SerializeField] private EnemyHP enemyHp;
        protected EnemyHP HpStruct
        {
            get 
            {
                ref EnemyHP hp = ref enemyHp;
                return hp;
            }
        }
        [SerializeField] private AttackData attackData;
        protected AttackData AttackInfo
        {
            get
            {
                ref AttackData data = ref attackData;
                return data;
            }
        }
        [SerializeField] Loot[] loots;
        public Loot[] Loots => loots;

        protected Transform _transform;
        //ткущее состояние врага
        private EnemyState state;
        //Свойство для изменения состояния и коректного завершения предыдущего
        protected EnemyState State
        {
            get => state;
            set
            {
                state.Dispose();
                state = value;
                state.SetAnimState(true);
            }
        }

        void Awake()
        {
            _transform = transform;
            //состояние по дефолту
            //В Start() наследника можно задать другое
            state = new IdleState(anim, -1);

            //определение данных о хп
            enemyHp.CreateHp(_transform);
        }
        //Задаются следующие состояния, по завершению предыдущего
        protected abstract void StateMachine();

        //Обработка введенного символа
        public void TakeDamage(string c)
        {
            if (enemyHp.isTakeDamage(c))
            {
                //состояние получения урона
                anim.SetTrigger(EnemyStateNames.TAKE_DAMAGE);
                if (!enemyHp.isAlive)
                {
                    //состояние смерти
                    anim.SetTrigger(EnemyStateNames.DEAD);
                    //убиваем врага (спавним лут и убираем его из счетчика)
                    GameManager.Instance.KillEnemy(this);
                    //состояние ожидание (в StateMachine() соверается переход)
                    State = new IdleState(anim, -1);
                }
            }
        }
        private void Update()
        {
            //если состояние окончено определяем следующее, иначе апдейтим
            if (state.EndCondition())
                StateMachine();
            else
               state.Update();
        }
    }
}
