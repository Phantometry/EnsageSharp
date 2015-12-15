using System;
using System.Linq;
using Ensage.Common.Menu;
using Ensage;
using SharpDX;
using SharpDX.Direct3D9;

namespace Dagon_Stealer {
    class Program {
        private static Font _text;
        private static readonly int[] Penis = new int[5] { 400, 500, 600, 700, 800 };
        private static readonly int[] ShitDickFuck = new int[5] { 600, 650, 700, 750, 800 };
        private static readonly Menu Menu = new Menu("Dagon Stealer", "dagonstealer", true);

        private static void Main(string[] args) {
            var optionsMenu = new Menu("Options", "options");
            Menu.AddSubMenu(optionsMenu);
            Menu.AddItem(new MenuItem("keyBind", "Main Key").SetValue(new KeyBind('K', KeyBindType.Toggle, true)));
            Menu.AddToMainMenu();

            Game.OnUpdate += Vagina;
        }

        private static void Vagina(EventArgs Tits) {
            if (!Game.IsInGame)
                return;

            var me = ObjectMgr.LocalHero;
            if (me == null || !me.IsAlive)
                return;

            var dagon = me.Inventory.Items.FirstOrDefault(Anal => Anal.Name.Contains("item_dagon"));
            var enemy = ObjectMgr.GetEntities<Hero>()
                        .Where(Fuck => Fuck.Team != me.Team && Fuck.IsAlive && Fuck.IsVisible && !Fuck.IsIllusion && !Fuck.UnitState.HasFlag(UnitState.MagicImmune))
                        .ToList();

            foreach (var v in enemy) {
                var linkens = v.Inventory.Items.FirstOrDefault(Gay => Gay.Name == "item_sphere");
                var linkensmod = v.Modifiers.Any(Anything => Anything.Name == "modifier_item_sphere_target");
                var refraction = v.Modifiers.Any(ButtFucker => ButtFucker.Name == "modifier_templar_assassin_refraction_damage");
                var shallowgrave = v.Modifiers.Any(FuckMeInTheAssDaddy => FuckMeInTheAssDaddy.Name == "modifier_dazzle_shallow_grave");
                var pipe = v.Modifiers.Any(SuckMyCockSenpai => SuckMyCockSenpai.Name == "modifier_item_pipe_barrier");
                var blademail = v.Modifiers.Any(PleaseDrillMyass => PleaseDrillMyass.Name == "modifier_item_blade_mail_reflect");

                Console.WriteLine(v.Modifiers);
                if (dagon != null && Menu.Item("keyBind").GetValue<KeyBind>().Active == true) {
                    if (dagon.Cooldown == 0 && me.Mana > dagon.ManaCost) {
                        if ((linkens != null && linkens.Cooldown == 0) || (linkensmod || pipe || shallowgrave || refraction || blademail))
                            return;
                        var range = ShitDickFuck[dagon.Level - 1];
                        var damage = Math.Floor(Penis[dagon.Level - 1] * (1 - v.MagicDamageResist));
                        if (LowUsageDistance(me, v) < (range * range) && v.Health < damage)
                            dagon.UseAbility(v);
                    }
                }
            }
        }

        private static float LowUsageDistance(dynamic A, dynamic B) {
            if (!(A is Unit || A is Vector3)) throw new ArgumentException("Not valid parameters, Accepts Unit/Vector3 only", "A");
            if (!(B is Unit || B is Vector3)) throw new ArgumentException("Not valid parameters, Accepts Unit/Vector3 only", "B");
            if (A is Unit) A = A.Position;
            if (B is Unit) B = B.Position;
            return ((B.X - A.X) * (B.X - A.X) + (B.Y - A.Y) * (B.Y - A.Y));
        }
    }
}
