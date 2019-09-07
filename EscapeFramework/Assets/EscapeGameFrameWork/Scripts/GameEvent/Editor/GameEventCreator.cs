using UnityEngine;
using UnityEditor;

namespace Qitz.EscapeFramework
{
    public class GameEventCreator
    {

        [MenuItem("GameObject/QitzEscapeGameEvent", priority = 21)]
        public static void GameObjectMenuItem()
        {
            Debug.Log("GameObjectMenuItem");
            PrefabFolder.ResourcesLoadInstantiateTo("EscapeGameEvent", Selection.activeGameObject.transform);

        }

    }
}