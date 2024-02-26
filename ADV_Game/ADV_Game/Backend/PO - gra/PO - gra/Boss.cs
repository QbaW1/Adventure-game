using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POProjekt
{
    public class Boss : Enemy
    {
        Item artifact;

        public Item Artifact { get => artifact; set => artifact = value; }

        public Boss()
        {

        }

        public Boss(string name, int hp, int dmg, int def, Difficulty difficulty, Item artifact)
            : base(name, hp, dmg, def, difficulty)
        {
            Artifact = artifact;
        }
    }
}
