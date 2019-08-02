using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Qitz.EscapeFramework
{
    public interface ICountEventDataStore
    {
        List<CountEventSetting> CountEventSettings { get; }
    }

    //[CreateAssetMenu]
    public class CountEventDataStore : ADataStore, ICountEventDataStore
    {
        public List<CountEventSetting> CountEventSettings => countEventSettings;
        [SerializeField]
        List<CountEventSetting> countEventSettings;
    }
}
