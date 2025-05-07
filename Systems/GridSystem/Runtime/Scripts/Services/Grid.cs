using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using GridSystem.Runtime.Core;
using UnityUtils;
using UnityTools.Library.Extensions;
using UnityTools.Library.Structs;
using UnityTools.Library.Utils;
using GridSystem.Runtime.Utils;
using GridSystem.Runtime.Enums;
using UnityEngine.Assertions;
using GridSystem.Runtime.ScriptableObjects;
using JetBrains.Annotations;

namespace GridSystem.Runtime.Services {
    /**
    * TODO:
     * - Add an offset to this transform using GlobalGridOffset
     * - Add a field for the ground gameObject
     * - Add an offset to the ground gameObject using GlobalGridOffset
    */
    public class Grid : MonoBehaviour {
        [Title("Grid settings")] [SerializeField]
        private LayerMask layerMask;

        [SerializeField, MinValue(1)] private AxisInt size;

        [SerializeField, EnumToggleButtons] private GridCellGap gridCellGap;

        [Title("Cell settings")] [SerializeField, Required]
        private Material cellMaterial;

        [SerializeField, ChildGameObjectsOnly, Required]
        private GameObject cellParent;

        [Title("Indicators")] [SerializeField, LabelText("Cell Selection"), Required]
        private PositionIndicator gridCellSelectionIndicator;

        [SerializeField, LabelText("Mouse World Position"), Required]
        private PositionIndicator gridMouseWorldPositionIndicator;

        private Vector2 CenterOfGrid {
            get {
                Vector2 center = new Vector2(transform.localPosition.x, transform.localPosition.z) +
                                 (size.GetValues() - Vector2.one) * DEFAULT_CELL_SIZE / 2f;
                return center + GlobalGridOffset();
            }
        }

        private Vector2 GlobalGridOffset() => new(
            size.GetValues().x % 2 == 0 ? 0f : DEFAULT_CELL_SIZE / 2f,
            size.GetValues().y % 2 == 0 ? 0f : DEFAULT_CELL_SIZE / 2f
        );

        private Vector2 ComputedCellSize {
            get {
                float cellGap = gridCellGap switch {
                    GridCellGap.None => 0f,
                    GridCellGap.Mid => 0.25f,
                    GridCellGap.Max => 0.5f,
                    _ => throw new ArgumentOutOfRangeException(nameof(gridCellGap), gridCellGap, null)
                };

                return new Vector2(DEFAULT_CELL_SIZE - cellGap, DEFAULT_CELL_SIZE - cellGap);
            }
        }

        private readonly Dictionary<Vector2Int, Vector3> cellWorldPositions = new();
        private Vector3 mouseWorldPosition;
        private bool isCursorHoveringGrid;
        private GameObject currentHoveredGridCell;

        private static Camera mainCamera;

        private const float Y_OFFSET = 0.01f;
        private const float DEFAULT_CELL_SIZE = 1f;

        private void Awake() {
            mainCamera = Camera.main;

            gridCellSelectionIndicator.Init(transform);
            gridMouseWorldPositionIndicator.Init(transform);

            Build();
        }

        private void Update() {
            if (IsCursorHoveringGrid()) {
                mouseWorldPosition =
                    MouseRaycasterUtils.GetRaycastHitPointFromMousePosition(mainCamera, 100f, layerMask);
            }

            gridCellSelectionIndicator.ProcessUpdate(new Vector3(
                    GetCellWorldPosition().x + ComputedCellSize.x / 2f,
                    GetCellWorldPosition().y,
                    GetCellWorldPosition().z + ComputedCellSize.y / 2f
                ),
                isCursorHoveringGrid
            );
            gridMouseWorldPositionIndicator.ProcessUpdate(
                -transform.position + mouseWorldPosition,
                isCursorHoveringGrid
            );
        }

        public Vector3 GetCellWorldPosition() {
            int x = Mathf.FloorToInt(mouseWorldPosition.x);
            int z = Mathf.FloorToInt(mouseWorldPosition.z);
            return cellWorldPositions.TryGetValue(new Vector2Int(x, z), out Vector3 position)
                ? -transform.position + position
                : Vector3.zero;
        }

