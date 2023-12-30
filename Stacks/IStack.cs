namespace Stacks;

public interface IStack<T>
{
    void Push(T value);
    T Pop();
    T Peek();
    bool IsEmpty();
}
