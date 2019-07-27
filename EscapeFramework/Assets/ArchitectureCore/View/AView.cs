using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Qitz.EscapeFramework
{
    public abstract class AView : MonoBehaviour, IView
    {
        protected EscapeGameController controller => this.GetController<EscapeGameController>();
    }
}
