using Assets.Scrypts.GameData;
using Assets.Scrypts.InputModule;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scrypts.Enemy
{
    public class PlayerAnimatorController : MonoBehaviour
    {
        [SerializeField] Animator anim;

        //событие в DrawInput
        //игрок ввел неверный символ
        public void SetMistake() =>
            anim.SetTrigger(PlayerStateNames.MISTAKE);

        //через подписку на верный ввод
        //игрок ввел верный символ
        public void SetAttack() =>
            anim.SetTrigger(PlayerStateNames.ATTACK);

        public void Reset()
        {
            anim.SetBool(PlayerStateNames.SELEBRATE, false);
            anim.SetBool(PlayerStateNames.LOSE, false);
            anim.ResetTrigger(PlayerStateNames.MISTAKE);
            anim.ResetTrigger(PlayerStateNames.ATTACK);
        }

        //обрабатывается в LevelManager
        //все враги побежденны
        public void SetVictory() =>
            anim.SetBool(PlayerStateNames.SELEBRATE, true);
        //все владения разрушены
        public void SetLose() =>
            anim.SetBool(PlayerStateNames.LOSE, true);

    }
}