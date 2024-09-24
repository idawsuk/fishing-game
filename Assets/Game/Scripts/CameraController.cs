using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

namespace FishingGame
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField] private List<Target> followTargets;
        [SerializeField] private Vector3 middlePoint;
        [SerializeField] private float followSpeed;
        [SerializeField] private Vector3 offset;
        [SerializeField] private float rotateSpeed;
        [SerializeField] private PlayerInputs input;

        [System.Serializable]
        public struct Target
        {
            public Transform Transform;
            [Range(0f, 1f)]
            public float Weight;
        }

        // Start is called before the first frame update
        void Start()
        {
            
        }

        // Update is called once per frame
        void Update()
        {
            middlePoint = Vector3.zero;
            int count = 0;
            for (int i = 0; i < followTargets.Count; i++)
            {
                if (followTargets[i].Transform.gameObject.activeSelf)
                {
                    middlePoint += followTargets[i].Transform.position * followTargets[i].Weight;
                    count++;
                }
            }

            middlePoint = middlePoint / count;


            Quaternion camTurnAngle = transform.rotation;
            if(input.CameraRotation.IsPressed())
            {
                camTurnAngle = Quaternion.AngleAxis(Input.GetAxis("Mouse X") * rotateSpeed, Vector3.up);
                offset = camTurnAngle * offset;
            }

            this.transform.position = Vector3.Lerp(this.transform.position, offset + middlePoint, followSpeed * Time.deltaTime);
            Quaternion toRotation = Quaternion.LookRotation(middlePoint - this.transform.position);
            this.transform.rotation = Quaternion.Lerp(this.transform.rotation, toRotation, Time.deltaTime * rotateSpeed);
        }
    }
}
