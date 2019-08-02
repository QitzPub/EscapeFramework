using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

namespace Qitz.EscapeFramework
{
    public interface IEscapeGameUserDataStore: ICanHoldItems, ICanHoldEvents, ICanHoldCountEvents
    {
        bool GetEventFlagValue(IEventFlagVO eventFlagVO);
        bool GetEventFlagValue(EventType eventType);
        bool InPossessionItem(ItemVO itemVO);
        bool InPossessionItem(ItemName itemName);
    }

    public class EscapeGameUserDataStore: IEscapeGameUserDataStore
    {

        const string SAVE_KEY = "EscapeGameSaveData";
        IEscapeGameUserVO userVO;
        List<CountEventSetting> countEventSettings;

        public IEscapeGameUserVO UserVO => userVO;
        public List<ItemVO> Items => UserVO.Items;

        public List<EventFlagVO> EventFlags => UserVO.EventFlags;

        public List<CountEventVO> CountEvents => UserVO.CountEvents;

        void SaveUserData()
        {
            PlayerPrefs.SetString(SAVE_KEY, userVO.ToJson());
            PlayerPrefs.Save();
        }

        IEscapeGameUserVO LoadUserData()
        {
            var json = PlayerPrefs.GetString(SAVE_KEY, "");
            if (json == "")
                return null; 
            return JsonUtility.FromJson<EscapeGameUserVO>(json);
        }

        public void AddItem(ItemVO item)
        {
            UserVO.AddItem(item);
            SaveUserData();
        }
        public void SetEventFlag(EventFlagVO eventFlag)
        {
            UserVO.SetEventFlag(eventFlag);
            SaveUserData();
        }

        public bool GetEventFlagValue(IEventFlagVO eventFlagVO)
        {
            bool existEventFlag = EventFlags.Exists(ef => ef.EventType == eventFlagVO.EventType);
            if (existEventFlag)
            {
                return EventFlags.FirstOrDefault(ef => ef.EventType == eventFlagVO.EventType).IsOn;
            }
            else
            {
                return false;
            }
        }

        public bool GetEventFlagValue(EventType eventType)
        {
            bool existEventFlag = EventFlags.Exists(ef => ef.EventType == eventType);
            if (existEventFlag)
            {
                return EventFlags.FirstOrDefault(ef => ef.EventType == eventType).IsOn;
            }
            else
            {
                return false;
            }
        }

        public bool InPossessionItem(ItemVO itemVO)
        {
            return Items.Exists(it => it.ItemName == itemVO.ItemName);
        }

        public bool InPossessionItem(ItemName itemName)
        {
            return Items.Exists(it => it.ItemName == itemName);
        }

        public void DecreaseItem(ItemVO item)
        {
            UserVO.DecreaseItem(item);
            SaveUserData();
        }

        public void IncrementEventCount(CountEventType countEvent)
        {
            var cev = CountEvents.FirstOrDefault(ce=>ce.CountEventType == countEvent);
            var countEventSetting = countEventSettings.FirstOrDefault(ces=>ces.CountEventType == countEvent);
            if(countEventSetting == null)
            {
                throw new Exception($"カウントイベント設定データが設定されていません{countEvent}");
            }
            if (cev == null)
            {
                UserVO.AddCountEvent(countEvent);
                cev = CountEvents.FirstOrDefault(ce => ce.CountEventType == countEvent);
            }
            if (cev.Count < countEventSetting.MaxCount)
            {
                UserVO.IncrementEventCount(countEvent);
                SaveUserData();
            }

        }

        public void DecrementEventCount(CountEventType countEvent)
        {
            var cev = CountEvents.FirstOrDefault(ce => ce.CountEventType == countEvent);
            var countEventSetting = countEventSettings.FirstOrDefault(ces => ces.CountEventType == countEvent);
            if (countEventSetting == null)
            {
                throw new Exception($"カウントイベント設定データが設定されていません{countEvent}");
            }
            if(cev == null)
            {
                UserVO.AddCountEvent(countEvent);
                cev = CountEvents.FirstOrDefault(ce => ce.CountEventType == countEvent);
            }

            if (cev.Count > countEventSetting.MinCount)
            {
                UserVO.DecrementEventCount(countEvent);
                SaveUserData();
            }

        }

        public EscapeGameUserDataStore(List<CountEventSetting> countEventSettings)
        {
            this.countEventSettings = countEventSettings;
            userVO = LoadUserData();
            if (userVO == null)
            {
                userVO = new EscapeGameUserVO();
                SaveUserData();
            }
        }

    }

    public interface ICanHoldItems
    {
        List<ItemVO> Items { get; }
        void AddItem(ItemVO item);
        void DecreaseItem(ItemVO item);
    }
    public interface ICanHoldEvents
    {
        List<EventFlagVO> EventFlags { get; }
        void SetEventFlag(EventFlagVO eventFlag);
    }
    public interface ICanHoldCountEvents
    {
        List<CountEventVO> CountEvents { get; }
        void IncrementEventCount(CountEventType countEvent);
        void DecrementEventCount(CountEventType countEvent);
    }

    public interface IEscapeGameUserVO: ICanHoldItems, ICanHoldEvents, ICanHoldCountEvents
    {
        void AddCountEvent(CountEventType countEvent);
        string ToJson();
    }


    [Serializable]
    public class EscapeGameUserVO: IEscapeGameUserVO
    {

        [SerializeField]
        List<ItemVO> items = new List<ItemVO>();
        [SerializeField]
        List<EventFlagVO> eventFlags = new List<EventFlagVO>();
        [SerializeField]
        List<CountEventVO> countEvents = new List<CountEventVO>();


        public List<ItemVO> Items => items;

        public List<EventFlagVO> EventFlags => eventFlags;

        public List<CountEventVO> CountEvents => countEvents;

        public void AddItem(ItemVO item)
        {
            items.Add(item);
        }

        public void DecreaseItem(ItemVO item)
        {
            items.Remove(item);
        }
        public void AddCountEvent(CountEventType countEvent)
        {
            CountEventVO cEvent = new CountEventVO(countEvent);
            CountEvents.Add(cEvent);
        }

        public void DecrementEventCount(CountEventType countEvent)
        {
            bool existCountEvent = countEvents.Exists(ce => ce.CountEventType == countEvent);
            if (existCountEvent)
            {
                CountEvents.FirstOrDefault(ce => ce.CountEventType == countEvent).DecrementCount();
            }
            else
            {
                CountEventVO cEvent = new CountEventVO(countEvent);
                cEvent.DecrementCount();
                CountEvents.Add(cEvent);
            }
        }

        public void IncrementEventCount(CountEventType countEvent)
        {
            bool existCountEvent = countEvents.Exists(ce=>ce.CountEventType == countEvent);
            if (existCountEvent)
            {
                CountEvents.FirstOrDefault(ce => ce.CountEventType == countEvent).IncrementCount();
            }
            else
            {
                CountEventVO cEvent = new CountEventVO(countEvent);
                cEvent.IncrementCount();
                CountEvents.Add(cEvent);
            }
        }

        public void SetEventFlag(EventFlagVO eventFlag)
        {
            if(eventFlags.Exists(ef=>ef.EventType== eventFlag.EventType))
            {
                int eventIndex = eventFlags.FindIndex(ef=>ef.EventType== eventFlag.EventType);
                eventFlags[eventIndex] = eventFlag;
            }
            else
            {
                eventFlags.Add(eventFlag);
            }
        }

        public string ToJson()
        {
            return JsonUtility.ToJson(this);
        }
    }
}