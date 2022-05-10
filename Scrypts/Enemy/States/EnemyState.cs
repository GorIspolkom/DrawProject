using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scrypts.Enemy
{
    //абстракция состояния врага
    public abstract class EnemyState : IDisposable
    {
        protected Action onEnd;
        protected Animator anim;
        private string stateKey;
        public void Subscribe(Action sub) => onEnd += sub;
        public EnemyState(Animator anim, string stateKey)
        {
            this.anim = anim;
            this.stateKey = stateKey;
        }
        public abstract bool EndCondition();
        public abstract void Update();

        //либо изменяем bool, либо активируем триггер
        public void SetAnimState(bool isState)
        {
            if (stateKey.StartsWith("is"))
                anim.SetBool(stateKey, isState);
            else if(isState)
                anim.SetTrigger(stateKey);
        }

        //вызывается при выходе из состояния
        public void Dispose()
        {
            anim.SetBool(stateKey, false);
            onEnd?.Invoke();
        }
    }
}
