using Assets.Scrypts.GameData;
using UnityEngine;

namespace Assets.Scrypts.Enemy
{
    public class HideState : EnemyState
    {
        float time;
        public HideState(Animator anim, EnemyHP enemyHP, float time) : base(anim, EnemyStateNames.HIDE)
        {
            this.time = Time.time + time;
            enemyHP.SwitchHide();
            onEnd += () => enemyHP.SwitchHide();
        }
        public override bool EndCondition()
        {
            return Time.time > time;
        }

        public override void Update()
        {
            //sitting
        }
    }
}
