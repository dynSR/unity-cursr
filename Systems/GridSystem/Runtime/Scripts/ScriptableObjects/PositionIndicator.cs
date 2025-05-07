using Sirenix.OdinInspector;
using UnityEngine;
using UnityTools.Library.Extensions;
using UnityEngine.Assertions;

namespace GridSystem.Runtime.ScriptableObjects {
    [CreateAssetMenu(fileName = "New Position Indicator",
        menuName = "Game/Position Indicators/Create new position indicator")]
    public class PositionIndicator : ScriptableObject {
        [field: SerializeField, Required] private GameObject IndicatorPrefab { get; set; }
        [field: SerializeField] private LayerMask associatedLayerMask;
        private GameObject indicator;

        public void Init(Transform parent) {
            Assert.IsNotNull(IndicatorPrefab, "IndicatorPrefab must be defined in order to create an instance");
            indicator = Instantiate(IndicatorPrefab, parent);
        }

        public void ProcessUpdate(Vector3 position, bool shouldBeDisplayed) {
            indicator.SetActive(shouldBeDisplayed);
            SetPosition(position);
        }

        private void SetPosition(Vector3 position) {
            Assert.IsNotNull(indicator, "indicator must be defined before trying to set its position");
            indicator.SetLocalPosition(position);
        }
    }
}