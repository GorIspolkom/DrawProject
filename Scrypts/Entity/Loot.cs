using System;
using UnityEngine;

namespace Assets.Scrypts.Entity
{
    [Serializable]
    public struct Loot
    {
        [Range(0.0f, 1.0f)]
        public float propability;
        public long value;
        public LootController lootPrefab;

        public Loot(float propability, long value, LootController lootPrefab)
        {
            this.propability = propability;
            this.value = value;
            this.lootPrefab = lootPrefab;
        }
        public static Loot operator +(Loot a, Loot b)
        {
            a.value += b.value;
            return a;
        }
        public void Spawn(Vector2 position)
        {
            GameObject.Instantiate(lootPrefab).InitLoot(position, value);
        }
    }
}
