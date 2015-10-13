using System;
using Ensage;
using SharpDX;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;


namespace Double_Tap_Blink
{
    class Program
    {
        private static bool active = false;
        private static int step = 1;
        const int KeyUp = 0x0101;
        const int KeyDown = 0x0100;
        private static Vector3 SmallDick = new Vector3(-7149, -6696, 383);
        private static Vector3 BigDick = new Vector3(7149, 6696, 383);
        private static Timer Anal = new Timer(500);
        private static void OnTimedEvent(Object source, ElapsedEventArgs e)
        {
            step = 1;
            Anal.Stop();
        }

        static void Main(string[] args)
        {
            if (!Game.IsInGame)
                return;
            var me = ObjectMgr.LocalHero;
            var W = me.Spellbook.SpellW;
            if ((me.ClassID == ClassID.CDOTA_Unit_Hero_QueenOfPain) || (me.ClassID == ClassID.CDOTA_Unit_Hero_AntiMage))
                active = true;
            if (active == true)
                Game.OnWndProc += Penis;
            Anal.Elapsed += OnTimedEvent;

        }

        static void Penis(WndEventArgs Vagina)
        {
            if (step == 1 && Vagina.Msg == KeyDown && Vagina.WParam == 'W')
            {
                step = 2;
                Anal.Start();
            }
            else if (step == 2 && Vagina.Msg == KeyUp && Vagina.WParam == 'W')
                step = 3;
            else if (step == 3 && Vagina.Msg == KeyDown && Vagina.WParam == 'W')
                BlinkToBase();
        }

        static void BlinkToBase()
        {
            var me = ObjectMgr.LocalHero;
            var W = me.Spellbook.SpellW;

            if (me.Team == Team.Dire)
                W.UseAbility(BigDick);

            else if (me.Team == Team.Radiant)
                W.UseAbility(SmallDick);

        }
    }
}


