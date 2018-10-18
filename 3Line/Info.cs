using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3Line {

    class Info {

        public int Damage { get; private set; }
        public int Heal { get; private set; }
        public int Bombs { get; private set; }
        public int Dynamite { get; private set; }
        public int X1;
        public int X2;
        public int Y1;
        public int Y2;

        public Info(int x1, int y1, int x2, int y2) {
            X1 = x1;
            X2 = x2;
            Y1 = y1;
            Y2 = y2;
        }

        public void AddDamage(int damageValue, int count = 3) {
            if (damageValue > 0)
                Damage += damageValue * count;
            else
                Heal += 4 * count;
            if (count == 4)
                Bombs++;
            if (count == 5)
                Dynamite++;
        }

        public int Value() {
            return Damage + Heal + Bombs * 10 + Dynamite * 50;
        }

        public bool HasValue() {
            return Damage != 0 || Heal != 0;
        }

        public override string ToString() {
            return $"Damage:   {Damage}\nHeal:     {Heal}\nBombs:    {Bombs}\nDynamite: {Dynamite}";
        }

    }

}
