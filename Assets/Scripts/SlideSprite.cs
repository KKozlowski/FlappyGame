using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Flapper
{
    public class SlideSprite : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer render;
        public float speed;

        void Update()
        {
            var offset = render.material.GetTextureOffset("_MainTex");
            offset += Vector2.right * Time.deltaTime * speed;
            render.material.SetTextureOffset("_MainTex", offset);
        }
    }
}