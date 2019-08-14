using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Qitz.EscapeFramework
{
    public class EscapeGameSystemInitializer 
    {

        [RuntimeInitializeOnLoadMethod]
        static void GameSystemInitialize()
        {
            var ga = new GameObject();
            var controller = PrefabFolder.ResourcesLoadInstantiateTo("EscapeGameController", ga.transform.parent);
            //var itemWindow = PrefabFolder.ResourcesLoadInstantiateTo("ItemWindowView", ga.transform.parent);
            Object.Destroy(ga);
            Object.DontDestroyOnLoad(controller);
            //Object.DontDestroyOnLoad(itemWindow);
        }

    }
}