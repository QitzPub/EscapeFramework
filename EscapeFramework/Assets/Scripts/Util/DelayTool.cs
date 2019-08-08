using UnityEngine;
using System.Collections;
using System;

namespace Qitz.EscapeFramework
{
    public class DelayTool : MonoBehaviour
    {
        static DelayTool self;

        public static DelayTool Tools
        {
            get{
                GameObject go = new GameObject("delayObject");
                self = go.AddComponent<DelayTool>();
                return self;
            }
        }

        public Coroutine Delay(float waitTime, Action action)
        {
            return self.StartCoroutine(_Delay(waitTime, action));
        }

        private IEnumerator _Delay(float waitTime, Action action)
        {
            yield return new WaitForSeconds(waitTime);
            action();
            Destroy(self.gameObject);
        }


        public void Stop(Coroutine coroutine)
        {
            self.StopCoroutine(coroutine);
            Destroy(self.gameObject);
        }

    }
}