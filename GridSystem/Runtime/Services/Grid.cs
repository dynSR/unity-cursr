using UnityEngine;
using Sirenix.OdinInspector;
using UnityUtils;
using UnityTools.Structs;
using UnityTools.Enums;
using UnityTools.Extensions;

namespace UnityTools.Grid.Runtime.Services {
    public class Grid : MonoBehaviour {
        [SerializeField,
         TitleGroup("Grid size", BoldTitle = true), HideLabel]
        private AxisIntPair gridSize;

        [SerializeField, TitleGroup("Grid starting point", BoldTitle = true), HideLabel]
        private AxisPair gridStartingPoint;

        [SerializeField, ToggleLeft, TitleGroup("Grid cell gap", BoldTitle = true)]
        private bool hasGap;

        [SerializeField, ShowIf("hasGap"), HideLabel]
        private AxisPair cellGap;

        [SerializeField, TitleGroup("Grid cell scale options", BoldTitle = true), ToggleLeft]
        private bool isScaleOverridden;

        [SerializeField, MinValue(1), HorizontalGroup("Cell scale option"), ShowIf("isScaleOverridden")]
        private int cellScale = 1;

        [SerializeField, Required,
         PreviewField(Height = (float)Sizes.Colossal, Alignment = ObjectFieldAlignment.Left),
         PropertySpace(SpaceAfter = (float)Sizes.Medium), HideLabel]
        private GameObject gridCellPrefab;

        private Transform GridHolderTrs => transform;
        private const float yOffset = 0.001f;
        private bool isGridBuilt = false;

        private void Awake() {
            if (!isGridBuilt) BuildGrid();
        }

        [Button(ButtonSizes.Large), HorizontalGroup("Buttons", Title = "Actions")]
        protected void BuildGrid() {
            DestroyGrid();
            GenerateGrid(gridSize.GetValue(), gridCellPrefab);
            isGridBuilt = true;
        }

        private void GenerateGrid(Vector2 size, GameObject cell) {
            for (int column = 0; column < size.y; column++) {
                for (int row = 0; row < size.x; row++) {
                    CreateCell(cell, row, column);
                }
            }
        }

        private void CreateCell(GameObject cell, int row, int column) {
            GameObject createdCell = Instantiate(
                cell,
                GetCellSpawnPoint(row, column),
                Quaternion.identity
            );
            createdCell.transform.SetParent(GridHolderTrs);
        }

        private Vector3 GetCellSpawnPoint(int row, int column) => new(
            -gridStartingPoint.GetValue().x + (cellScale + GetCellGap().x) * row,
            yOffset,
            -gridStartingPoint.GetValue().y + (cellScale + GetCellGap().y) * column
        );

        private Vector2 GetCellGap() => hasGap ? cellGap.GetValue() : Vector2.zero;

        [Button(ButtonSizes.Large), HorizontalGroup("Buttons")]
        private void DestroyGrid() {
            if (!GridHolderTrs.HasChildren()) return;

            isGridBuilt = false;
            GridHolderTrs.DestroyChildrenImmediate();
        }

        #region Editor

#if UNITY_EDITOR
        private void OnValidate() {
            if (!hasGap) cellGap.Reset();
            if (!isScaleOverridden) cellScale = 1;
        }

        private void OnDrawGizmosSelected() => PreVisualizeGridCells(Color.red);

        private void PreVisualizeGridCells(Color withColor) {
            for (int y = 0; y < gridSize.GetValue().y; y++) {
                for (int row = 0; row < gridSize.GetValue().x; row++) {
                    DrawWireCell(
                        GetCellSpawnPoint(row, y),
                        new Vector3(cellScale, 0.01f, cellScale),
                        withColor
                    );
                }
            }
        }

        private void DrawWireCell(Vector3 position, Vector3 scale, Color cellColor) {
            Gizmos.color = cellColor;
            Gizmos.DrawWireCube(position, scale);
        }

#endif

        #endregion
    }
}