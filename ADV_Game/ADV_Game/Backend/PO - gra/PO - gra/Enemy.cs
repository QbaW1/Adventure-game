using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POProjekt
{
    public enum Difficulty { easy, hard, master }
    public class Enemy : ICloneable
    {
        string name;
        int hp;
        int dmg;
        int def;
        Item enemyItem;
        Difficulty difficulty;

        public string Name { get => name; set => name = value; }
        public int Hp { get => hp; set => hp = value; }
        public int Dmg { get => dmg; set => dmg = value; }
        public int Def { get => def; set => def = value; }
        public Item EnemyItem { get => enemyItem; set => enemyItem = value; }
        public Difficulty Difficulty { get => difficulty; set => difficulty = value; }

        public Enemy()
        {

        }

        public Enemy(string name, int hp, int dmg, int def, Difficulty difficulty)
        {
            Name = name;
            Hp = hp;
            Dmg = dmg;
            Def = def;
            switch (difficulty)
            {
                case Difficulty.easy:
                    break;
                case Difficulty.hard:
                    Hp = (int)(hp * 1.5);
                    Dmg = (int)(dmg * 1.4);
                    Def = (int)(def * 1.25);
                    break;
                case Difficulty.master:
                    Hp = hp * 2;
                    Dmg = (int)(dmg * 1.8);
                    Def = (int)(def * 1.5);
                    break;
            }
        }

        public Enemy(string name, int hp, int dmg, int def, Difficulty difficulty, Item enemyItem)
            : this(name, hp, dmg, def, difficulty)
        {
            EnemyItem = enemyItem;
        }

        public int EnemyAttack(int dmg)
        {
            Random r = new();
            int attack = r.Next((int)(0.88 * dmg), (int)(1.12 * dmg));
            return attack;
        }


        public object Clone() { return MemberwiseClone(); }

        public void ChangeName(string name) => Name = name;
    }
}
