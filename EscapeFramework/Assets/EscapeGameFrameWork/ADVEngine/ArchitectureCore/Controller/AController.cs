using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Qitz.ArchitectureCore.ADVGame
{
    public abstract class AController<T> : MonoBehaviour, IController where T : IRepository
    {

        protected abstract T Repository { get; }

    }
}
