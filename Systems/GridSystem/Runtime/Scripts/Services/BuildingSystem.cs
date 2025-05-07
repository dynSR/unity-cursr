using System.Collections.Generic;
using System.Numerics;
using GridSystem.Runtime.Enums;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.InputSystem;
using UnityTools.Library.Extensions;
using Quaternion = UnityEngine.Quaternion;
using Vector3 = UnityEngine.Vector3;

namespace GridSystem.Runtime.Services {
    [RequireComponent(typeof(Grid))]
    public class BuildingSystem : MonoBehaviour {
        [SerializeField] private List<GameObject> buildings = new();
        [SerializeField] private bool isBuilding;

        private GameObject currentBuilding;
        private GridCell gridHoveredCell;
        private Grid Grid => transform.GetOrAddComponent<Grid>();

        private void Update() {
            if (Keyboard.current.bKey.wasPressedThisFrame) {
                isBuilding = !isBuilding;
                currentBuilding = isBuilding
                    ? Instantiate(GetRandomBuilding(), transform.position, Quaternion.identity)
                    : null;
            }

            if (Mouse.current.leftButton.wasPressedThisFrame && isBuilding) {
                Build();
            }

            SetBuiltBuildingPosition();
        }

        private void Build() {
            isBuilding = false;
            currentBuilding = null;

            gridHoveredCell.SetState(GridCellState.Occupied);
            gridHoveredCell = null;
        }

        private void SetBuiltBuildingPosition() {
            if (!isBuilding) return;
            Assert.IsNotNull(currentBuilding, "Current building must be defined before trying to set its position");
            Vector3 buildingScale = currentBuilding.transform.localScale;
            currentBuilding.SetPosition(new Vector3(
                Grid.GetCellWorldPosition().x + buildingScale.x / 2f,
                Grid.GetCellWorldPosition().y + buildingScale.y / 2f,
                Grid.GetCellWorldPosition().z + buildingScale.z / 2f
            ));
        }

        private GameObject GetRandomBuilding() => buildings.Random();
    }
}