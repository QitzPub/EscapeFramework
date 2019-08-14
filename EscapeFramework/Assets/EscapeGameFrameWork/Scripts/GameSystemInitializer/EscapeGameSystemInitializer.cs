using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Qitz.EscapeFramework
{
    public class EscapeGameSystemInitializer 
    {

        [RuntimeInitializeOnLoadMethod]
        public static void GameSystemInitialize()
        {
            var _controller = Object.FindObjectOfType<EscapeGameController>();
            if(_controller != null)
            {
                Object.Destroy(_controller.gameObject);
                ViewExtensions.CashClear();
            }

            var ga = new GameObject();
            var controller = PrefabFolder.ResourcesLoadInstantiateTo("EscapeGameController", ga.transform.parent);
            //var itemWindow = PrefabFolder.ResourcesLoadInstantiateTo("ItemWindowView", ga.transform.parent);
            Object.Destroy(ga);
            Object.DontDestroyOnLoad(controller);
            //Object.DontDestroyOnLoad(itemWindow);
        }

    }
}