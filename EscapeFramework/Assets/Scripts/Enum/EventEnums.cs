using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Qitz.EscapeFramework
{
    public enum EventType
    {
        トンカチを入手,
    }

    public enum EventFlag
    {
        ON,
        OFF,
    }
    public enum DisplayEventProgress
    {
        表示する,
        非表示にする,

    }
    public enum ItemEventProgress
    {
        アイテムを増やす,
        アイテムを減らす,
    }

    public enum ItemPossession
    {
        持っている,
        持っていない,
    }

    public enum IGnitionPointItem
    {
        アイテムを持っている,
        アイテムを持っていない,
    }

    public enum IGnitionPointEvent
    {
        イベントフラグがON,
        イベントフラグがOFF,
    }

    public enum EventExecuteTiming
    {
        シーン読み込み時,
        クリックされた時,
    }

}
