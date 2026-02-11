using Godot;

namespace GA.Platformer3D
{
    /// <summary>
    /// An interface for pooling objects of type <see cref="Node"/>.
    /// </summary>
    /// <typeparam name="T">An object of type <see cref="Node"/>.</typeparam>
    public interface IPool<T>
        where T : Node
    {
        /// <summary>
        /// Gets an object from the pool.
        /// </summary>
        ///
        /// <returns>
        /// An object from the pool if any are available,
        /// <c>null</c> otherwise.
        /// </returns>
        T Get();

        /// <summary>
        ///
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        bool Return(T item);
    }
}
