using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Inventory
{
    public class ItemDatabase : MonoBehaviour
    {
        public List<Item> itemDatabase = new();

        private void Awake()
        {
            LoadDatabase("Assets/Resources/ItemData.txt");
        }

        public Item GetItemByID(int id)
        {
            for (int i = 0; i < itemDatabase.Count; i++)
            {
                if (itemDatabase[i].id == id)
                {
                    return itemDatabase[i];
                }
            }

            return itemDatabase[0];
        }

        private Item ParseItem(StreamReader sr, string firstLine)
        {
            int id = int.Parse(firstLine.Replace("id: ", ""));
            string name = sr.ReadLine().Replace("name: ", "");
            bool stackable = bool.Parse(sr.ReadLine().Replace("stackable: ", ""));
            bool placeable = bool.Parse(sr.ReadLine().Replace("placeable: ", ""));
            string slug = sr.ReadLine().Replace("slug: ", "");
            return new Item(
                id: id,
                name: name,
                stackable: stackable,
                placeable: placeable,
                slug: slug);
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
    }
}