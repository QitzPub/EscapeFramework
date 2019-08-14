using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Qitz.EscapeFramework
{
    public class StartButtonView : MonoBehaviour,IView
    {
        public void ClearUserDate()
        {
            this.GetController<EscapeGameController>().ClearUserData();
        }
    }
}