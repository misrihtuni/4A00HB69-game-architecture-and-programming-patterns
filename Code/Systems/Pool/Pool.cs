using System.Collections.Generic;
using Godot;

namespace GA.Platformer3D
{
    /// <summary>
    /// Object pool for objects of type <see cref="Node"/>.
    /// </summary>
    /// <typeparam name="T">Type of the nodes in the pool.</typeparam>
    public class Pool<T> : IPool<T>
        where T : Node
    {
        ///////////////////////////////////////////////////////////////////////
        // Fields
        ///////////////////////////////////////////////////////////////////////

        private PackedScene _sourceScene = null;
        private bool _canGrow = false;
        private Queue<T> _availableItems = null;
        private HashSet<T> _activeItems = null;

        ///////////////////////////////////////////////////////////////////////
        // Public Methods
        ///////////////////////////////////////////////////////////////////////

        /// <summary>
        /// Creates a new object pool.
        /// </summary>
        ///
        /// <param name="sourceScene">
        ///  The scene resource that is used to create objects in the pool.
        /// </param>
        /// <param name="capacity">
        ///  Maximum number of instances allowed to be created.
        /// </param>
        /// <param name="canGrow">
        ///  If the pool can create new objects even if the maximum number of
        ///  active instances has already been reached.
        /// </param>
        public Pool(PackedScene sourceScene, int capacity, bool canGrow = false)
        {
            _sourceScene = sourceScene;
            _canGrow = canGrow;

            // Queue capacity cannot be negative.
            if (capacity < 0)
            {
                capacity = 0;
            }

            _availableItems = new Queue<T>(capacity);
            _activeItems = new HashSet<T>();

            for (int i = 0; i < capacity; i++)
            {
                Add();
            }
        }

        public T Get()
        {
            T item;

            if (_availableItems.Count > 0)
            {
                item = _availableItems.Dequeue();
            }
            else if (_canGrow)
            {
                item = Add(activateOnAdd: true);
            }
            else
            {
                return null;
            }

            _activeItems.Add(item);
            OnGet(item);
            return item;
        }

        public bool Return(T item)
        {
            if (item == null || !_activeItems.Contains(item))
            {
                // The item is empty or the item was not created by the pool.
                return false;
            }

            _activeItems.Remove(item);
            _availableItems.Enqueue(item);
            OnReturn(item);
            return true;
        }

        ///////////////////////////////////////////////////////////////////////
        // Protected Methods
        ///////////////////////////////////////////////////////////////////////

        /// <summary>
        /// Custom activation functionality.
        /// </summary>
        /// <param name="item"></param>
        protected virtual void OnGet(T item) { }

        /// <summary>
        /// Custom deactivation functionality.
        /// </summary>
        /// <param name="item"></param>
        protected virtual void OnReturn(T item) { }

        ///////////////////////////////////////////////////////////////////////
        // Private Methods
        ///////////////////////////////////////////////////////////////////////

        /// <summary>
        /// Adds a new item to the item pools.
        /// </summary>
        /// <param name="activateOnAdd">If the item should be activated when added.</param>
        /// <returns>The item that was added.</returns>
        private T Add(bool activateOnAdd = false)
        {
            T item = _sourceScene.Instantiate<T>();

            if (!activateOnAdd)
            {
                _availableItems.Enqueue(item);
            }

            return item;
        }
    }
}
