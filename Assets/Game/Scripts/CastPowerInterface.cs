using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace FishingGame
{
    public class CastPowerInterface : MonoBehaviour
    {
        [SerializeField] private CanvasGroup canvasGroup;
        [SerializeField] private Image power;
        [SerializeField] private CastFishingRodAction action;
        [SerializeField] private Color fromColor;
        [SerializeField] private Color toColor;

        private bool isOn = false;

        // Start is called before the first frame update
        void Start()
        {
            canvasGroup.alpha = 0;
            action.OnBackCastingStarted += OnBackCastingStarted;
        }

        // Update is called once per frame
        void Update()
        {
            if (!isOn)
                return;

            power.fillAmount = action.Power;
            power.color = Color.Lerp(fromColor, toColor, action.Power);
        }

        private void OnBackCastingStarted(bool isStarted)
        {
            canvasGroup.alpha = isStarted ? 1 : 0;
            isOn = isStarted;
        }
    }
}
