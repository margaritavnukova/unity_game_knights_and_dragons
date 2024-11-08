using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace прототип1
{
    public class Mob
    {
        public int health { get; protected set; }
        public int attack { get; protected set; }
        public int defence { get; protected set; }
        public bool IsDefended { get; protected set; }
        public int damageTaken { get; protected set; }
        public int healingDone { get; protected set; }

        protected Random random = new Random();

        public Mob()
        {
            health = 0;
            attack = 0;
            defence = 0;
            IsDefended = false;
        }

        public Mob(int H, int A)
        {
            health = H;
            attack = A;
        }

        public virtual void RecievedAttack(Player player)
        {
            damageTaken = player.attack * random.Next(5, 8);
            health -= damageTaken;
        }

        public virtual void DoAttack(Player player)
        {
            if (player.IsDefended)
            {
                player.IsDefended = false;
            }
            else
            {
                player.damageTaken = attack * random.Next(5, 8);
                player.health -= player.damageTaken;
            }
        }
    }
}
