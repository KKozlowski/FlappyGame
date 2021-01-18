using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Flapper.UI
{
    public class SplashMenu : MonoBehaviour
    {
        public void ClickStart()
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(1);
        }
    }
}