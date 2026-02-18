using Godot;
using System;

namespace GA.Platformer3D
{
	public partial class Gun : Node3D
	{
		[Export]
		public Node3D FiringPoint
		{
			get;
			set;
		}
	}
}
