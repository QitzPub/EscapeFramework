using System.Collections;
using System.Collections.Generic;
using Qitz.ArchitectureCore.ADVGame;
using UnityEngine;
namespace Qitz.ADVGame
{
    public class ADVSystemInitializer
    {
        [RuntimeInitializeOnLoadMethod]
        public static void ADVSystemInitialize()
        {

            var ga = new GameObject();
            var controller = PrefabFolder.ResourcesLoadInstantiateTo("ADVSystem", ga.transform.parent);
            Object.Destroy(ga);
            Object.DontDestroyOnLoad(controller);
        }
    }
}
