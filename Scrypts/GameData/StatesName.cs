using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scrypts.GameData
{
    class EnemyStateNames
    {
        public const string HIDE = "isHide";
        public const string ATTACK = "isAttack";
        public const string WALK = "isMove";
        public const string TAKE_DAMAGE = "TakingDamage";
        public const string DEAD = "DeadTrigger";
        public const string SELEBRATE = "isStolen";
    }
    class PlayerStateNames
    {
        public const string ATTACK = "Attack";
        public const string MISTAKE = "Mistake";
        public const string SELEBRATE = "isVictory";
        public const string LOSE = "isLose";
    }
}
