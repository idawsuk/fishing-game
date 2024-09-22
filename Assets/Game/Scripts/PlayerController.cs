using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FishingGame
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private Vector3 lookAtDirection;
        [SerializeField] private float turnRate = 3;
        private bool lookAtTarget = false;

        // Start is called before the first frame update
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
            if(lookAtTarget)
            {
                Quaternion toRotation = Quaternion.FromToRotation(transform.forward, lookAtDirection);
                transform.rotation = Quaternion.Lerp(transform.rotation, toRotation, turnRate * Time.deltaTime);
            }
        }

        public void LookAt(Vector3 target)
        {
            target.y = transform.position.y;
            lookAtDirection = target - transform.position;
            lookAtTarget = true;
            //transform.LookAt(target, Vector3.up);
        }
    }
}
