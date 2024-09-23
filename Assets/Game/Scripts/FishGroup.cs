using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FishingGame
{
    public class FishGroup : MonoBehaviour
    {
        [SerializeField] private float circleSize;

        // Start is called before the first frame update
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
        
        }

        public Vector3 GetRandomPosition()
        {
            Vector3 pos = Random.insideUnitSphere * circleSize;
            pos += this.transform.position;
            pos.y = this.transform.position.y;

            return pos;
        }
    }
}
