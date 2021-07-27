namespace Toolkits.ObjectPooling
{
    /// <summary>
    /// An interface used to specify how an object of type <typeparamref name="T"/> should be created.
    /// </summary>
    /// <typeparam name="T">The type of object to create.</typeparam>
    public interface IFactory<T>
    {
        /// <summary>
        /// Creates and returns a new instance of an object with type <typeparamref name="T"/>.
        /// </summary>
        /// <returns>The new instance of type <typeparamref name="T"/>.</returns>
        T Create();
    }
}
