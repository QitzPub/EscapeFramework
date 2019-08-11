using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Linq;

namespace Qitz.EscapeFramework
{

    //ダサいやり方だが、CustomEditorのTypeを複数設定できない＋基底クラスに設定しても反映されないのでコピペ=================================
    //============================================
    //表示イベントのエクステンション
    [CustomEditor(typeof(GameEvent))]
    public class GameEventExtentions : Editor
    {
        GameEvent gameEvent;


        private void Awake()
        {
            this.gameEvent = target as GameEvent;
        }
        //======================================
        public override void OnInspectorGUI()
        {

            //基本設定ここから===========================================
            gameEvent.EventType = (EventType)EditorGUILayout.EnumPopup("イベント種類:", gameEvent.EventType);
            if (gameEvent.EventType == EventType.イベントの種類を設定してくださいまし) return;

            gameEvent.EventExecuteTiming = (EventExecuteTiming)EditorGUILayout.EnumPopup("イベント実行タイミング:", gameEvent.EventExecuteTiming);
            if(gameEvent.EventExecuteTiming == EventExecuteTiming.アイテムがドロップされた時)
            {
                gameEvent.DropedItemName = (ItemName)EditorGUILayout.EnumPopup("対象アイテム:", gameEvent.DropedItemName);

            }
            if (gameEvent.EventExecuteTiming == EventExecuteTiming.指定のイベントが実行完了した時)
            {
                serializedObject.Update();
                // 第二引数をtrueにする
                EditorGUILayout.PropertyField(serializedObject.FindProperty("chainEvent"), true);
                serializedObject.ApplyModifiedProperties();

            }

            EditorGUILayout.LabelField("");
            //基本設定ここまで===========================================




            //ADVWindow設定ここから==========================================
            if (gameEvent.EventType == EventType.メッセージWindowイベント)
            {
                serializedObject.Update();
                // 第二引数をtrueにする
                EditorGUILayout.PropertyField(serializedObject.FindProperty("texts"), true);
                serializedObject.ApplyModifiedProperties();
            }
            //ADVWindow設定ここまで==========================================

            //BGMEvent設定ここから==========================================
            if (gameEvent.EventType == EventType.BGMイベント)
            {
                gameEvent.BGMName = (BGMName)EditorGUILayout.EnumPopup("BGM名:", gameEvent.BGMName);
                gameEvent.BGMAudioCommandType = (AudioCommandType)EditorGUILayout.EnumPopup("", gameEvent.BGMAudioCommandType);
            }
            //BGMEvent設定ここまで==========================================

            //DisplayEvent設定ここから==========================================
            if (gameEvent.EventType == EventType.表示ー非表示イベント)
            {
                gameEvent.DisplayEventProgress = (DisplayEventProgress)EditorGUILayout.EnumPopup("表示ー非表示方式:", gameEvent.DisplayEventProgress);
            }
            //DisplayEvent設定ここまで==========================================

            //EventFlagEvent設定ここから==========================================
            if (gameEvent.EventType == EventType.フラグイベント)
            {
                gameEvent.EventName = (EventName)EditorGUILayout.EnumPopup("イベント名:", gameEvent.EventName);
                gameEvent.EventFlag = (EventFlag)EditorGUILayout.EnumPopup("フラグ切り替え:", gameEvent.EventFlag);

            }
            //EventFlagEvent設定ここまで==========================================

            //CountEvent設定ここから==========================================
            if (gameEvent.EventType == EventType.カウントイベント)
            {
                gameEvent.CountEventName = (CountEventType)EditorGUILayout.EnumPopup("カウントイベント名:", gameEvent.CountEventName);
                gameEvent.CountEventProgress = (CountEventProgress)EditorGUILayout.EnumPopup("", gameEvent.CountEventProgress);

            }
            //CountEvent設定ここまで==========================================

            //ItemEvent設定ここから==========================================
            if (gameEvent.EventType == EventType.アイテムイベント)
            {
                gameEvent.ItemName = (ItemName)EditorGUILayout.EnumPopup("アイテムイベント名:", gameEvent.ItemName);
                gameEvent.ItemEventProgress = (EventProgress)EditorGUILayout.EnumPopup("", gameEvent.ItemEventProgress);

            }
            //ItemEvent設定ここまで==========================================

            //SceneTransitionEvent設定ここから==========================================
            if (gameEvent.EventType == EventType.シーン遷移イベント)
            {
                gameEvent.SceneName = EditorGUILayout.TextField("シーン名:", gameEvent.SceneName);
            }
            //SceneTransitionEvent設定ここまで==========================================

            //ScreenEffectEvent設定ここから==========================================
            if (gameEvent.EventType == EventType.スクリーンエフェクトイベント)
            {
                gameEvent.ScreenEffect = (ScreenEffectName)EditorGUILayout.EnumPopup("エフェクト:", gameEvent.ScreenEffect);
            }
            //ScreenEffectEvent設定ここまで==========================================

            //SEEvent設定ここから==========================================
            if (gameEvent.EventType == EventType.SEイベント)
            {
                gameEvent.SEName = (SEName)EditorGUILayout.EnumPopup("SE名:", gameEvent.SEName);
                gameEvent.SEAudioCommandType = (AudioCommandType)EditorGUILayout.EnumPopup("", gameEvent.SEAudioCommandType);

            }
            //SEEvent設定ここまで==========================================

            //SpriteChangeEvent設定ここから==========================================
            if (gameEvent.EventType == EventType.イメージ切り替えイベント)
            {
                var options = new[] { GUILayout.Width(64), GUILayout.Height(64) };
                gameEvent.ChangeSprite = (Sprite)EditorGUILayout.ObjectField(gameEvent.ChangeSprite, typeof(Sprite), false, options);

            }
            //SpriteChangeEvent設定ここまで==========================================
            //アイテムウィンドウ設定ここから==========================================
            if (gameEvent.EventType == EventType.アイテム欄の表示ー非表示切り替え)
            {
                gameEvent.ItemWinodwEvent = (ItemWinodwEvent)EditorGUILayout.EnumPopup("アイテム欄を", gameEvent.ItemWinodwEvent);

            }
            //アイテムウィンドウ設定ここまで==========================================

            //イベント遅延処理設定ここから==========================================
            if (gameEvent.EventExecuteTiming != EventExecuteTiming.Update実行)
            {
                EditorGUILayout.LabelField("");
                gameEvent.UseDelay = EditorGUILayout.Toggle("イベント遅延を設定する", gameEvent.UseDelay);
                if (gameEvent.UseDelay)
                {
                    gameEvent.DelayTime = EditorGUILayout.FloatField("遅延時間", gameEvent.DelayTime);
                }
            }

            //イベント遅延処理設定ここまで==========================================

            //=======イベント制限ここから==================
            EditorGUILayout.LabelField("");
            gameEvent.UseItemRestrictedSetting = EditorGUILayout.Toggle("アイテム所持実行制限", gameEvent.UseItemRestrictedSetting);
            if (gameEvent.UseItemRestrictedSetting)
            {

                EditorGUILayout.LabelField("=======アイテム所持イベント実行制限==========");

                foreach (var ignitionPoint in gameEvent.ItemIGnitions)
                {
                    ignitionPoint.ItemName = (ItemName)EditorGUILayout.EnumPopup("アイテム:", ignitionPoint.ItemName);
                    ignitionPoint.IGnitionPointItem = (IGnitionPointItem)EditorGUILayout.EnumPopup("アイテム所持条件", ignitionPoint.IGnitionPointItem);
                    EditorGUILayout.LabelField("時にイベントが実行可能");
                    EditorGUILayout.LabelField("");
                }

                if (GUILayout.Button("制限条件を増やす"))
                {
                    gameEvent.ItemIGnitions.Add(new ItemIGnitionPoint());
                }
                if (GUILayout.Button("制限条件を減らす"))
                {
                    if (gameEvent.ItemIGnitions.LastOrDefault() != null)
                        gameEvent.ItemIGnitions.Remove(gameEvent.ItemIGnitions.LastOrDefault());
                }
                EditorGUILayout.LabelField("========================================");
            }
            EditorGUILayout.LabelField("");
            gameEvent.UseEventFlagRestrictedSetting = EditorGUILayout.Toggle("フラグ実行制限", gameEvent.UseEventFlagRestrictedSetting);
            if (gameEvent.UseEventFlagRestrictedSetting)
            {
                EditorGUILayout.LabelField("=======イベントフラグ実行制限==========");
                foreach (var ignitionPoint in gameEvent.EventFlagIGnitions)
                {
                    ignitionPoint.EventType = (EventName)EditorGUILayout.EnumPopup("対象のイベントフラグ:", ignitionPoint.EventType);
                    ignitionPoint.EventFlag = (EventFlag)EditorGUILayout.EnumPopup("イベントフラグの状態", ignitionPoint.EventFlag);
                    EditorGUILayout.LabelField("時にイベントが実行可能");
                    EditorGUILayout.LabelField("");
                }

                if (GUILayout.Button("制限条件を増やす"))
                {
                    gameEvent.EventFlagIGnitions.Add(new EventFlagIGnitionPoint());
                }
                if (GUILayout.Button("制限条件を減らす"))
                {
                    if (gameEvent.EventFlagIGnitions.LastOrDefault() != null)
                        gameEvent.EventFlagIGnitions.Remove(gameEvent.EventFlagIGnitions.LastOrDefault());
                }
                EditorGUILayout.LabelField("========================================");
            }
            EditorGUILayout.LabelField("");
            gameEvent.UseCountEventRestrictedSetting = EditorGUILayout.Toggle("カウント実行制限", gameEvent.UseCountEventRestrictedSetting);
            if (gameEvent.UseCountEventRestrictedSetting)
            {
                EditorGUILayout.LabelField("=======カウントイベント実行制限==========");
                foreach (var ignitionPoint in gameEvent.CountEventIGnitions)
                {
                    ignitionPoint.CountEventName = (CountEventType)EditorGUILayout.EnumPopup("対象のカウントイベント:", ignitionPoint.CountEventName);
                    ignitionPoint.CountEventValue = (int)EditorGUILayout.IntField("カウントイベントの値", ignitionPoint.CountEventValue);
                    ignitionPoint.CountEventJudge = (CountEventJudge)EditorGUILayout.EnumPopup("", ignitionPoint.CountEventJudge);
                    EditorGUILayout.LabelField("時にイベントが実行可能");
                }

                if (GUILayout.Button("制限条件を増やす"))
                {
                    gameEvent.CountEventIGnitions.Add(new CountEventIGnitionPoint());
                }
                if (GUILayout.Button("制限条件を減らす"))
                {
                    if (gameEvent.CountEventIGnitions.LastOrDefault() != null)
                        gameEvent.CountEventIGnitions.Remove(gameEvent.CountEventIGnitions.LastOrDefault());
                }
                EditorGUILayout.LabelField("========================================");
            }
            //=======イベント制限ここまで==================

            //======イベント追加ここから=================================
            if (GUILayout.Button("イベントをさらに追加する"))
            {
                gameEvent.gameObject.AddComponent<GameEvent>();
            }
            //=====イベント追加ここまで====================================
            //if (DrawDefaultInspector())
            //{
            //    // ここで更新処理を入れる
            //}

            //!!!!!!!!!!!!エディターバグのハックfix!!!!!!!!!!!!
            Undo.RecordObject(target, "Update");
            EditorUtility.SetDirty(gameEvent);
        }
        //======================================
    }


}
