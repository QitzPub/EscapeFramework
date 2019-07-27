using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

namespace Qitz.EscapeFramework
{
    public interface IEscapeGameUserDataStore: ICanHoldItems, ICanHoldEvents
    {
    }

    public class EscapeGameUserDataStore: IEscapeGameUserDataStore
    {

        const string SAVE_KEY = "EscapeGameSaveData";
        IEscapeGameUserVO userVO;

        public IEscapeGameUserVO UserVO => userVO;
        public List<ItemVO> Items => UserVO.Items;

        public List<EventFlagVO> EventFlags => UserVO.EventFlags;

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

        public EscapeGameUserDataStore()
        {
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
    }
    public interface ICanHoldEvents
    {
        List<EventFlagVO> EventFlags { get; }
        void SetEventFlag(EventFlagVO eventFlag);
    }

    public interface IEscapeGameUserVO: ICanHoldItems, ICanHoldEvents
    {
        string ToJson();
    }


    [Serializable]
    public class EscapeGameUserVO: IEscapeGameUserVO
    {

        [SerializeField]
        List<ItemVO> items = new List<ItemVO>();
        [SerializeField]
        List<EventFlagVO> eventFlags = new List<EventFlagVO>();

        public List<ItemVO> Items => throw new NotImplementedException();

        public List<EventFlagVO> EventFlags => throw new NotImplementedException();

        public void AddItem(ItemVO item)
        {
            items.Add(item);
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