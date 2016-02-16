
using System;
using System.Linq;
using Ensage.Common.Menu;
using Ensage;
using SharpDX;
using SharpDX.Direct3D9;
using System.Collections.Generic;

namespace Dagon_Stealer {
    class Program {
        private static Font _text;
        private static readonly int[] Penis = new int[5] { 400, 500, 600, 700, 800 };
        private static readonly int[] ShitDickFuck = new int[5] { 600, 650, 700, 750, 800 };
        private static readonly Menu Menu = new Menu("Dagon Stealer", "dagonstealer", true);

        private static List<string> Ignore = new List<string> 
        {
            "modifier_item_sphere_target",
            "modifier_templar_assassin_refraction_absorb",
            "modifier_dazzle_shallow_grave",
            "modifier_item_pipe_barrier",
            "modifier_nyx_assassin_spiked_carapace",
            "modifier_item_blade_mail_reflect",
            "modifier_item_lotus_orb_active",
        };

        private static Dictionary<string, bool> enemies = new Dictionary<string, bool>();

        private static void Main(string[] args) {
            Menu.AddItem(new MenuItem("keyBind", "Main Key").SetValue(new KeyBind('K', KeyBindType.Toggle, true)));
            Menu.AddItem(new MenuItem("enemies", "Heroes").SetValue(new HeroToggler(enemies)));
            Menu.AddToMainMenu();

            Game.OnUpdate += Vagina;
        }

        private static void Vagina(EventArgs Tits) {
            if (!Game.IsInGame)
                return;

            var me = ObjectMgr.LocalHero;
            if (me == null || !me.IsAlive)
                return;

            foreach (var v in ObjectMgr.GetEntities<Hero>().Where(x => x.Team != me.Team && !x.IsIllusion)) {
                if (!enemies.ContainsKey(v.Name)) {
                    if (enemies.Count > 4) enemies.Clear();
                    enemies.Add(v.Name, true);
                    Menu.Item("enemies").SetValue(new HeroToggler(enemies));
                }
            }

            var dagon = me.Inventory.Items.FirstOrDefault(Anal => Anal.Name.Contains("item_dagon"));
            var enemy = ObjectMgr.GetEntities<Hero>()
                        .Where(Fuck => Fuck.Team != me.Team && Fuck.IsAlive && Fuck.IsVisible && !Fuck.IsIllusion && !Fuck.UnitState.HasFlag(UnitState.MagicImmune))
                        .ToList();

            if (me.Inventory.Items.Any(v => v.IsChanneling) || me.Spellbook.Spells.Any(v => v.IsChanneling)) return;

            foreach (var v in enemy) {
                var linkens = v.Inventory.Items.FirstOrDefault(Gay => Gay.Name == "item_sphere");

                if (dagon != null && Menu.Item("keyBind").GetValue<KeyBind>().Active == true && Menu.Item("enemies").GetValue<HeroToggler>().IsEnabled(v.Name)) {
                    if (dagon.Cooldown == 0 && me.Mana > dagon.ManaCost) {
                        if ((linkens != null && linkens.Cooldown == 0) || v.Modifiers.Any(x => Ignore.Contains(x.Name)))
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
