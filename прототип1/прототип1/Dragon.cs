using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace прототип1
{
    public class Dragon : Mob
    {
        int level;

        public Dragon()
            : base() { }

        public Dragon(int H, int A, int L) 
            : base (H, A)
        {
            level = L;
            health *= level;
            IsDefended = false;
        }

        public Dragon(Dragon dragon, int L, int H)
            : base(H, dragon.attack)
        {
            level = L;
            health *= level;
            attack = level * 10;
            IsDefended = false;
        }

        public override void RecievedAttack(Player player)
        {
            base.RecievedAttack(player);
        }

        public override void DoAttack(Player player)
        {
            base.DoAttack(player);
        }
    }
}
