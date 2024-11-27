using Monster_Hunter.Monster_Hunter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace Monster_Hunter
{
    using System;

    namespace Monster_Hunter
    {
        public class Hunter : Character
        {
            private string name;
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
                State = new NormalState();
                CurrentHP = MaxHP;
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

            private bool IsValidPosition(int x, int y)
            {
                // Check map boundaries and obstacles
                return map.MapArray[y][x] != '#' && map.MapArray[y][x] != 'M'; // '#' for walls, 'M' for monsters
            }

            private void StartStateTimer()
            {
                stateTimer?.Stop();
                stateTimer = new Timer(10000); // 10 seconds
                stateTimer.Elapsed += (sender, e) =>
                {
                    State = new NormalState();
                    stateTimer.Stop();
                };
                stateTimer.Start();
            }
        }

        public interface State
        {
            void ApplyState(Hunter hunter);
        }

        public class NormalState : State
        {
            public void ApplyState(Hunter hunter)
            {
                hunter.Strength = 5;
                hunter.Armor = 3;
                hunter.FreezeTimeMilliseconds = 1000; // Default freeze time
            }
        }

        public class StrongState : State
        {
            public void ApplyState(Hunter hunter)
            {
                hunter.Strength *= 2; // Double strength
                hunter.Armor = (int)(hunter.Armor * 1.5); // 1.5x defense
                hunter.CurrentHP = hunter.MaxHP; // Full HP
            }
        }

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

        public class InvisibleState : State
        {
            public void ApplyState(Hunter hunter)
            {
                // No changes to attributes but avoids combat
            }
        }

        public class FastState : State
        {
            public void ApplyState(Hunter hunter)
            {
                hunter.FreezeTimeMilliseconds /= 2; // Halve freeze time
            }
        }
    }
}