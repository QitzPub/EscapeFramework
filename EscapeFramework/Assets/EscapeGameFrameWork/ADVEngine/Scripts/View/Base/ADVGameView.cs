using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Qitz.ArchitectureCore.ADVGame;

namespace Qitz.ADVGame
{

    public abstract class ADVGameView : AView
    {
        protected IADVGameController aDVGameController => this.GetController<ADVGameController>();
    }

    public interface IReceivableADVContoroller
    {

    }

    public static class ADVContorollerExtensions
    {

        static IController controller;
        public static T GetController<T>(this IReceivableADVContoroller receivable) where T : Component, IController
        {
            if (controller == null)
            {
                controller = Object.FindObjectOfType<T>();
            }
            return controller as T;
        }
    }
}
