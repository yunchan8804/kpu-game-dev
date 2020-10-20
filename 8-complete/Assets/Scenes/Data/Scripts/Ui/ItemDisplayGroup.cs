using UnityEngine;

namespace Scenes.Data.Ui
{
    using System;
    using UnityEngine.Serialization;

    public class ItemDisplayGroup : MonoBehaviour
    {
         public ItemDatabase itemDatabase;
         public ItemDisplayUi displayUiPrefab;

         public void Start()
         {
             foreach (var item in itemDatabase.ItemDatas)
             {
                 var go = Instantiate(displayUiPrefab.gameObject);
                 go.transform.parent = transform;
                 go.GetComponent<ItemDisplayUi>().SetItemData(item);
             }
         }
    }
}
