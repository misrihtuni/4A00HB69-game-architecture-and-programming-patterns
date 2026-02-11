using Godot;

namespace GA.Platformer3D
{
	public class ProjectilePool : Pool<Projectile>
	{
		public ProjectilePool(PackedScene sourceScene, int capacity, bool canGrow = false)
			: base(sourceScene, capacity, canGrow)
		{
		}

		protected override void OnGet(Projectile item)
		{
			LevelManager.Active.ProjectileParent.AddChild(item);
		}

		protected override void OnReturn(Projectile item)
		{
			LevelManager.Active.ProjectileParent.CallDeferred("remove_child", item);
		}
	}
}