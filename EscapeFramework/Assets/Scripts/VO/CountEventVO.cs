using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Qitz.EscapeFramework
{
    public interface ICountEventVO
    {
        int Count { get; }
        CountEventType CountEventType { get; }
        void IncrementCount();
        void DecrementCount();
    }

    [System.Serializable]
    public class CountEventVO:ICountEventVO
    {
        [SerializeField]
        CountEventType countEventType;
        public CountEventType CountEventType => countEventType;
        [SerializeField,HideInInspector]
        int count = 0;
        public int Count => count;

        public CountEventVO(CountEventType countEventType)
        {
            this.countEventType = countEventType;
        }

        public void IncrementCount()
        {
            count++;
        }

        public void DecrementCount()
        {
            count--;
        }
    }

    [System.Serializable]
    public class CountEventSetting: ICountEventSetting
    {
        [SerializeField]
        CountEventType countEventType;
        [SerializeField]
        int maxCount = 1;
        [SerializeField]
        int minCount = 0;

        public CountEventType CountEventType => countEventType;

        public int MaxCount => maxCount;

        public int MinCount => minCount;
    }

    public interface ICountEventSetting
    {
        CountEventType CountEventType { get; }
        int MaxCount { get; }
        int MinCount { get; }
    }


}
