using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IItemVO
{
    ItemName ItemName { get; }
}

[System.Serializable]
public class ItemVO : IItemVO
{
    [SerializeField]
    int itemName;
    public ItemName ItemName => (ItemName)itemName;
    public ItemVO(ItemName itemName)
    {
        this.itemName = (int)itemName;
    }
}
