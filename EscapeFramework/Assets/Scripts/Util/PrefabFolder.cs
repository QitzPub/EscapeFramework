using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Qitz.EscapeFramework
{
    public class PrefabFolder
    {
        public static T InstantiateTo<T>(MonoBehaviour obj, Transform parent) where T : Component
        {
            var script = Object.Instantiate(obj).GetComponent<T>();
            script.transform.SetParent(parent);
            script.transform.localPosition = Vector3.zero;
            script.transform.localScale = Vector3.one;
            return script;
        }
        public static T InstantiateTo<T>(MonoBehaviour obj) where T : Component
        {
            var script = Object.Instantiate(obj).GetComponent<T>();
            script.transform.localPosition = Vector3.zero;
            script.transform.localScale = Vector3.one;
            return script;
        }
        public static T InstantiateTo<T>(GameObject obj, Transform parent) where T : Component
        {
            var script = Object.Instantiate(obj).GetComponent<T>();
            script.transform.SetParent(parent);
            script.transform.localPosition = Vector3.zero;
            script.transform.localScale = Vector3.one;
            return script;
        }
        public static GameObject InstantiateTo(GameObject obj, Transform parent)
        {
            var ga = Object.Instantiate(obj);
            ga.transform.SetParent(parent);
            ga.transform.localPosition = Vector3.zero;
            ga.transform.localScale = Vector3.one;
            return ga;
        }
        public static GameObject InstantiateTo(GameObject obj)
        {
            var ga = Object.Instantiate(obj);
            ga.transform.localPosition = Vector3.zero;
            ga.transform.localScale = Vector3.one;
            return ga;
        }
        public static T ResourcesLoadInstantiateTo<T>(string path, Transform parent) where T : Component
        {

            var obj = (GameObject)Resources.Load(path);
            var script = Object.Instantiate(obj).GetComponent<T>();
            script.transform.SetParent(parent);
            script.transform.localPosition = Vector3.zero;
            script.transform.localScale = Vector3.one;
            return script;
        }
        public static GameObject ResourcesLoadInstantiateTo(string path, Transform parent)
        {
            var obj = (GameObject)Resources.Load(path);
            var script = Object.Instantiate(obj);
            script.transform.SetParent(parent);
            script.transform.localPosition = Vector3.zero;
            script.transform.localScale = Vector3.one;
            return script;
        }
        public static GameObject ResourcesLoadInstantiateTo(string path)
        {
            var obj = (GameObject)Resources.Load(path);
            var script = Object.Instantiate(obj);
            script.transform.localPosition = Vector3.zero;
            script.transform.localScale = Vector3.one;
            return script;
        }
    }

}