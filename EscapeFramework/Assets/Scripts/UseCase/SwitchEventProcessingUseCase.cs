using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Qitz.EscapeFramework
{
    public interface IExecutable
    {
        void Execute();
    }

    public class SwitchEventProcessingUseCase: IExecutable
    {
        ISwitchEvent switchEvent;
        IEscapeGameUserDataStore escapeGameUserDataStore;

        public SwitchEventProcessingUseCase(ISwitchEvent switchEvent, IEscapeGameUserDataStore escapeGameUserDataStore)
        {
            this.switchEvent = switchEvent;
            this.escapeGameUserDataStore = escapeGameUserDataStore;
        }

        public void Execute()
        {
            bool matchEventConditions = escapeGameUserDataStore.GetEventFlagValue(switchEvent.EventFlagVO);
            if (matchEventConditions)
            {
                DoMatchEvent();
            }
        }

        void DoMatchEvent()
        {
            if (switchEvent.EventProgress == DisplayEventProgress.表示する)
            {
                switchEvent.GameObject.SetActive(true);
            }
            else if (switchEvent.EventProgress == DisplayEventProgress.非表示にする)
            {
                switchEvent.GameObject.SetActive(false);
            }
        }


    }
}