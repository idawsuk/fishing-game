using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FishingGame
{
    public class PlayerController : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
        
        }

        public void LookAt(Vector3 target)
        {
            target.y = transform.position.y;
            transform.LookAt(target, Vector3.up);
        }
    }
}
