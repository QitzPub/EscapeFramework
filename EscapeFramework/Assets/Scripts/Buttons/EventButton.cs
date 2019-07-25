using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class EventButton : Button
{
    [SerializeField]
    EventType eventType;
    [SerializeField]
    string value;

    public override void OnPointerDown(PointerEventData eventData)
    {
        base.OnPointerDown(eventData);
        //ここにコントローラーへTypeとStringValueの値を飛ばす処理を設定する
    }
}
