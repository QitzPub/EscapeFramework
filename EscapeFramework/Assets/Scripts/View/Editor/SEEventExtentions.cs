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
    [CustomEditor(typeof(SEEvent))]
    public class SEEventExtentions : Editor
    {
        public override void OnInspectorGUI()
        {
            var _target = target as AEvent;
            float buttonYPostion = 0;
            float elemntHight = 72.0f;
            float buttonWidth = 120.0f;
            float itemButtonOffsetY = 20.0f;
            float addOffseyY = 70.0f;


            if (_target.UseItemRestrictedSetting)
            {

                EditorGUILayout.LabelField("=======アイテム所持イベント実行制限==========");

                foreach (var ignitionPoint in _target.ItemIGnitions)
                {
                    ignitionPoint.ItemName = (ItemName)EditorGUILayout.EnumPopup("アイテム:", ignitionPoint.ItemName);
                    ignitionPoint.IGnitionPointItem = (IGnitionPointItem)EditorGUILayout.EnumPopup("アイテム所持条件", ignitionPoint.IGnitionPointItem);
                    EditorGUILayout.LabelField("時にイベントが実行可能");
                    EditorGUILayout.LabelField("");
                    buttonYPostion += elemntHight;
                }

                if (GUI.Button(new Rect(0.0f, buttonYPostion + itemButtonOffsetY, buttonWidth, 20.0f), "制限条件を増やす"))
                {
                    _target.ItemIGnitions.Add(new ItemIGnitionPoint());
                }
                if (GUI.Button(new Rect(buttonWidth + 20.0f, buttonYPostion + itemButtonOffsetY, buttonWidth, 20.0f), "制限条件を減らす"))
                {
                    if (_target.ItemIGnitions.LastOrDefault() != null)
                        _target.ItemIGnitions.Remove(_target.ItemIGnitions.LastOrDefault());
                }
                EditorGUILayout.LabelField("");
                EditorGUILayout.LabelField("");
                EditorGUILayout.LabelField("========================================");
                itemButtonOffsetY += addOffseyY;
            }
            if (_target.UseEventFlagRestrictedSetting)
            {
                EditorGUILayout.LabelField("=======イベントフラグ実行制限==========");
                foreach (var ignitionPoint in _target.EventFlagIGnitions)
                {
                    ignitionPoint.EventType = (EventType)EditorGUILayout.EnumPopup("対象のイベントフラグ:", ignitionPoint.EventType);
                    ignitionPoint.EventFlag = (EventFlag)EditorGUILayout.EnumPopup("イベントフラグの状態", ignitionPoint.EventFlag);
                    EditorGUILayout.LabelField("時にイベントが実行可能");
                    EditorGUILayout.LabelField("");
                    buttonYPostion += elemntHight;
                }

                if (GUI.Button(new Rect(0.0f, buttonYPostion + itemButtonOffsetY, buttonWidth, 20.0f), "制限条件を増やす"))
                {
                    _target.EventFlagIGnitions.Add(new EventFlagIGnitionPoint());
                }
                if (GUI.Button(new Rect(buttonWidth + 20.0f, buttonYPostion + itemButtonOffsetY, buttonWidth, 20.0f), "制限条件を減らす"))
                {
                    if (_target.EventFlagIGnitions.LastOrDefault() != null)
                        _target.EventFlagIGnitions.Remove(_target.EventFlagIGnitions.LastOrDefault());
                }
                EditorGUILayout.LabelField("");
                EditorGUILayout.LabelField("");
                EditorGUILayout.LabelField("========================================");
                itemButtonOffsetY += addOffseyY;
            }
            if (_target.UseCountEventRestrictedSetting)
            {
                EditorGUILayout.LabelField("=======カウントイベント実行制限==========");
                foreach (var ignitionPoint in _target.CountEventIGnitions)
                {
                    ignitionPoint.CountEventName = (CountEventType)EditorGUILayout.EnumPopup("対象のカウントイベント:", ignitionPoint.CountEventName);
                    ignitionPoint.CountEventValue = (int)EditorGUILayout.IntField("カウントイベントの値", ignitionPoint.CountEventValue);
                    ignitionPoint.CountEventJudge = (CountEventJudge)EditorGUILayout.EnumPopup("", ignitionPoint.CountEventJudge);
                    EditorGUILayout.LabelField("時にイベントが実行可能");
                    buttonYPostion += elemntHight;
                }



                if (GUI.Button(new Rect(0.0f, buttonYPostion + itemButtonOffsetY, buttonWidth, 20.0f), "制限条件を増やす"))
                {
                    _target.CountEventIGnitions.Add(new CountEventIGnitionPoint());
                }
                if (GUI.Button(new Rect(buttonWidth + 20.0f, buttonYPostion + itemButtonOffsetY, buttonWidth, 20.0f), "制限条件を減らす"))
                {
                    if (_target.CountEventIGnitions.LastOrDefault() != null)
                        _target.CountEventIGnitions.Remove(_target.CountEventIGnitions.LastOrDefault());
                }
                EditorGUILayout.LabelField("");
                EditorGUILayout.LabelField("");
                EditorGUILayout.LabelField("========================================");
            }
            base.OnInspectorGUI();
        }
    }
    //======================================

}
