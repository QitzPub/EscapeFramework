using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Qitz.EscapeFramework
{
    public enum EventType
    {
        イベントの種類を設定してくださいまし,
        メッセージWindowイベント,
        BGMイベント,
        SEイベント,
        表示ー非表示イベント,
        フラグイベント,
        カウントイベント,
        アイテムイベント,
        シーン遷移イベント,
        イメージ切り替えイベント,
        スクリーンエフェクトイベント,
    }


    public enum EventName
    {
        トンカチを入手,
        ぶたの貯金箱を破壊,
        金庫を開ける,
    }

    public enum BGMName
    {
        test1,
        test2,
    }
    public enum SEName
    {
        se_test1,
        se_test2,
    }
    public enum AudioCommandType
    {
        再生する,
    }
    public enum ScreenEffectName
    {
        画面暗転,
        画面暗転解除,
        画面操作不能にする,
        画面操作不能解除,
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
    public enum CountEventProgress
    {
        増やす,
        減らす,
        初期値0にする,
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
        アイテムがドロップされた時,
    }
    public enum CountEventJudge
    {
        等しい,
        以上,
        以下,
    }

}
