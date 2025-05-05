using System.Collections.Generic;
using UnityTools.Library.Extensions;
using UnityEngine;

namespace UnityTools.Library.Interfaces {
    public interface IAnimation {
        List<Texture2D> Frames { get; }
        int FrameRate { get; }
        bool IsLooping();

        bool HasFrames() => !Frames.IsEmpty();
    }
}