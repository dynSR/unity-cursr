using Sirenix.OdinInspector;
using UnityEngine;
using UnityTools.Library.Extensions;
using UnityTools.Systems.GridSystem.Runtime.Enums;
using UnityTools.Systems.GridSystem.Runtime.Utils;

namespace UnityTools.Systems.GridSystem.Runtime.Services {
    public class GridSystem : MonoBehaviour {
        [SerializeField, EnumToggleButtons] private GridSize size;
        [SerializeField] private LayerMask layer;

        [SerializeField, Title("Overlay settings"), Required]
        private Material overlayMaterial;

        [SerializeField, Range(0.01f, 0.15f)] private float overlayThickness = 0.01f;

        [SerializeField, ChildGameObjectsOnly, Title("Indicators"), Required]
        private GameObject mouseIndicator;

        [SerializeField, ChildGameObjectsOnly, Required]
        private GameObject cellIndicator;

        private UnityEngine.Grid Grid => transform.GetOrAddComponent<UnityEngine.Grid>();
        private const float cellIndicatorYOffset = 0.01f;
        private static readonly int sizeId = Shader.PropertyToID("_Size");
        private static readonly int thicknessId = Shader.PropertyToID("_Thickness");


        private void Update() {
            Vector3 pointFromMousePosition =
                MouseRaycasterUtils.GetRaycastHitPointFromMousePosition(Camera.main, 100f, layer);
            mouseIndicator.SetLocalPosition(pointFromMousePosition);
            SetCellIndicatorPosition(pointFromMousePosition);
        }

        private void AdjustGridSize() {
            if (size == GridSize.Small) {
                Grid.cellSize = new Vector3(0.5f, 0.5f, 0.5f);
                cellIndicator.SetScale(new Vector3(0.19f, 0.19f, 0));
                overlayMaterial.SetFloat(sizeId, 2f);
                return;
            }

            Grid.cellSize = new Vector3(1f, 1f, 1f);
            cellIndicator.SetScale(new Vector3(0.38f, 0.38f, 0));
            overlayMaterial.SetFloat(sizeId, 1f);
        }

        private void SetGridOverlayThickness(float value) {
            if (Mathf.Approximately(overlayMaterial.GetFloat(thicknessId), value)) return;
            overlayMaterial.SetFloat(thicknessId, value);
        }

        private void SetCellIndicatorPosition(Vector3 pointFromMousePosition) {
            Vector3 cellWorldPosition = GetCellWorldPosition(pointFromMousePosition);
            if (cellWorldPosition == Vector3.zero) {
                cellIndicator.SetLocalPosition(Vector3.zero);
                return;
            }

            Vector3 gridCellSize = Grid.cellSize;
            Vector2 gridOffset = Grid.cellLayout == GridLayout.CellLayout.Isometric
                ? new Vector2(0, gridCellSize.z / 2f)
                : new Vector2(gridCellSize.x / 2f, gridCellSize.z / 2f);
            Vector3 cellIndicatorPosition = new(
                GetCellWorldPosition(pointFromMousePosition).x + gridOffset.x,
                cellIndicatorYOffset,
                GetCellWorldPosition(pointFromMousePosition).z + gridOffset.y
            );
            cellIndicator.SetLocalPosition(cellIndicatorPosition);
        }

        private Vector3 GetCellWorldPosition(Vector3 mouseHitPosition) => Grid.CellToWorld(
            GetGridWorldPosition(mouseHitPosition));

        private Vector3Int GetGridWorldPosition(Vector3 mouseHitPosition) => Grid.WorldToCell(mouseHitPosition);

        #region Editor

#if UNITY_EDITOR
        private void OnValidate() {
            AdjustGridSize();
            SetGridOverlayThickness(overlayThickness);
        }
#endif

        #endregion
    }
}