
using UnityEngine;
using Qitz.ArchitectureCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Qitz.EscapeFramework
{
    public interface IEscapeGameController
    {
    }

    public class EscapeGameController : AController<EscapeGameRepository>, IEscapeGameController
    {
        [SerializeField]
        EscapeGameRepository repository;
        protected override EscapeGameRepository Repository { get { return repository; } }
    }
}