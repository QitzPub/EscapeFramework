using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Qitz.EscapeFramework
{

    [RequireComponent(typeof(Button))]
    [RequireComponent(typeof(Image))]
    public class AEvent : MonoBehaviour
    {
        public Button Button => this.GetComponent<Button>();
        [SerializeField, HeaderAttribute("イベント発火制限条件")]
        AIgnitionPointBase aIgnitionPointBase;
        public AIgnitionPointBase AIgnitionPointBase => aIgnitionPointBase;
    }
}