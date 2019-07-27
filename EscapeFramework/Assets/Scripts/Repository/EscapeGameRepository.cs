
using System.Collections.Generic;
using UnityEngine;

namespace Qitz.EscapeFramework
{
    public interface IEscapeGameRepository
    {
        IItemDataStore ItemDataStore { get; }
        IEscapeGameUserDataStore EscapeGameUserDataStore { get; }
    }

    public class EscapeGameRepository : ARepository, IEscapeGameRepository
    {
        [SerializeField]
        ItemDataStore itemDataStore;
        public IItemDataStore ItemDataStore => itemDataStore;
        EscapeGameUserDataStore escapeGameUserDataStore = new EscapeGameUserDataStore();
        public IEscapeGameUserDataStore EscapeGameUserDataStore => escapeGameUserDataStore;
    }
}