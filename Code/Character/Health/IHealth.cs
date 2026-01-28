namespace GA.Platformer3D
{
    public interface IHealth
    {
        /// <summary>
        /// TODO: Add documentation.
        /// </summary>
        int CurrentHP { get; }

        /// <summary>
        /// TODO: Add documentation.
        /// </summary>
        int MaxHP { get; }

        /// <summary>
        /// TODO: Add documentation.
        /// </summary>
        bool IsAlive { get; }

        /// <summary>
        /// TODO: Add documentation.
        /// </summary>
        bool IsImmortal { get; }

        /// <summary>
        /// Increases hit points by <paramref name="amount"/>.
        /// </summary>
        /// <param name="amount">The number of hit points to add.</param>
        void Heal(int amount);

        /// <summary>
        /// Decreases hit points by <paramref name="amount"/> if the user is
        /// not immortal.
        /// </summary>
        /// <param name="amount">The number of hit points to </param>
        /// <returns><c>true</c> if damage was taken, <c>false</c> otherwise.</returns>
        /// <seealso cref="IsImmortal"/>
        bool TakeDamage(int amount);
    }
}
