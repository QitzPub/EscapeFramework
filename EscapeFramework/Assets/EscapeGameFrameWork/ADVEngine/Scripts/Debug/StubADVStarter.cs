using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Qitz.ADVGame
{
    public class StubADVStarter : ADVGameView
    {
        [SerializeField]
        TextAsset sampleMacro;
        // Start is called before the first frame update
        void Start()
        {
            this.aDVGameController.StartADV(sampleMacro.text,()=> { });
        }

    }
}
