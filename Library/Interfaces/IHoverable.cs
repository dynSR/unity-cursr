using System;
using UnityEngine;
using CursorSystem.Runtime.Enums;

namespace UnityTools.Library.Interfaces {
    public interface IHoverable {
        bool IsHoverable { get; }
        CursorType AssociatedCursorType { get; }
        GameObject HoverEffect { get; }

        bool UsesOutline { get; }
        float OutlineWidth { get; }
        Color OutlineColor { get; }
        void SetOutlineProperties();

        void Hover();
        void CancelHover();
        bool IsHovered();

        void OnMouseOver();
        void OnMouseExit();

        public static Action<CursorType> OnHover { get; set; } = delegate { };
    }
}