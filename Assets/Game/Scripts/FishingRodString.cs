using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FishingGame
{
    public class FishingRodString : MonoBehaviour
    {
        [SerializeField] private LineRenderer lineRenderer;
        [SerializeField] private Transform fishingRodTip;
        [SerializeField] private Transform tackle;

        // Start is called before the first frame update
        void Start()
        {
        
        }

        private void OnEnable()
        {
            lineRenderer.enabled = true;
        }

        private void OnDisable()
        {
            lineRenderer.enabled = false;
        }

        // Update is called once per frame
        void Update()
        {
            lineRenderer.SetPosition(0, fishingRodTip.transform.position);
            lineRenderer.SetPosition(1, tackle.transform.position);
        }
    }
}
