using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Qitz.EscapeFramework
{
    public enum EventType
    {
        トンカチを入手,
        ぶたの貯金箱を破壊,
        金庫を開ける,
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
    public enum EventProgress
    {
        増やす,
        減らす,
    }
    public enum CountEventType
    {
        ダイアルロック上の数字,
        ダイアルロック真ん中上の数字,
        ダイアルロック真ん中下の数字,
        ダイアルロック下の数字,
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
        Update実行,
        シーン読み込み時,
        クリックされた時,
    }
    public enum CountEventJudge
    {
        等しい,
        以上,
        以下,
    }

}
