
using System.Collections.Generic;
using UnityEngine;

namespace Qitz.EscapeFramework
{
    public interface IEscapeGameRepository
    {
        IItemDataStore ItemDataStore { get; }
        IEscapeGameUserDataStore EscapeGameUserDataStore { get; }
    }
    //[CreateAssetMenu]
    public class EscapeGameRepository : ARepository, IEscapeGameRepository
    {
        [SerializeField]
        ItemDataStore itemDataStore;
        public IItemDataStore ItemDataStore => itemDataStore;
        EscapeGameUserDataStore escapeGameUserDataStore;
        public IEscapeGameUserDataStore EscapeGameUserDataStore => escapeGameUserDataStore;

        public void Initialize()
        {
            escapeGameUserDataStore = new EscapeGameUserDataStore();
        }

    }
}