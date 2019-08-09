using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Qitz.EscapeFramework
{
    public interface ISceneTransitionEvent
    {
        string SceneName { get; }
    }

    public class SceneTransitionEvent : AEscapeGameEvent, ISceneTransitionEvent
    {
        [SerializeField, HeaderAttribute("次のシーンに遷移する")]
        string sceneName;
        public string SceneName => sceneName;
    }
}