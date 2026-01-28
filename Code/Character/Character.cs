using Godot;

namespace GA.Platformer3D
{
    public abstract partial class Character : CharacterBody3D
    {
        [Export]
        private float _speed = 5.0f;

        [Export]
        private float _jumpVelocity = 4.5f;

        [Export]
        private Node3D _rig = null;

        [Export]
        private float _rotationSpeed = 15f;

        public float RotationSpeed => _rotationSpeed;

        /// <summary>
        /// Reference to the character rig.
        /// </summary>
        public Node3D CharacterRig => _rig;

        public float CurrentSpeed => Velocity.Length();

        public float MaxSpeed
        {
            get => _speed;
            // TODO: Validate the value before setting it.
            // TODO: Notify about the change.
            protected set => _speed = value;
        }

        public float JumpVelocity
        {
            get => _jumpVelocity;
            // TODO: Validate the value before setting it.
            // TODO: Notify about the change.
            protected set => _jumpVelocity = value;
        }
    }
}
