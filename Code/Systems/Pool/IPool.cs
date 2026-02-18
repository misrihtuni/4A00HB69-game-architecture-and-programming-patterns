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
        /// Returns an object bak to the pool.
        /// </summary>
        /// <param name="item">Object to return to the pool.</param>
        /// <returns>
        ///  <c>true</c> if the object was returned to the pool successfully,
        ///  <c>false</c> otherwise.
        /// </returns>
        bool Return(T item);
    }
}
