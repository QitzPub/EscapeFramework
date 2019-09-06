using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

namespace Qitz.EscapeFramework
{
    public interface IJudgeIgnitionOverTheLimitUseCase
    {
        bool JudgeEventIgnitionOverTheLimit(AEvent aEvent);
    }

    public class JudgeIgnitionOverTheLimitUseCase: IJudgeIgnitionOverTheLimitUseCase
    {
        IEscapeGameUserDataStore escapeGameUserDataStore;
        IItemSelectUseCase itemSelectUseCase;
        public JudgeIgnitionOverTheLimitUseCase(IEscapeGameUserDataStore escapeGameUserDataStor, IItemSelectUseCase itemSelectUseCase)
        {
            this.escapeGameUserDataStore = escapeGameUserDataStor;
            this.itemSelectUseCase = itemSelectUseCase;
        }

        public bool JudgeEventIgnitionOverTheLimit(AEvent aEvent)
        {
            bool itemRestrictedOver = true;
            if (aEvent.UseItemRestrictedSetting)
            {
                itemRestrictedOver = JudgeItemEventsIgnitionOverTheLimit(aEvent);
            }

            bool eventFlagRestrictedOver = true;
            if (aEvent.UseEventFlagRestrictedSetting)
            {
                eventFlagRestrictedOver = JudgeEventsFlagIgnitionOverTheLimit(aEvent);
            }

            bool itemSelectRestrictedOver = true;
            if (aEvent.UseSelectedItemRestrictedSetting)
            {
                itemSelectRestrictedOver = JudgeSelectItemIgnitionOverTheLimit(aEvent);
            }

            bool countEventRestrictedOver = true;
            if (aEvent.UseCountEventRestrictedSetting)
            {
                countEventRestrictedOver = JudgeCountEventsIgnitionOverTheLimit(aEvent);
            }
            return itemRestrictedOver && eventFlagRestrictedOver && itemSelectRestrictedOver && countEventRestrictedOver;
        }

        bool JudgeItemEventsIgnitionOverTheLimit(AEvent aEvent)
        {
            if (!aEvent.UseItemRestrictedSetting) return true;
            return aEvent.ItemIGnitions.All(ii => JudgeItemIgnitionOverTheLimit(ii));
        }

        bool JudgeItemIgnitionOverTheLimit(ItemIGnitionPoint ii)
        {
            if (ii.IGnitionPointItem == IGnitionPointItem.アイテムを持っている)
            {
                return escapeGameUserDataStore.InPossessionItem(ii.ItemName);
            }
            else if (ii.IGnitionPointItem == IGnitionPointItem.アイテムを持っていない)
            {
                return !escapeGameUserDataStore.InPossessionItem(ii.ItemName);
            }
            else
            {
                throw new System.Exception($"想定されない形式です");
            }
        }


        bool JudgeEventsFlagIgnitionOverTheLimit(AEvent aEvent)
        {
            if (!aEvent.UseEventFlagRestrictedSetting) return true;
            return aEvent.EventFlagIGnitions.All(ei => JudgeEventFlagIgnitionOverTheLimit(ei));
        }

        bool JudgeEventFlagIgnitionOverTheLimit(EventFlagIGnitionPoint ei)
        {
            if (ei.EventFlag == EventFlag.ON)
            {
                return escapeGameUserDataStore.GetEventFlagValue(ei.EventType);
            }
            else if (ei.EventFlag == EventFlag.OFF)
            {
                return !escapeGameUserDataStore.GetEventFlagValue(ei.EventType);
            }
            else
            {
                throw new System.Exception($"想定されない形式です");
            }
        }
        bool JudgeSelectItemIgnitionOverTheLimit(AEvent aEvent)
        {
            if (!aEvent.UseSelectedItemRestrictedSetting) return true;
            return aEvent.SelectItemIGnitions.All(ei => JudgeSelectItemIgnitionOverTheLimit(ei));
        }

        bool JudgeSelectItemIgnitionOverTheLimit(ItemSelectIGnitionPoint ei)
        {
            if (ei.SelectItemState == SelectItemState.アイテムが選択されている)
            {
                return itemSelectUseCase.SelectedItem == ei.ItemName;
            }
            else if (ei.SelectItemState == SelectItemState.アイテムが選択されていない)
            {
                return itemSelectUseCase.SelectedItem != ei.ItemName;
            }
            else
            {
                throw new System.Exception($"想定されない形式です");
            }
        }

        bool JudgeCountEventsIgnitionOverTheLimit(AEvent aEvent)
        {
            if (!aEvent.UseCountEventRestrictedSetting) return true;
            return aEvent.CountEventIGnitions.All(ce => JudgeCountEventIgnitionOverTheLimit(ce));
        }

        bool JudgeCountEventIgnitionOverTheLimit(CountEventIGnitionPoint ce)
        {
            if (ce.CountEventJudge == CountEventJudge.等しい)
            {
                var val = escapeGameUserDataStore.GetCountEventValue(ce.CountEventName);
                return val == ce.CountEventValue;
            }
            else if (ce.CountEventJudge == CountEventJudge.以上)
            {
                var val = escapeGameUserDataStore.GetCountEventValue(ce.CountEventName);
                return val >= ce.CountEventValue;
            }
            else if (ce.CountEventJudge == CountEventJudge.以下)
            {
                var val = escapeGameUserDataStore.GetCountEventValue(ce.CountEventName);
                return val <= ce.CountEventValue;
            }
            else
            {
                throw new System.Exception($"想定されない形式です");
            }
        }
    }
}