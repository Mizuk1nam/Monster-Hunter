using Monster_Hunter.Monster_Hunter;
using System;
using System.Collections.Generic;
using System.Timers;
using System.Xml.Linq;

namespace Monster_Hunter
{
    public class Hunter : Character
    {
        private string name;
        private DateTime lastMoveTime; // To track freeze time
        private string moveKeys = "WASD"; // Movement keys
        private int level;
        public int Level { get; set; }
        public Shield Shield { get; set; }
        public string Name
        {
            get => name;
            set
            {
                if (value.Length > 20)
                    throw new ArgumentException("Name cannot exceed 20 characters.");
                name = value;
            }
        }

        private int score;

        public int Score
        {
            get => score;
            set
            {
                if (value < 0 || value > 100000)
                    throw new ArgumentException("Score must be between 0 and 100,000.");
                score = value;
            }
        }

        private State state;

        public State State
        {
            get => state;
            set
            {
                // Apply new state and reset timer for state change
                state = value;
                state.ApplyState(this);
                StartStateTimer();
            }
        }

        private Timer stateTimer;

        public Hunter(int startX, int startY, Map gameMap)
            : base(startX, startY, gameMap)
        {
            if (gameMap == null)
                throw new ArgumentNullException(nameof(gameMap));

            FreezeTimeMilliseconds = 1000; // Default freeze time
            State = new NormalState(); // Default state
            CurrentHP = MaxHP;
            lastMoveTime = DateTime.Now;
        }

        public override bool Move(int newX, int newY)
        {
            if (newX < 0 || newX >= map.Width || newY < 0 || newY >= map.Height)
                return false;

            if (IsValidPosition(newX, newY))
            {
                X = newX;
                Y = newY;
                return true;
            }
            return false;
        }
        public void AttackMonster(Monster monster, Map map)
        {
            int damage = this.Strength - monster.Armor;

            if (damage > 0)
            {
                monster.CurrentHP -= damage;
                Console.SetCursorPosition(0, map.Height + 1);
                Console.WriteLine($"Hunter attacks! Monster takes {damage} damage. Monster's HP: {monster.CurrentHP}");

                // Check if the monster is dead
                if (monster.IsDead())
                {
                    Console.WriteLine("The monster has been defeated!");
                }
            }
        }

        private bool IsValidPosition(int x, int y)
        {
            // Check map boundaries and obstacles
            return map.MapArray[y][x] != '#' && map.MapArray[y][x] != 'M'; // '#' for walls, 'M' for monsters
        }

        private void StartStateTimer()
        {
            stateTimer?.Stop(); // Stop any existing timer
            stateTimer = new Timer(10000); // 10 seconds duration for the current state
            stateTimer.Elapsed += (sender, e) =>
            {
                State = new NormalState(); // Revert back to NormalState after 10 seconds
                stateTimer.Stop();
            };
            stateTimer.Start(); // Start the timer
        }

        // To stop the timer manually 
        public void StopStateTimer()
        {
            stateTimer?.Stop();
            State = new NormalState(); // Return to NormalState if the timer is stopped manually
        }
        public bool IsAdjacentToMonster(Monster monster)
        {
            int deltaX = this.X - monster.X;
            int deltaY = this.Y - monster.Y;

            // Check if both deltas are either -1, 0, or 1 (indicating adjacency)
            return (deltaX >= -1 && deltaX <= 1) && (deltaY >= -1 && deltaY <= 1) && (deltaX != 0 || deltaY != 0);
        }
    }

    // State Interface
    public interface State
    {
        void ApplyState(Hunter hunter);
    }

    // NormalState: Default state
    public class NormalState : State
    {
        public void ApplyState(Hunter hunter)
        {
            hunter.Strength = 5;
            hunter.Armor = 3;
            hunter.FreezeTimeMilliseconds = 1000; // Default freeze time
        }
    }

    // StrongState: Double strength and boost armor
    public class StrongState : State
    {
        public void ApplyState(Hunter hunter)
        {
            hunter.Strength *= 2; // Double strength
            hunter.Armor = (int)(hunter.Armor * 1.5); // 1.5x defense
            hunter.CurrentHP = hunter.MaxHP; // Full HP
        }
    }

    // PoisonedState: Halves strength and armor, adds a poison effect
    public class PoisonedState : State
    {
        public void ApplyState(Hunter hunter)
        {
            hunter.CurrentHP -= 5; // Lose 5 HP
            hunter.Strength /= 2; // Halve strength
            hunter.Armor /= 2; // Halve defense
            hunter.FreezeTimeMilliseconds = (int)(hunter.FreezeTimeMilliseconds * 1.25); // 25% longer freeze time
        }
    }

    // InvisibleState: No changes to attributes, but avoids combat
    public class InvisibleState : State
    {
        public void ApplyState(Hunter hunter)
        {
            // No changes to attributes
        }
    }

    // FastState: Halve freeze time, increases speed
    public class FastState : State
    {
        public void ApplyState(Hunter hunter)
        {
            hunter.FreezeTimeMilliseconds /= 2; // Halve freeze time
        }
    }
    

}
