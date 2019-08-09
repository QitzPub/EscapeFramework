using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Qitz.EscapeFramework
{
    public class ItemDropSceneTransitionEvent : AItemDropEvent, ISceneTransitionEvent
    {
        [SerializeField, HeaderAttribute("次のシーンに遷移する")]
        string sceneName;
        public string SceneName => sceneName;
    }
}