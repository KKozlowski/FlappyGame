using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Flapper
{
    public class KillTrigger : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.tag == "Bird")
                Debug.LogError("EEEE");
        }
    }
}