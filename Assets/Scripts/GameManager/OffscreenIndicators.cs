using System.Collections.Generic;
using UnityEngine;

namespace Custom.Indicators
{
    [RequireComponent(typeof(Canvas))]
    public class OffscreenIndicators : MonoBehaviour
    {
        public Camera activeCamera;
        public List<Indicator> targetIndicators;
        public GameObject indicatorPrefab;
        public Vector2 offset;

        void Start()
        {
            InstantiateIndicators();
        }

        void Update()
        {
            foreach (var targetIndicator in targetIndicators)
            {
                // Kiểm tra nếu target bị null hoặc không hoạt động
                if (targetIndicator.target == null || !targetIndicator.target.gameObject.activeSelf)
                {
                    if (targetIndicator.indicatorUI.gameObject.activeSelf)
                        targetIndicator.indicatorUI.gameObject.SetActive(false); // Ẩn chỉ báo
                    continue;
                }

                bool isOffscreen = IsTargetOffscreen(targetIndicator.target.position);

                if (isOffscreen)
                {
                    UpdatePosition(targetIndicator);
                    if (!targetIndicator.indicatorUI.gameObject.activeSelf)
                    {
                        targetIndicator.indicatorUI.gameObject.SetActive(true); // Hiển thị chỉ báo
                    }
                }
                else if (targetIndicator.indicatorUI.gameObject.activeSelf)
                {
                    targetIndicator.indicatorUI.gameObject.SetActive(false); // Ẩn chỉ báo
                }
            }
        }

        private void InstantiateIndicators()
        {
            foreach (var targetIndicator in targetIndicators)
            {
                if (targetIndicator.indicatorUI == null)
                {
                    targetIndicator.indicatorUI = Instantiate(indicatorPrefab, transform).transform;
                    targetIndicator.rectTransform = targetIndicator.indicatorUI.GetComponent<RectTransform>()
                                              ?? targetIndicator.indicatorUI.gameObject.AddComponent<RectTransform>();
                }
            }
        }

        public void AddTarget(GameObject targetObject)
        {
            if (targetObject == null) return;

            var newIndicator = new Indicator
            {
                target = targetObject.transform,
                indicatorUI = Instantiate(indicatorPrefab, transform).transform
            };
            newIndicator.rectTransform = newIndicator.indicatorUI.GetComponent<RectTransform>()
                                         ?? newIndicator.indicatorUI.gameObject.AddComponent<RectTransform>();
            targetIndicators.Add(newIndicator);
        }

        private void UpdatePosition(Indicator targetIndicator)
        {
            var rect = targetIndicator.rectTransform.rect;
            var screenPosition = activeCamera.WorldToScreenPoint(targetIndicator.target.position);

            if (screenPosition.z < 0)
            {
                screenPosition *= -1;
            }

            screenPosition.x = Mathf.Clamp(screenPosition.x, rect.width / 2, Screen.width - rect.width / 2) + offset.x;
            screenPosition.y = Mathf.Clamp(screenPosition.y, rect.height / 2, Screen.height - rect.height / 2) + offset.y;

            targetIndicator.indicatorUI.position = screenPosition;
            targetIndicator.indicatorUI.rotation = Quaternion.LookRotation(Vector3.forward, (screenPosition - targetIndicator.indicatorUI.position).normalized);
        }

        private bool IsTargetOffscreen(Vector3 targetPosition)
        {
            Vector3 screenPoint = activeCamera.WorldToViewportPoint(targetPosition);
            return screenPoint.x < 0 || screenPoint.x > 1 || screenPoint.y < 0 || screenPoint.y > 1 || screenPoint.z < 0;
        }

        public void SyncIndicatorState(Transform target, bool isActive)
        {
            var indicator = targetIndicators.Find(ind => ind.target == target);
            if (indicator != null && indicator.indicatorUI != null)
            {
                indicator.indicatorUI.gameObject.SetActive(isActive);
            }
        }
    }

    [System.Serializable]
    public class Indicator
    {
        public Transform target;
        public Transform indicatorUI;
        [HideInInspector]
        public RectTransform rectTransform;
    }
}
