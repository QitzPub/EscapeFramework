
using UnityEngine;

namespace Qitz.ArchitectureCore.ADVGame
{
    public interface IView
    {
    }

    public static class ViewExtensions
    {

        static IController controller;

        public static T GetController<T>(this IView view) where T : Component, IController
        {
            if (controller == null)
            {
                controller = Object.FindObjectOfType<T>();
            }
            return controller as T;
        }
    }
}

