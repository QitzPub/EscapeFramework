using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Qitz.EscapeFramework
{
    [RequireComponent(typeof(ItemDropable)),Serializable]
    public class GameEvent : AEvent
    {
        private void Awake()
        {
            this.Button.transition = UnityEngine.UI.Selectable.Transition.None;
            EventToken = Guid.NewGuid().ToString();
        }
        //基本設定ここから==========================================
        public EventType EventType;
        public EventExecuteTiming EventExecuteTiming;
        public ItemName TargetItemName;
        public ItemDropable DropableView => this.GetComponent<ItemDropable>();
        [SerializeField]
        GameEvent chainEvent;
        public GameEvent ChainEvent => chainEvent;
        public string EventToken { get; private set; }
        //基本設定ここまで==========================================
        //TextWindow設定ここから==========================================
        [SerializeField]
        List<string> texts;
        public List<string> Texts => texts;
        //TextWindow設定ここまで==========================================
        //ADVWindow設定ここから==========================================
        [SerializeField]
        TextAsset advMacro;
        public string AdvMacro => advMacro.text;
        //ADVWindow設定ここまで==========================================
        //BGMEvent設定ここから==========================================
        public BGMName BGMName;
        public AudioCommandType BGMAudioCommandType;
        //BGMEvent設定ここまで==========================================
        //DisplayEvent設定ここから==========================================
        public DisplayEventProgress DisplayEventProgress;
        //DisplayEvent設定ここまで==========================================
        //EventFlagEvent設定ここから==========================================
        public EventName EventName;
        public EventFlag EventFlag;
        public EventFlagVO EventFlagVO => new EventFlagVO(EventName, EventFlag == EventFlag.ON);
        //EventFlagEvent設定ここまで==========================================
        //CountEvent設定ここから==========================================
        public CountEventType CountEventName;
        public CountEventProgress CountEventProgress;
        public CountEventVO CountEventVO => new CountEventVO(CountEventName);
        //CountEvent設定ここまで==========================================
        //ItemEvent設定ここから==========================================
        public ItemName ItemName;
        public EventProgress ItemEventProgress;
        public ItemVO ItemVO => new ItemVO(ItemName);
        //ItemEvent設定ここまで==========================================
        //SceneTransitionEvent設定ここから==========================================
        public string SceneName;
        //SceneTransitionEvent設定ここまで==========================================
        //ScreenEffectEvent設定ここから==========================================
        public ScreenEffectName ScreenEffect;
        //ScreenEffectEvent設定ここまで==========================================
        //SEEvent設定ここから==========================================
        public SEName SEName;
        public AudioCommandType SEAudioCommandType;
        //SEEvent設定ここまで==========================================
        //SpriteChangeEvent設定ここから==========================================
        public Sprite ChangeSprite;
        //SpriteChangeEvent設定ここまで==========================================
        //アイテムウィンドウ設定ここから==========================================
        public ItemWinodwEvent ItemWinodwEvent;
        //アイテムウィンドウ設定ここまで==========================================
    }
}