namespace GA.Platformer3D
{
    public interface IHealth
    {
        /// <summary>
        /// The current HP of the user.
        /// </summary>
        int CurrentHP { get; }

        /// <summary>
        /// The maximum HP of the user.
        /// </summary>
        int MaxHP { get; }

        /// <summary>
        /// The initial HP of the user.
        /// </summary>
        int InitialHP { get; }

        /// <summary>
        /// Tells whether the user is currently alive.
        /// </summary>
        bool IsAlive { get; }

        /// <summary>
        /// Tells if the
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
