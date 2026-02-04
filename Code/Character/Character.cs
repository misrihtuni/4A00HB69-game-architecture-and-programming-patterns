using Godot;

namespace GA.Platformer3D
{
    public abstract partial class Character : CharacterBody3D
    {
        ///////////////////////////////////////////////////////////////////////
        // Exports
        ///////////////////////////////////////////////////////////////////////

        [Export]
        private float _speed = 5.0f;

        [Export]
        private float _jumpVelocity = 4.5f;

        [Export]
        private Node3D _rig = null;

        [Export]
        private float _rotationSpeed = 15f;

        ///////////////////////////////////////////////////////////////////////
        // Properties
        ///////////////////////////////////////////////////////////////////////

        /// <summary>
        /// The character's health.
        /// </summary>
        public IHealth Health { get; private set; }

        /// <summary>
        /// Indicates if the character is currently jumping.
        /// </summary>
        public bool IsJumping { get; protected set; } = false;

        /// <summary>
        /// Is the character currently striking with a sword.
        /// </summary>
        public bool IsStriking { get; protected set; } = false;

        /// <summary>
        /// Is the character currently shooting with a gun.
        /// </summary>
        public bool IsShooting { get; protected set; } = false;

        /// <summary>
        /// A reference to the character rig.
        /// </summary>
        public Node3D CharacterRig => _rig;

        public float CurrentSpeed => Velocity.Length();

        public float RotationSpeed => _rotationSpeed;

        public float MaxSpeed
        {
            get => _speed;
            protected set
            {
                // TODO: Validate the value before setting it. Notify about the change is nesessary.
                _speed = value;
            }
        }

        public float JumpVelocity
        {
            get => _jumpVelocity;
            protected set
            {
                // TODO: Validate the value before setting it. Notify about the change is nesessary.
                _jumpVelocity = value;
            }
        }

        ///////////////////////////////////////////////////////////////////////
        // Methods
        ///////////////////////////////////////////////////////////////////////

        public override void _Ready()
        {
            // TODO: Replace with an extension method that doesn't use the node path.
            Health = GetNode<IHealth>("Health");
        }
    }
}
