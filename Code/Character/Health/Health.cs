using System;
using Godot;

namespace GA.Platformer3D
{
    [GlobalClass]
    public partial class Health : Node, IHealth
    {
        [Signal]
        public delegate void HealthChangedEventHandler(int newHp);

        [Export(PropertyHint.Range, "0,1,or_greater")]
        private int _maxHP = 10;

        [Export(PropertyHint.Range, "0,1,or_greater")]
        private int _initialHP = 10;

        private int _currentHp = 0;

        public int CurrentHP
        {
            get => _currentHp;
            // TODO: Does this need to be public?
            set
            {
                _currentHp = Mathf.Clamp(value, min: 0, max: MaxHP);
                EmitSignal(SignalName.HealthChanged, _currentHp);
            }
        }

        public int MaxHP => _maxHP;

        public int InitialHP => _initialHP;

        public bool IsAlive => CurrentHP > 0;

        // TODO: Does the setter require validation?
        public bool IsImmortal { get; set; }

        public void Heal(int amount)
        {
            if (amount < 0)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(amount),
                    $"{nameof(Heal)} doesn't support negative values."
                );
            }

            CurrentHP += amount;
        }

        public bool TakeDamage(int amount)
        {
            if (amount < 0)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(amount),
                    $"{nameof(TakeDamage)} doesn't support negative values."
                );
            }

            if (IsImmortal)
            {
                return false;
            }

            CurrentHP -= amount;
            return true;
        }
    }
}
