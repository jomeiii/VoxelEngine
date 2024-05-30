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
        public bool placeable;
        public string slug;
        public Sprite sprite;
        public GameObject gameObject;

        public Item(int id, string name, bool stackable, bool placeable, string slug)
        {
            this.id = id;
            this.name = name;
            this.stackable = stackable;
            this.slug = slug;
            this.placeable = placeable;
            sprite = Resources.Load<Sprite>("Sprites/" + slug);
            gameObject = Resources.Load<GameObject>("Blocks/" + slug + "Block");
        }
    }
}