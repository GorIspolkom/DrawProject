using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scrypts.Enemy
{ 
    //состояние ожидания некторое время
    class IdleState : EnemyState
    {
        float timer;
        public IdleState(Animator anim, float time) : base(anim, "")
        {
            timer = Time.time + time;
        }
        public override bool EndCondition()
        {
            return timer < Time.time;
        }
        public override void Update()
        {

        }
    }
}
