using Godot;

namespace GA.Platformer3D
{
    public class ProjectilePool : Pool<Projectile>
    {
        public ProjectilePool(PackedScene sourceScene, int capacity, bool canGrow = false)
            : base(sourceScene, capacity, canGrow) { }

        /// <summary>
        /// Adds the given <paramref name="projectile"/> to the level scene.
        /// </summary>
        /// <param name="projectile">Projectile to add.</param>
        protected override void OnGet(Projectile projectile)
        {
            LevelManager.Active.ProjectileParent.AddChild(projectile);
        }

        /// <summary>
        /// Adds the given <paramref name="projectile"/> from the level scene.
        /// </summary>
        /// <param name="projectile">Projectile to remove.</param>
        protected override void OnReturn(Projectile projectile)
        {
            LevelManager.Active.ProjectileParent.CallDeferred(Node.MethodName.RemoveChild, projectile);
        }
    }
}
