using System;
using UnityEngine;

namespace Script
{
    public class ScrollingBG : MonoBehaviour
    {
        public float speed;
        Transform player;
        [SerializeField] private Renderer bgRenderer;

        void Start ()
        {
            player = GameObject.FindWithTag("Player").transform;
        }
        private void Update()
        {
            bgRenderer.material.mainTextureOffset += new Vector2(speed * Time.deltaTime, 0);
            if(Vector3.Distance(player.position,transform.position) >= 10)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y+5, transform.position.z) ;
            }
        }

       

    }
}