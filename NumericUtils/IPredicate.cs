namespace NumericUtils
{
    public interface IPredicate<T>
    {
        bool IsTrue(T item);
    }
}
