using System;
using System.Linq;
using Ensage;
using SharpDX;
using SharpDX.Direct3D9;



namespace Dagon_Stealer
{
    class Program
    {
        const int WM_KEYUP = 0x0101;

        private static bool _enabled = true;
        private static Font _text;
        private static int[] Penis = new int[5] { 400, 500, 600, 700, 800 };
        private static int[] ShitDickFuck = new int[5] { 600, 650, 700, 750, 800 };
        static void Main(string[] args)
        {
            _text = new Font(
               Drawing.Direct3DDevice9,
               new FontDescription
               {
                  
                   FaceName = "Tahoma",
                   Height = 13,
                   OutputPrecision = FontPrecision.Default,
                   Quality = FontQuality.Default
               });

            Drawing.OnPreReset += Drawing_OnPreReset;
            Drawing.OnPostReset += Drawing_OnPostReset;
            Drawing.OnEndScene += Drawing_OnEndScene;
            AppDomain.CurrentDomain.DomainUnload += CurrentDomain_DomainUnload;
            Game.OnUpdate += Vagina;
            Game.OnWndProc += Game_OnWndProc;
        }

        static void Vagina(EventArgs Tits)
        {
            if (!Game.IsInGame)
                return;
            var me = ObjectMgr.LocalHero;
            var dagon = me.Inventory.Items.FirstOrDefault(Anal => Anal.Name.Substring(0, 10) == "item_dagon");
            var enemy = ObjectMgr.GetEntities<Hero>().Where(Fuck => Fuck.Team != me.Team && Fuck.IsAlive && Fuck.IsVisible && !Fuck.IsIllusion && !Fuck.UnitState.HasFlag(UnitState.MagicImmune)).ToList();
            foreach (var v in enemy)
            {
                var linkens = v.Inventory.Items.FirstOrDefault(Gay => Gay.Name == "item_sphere");
                var linkensmod = v.Modifiers.Any(Anything => Anything.Name == "modifier_item_sphere_target");

                if (dagon != null && _enabled == true)
                {
      
                    if (dagon.AbilityState == AbilityState.OnCooldown || me.Mana < dagon.ManaCost)
                        return;
                    if (linkens != null && (linkensmod || linkens.AbilityState == AbilityState.Ready))
                        return;
                    var range = ShitDickFuck[dagon.Level - 1];
                    var damage = Math.Floor(Penis[dagon.Level - 1] * (1 - v.MagicDamageResist / 100));
                    if (GetDistance2D(v) < range && v.Health < damage)
                        dagon.UseAbility(v);
                }

            }

        }
        static double GetDistance2D(Hero hero)
        {
            var MyHero = ObjectMgr.LocalHero;
            return Math.Sqrt(Math.Pow(hero.Position.X - MyHero.Position.X, 2) + Math.Pow(hero.Position.Y - MyHero.Position.Y, 2));
        }
        private static void Game_OnWndProc(WndEventArgs args)
        {
            if (args.Msg != WM_KEYUP || args.WParam != 'K' || Game.IsChatOpen)
                return;
            _enabled = !_enabled;
        }

        static void CurrentDomain_DomainUnload(object sender, EventArgs e)
        {
            _text.Dispose();
        }

        static void Drawing_OnEndScene(EventArgs args)
        {
            if (Drawing.Direct3DDevice9 == null || Drawing.Direct3DDevice9.IsDisposed || !Game.IsInGame)
                return;

            var player = ObjectMgr.LocalPlayer;
            if (player == null || player.Team == Team.Observer)
                return;

            if (_enabled)
                _text.DrawText(null, "Dagon Stealer: On!", 75, 535, Color.GreenYellow);
            else
                _text.DrawText(null, "Dagon Stealer: Off!", 75, 535, Color.Tomato);
        }

        static void Drawing_OnPostReset(EventArgs args)
        {
            _text.OnResetDevice();
        }

        static void Drawing_OnPreReset(EventArgs args)
        {
            _text.OnLostDevice();
        }
        
    }
}


