using System;
using Godot;

namespace GA.Platformer3D
{
    public partial class LevelManager : Node3D
    {
        ///////////////////////////////////////////////////////////////////////
        // Fields
        ///////////////////////////////////////////////////////////////////////

        [Export]
        private bool _useProjectilePool = false;

        [Export]
        private PackedScene _projectileScene = null;

        // TODO: Is it even necessary to allow setting this from the editor?
        [Export]
        private Node3D _projectileParent = null;

        ///////////////////////////////////////////////////////////////////////
        // Properties
        ///////////////////////////////////////////////////////////////////////

        public static LevelManager Active { get; private set; }

        ///////////////////////////////////////////////////////////////////////
        // Public Methods
        ///////////////////////////////////////////////////////////////////////

        public override void _EnterTree()
        {
            // Sets itself as the active level manager when the node enters the
            // node tree.
            Active = this;
        }

        public override void _ExitTree()
        {
            // Removes itself from the active level manager role when the node
            // exits the node tree.
            Active = null;
        }

        public override void _Ready()
        {
            if (_projectileScene == null)
            {
                GD.PushError($"The projectile scene is not assigned.");
            }

            if (_projectileParent == null)
            {
                GD.PushError($"The projectile parent is not assigned.");
                // TODO: Consider creating an empty node under LevelManager.
                _projectileParent = this;
            }
        }

        /// <summary>
        /// Spawns a <see cref="Projectile"/> into the game world.
        /// </summary>
        /// <param name="position">Spawn point of the projectile.</param>
        /// <param name="direction">Where the projectile should be headed.</param>
        /// <param name="collisionLayer">The layer(s) the projectile is on.</param>
        /// <param name="collisionMask">The layer(s) the projectile should target.</param>
        /// <returns>The spawned projectile.</returns>
        /// <exception cref="InvalidOperationException">Projectile scene is not assigned.</exception>
        public Projectile SpawnProjectile(Vector3 position, Vector3 direction, uint collisionLayer, uint collisionMask)
        {
            if (_projectileScene == null)
            {
                throw new InvalidOperationException($"Projectile scene is not assigned.");
            }

            Projectile projectile = null;

            if (_useProjectilePool)
            {
                // TODO: Fetch the projectile from the pool.
                throw new NotImplementedException();
            }
            else
            {
                projectile = _projectileScene.Instantiate<Projectile>();
                _projectileParent.AddChild(projectile);
            }

            if (projectile != null)
            {
                projectile.CollisionLayer = collisionLayer;
                projectile.CollisionMask = collisionMask;
                projectile.GlobalPosition = position;
                projectile.Dispose += OnDisposeProjectile;
                projectile.Launch(direction);
            }

            return projectile;
        }

        ///////////////////////////////////////////////////////////////////////
        // Private Methods
        ///////////////////////////////////////////////////////////////////////

        private void OnDisposeProjectile(Projectile projectile)
        {
            // Unsubscribe from the Dispose signal.
            projectile.Dispose -= OnDisposeProjectile;

            if (_useProjectilePool)
            {
                throw new NotImplementedException();
            }
            else
            {
                projectile.QueueFree();
            }
        }
    }
}
