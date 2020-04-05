using System;
using UnityEngine;
using UnityEngine.UI;

namespace Lockdown.Game
{
    public class UiManager : MonoBehaviour
    {
        [SerializeField] private Text messageText;

        private void Start()
        {
            messageText.enabled = false;
        }

        public void ShowText(string placeYourBase)
        {
            messageText.enabled = true;
            messageText.text = placeYourBase;
        }

        public void HideText()
        {
            messageText.enabled = false;
        }
    }
}