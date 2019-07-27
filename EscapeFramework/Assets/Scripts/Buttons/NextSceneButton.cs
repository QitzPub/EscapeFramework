using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Qitz.EscapeFramework
{
    [RequireComponent(typeof(Button))]
    public class NextSceneButton : MonoBehaviour
    {

        [SerializeField]
        string SceneName;
        public void GotoScene()
        {
            SceneManager.LoadScene(SceneName);

        }
    }
}