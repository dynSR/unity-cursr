using System;
using UnityEngine;
using CursR.Runtime.Enums;

namespace CursR.Runtime.Interfaces {
    public interface IHoverable {
        bool IsHoverable { get; }
        CursorType AssociatedCursorType { get; }
        GameObject HoverEffect { get; }

        bool UsesOutline { get; }
        float OutlineWidth { get; }
        Color OutlineColor { get; }
        Outline GetOutline();

        void Hover();
        void CancelHover();
        bool IsHovered();

        void OnMouseOver();
        void OnMouseExit();

        public static Action<CursorType> OnHover { get; set; } = delegate { };
    }
}