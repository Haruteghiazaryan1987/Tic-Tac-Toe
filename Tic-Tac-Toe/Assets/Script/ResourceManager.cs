using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Script
{
    public class ResourceManager : MonoBehaviour
    {
        private const string ITEM_SPRITE_FORMAT = "Sprites/{0}_ItemSprite";
        private Dictionary<int, Sprite> itemsSprite;

        private void Awake()
        {
            LoadResources();
        }

        public void LoadResources()    
        {
            itemsSprite = new Dictionary<int, Sprite>();
            var itemNames = Enum.GetNames(typeof(ItemTypes));
            for (int i = 0; i < itemNames.Length; i++)
            {
                var item = Resources.Load<Sprite>(string.Format(ITEM_SPRITE_FORMAT, itemNames[i].ToLower())); 
                itemsSprite.Add(i, item);
            }
        }

        public Sprite GetSpriteByItemType(ItemTypes item)
        {
            int index = (int)item;
            if (!itemsSprite.ContainsKey(index))
            {
                return null;
            }
            return itemsSprite[index];
       }
    }
}
