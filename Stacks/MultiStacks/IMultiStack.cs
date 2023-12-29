namespace Stacks.MultiStacks;

public interface IMultiStack<T>
{
    void Push(int stackNum, T value);
    T Pop(int stackNum);
    T Peek(int stackNum);
    bool IsEmpty(int stackNum);
}
