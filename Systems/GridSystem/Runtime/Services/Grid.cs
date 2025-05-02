using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityTools.Systems.GridSystem.Runtime.Helpers;
using UnityUtils;
using UnityTools.Library.Extensions;
using UnityTools.Library.Structs;
using UnityTools.Library.Utils;
using UnityTools.Systems.GridSystem.Runtime.Utils;

namespace UnityTools.Systems.GridSystem.Runtime.Services {
    public class Grid : MonoBehaviour {
        [SerializeField, MinValue(1)] private Axis gridSize;

        [Title("Cell properties")] [SerializeField, Range(0, 0.8f)]
        private float cellGap;

        [SerializeField, Required] private Material cellMaterial;

        [Title("Other grid settings")] [SerializeField, Required]
        private GameObject holder;

        [SerializeField, Required] private Axis offset;

        [SerializeField] private LayerMask gridLayerMask;

        [Title("Indicators")] [SerializeField, ChildGameObjectsOnly, Required]
        private GameObject cursorHitIndicator;

        [SerializeField, ChildGameObjectsOnly, Required]
        private GameObject cellSelectionIndicator;

        private readonly List<Vector3> cellWorldPositions = new();
        private static Camera MainCamera => Camera.main;

        private const float cellSize = 1f;
        private const float yOffset = 0.01f;

        private void Awake() => BuildGrid();

        private void Update() {
            Vector3 mouseHitPosition =
                MouseRaycasterUtils.GetRaycastHitPointFromMousePosition(MainCamera, 100f, gridLayerMask);
            cursorHitIndicator.SetPosition(mouseHitPosition);
            SetCellSelectionIndicatorPosition(mouseHitPosition);

            cursorHitIndicator.SetActive(IsCursorHoveringGrid());
            cellSelectionIndicator.SetActive(IsCursorHoveringGrid());
        }

        private bool IsCursorHoveringGrid() => MouseRaycasterUtils.HitObject(MainCamera, 100f, gridLayerMask) != null;

        private void SetCellSelectionIndicatorPosition(Vector3 mouseHitPoint) {
            Vector3 cellWorldPos = cellWorldPositions.Find(pos => pos == new Vector3(
                Mathf.FloorToInt(mouseHitPoint.x),
                yOffset,
                Mathf.FloorToInt(mouseHitPoint.z)
            ));
            cellSelectionIndicator.SetPosition(new Vector3(
                    cellWorldPos.x + GetCellSize() / 2f,
                    yOffset,
                    cellWorldPos.z + GetCellSize() / 2f
                )
            );
        }

        [Button]
        public void BuildGrid() {
            DestroyGrid();
            ForEachCells((row, column) => {
                if (holder.transform.childCount >= gridSize.GetCombinedValueInt()) return;
                Cell cell = Cell.Factory.Create(
                    cellMaterial,
                    holder.transform,
                    new Vector3(GetCellSize(), GetCellSize(), 0),
                    new Vector3(
                        GetXPositionInGrid(row) - GetCellSize() / 2,
                        yOffset,
                        GetXPositionInGrid(column) - GetCellSize() / 2
                    ),
                    $"Cell {row}-{column}"
                );
                cellWorldPositions.Add(cell.GetWorldPosition());
            });
        }

        [Button]
        public void DestroyGrid() {
            if (holder == null || !holder.HasChildren()) return;

            holder.DestroyChildrenImmediate();
            cellWorldPositions.Clear();
        }

        private void ForEachCells(Action<int, int> action) {
            for (int column = 0; column < gridSize.GetIntY(); column++) {
                for (int row = 0; row < gridSize.GetIntX(); row++) {
                    action?.Invoke(row, column);
                }
            }
        }

        private float GetXPositionInGrid(int row) => offset.GetX() + row;
        private float GetZPositionInGrid(int column) => offset.GetY() + column;

        private float GetCellSize() => cellSize - cellGap;

        #region Editor

#if UNITY_EDITOR
        [Title("Debug")] [SerializeField, ToggleLeft]
        private bool showGizmos = true;

        private void OnDrawGizmosSelected() {
            if (!showGizmos) return;

            ForEachCells((row, column) => {
                GizmosUtils.DrawColoredWireCube(
                    new Vector3(
                        GetXPositionInGrid(row),
                        yOffset,
                        GetZPositionInGrid(column)
                    ),
                    new Vector3(GetCellSize(), 0, GetCellSize()),
                    Color.green
                );
            });
        }
#endif

        #endregion
    }
}