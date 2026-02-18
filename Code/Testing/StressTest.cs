using GA.Common;
using Godot;
using System.Collections.Generic;
using System.Diagnostics;

namespace GA.Platformer3D
{
	public partial class StressTest : Node3D
	{
		// Hard-coded keys for testing. Bad practice, but for testing it's fine.
		private const Key TOGGLE_TEST_KEY = Key.T;
		private const Key TOGGLE_POOLING_KEY = Key.P;
		private const Key RESET_STATS_KEY = Key.R;

		[Export] private int _burstSize = 50; // Number of projectiles per burst
		[Export] private float _burstInterval = 1.0f; // Time between bursts in seconds
		[Export] private int _burstCount = 10; // Total number of bursts to fire

		private IList<Gun> _guns = null;
		private bool _isTestRunning = false;
		private float _testTimer = 0f;
		private float _burstTimer = 0f;
		private int _projectilesFired = 0;
		private int _burstsFired = 0;

		public override void _Ready()
		{
			_guns = this.GetChildren<Gun>(recursive: false);
			GD.Print($"Found {_guns.Count} guns for stress testing.");
			GD.Print($"Object pooling {GetPoolStatus()}.");
		}

		public override void _Input(InputEvent @event)
		{
			if (@event is InputEventKey keyEvent && keyEvent.Pressed)
			{
				switch (keyEvent.Keycode)
				{
					case TOGGLE_POOLING_KEY:
						LevelManager.Active.UseProjectilePool = !LevelManager.Active.UseProjectilePool;
						GD.Print($"Object pooling {GetPoolStatus()}.");
						break;
					case TOGGLE_TEST_KEY:
						ToggleStressTest();
						break;
				}
			}
		}

		public override void _Process(double delta)
		{
			float deltaTime = (float)delta;
			if (_isTestRunning)
			{
				_burstTimer += deltaTime;
				_testTimer += deltaTime;

				if (_burstTimer >= _burstInterval)
				{
					_burstTimer = 0f;
					_burstsFired++;
					FireBurst();
				}

				if (_burstsFired >= _burstCount)
				{
					ToggleStressTest();
				}
			}
		}

		private void FireBurst()
		{
			Stopwatch stopwatch = Stopwatch.StartNew();
			int projectilesFired = 0;

			foreach (Gun gun in _guns)
			{
				for (int i = 0; i < _burstSize; i++)
				{
					Vector3 baseDirection = gun.FiringPoint.GlobalBasis.Z * -1; // Forward == -Z.
					Vector3 randomOffset = new Vector3(
						GD.Randf() - 0.5f * 0.2f,
						GD.Randf() - 0.5f * 0.2f,
						0
					);

					Vector3 direction = (baseDirection + randomOffset).Normalized();

					Projectile projectile = LevelManager.Active.SpawnProjectile(gun.FiringPoint.GlobalPosition, direction, 0, 0);
					if (projectile != null)
					{
						_projectilesFired++;
						projectilesFired++;
					}
				}
			}

			stopwatch.Stop();

			GD.Print($"[{GetPoolStatus()}] Burst {_burstsFired}: {projectilesFired} projectiles in {stopwatch.ElapsedMilliseconds}ms");
		}

		private void ToggleStressTest()
		{
			_isTestRunning = !_isTestRunning;
			if (_isTestRunning)
			{
				StartTest();
			}
			else
			{
				StopTest();
			}
		}

		private void StartTest()
		{
			_testTimer = 0f;
			_burstTimer = 0f;
			_projectilesFired = 0;
			_burstsFired = 0;

			GD.Print($"=== STRESS TEST STARTED ({GetPoolStatus()}) ===");
			GD.Print($"Burst size: {_burstSize} projectiles");
			GD.Print($"Burst interval: {_burstInterval}s");
			GD.Print($"Total bursts to fire: {_burstCount}");
			GD.Print($"Expected total projectiles: {_burstCount * _burstSize * _guns.Count}");
		}

		private void StopTest()
		{
			GD.Print($"=== STRESS TEST COMPLETED ({GetPoolStatus()}) ===");
			GD.Print($"Total projectiles fired: {_projectilesFired}");
			GD.Print($"Test duration: {_testTimer:F2}s");
			GD.Print($"Average rate: {_projectilesFired / _testTimer:F1} projectiles/second");
		}

		private string GetPoolStatus()
		{
			return LevelManager.Active.UseProjectilePool ? "ENABLED" : "DISABLED";
		}
	}
}