using Assets.Scrypts.LevelManagerSystem;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scrypts.Entity
{
    public class FortressController : MonoBehaviour
    {
        [SerializeField] Sprite[] views;
        [SerializeField] float hp;
        [SerializeField] bool isRegenable;
        [SerializeField] float regenHpPerSecond;
        [SerializeField] Transform healthBar;
        [SerializeField] SpriteRenderer image;

        private int curSprite;
        private float curHp;
        public float CurHp { get => curHp; set => UpdateHp(value); }
        public float RegenPerSecond
        {
            get => isRegenable ? regenHpPerSecond : 0;
            set
            {
                isRegenable = value > 0;
                regenHpPerSecond = value;

                if (isRegenable)
                    StartCoroutine(Regenerate());
            }
        }

        void Awake()
        {
            Array.Reverse(views);
            CurHp = hp;

            if (isRegenable)
                StartCoroutine(Regenerate());
        }
        public void TakeDamage(float dmg)
        {
            CurHp -= dmg;
            if (curHp == 0)
                GameManager.Instance.DestroyFortress(this);
        }
        private void UpdateHp(float newHp)
        {
            curHp = Mathf.Clamp(newHp, 0, hp);
            healthBar.localScale = new Vector3(curHp / hp, 1f);
            int index = (int)((int)curHp * (views.Length - 1) / hp);
            if (index != curSprite)
            {
                image.sprite = views[index];
                curSprite = index;
            }
        }
        private IEnumerator Regenerate()
        {
            while (gameObject.activeSelf)
            {
                if (curHp < hp)
                    CurHp += regenHpPerSecond * Time.deltaTime;
                yield return null;
            }
        }
    }
}
