using System;
using GridSystem.Runtime.Enums;
using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;
using UnityTools.Library.Extensions;
using UnityTools.Library.Utils;

public class GridCell : MonoBehaviour {

    public GridCellState state = GridCellState.Idle;
    
    [Title("Materials")]
    [SerializeField] private Material idleMaterial;
    [SerializeField] private Material selectedMaterial;
    [SerializeField] private Material occupiedMaterial;

    private MeshRenderer MeshRenderer => transform.GetOrAddComponent<MeshRenderer>();
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() { }

    // Update is called once per frame
    void Update() { }

    public void SetState(GridCellState state) {
        if (this.state == state) return;

        this.state = state;
        MeshRenderer.material = state switch {
            GridCellState.Idle => idleMaterial,
            GridCellState.Selected => selectedMaterial,
            GridCellState.Occupied => occupiedMaterial,
            _ => throw new ArgumentOutOfRangeException(nameof(state), state, null)
        };
    }

    public bool IsOccupied() => state == GridCellState.Occupied;
}