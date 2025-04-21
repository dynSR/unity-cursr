using CursR.Runtime.Enums;

namespace CursR.Runtime.Interfaces {
    public interface ICursorProperty<out T> {
        CursorSize AssociatedCursorSize { get; }
        T Value { get; }
        T Get() => Value;
    }
}