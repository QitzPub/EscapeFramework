using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Qitz.EscapeFramework
{
    [RequireComponent(typeof(Button))]
    [RequireComponent(typeof(Image))]
    public abstract class AEventViewBase : MonoBehaviour,IView
    {
        [SerializeField]
        EventExecuteTiming eventExecuteTiming;
        public EventExecuteTiming EventExecuteTiming => eventExecuteTiming;
        [SerializeField, HeaderAttribute("イベント発火制限条件")]
        AIgnitionPointBase aIgnitionPointBase;
        public AIgnitionPointBase AIgnitionPointBase => aIgnitionPointBase;
        public Button Button => this.GetComponent<Button>();
    }
}