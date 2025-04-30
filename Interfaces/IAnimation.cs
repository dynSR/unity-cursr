using System.Collections.Generic;
using UnityTools.Extensions;
using UnityEngine;

namespace UnityTools.Interfaces {
    public interface IAnimation {
        List<Texture2D> Frames { get; }
        int FrameRate { get; }
        bool IsLooping();

        bool HasFrames() => !Frames.IsEmpty();
    }
}