        private bool IsCursorHoveringGrid() {
            isCursorHoveringGrid = GetHoveredGridCell() is not null;
            return isCursorHoveringGrid;
        }

        [CanBeNull]
        public GameObject GetHoveredGridCell() {
            Assert.IsNotNull(mainCamera, "Main Camera was not found in this scene, please provide one");
            currentHoveredGridCell = MouseRaycasterUtils.HitObject(mainCamera, 100f, layerMask);
            return currentHoveredGridCell;
        }

        [Button]
        public void Build() {
            Destroy();

            Assert.IsNotNull(cellParent, "cellParent must be defined before creating cells");
            Assert.IsNotNull(cellMaterial, "cellMaterial must be defined before creating cells");

            Vector3 cellScale = new(ComputedCellSize.x, ComputedCellSize.y, 0);
            ForEachCellsInGrid((row, column) => {
                if (HasEnoughCellsBeenCreated()) return;

                Vector3 cellPosition = new(
                    GetXPositionInGrid(row),
                    0,
                    GetZPositionInGrid(column)
                );

                CellMesh cellMesh = CellMesh.Factory.Create(
                    cellMaterial,
                    cellParent.transform,
                    cellScale,
                    cellPosition,
                    $"Cell {row}-{column}"
                );

                cellWorldPositions.Add(
                    new Vector2Int(
                        Mathf.FloorToInt(cellMesh.GetWorldPosition().x / DEFAULT_CELL_SIZE),
                        Mathf.FloorToInt(cellMesh.GetWorldPosition().z / DEFAULT_CELL_SIZE)
                    ),
                    new Vector3(
                        cellMesh.GetWorldPosition().x,
                        cellMesh.GetWorldPosition().y,
                        cellMesh.GetWorldPosition().z
                    )
                );
            });
        }

        [Button]
        public void Destroy() {
            Assert.IsNotNull(cellParent, "cellParent must be defined before trying to destroy grid");
            if (!cellParent.HasChildren()) return;

            cellParent.DestroyChildrenImmediate();
            cellWorldPositions.Clear();
        }

        private bool HasEnoughCellsBeenCreated() {
            Assert.IsNotNull(
                cellParent,
                "cellParent must be defined before checking if enough cells have been created"
            );
            return cellParent.transform.childCount >= size.GetCombinedValue();
        }

        private void ForEachCellsInGrid(Action<int, int> action) {
            if (size.GetCombinedValue() == 0) return;
            for (int row = 0; row < size.GetValues().x; row++) {
                for (int column = 0; column < size.GetValues().y; column++) {
                    action?.Invoke(row, column);
                }
            }
        }

        private float GetXPositionInGrid(int row) => transform.localPosition.x + -CenterOfGrid.x + row;
        private float GetZPositionInGrid(int column) => transform.localPosition.z + -CenterOfGrid.y + column;

        #region Editor

#if UNITY_EDITOR
        [Title("Debug")] [SerializeField, ToggleLeft]
        private bool showGizmos = true;

        [SerializeField, ToggleLeft] private bool showHandlesPosition = true;

        private void OnDrawGizmos() {
            if (!showGizmos) return;

            ForEachCellsInGrid((row, column) => {
                Vector3 cellPosition = new(
                    GetXPositionInGrid(row),
                    Y_OFFSET,
                    GetZPositionInGrid(column)
                );

                PrevisualizationUtils.DrawColoredWireCube(
                    cellPosition,
                    new Vector3(ComputedCellSize.x, 0, ComputedCellSize.y),
                    Color.black
                );

                // Grid center point
                PrevisualizationUtils.DrawColoredCube(
                    transform.localPosition,
                    Vector3.one * 0.1f,
                    Color.red
                );

                if (showHandlesPosition) {
                    string text = $"{row},{column}";
                    PrevisualizationUtils.DrawText(
                        new Vector3(
                            cellPosition.x + text.Length / 4.65f,
                            cellPosition.y,
                            cellPosition.z + text.Length / 3.5f
                        ),
                        text
                    );
                }
            });
        }
#endif

        #endregion
    }
}