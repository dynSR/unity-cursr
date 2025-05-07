using System;
using UnityEngine;

namespace UnityTools.Library.Interfaces {
    public interface IPairedValues<out T, out TValues> : ISerializationCallbackReceiver
        where T : struct, IComparable<T> {
        bool IsUniform { get; }
        T First { get; }
        T Second { get; }
        T GetCombinedValue();
        TValues GetValues();
    }
}