using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Qitz.EscapeFramework
{
    public class TextAnimation : MonoBehaviour
    {
        //public string[] scenarios;
        [SerializeField] Text uiText;

        [SerializeField]
        [Range(0.001f, 0.3f)]
        float intervalForCharacterDisplay = 0.05f;

        private string currentText = string.Empty;
        private float timeUntilDisplay = 0;
        private float timeElapsed = 1;
        private int currentLine = 0;
        private int lastUpdateCharacter = -1;

        // 文字の表示が完了しているかどうか
        public bool IsCompleteDisplayText
        {
            get {
                if (currentText == string.Empty) return true;
                return Time.time > timeElapsed + timeUntilDisplay; 
            }
        }


        void Update()
        {
            int displayCharacterCount = (int)(Mathf.Clamp01((Time.time - timeElapsed) / timeUntilDisplay) * currentText.Length);
            if (displayCharacterCount != lastUpdateCharacter)
            {
                uiText.text = currentText.Substring(0, displayCharacterCount);
                lastUpdateCharacter = displayCharacterCount;
            }
        }

        public void ShowAllText()
        {
            timeUntilDisplay = 0;
        }

        public void SetNextLine(string Text)
        {
            currentText = Text;
            timeUntilDisplay = currentText.Length * intervalForCharacterDisplay;
            timeElapsed = Time.time;
            currentLine++;
            lastUpdateCharacter = -1;
        }
    }
}