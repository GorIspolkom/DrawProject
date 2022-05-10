using Assets.Scrypts.GameData;
using Assets.Scrypts.LevelManagerSystem;
using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scrypts.Enemy
{
    public abstract class SymbolOutputController : MonoBehaviour
    {
        protected SymbolCloseType closeType;
        protected List<SpriteRenderer> sprites;
        protected float width;

        //создать последовательность символов
        public void InitSymbolChain(string[] symbols, SymbolCloseType closeType)
        {
            this.closeType = closeType;
            sprites = new List<SpriteRenderer>();
            int size = symbols.Length;
            width = 0f;
            for (int i = 0; i < size; i++)
            {
                Sprite sprite = LevelData.levelData.GetSpriteOf(symbols[i]);
                CreateSpriteObject(sprite);
                width += sprites[i].sprite.rect.width;
            }
            width = -Camera.main.ScreenToWorldPoint(new Vector3(width/size * transform.localScale.x, 0)).x;
            AlignSprites();
        }
        public void TakeDamage(int index)
        {
            Destroy(sprites[index].gameObject);
            sprites.RemoveAt(index);
            AlignSprites();
        }
        //пр¤чем все символы
        public void HideSymbols(bool isNeedHide = true)
        {
            if (isNeedHide)
                for (int i = 0; i < sprites.Count; i++) 
                {
                    sprites[i].DOKill();
                    sprites[i].DOFade(EnemyData.SymbolOnHideFadeTo, EnemyData.TimeForHideSymbols);
                }
            else
                for (int i = 0; i < sprites.Count; i++)
                {
                    sprites[i].DOKill();
                    sprites[i].DOFade(1, EnemyData.TimeForHideSymbols);
                }
        }
        //создаем символ
        protected SpriteRenderer CreateSpriteObject(Sprite sprite)
        {
            SpriteRenderer spriteRenderer = new GameObject("Symbol").AddComponent<SpriteRenderer>();
            spriteRenderer.transform.SetParent(transform, false);
            spriteRenderer.sprite = sprite;
            spriteRenderer.sortingOrder = 3;
            sprites.Add(spriteRenderer);
            return spriteRenderer;
        }
        //добавить символ
        public void AddSymbol(string symbolSpriteName, SymbolCloseType addType = SymbolCloseType.Right) =>
            AddSymbol(LevelData.levelData.GetSpriteOf(symbolSpriteName), addType);
        public void AddSymbol(Sprite symbolSprite, SymbolCloseType addType = SymbolCloseType.Right)
        {
            if (addType == SymbolCloseType.Right)
            {
                SpriteRenderer sprite = CreateSpriteObject(symbolSprite);
                sprites[sprites.Count - 1] = sprites[0];
                sprites[0] = sprite;
            }
            else
                CreateSpriteObject(symbolSprite);
            AlignSprites();
        }
        //уничтожаем символ по индексу
        //смещает все спрайты к индексу
        protected abstract void AlignSprites();
    }
}