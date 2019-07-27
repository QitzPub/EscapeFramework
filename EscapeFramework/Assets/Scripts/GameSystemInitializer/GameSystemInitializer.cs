using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Qitz.EscapeFramework
{
    public class GameSystemInitializer : MonoBehaviour
    {

        [RuntimeInitializeOnLoadMethod]
        static void GameSystemInitialize()
        {
            var ga = new GameObject();
            var controller = PrefabFolder.ResourcesLoadInstantiateTo("EscapeGameController", ga.transform.parent);
            Object.Destroy(ga);
            //デバッグオブジェクトの作成
            Object.DontDestroyOnLoad(controller);
        }

    }
}