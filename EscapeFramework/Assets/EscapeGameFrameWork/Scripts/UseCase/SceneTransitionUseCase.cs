using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Qitz.EscapeFramework
{
    public interface ISceneTransitionUseCase
    {
        void GotoScene(string sceneName);
    }
    public class SceneTransitionUseCase: ISceneTransitionUseCase
    {
        public void GotoScene(string sceneName)
        {
            SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
        }
    }
}