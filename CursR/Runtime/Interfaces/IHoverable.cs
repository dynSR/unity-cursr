using System;
using UnityEngine;

namespace CursR.Runtime.Interfaces {
    public interface IHoverable {
        bool IsHoverable { get; }
        GameObject HoverEffect { get; }

        bool UsesOutline { get; }
        float OutlineWidth { get; }
        Color OutlineColor { get; }
        Outline GetOutline();

        void Hover();
        void CancelHover();
        bool IsHovered();

        public Action OnHover { get; }
    }
}