using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Qitz.EscapeFramework
{
    public class ItemEventProcessingUseCase : IExecutable
    {
        IItemEventView itemEvent;
        IEscapeGameUserDataStore escapeGameUserDataStore;

        public ItemEventProcessingUseCase(IItemEventView itemEvent, IEscapeGameUserDataStore escapeGameUserDataStore)
        {
            this.itemEvent = itemEvent;
            this.escapeGameUserDataStore = escapeGameUserDataStore;
        }

        public void Execute()
        {
            bool inPossessionItemConditions = itemEvent.ItemPossession == ItemPossession.持っている && escapeGameUserDataStore.InPossessionItem(itemEvent.ItemName);
            bool dontHaveItemConditions = itemEvent.ItemPossession == ItemPossession.持っていない && !escapeGameUserDataStore.InPossessionItem(itemEvent.ItemName);
            if (inPossessionItemConditions)
            {
                ExectuteItemEvent();
            }
            else if (dontHaveItemConditions)
            {
                ExectuteItemEvent();
            }
        }

        void ExectuteItemEvent()
        {
            if (itemEvent.EventProgress == DisplayEventProgress.表示する)
            {
                itemEvent.GameObject.SetActive(true);
            }
            else if (itemEvent.EventProgress == DisplayEventProgress.非表示にする)
            {
                itemEvent.GameObject.SetActive(false);
            }
        }

    }
}