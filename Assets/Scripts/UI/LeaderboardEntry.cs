using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Flapper.UI
{
    public class LeaderboardEntry : MonoBehaviour
    {
        [SerializeField] private Text display;

        public void Show(int position, int value)
        {
            display.text = $"<color=grey>{position}.</color> {value}";
            gameObject.SetActive(true);
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }
    }
}