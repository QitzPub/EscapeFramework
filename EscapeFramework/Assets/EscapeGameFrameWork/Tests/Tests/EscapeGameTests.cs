using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using Qitz.EscapeFramework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class EscapeGameTests
    {
        EscapeGameController controller;
        // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
        // `yield return null;` to skip a frame.
        [UnityTest]
        public IEnumerator ユーザーのセーブデータDump()
        {
            controller = Object.FindObjectOfType<EscapeGameController>();
            controller.DumpUserdata();
            // Use the Assert class to test conditions.
            // Use yield to skip a frame.
            yield return null;
        }
    }
}
