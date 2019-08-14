using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Qitz.EscapeFramework
{
    public interface IEscapeGameDefinsDataStore
    {
        int MAX_ITEM_LIST_COUNT { get; }
    }

    [CreateAssetMenu]
    public class EscapeGameDefinsDataStore : ScriptableObject, IEscapeGameDefinsDataStore
    {
        [SerializeField]
        int maxItemListCount = 10;
        public int MAX_ITEM_LIST_COUNT => maxItemListCount;
    }
}