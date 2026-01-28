using System;
using Godot;

namespace GA.Platformer3D
{
    public partial class PlayerCharacter : Character
    {
        // TODO: Refactor this. Move common code between player and enemy to base class.
        /// <summary>
        /// Performs the physics calculations.
        /// </summary>
        /// <param name="delta">Time in seconds since last frame.</param>
        public override void _PhysicsProcess(double delta)
        {
            Vector3 velocity = Velocity;

            // Add the gravity.
            if (!IsOnFloor())
            {
                velocity += GetGravity() * (float)delta;
            }

            // Handle Jump.
            if (Input.IsActionJustPressed("jump") && IsOnFloor())
            {
                velocity.Y = JumpVelocity;
            }

            // Get the input direction and handle the movement/deceleration.
            // As good practice, you should replace UI actions with custom gameplay actions.
            Vector2 inputDir = Input.GetVector("left", "right", "up", "down");
            Vector3 direction = (Transform.Basis * new Vector3(inputDir.X, 0, inputDir.Y)).Normalized();

            if (direction != Vector3.Zero)
            {
                GD.Print("input=", inputDir, " basis=", Transform.Basis, " dir=", direction);
                velocity.X = direction.X * MaxSpeed;
                velocity.Z = direction.Z * MaxSpeed;

                // Input exists, rotate the character rig.
                if (CharacterRig != null)
                {
                    Vector3 targetDirection = new Vector3(direction.X, 0, direction.Z);
                    float targetAngle = Mathf.Atan(targetDirection.X / targetDirection.Z);
                    Quaternion targetRotation = new Quaternion(Vector3.Up, targetAngle);

                    Vector3 test = new Vector3(x: direction.X, y: 0, z: direction.Z).Normalized();

                    CharacterRig.Quaternion = CharacterRig.Quaternion.Slerp(
                        targetRotation,
                        (float)delta * RotationSpeed
                    );
                }
            }
            else
            {
                velocity.X = Mathf.MoveToward(Velocity.X, 0, MaxSpeed);
                velocity.Z = Mathf.MoveToward(Velocity.Z, 0, MaxSpeed);
            }

            Velocity = velocity;
            MoveAndSlide();
        }
    }
}
