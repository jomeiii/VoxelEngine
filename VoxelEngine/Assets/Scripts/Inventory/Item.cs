using System;
using UnityEngine;

namespace Inventory
{
    [Serializable]
    public struct Item
    {
        public int id;
        public string name;
        public bool stackable;
        public string slug;
        public Sprite sprite;

        public Item(int id, string name, bool stackable, string slug)
        {
            this.id = id;
            this.name = name;
            this.stackable = stackable;
            this.slug = slug;
            sprite = Resources.Load<Sprite>("Sprites/"+slug);
        }
    }
}