using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace Qitz.EscapeFramework
{

    //ダサいやり方だが、CustomEditorのTypeを複数設定できない＋基底クラスに設定しても反映されないのでコピペ=================================
    //============================================
    //表示イベントのエクステンション
    [CustomEditor(typeof(ItemDropEventFlagEvent))]
    public class ItemDropEventFlagEventExtentions : Editor
    {
        public override void OnInspectorGUI()
        {
            var _target = target as AEvent;
            if (_target.UseItemRestrictedSetting)
            {
                EditorGUILayout.LabelField("=======アイテム所持イベント実行制限==========");
                _target.ItemName = (ItemName)EditorGUILayout.EnumPopup("アイテム:", _target.ItemName);
                _target.IGnitionPointItem = (IGnitionPointItem)EditorGUILayout.EnumPopup("アイテム所持条件", _target.IGnitionPointItem);
                EditorGUILayout.LabelField("時にイベントが実行可能");
                EditorGUILayout.LabelField("========================================");
            }
            if (_target.UseEventFlagRestrictedSetting)
            {
                EditorGUILayout.LabelField("=======イベントフラグ実行制限==========");
                _target.EventType = (EventType)EditorGUILayout.EnumPopup("対象のイベントフラグ:", _target.EventType);
                _target.EventFlag = (EventFlag)EditorGUILayout.EnumPopup("イベントフラグの状態", _target.EventFlag);
                EditorGUILayout.LabelField("時にイベントが実行可能");
                EditorGUILayout.LabelField("========================================");
            }
            if (_target.UseCountEventRestrictedSetting)
            {
                EditorGUILayout.LabelField("=======カウントイベント実行制限==========");
                _target.EventType = (EventType)EditorGUILayout.EnumPopup("対象のカウントイベント:", _target.CountEventName);
                _target.CountEventValue = (int)EditorGUILayout.IntField("カウントイベントの値", _target.CountEventValue);
                _target.CountEventJudge = (CountEventJudge)EditorGUILayout.EnumPopup("", _target.CountEventJudge);
                EditorGUILayout.LabelField("時にイベントが実行可能");
                EditorGUILayout.LabelField("========================================");
            }
            base.OnInspectorGUI();
        }
    }
    //======================================

}
