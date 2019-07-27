using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
namespace Qitz.EscapeFramework
{
    [RequireComponent(typeof(Button))]
    public class EventSwitchButton : AView
    {
        [SerializeField]
        EventType eventType;
        public void SendEventToGameController()
        {

            //controllerにイベントを送る処理
            //this.controller
        }

    }
}