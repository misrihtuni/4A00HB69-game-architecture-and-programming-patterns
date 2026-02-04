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

        ///////////////////////////////////////////////////////////////////////
        // Properties
        ///////////////////////////////////////////////////////////////////////

        public static LevelManager Active { get; private set; }

        ///////////////////////////////////////////////////////////////////////
        // Methods
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

        public override void _Ready() { }

        public override void _Process(double delta) { }
    }
}
