using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace FishingGame
{
    public class FishingUIController : MonoBehaviour
    {
        [SerializeField] private FishingAction fishingAction;
        [SerializeField] private Tackle tackle;
        [Header("UI Reference")]
        [SerializeField] private RectTransform mainPanel;
        [SerializeField] private RectTransform fishCaughtPanel;
        [SerializeField] private RectTransform fishEscapePanel;
        [SerializeField] private TextMeshProUGUI fishName;
        
        [SerializeField] private Button closeButton;

        private Vector2 initialSize;

        private void Awake()
        {
            initialSize = mainPanel.sizeDelta;
            mainPanel.sizeDelta = Vector2.zero;

            fishingAction.OnFishingEnd += OnFishingEnd;
            closeButton.onClick.AddListener(Hide);
            SetObjectsActive(false);
        }

        // Start is called before the first frame update
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
        
        }

        private void OnFishingEnd(Fish fish)
        {
            if(fish != null)
            {
                fishName.text = fish.DisplayName;
                Show(fishCaughtPanel);
            } else
            {
                if(tackle.IsInteractedWithFish)
                {
                    Show(fishEscapePanel);
                }
            }
        }

        private void Show(RectTransform content)
        {
            mainPanel.DOSizeDelta(initialSize, .3f).SetEase(Ease.OutQuad).OnComplete(() =>
            {
                content.gameObject.SetActive(true);
                closeButton.gameObject.SetActive(true);
            });
        }

        private void Hide()
        {
            SetObjectsActive(false);

            mainPanel.DOSizeDelta(Vector2.zero, .3f).SetEase(Ease.OutQuad);
        }

        private void SetObjectsActive(bool active)
        {
            fishCaughtPanel.gameObject.SetActive(active);
            fishEscapePanel.gameObject.SetActive(active);
            closeButton.gameObject.SetActive(active);
        }
    }
}
