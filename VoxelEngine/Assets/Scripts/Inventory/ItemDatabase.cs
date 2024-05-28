using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Inventory
{
    public class ItemDatabase : MonoBehaviour
    {
        public List<Item> itemDatabase = new();

        private void Start()
        {
            LoadDatabase("Assets/Resources/ItemData.txt");
        }

        private void LoadDatabase(string path)
        {
            try
            {
                using StreamReader sr = new(path);

                while (sr.ReadLine() is { } line)
                {
                    Item newItem = ParseItem(sr, line);
                    itemDatabase.Add(newItem);

                    string delimiter = sr.ReadLine();
                    if (delimiter == ";")
                        break;
                    if (delimiter != ",")
                    {
                        // (stepa) TODO: Переделать под отдельный класс для дебага 
                        Debug.LogError("Unrecognized delimiter!");
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                // (stepa) TODO: Переделать под отдельный класс для дебага 
                Debug.LogError($"Failed to load item database: {ex.Message}");
            }
        }

        private Item ParseItem(StreamReader sr, string firstLine)
        {
            int id = int.Parse(firstLine.Replace("id: ", ""));
            string name = sr.ReadLine().Replace("name: ", "");
            bool stackable = bool.Parse(sr.ReadLine().Replace("stackable: ", ""));
            string slug = sr.ReadLine().Replace("slug: ", "");
            return new Item(id, name, stackable, slug);
        }
    }

    [Serializable]
    public struct Item
    {
        public int id;
        public string name;
        public bool stackable;
        public string slug;

        public Item(int id, string name, bool stackable, string slug)
        {
            this.id = id;
            this.name = name;
            this.stackable = stackable;
            this.slug = slug;
        }
    }
}