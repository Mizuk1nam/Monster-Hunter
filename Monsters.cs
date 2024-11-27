using Monster_Hunter.Monster_Hunter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monster_Hunter
{
    public class Monsters
    {
        private static List<Monster> monsters;

        public Monsters()
        {
            monsters = new List<Monster>();
        }

        // Add a monster to the list
        public void AddMonster(Monster monster)
        {
            monsters.Add(monster);
        }

        // Find monsters by position (X, Y)
        public List<Monster> FindMonstersAtPosition(int x, int y)
        {
            try
            {
                // Filter the list to find all monsters at the given position
                var foundMonsters = monsters.Where(monster => monster.X == x && monster.Y == y).ToList();
                return foundMonsters;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while finding monsters: {ex.Message}");
                return new List<Monster>();
            }
        }
        public List<Monster> GetMonsters()
        {
            return monsters;
        }
    }
}
