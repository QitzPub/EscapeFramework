using UnityEngine;
using System.Collections;
using System;

namespace Qitz.EscapeFramework
{
    public class DelayTool : MonoBehaviour
    {
        //static DelayTool self;

        public static DelayTool Tools
        {
            get{
                GameObject go = new GameObject("delayObject");
                var self = go.AddComponent<DelayTool>();
                return self;
            }
        }

        public Coroutine Delay(float waitTime, Action action)
        {
            return this.StartCoroutine(_Delay(waitTime, action));
        }

        private IEnumerator _Delay(float waitTime, Action action)
        {
            yield return new WaitForSeconds(waitTime);
            action();
            DestroyImmediate(this.gameObject);
        }


        public void Stop(Coroutine coroutine)
        {
            //this.StopCoroutine(coroutine);
            DestroyImmediate(this.gameObject);
        }

    }
}