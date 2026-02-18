using Godot;

namespace GA.Platformer3D
{
    public partial class Projectile : Area3D
    {
        ///////////////////////////////////////////////////////////////////////
        // Signals
        ///////////////////////////////////////////////////////////////////////

        [Signal]
        public delegate void DisposeEventHandler(Projectile projectile);

        ///////////////////////////////////////////////////////////////////////
        // Fields
        ///////////////////////////////////////////////////////////////////////

        [Export(PropertyHint.Range, "0,100,or_greater,suffix:m/s")]
        private float _speed = 10.0f;

        [Export(PropertyHint.Range, "0,100,or_greater,suffix:points")]
        private float _damage = 10.0f;

        [Export(PropertyHint.Range, "0,60,or_greater,suffix:s")]
        private float _maxAge = 5.0f;

        private float _age = 0.0f;

        private Vector3 _direction = Vector3.Zero;

        ///////////////////////////////////////////////////////////////////////
        // Properties
        ///////////////////////////////////////////////////////////////////////

        public bool IsLaunched => !_direction.IsZeroApprox();

        ///////////////////////////////////////////////////////////////////////
        // Public Methods
        ///////////////////////////////////////////////////////////////////////

        public override void _EnterTree()
        {
            BodyEntered += OnBodyEntered;
        }

        public override void _ExitTree()
        {
            BodyEntered -= OnBodyEntered;
        }

        public override void _Process(double delta)
        {
            if (!IsLaunched)
            {
                return;
            }

            _age += (float)delta;

            if (_age > _maxAge)
            {
                Reset();
            }
        }

        public override void _PhysicsProcess(double delta)
        {
            if (IsLaunched)
            {
                GlobalPosition += _direction * _speed * (float)delta;
            }
        }

        public void Launch(Vector3 direction)
        {
            _direction = direction.Normalized();
        }

        public void Reset()
        {
            _direction = Vector3.Zero;
            _age = 0.0f;
            EmitSignal(SignalName.Dispose, this);
        }

        ///////////////////////////////////////////////////////////////////////
        // Private Methods
        ///////////////////////////////////////////////////////////////////////

        private void OnBodyEntered(Node3D node) { }
    }
}
