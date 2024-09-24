using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FishingGame
{
    public class Fish : MonoBehaviour
    {
        [SerializeField] private string displayName;
        [SerializeField] private float biteDuration = 3f;
        [SerializeField] private float eatDuration = 5f;
        [SerializeField] private float moveInterval = 5f;
        [SerializeField] private float moveSpeed = 3;
        [SerializeField] private float turnRate = 2;
        private float time = 0;
        private bool isBiting = false;
        private bool isPulled = false;
        private Tackle tackle;
        private Vector3 lookAtPosition;
        private float moveTime = 0;
        private FishGroup fishGroup;
        private Vector3 moveTargetPos;

        public bool IsBiting => isBiting;
        public string DisplayName => displayName;

        // Start is called before the first frame update
        void Start()
        {
            fishGroup = GetComponentInParent<FishGroup>();
            moveTargetPos = fishGroup.GetRandomPosition();
            moveTargetPos.y = transform.position.y;
        }

        // Update is called once per frame
        void Update()
        {
            if(tackle != null && tackle.IsInWater)
            {
                time += Time.deltaTime;
                lookAtPosition = tackle.transform.position;
                lookAtPosition.y = transform.position.y;
                transform.position = Vector3.Lerp(transform.position, lookAtPosition, Time.deltaTime);
                transform.LookAt(lookAtPosition, Vector3.up);

                if(time >= biteDuration && time <= eatDuration)
                {
                    isBiting = true;
                    tackle.Eating();
                } else
                {
                    isBiting = false;
                    if(time > eatDuration)
                    {
                        tackle.EatFinish();
                        tackle = null;
                    }
                }
            } else if(!isPulled)
            {
                moveTime += Time.deltaTime;
                if(moveTime > moveInterval)
                {
                    moveTime = 0;
                    moveTargetPos = fishGroup.GetRandomPosition();
                    moveTargetPos.y = transform.position.y;
                }

                transform.position = Vector3.Lerp(transform.position, moveTargetPos, Time.deltaTime * moveSpeed);
                //Quaternion toRotation = Quaternion.FromToRotation(transform.forward, moveTargetPos - transform.position);
                Quaternion toRotation = Quaternion.LookRotation(moveTargetPos - transform.position, Vector3.up);
                transform.rotation = Quaternion.Lerp(transform.rotation, toRotation, turnRate * Time.deltaTime);
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if(other.tag == "Tackle")
            {
                time = 0;
                Tackle t = other.GetComponent<Tackle>();
                if(t != null && t.IsBaitAvailable && !t.IsBitten)
                {
                    tackle = t;
                    tackle.Bite(this);
                } else
                {
                    tackle = null;
                }
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.tag == "Tackle")
            {
                if(tackle != null)
                {
                    tackle.Bite(null);
                    tackle = null;
                }
            }
        }

        public void Pull()
        {
            isBiting = false;
            isPulled = true;
            tackle = null;
            //gameObject.SetActive(false);
        }

        public void Escape()
        {
            isBiting = false;
            tackle = null;
        }
    }
}
