using Godot;

namespace GA.Platformer3D
{
    public partial class Health : Node, IHealth
    {
        private int _currentHp = 0;

        [Signal]
        public delegate void HealthChangedEventHandler(int newHp);

        public int CurrentHP
        {
            get => _currentHp;
            set
            {
                _currentHp = Mathf.Clamp(value, min: 0, max: MaxHP);
                EmitSignal(SignalName.HealthChanged, _currentHp);
            }
        }

        public int MaxHP { get; }

        public bool IsAlive { get; }

        public bool IsImmortal { get; }

        public void Heal(int amount)
        {
            throw new System.NotImplementedException();
        }

        public bool TakeDamage(int amount)
        {
            throw new System.NotImplementedException();
        }
    }
}
