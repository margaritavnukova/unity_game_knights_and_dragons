using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace прототип1
{
    public enum battleClass
    {
        Рыцарь = 1,
        Маг,
        Друид
    }

    public class Player : Mob
    {
        public battleClass playerClass { get; private set; }
        public int maxHealth { get; private set; }
        int healthCoef = 0;

        public Player()
            : base() {}

        public Player(int H, int A, int C)
            : base(H, A)
        {
            ChangeClass(C);
        }

        public void ChangeClass(int C)
        {
            playerClass = (battleClass)C;

            switch (C)
            {
                case 1:
                    maxHealth = health = 200;
                    healthCoef = 2;
                    attack = 30; //150 - 240
                    IsDefended = true;
                    break;
                case 2:
                    maxHealth = health = 100;
                    healthCoef = 1;
                    attack = 40; //200 - 320
                    IsDefended = false;
                    break;
                case 3:
                    maxHealth = health = 300;
                    healthCoef = 3;
                    attack = 25; //125 - 200
                    IsDefended = false;
                    break;
            }
        }

        public void Upgrade(int dragonLevel)
        {
            health = maxHealth = dragonLevel * healthCoef * 100;
            attack += dragonLevel * 10;
        }

        public void Heal(int dragonLevel)
        {
            healingDone = 25 * healthCoef * random.Next(3,6) * dragonLevel;
            health += healingDone;
        }

        public void Defend()
        {
            IsDefended = true;
        }
    }
}
