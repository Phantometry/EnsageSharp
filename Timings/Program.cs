/*
“You have to be perfect, ideal, like timings. 
When you trying to hit a stun after hex, timings is what you need. 
When you trying to fuck bitches with euls, timings is what you need. 
When you trying to dodge sun strike or torrent, timings is what you don't need. 
You can be good, but your timings suck? Be perfect my friend - use timings.” (c) Bruce Phantometry 2k15 
*/


using System;
using System.Collections.Generic;
using System.Linq;
using Ensage;
using SharpDX;

namespace Timings
{
    internal class Program
    {
        private static List<ModifierInfo> cake = new List<ModifierInfo>();

        private static float scaleX;
        private static float scaleY;

        private static float linaTime, bhTime, torrentTime, linaTimer, bhTimer, torrentTimer, leshTime, leshTimer, stormTime, stormTimer;
        private static string time = "cakescript1224";

        private static readonly List<string> IgnoreList = new List<string>()
            {
              "modifier_phantom_lancer_juxtapose_illusion",
              "modifier_drow_ranger_trueshot_aura",
              "modifier_beastmaster_wild_axe_invulnerable",
              "modififer_pipe_debuff",
              "modifier_bristleback_warpath_stack",
              "modifier_bane_fiends_grip_self",
              "modifier_brewmaster_primal_split",
              "modifier_bristleback_quill_spray_stack",
              "modifier_abaddon_frostmourne_buff",
              "modifier_rubick_null_field_effect",
              "modifier_rubick_null_field",
              "modifier_abaddon_frostmourne_debuff",
              "modifier_kunkka_ghost_ship_damage_absorb",
              "modifier_jakiro_liquid_fire_burn",
              "modifier_beastmaster_inner_beast_aura",
              "modifier_crystal_maiden_brilliance_aura",
              "modifier_elder_titan_natural_order_aura",
              "modifier_nightstalker_darkness_thinker",
              "modifier_luna_lunar_blessing_aura",
              "modifier_omniknight_degen_aura",
              "modifier_obsidian_destroyer_essence_aura",
              "modifier_sand_king_caustic_finale_orb",
              "modifier_spirit_breaker_empowering_haste",
              "modifier_bounty_hunter_track_effect",
              "modifier_spirit_breaker_empowering_haste_aura",

            };

        private static readonly List<string> SpecialCases = new List<string>()
            {
              "modifier_lina_light_strike_array",
              "modifier_kunkka_torrent_thinker",
              "modifier_enigma_black_hole_thinker",
              "modifier_leshrac_split_earth_thinker",
              "modifier_spirit_breaker_charge_of_darkness_vision",
              "modifier_storm_spirit_static_remnant_thinker",
              "modifier_invoker_sun_strike",
            };

        public static void Main(string[] cyka)
        {
            scaleX = ((float)Drawing.Width / 1366);
            scaleY = ((float)Drawing.Height / 768);

            Unit.OnModifierAdded += ModifierAdded;
            Unit.OnModifierRemoved += ModifierRemoved;
            Drawing.OnDraw += OnDraw;
        }


        static void OnDraw(EventArgs args)
        {
            if (!Game.IsInGame)
                return;

            var player = ObjectMgr.LocalPlayer;
            if (player == null || player.Team == Team.Observer)
                return;


            foreach (var v in cake)
            {
                var unit = v.Unit;
                if (unit.IsValid && unit.IsAlive && unit.IsVisible)
                {
                    Vector2 screenPos;
                    var unitPos = unit.Position + new Vector3(0, 0, unit.HealthBarOffset);
                    if (!Drawing.WorldToScreen(unitPos, out screenPos))
                        continue;

                    try
                    {
                        var mod = v.Modifier;

                        var start = screenPos + new Vector2(-51 * scaleX, -22 * scaleY);
                        v.PicPosition = start + new Vector2((float)62.5 * scaleX, 14 * scaleY);

                        Drawing.DrawRect(v.PicPosition, new Vector2(25 * scaleX, 25 * scaleY), Drawing.GetTexture(string.Format("materials/ensage_ui/modifier_textures/{0}.vmat", mod.TextureName)));

                        // Draw text
                        bool random = true;
                        if (SpecialCases.Contains(mod.Name))
                        {
                            random = false;
                            switch (mod.Name)
                            {
                                case "modifier_lina_light_strike_array":
                                    linaTimer = linaTime + 0.5f - Game.GameTime;
                                    time = linaTimer.ToString("0.0");
                                    break;
                                case "modifier_kunkka_torrent_thinker":
                                    torrentTimer = torrentTime + 1.6f - Game.GameTime;
                                    time = torrentTimer.ToString("0.0");
                                    break;
                                case "modifier_enigma_black_hole_thinker":
                                    bhTimer = bhTime + 4.0f - Game.GameTime;
                                    time = bhTimer.ToString("0.0");
                                    break;
                                case "modifier_leshrac_split_earth_thinker":
                                    leshTimer = leshTime + 0.35f - Game.GameTime;
                                    time = leshTimer.ToString("0.0");
                                    break;
                                case "modifier_storm_spirit_static_remnant_thinker":
                                    stormTimer = stormTime + 12.0f - Game.GameTime;
                                    time = stormTimer.ToString("0.0");
                                    break;
                                case "modifier_invoker_sun_strike":
                                    random = true;
                                    break;
                                case "modifier_spirit_breaker_charge_of_darkness_vision":
                                    time = "ARGHH !";
                                    break;
                                default:
                                    random = true;
                                    break;
                            }
                            if (time.Contains("-"))
                                return;
                        }

                        // Draw text
                        var text = random ? mod.RemainingTime.ToString("0.0") : time;
                        var value = mod.Name != "modifier_spirit_breaker_charge_of_darkness_vision"
                            ? double.Parse(text) : 1.2;

                        if (value < 0.05)
                            continue;

                        Vector2 textPos = new Vector2((v.PicPosition.X) + (27 * scaleX), (v.PicPosition.Y - (0 * scaleY)));
                        Drawing.DrawText(text, textPos, new Vector2(23, 200), value < 1 ? Color.Red : Color.Aquamarine, FontFlags.AntiAlias | FontFlags.Additive | FontFlags.DropShadow);
                    }
                    catch (Exception)
                    {
                        //
                    }
                }

            }
        }

        static void ModifierAdded(Unit unit, ModifierChangedEventArgs args)
        {
            var modif = args.Modifier;

            try
            {
                if (IgnoreList.Contains(args.Modifier.Name)
                    || ((args.Modifier.Name.Contains("item") || args.Modifier.Name.Contains("truesight") || args.Modifier.Name.Contains("glyph") || args.Modifier.Name.Contains("aura"))
                    && !((args.Modifier.Name.Contains("item_pipe_barrier") || (args.Modifier.Name.Contains("windwalk"))))))
                    return;

                if (args.Modifier.RemainingTime < 150 && (args.Modifier.RemainingTime > 0.1 || SpecialCases.Contains(args.Modifier.Name))
                    && (SpecialCases.Contains(args.Modifier.Name) || unit.ClassID == ClassID.CDOTA_BaseNPC || args.Modifier.IsDebuff || args.Modifier.RemainingTime >= GetCurrentTime(unit))
                    && unit.ClassID != ClassID.CDOTA_BaseNPC_Creep
                    && unit.ClassID != ClassID.CDOTA_BaseNPC_Creep_Lane
                    && unit.ClassID != ClassID.CDOTA_BaseNPC_Creep_Neutral
                    && unit.ClassID != ClassID.CDOTA_BaseNPC_Creep_Siege
                    && unit.ClassID != ClassID.CDOTA_BaseNPC_Tower)
                {
                    if (SpecialCases.Contains(args.Modifier.Name))
                        switch (modif.Name)
                        {
                            case "modifier_lina_light_strike_array":
                                {
                                    linaTime = Game.GameTime;
                                    var effect = unit.AddParticleEffect(@"particles\ui_mouseactions\range_display.vpcf");
                                    effect.SetControlPoint(1, new Vector3(225, 0, 0));
                                }
                                break;
                            case "modifier_kunkka_torrent_thinker":
                                {
                                    torrentTime = Game.GameTime;
                                    var effect = unit.AddParticleEffect(@"particles\ui_mouseactions\range_display.vpcf");
                                    effect.SetControlPoint(1, new Vector3(225, 0, 0));
                                }
                                break;
                            case "modifier_enigma_black_hole_thinker":
                                bhTime = Game.GameTime;
                                break;
                            case "modifier_storm_spirit_static_remnant_thinker":
                                stormTime = Game.GameTime;
                                break;
                            case "modifier_leshrac_split_earth_thinker":
                                {
                                    leshTime = Game.GameTime;
                                    var lesh =
                                        ObjectMgr.GetEntities<Hero>()
                                            .FirstOrDefault(x => x.ClassID == ClassID.CDOTA_Unit_Hero_Leshrac);
                                    var effect = unit.AddParticleEffect(@"particles\ui_mouseactions\range_display.vpcf");
                                    effect.SetControlPoint(
                                        1,
                                        new Vector3(
                                            lesh.Spellbook.SpellQ.AbilityData.FirstOrDefault(x => x.Name == "radius")
                                                .GetValue(lesh.Spellbook.SpellQ.Level - 1),
                                            0,
                                            0));
                                }
                                break;
                            case "modifier_invoker_sun_strike":
                                {
                                    var effect = unit.AddParticleEffect(@"particles\ui_mouseactions\range_display.vpcf");
                                    effect.SetControlPoint(1, new Vector3(175, 0, 0));
                                }
                                break;
                        }

                    var r = cake.FirstOrDefault(x => x.Unit.Name == unit.Name);

                    if (unit.Name != "npc_dota_thinker" && r != null && GetCurrentTime(unit) >= 0 && !SpecialCases.Contains(args.Modifier.Name))
                        cake.Remove(r);

                    cake.Add(new ModifierInfo(modif, unit, new Vector2(0, 0)));
                }
            }
            catch (Exception)
            {
                cake.Clear();
                cake.Add(new ModifierInfo(modif, unit, new Vector2(0, 0)));
            }

        }

        static float GetCurrentTime(Unit unit)
        {
            var foundEntry = cake.FirstOrDefault(x => x.Unit.ClassID == unit.ClassID);
            return foundEntry != null ? foundEntry.Modifier.RemainingTime : -1;
        }

        static string GetCurrentName(Unit unit)
        {
            var findUnit = cake.FirstOrDefault(x => x.Unit.ClassID == unit.ClassID);
            return findUnit == null ? "none" : findUnit.Modifier.Name;
        }

        static void ModifierRemoved(Unit unit, ModifierChangedEventArgs args)
        {
            var mod = args.Modifier;
            if (IgnoreList.Contains(mod.Name)
                || ((mod.Name.Contains("item") || mod.Name.Contains("truesight") || mod.Name.Contains("glyph"))
                && !((mod.Name.Contains("item_pipe_barrier") || (mod.Name.Contains("windwalk"))))))
                return;

            try
            {
                var r = cake.FirstOrDefault(x => x.Unit.Name == unit.Name && x.Modifier.Name == mod.Name);

                if (r != null)
                    cake.Remove(r);
            }
            catch (Exception)
            {

            }
        }

        public class ModifierInfo : IEquatable<ModifierInfo>
        {
            public ModifierInfo(Modifier modifier, Unit unit, Vector2 picPos)
            {
                this.Modifier = modifier;
                this.Unit = unit;
                this.PicPosition = picPos;
            }
            public Modifier Modifier { get; set; }
            public Unit Unit { get; set; }
            public Vector2 PicPosition { get; set; }
            public bool Equals(ModifierInfo other)
            {
                if (other == null) return false;
                return (this.Unit.Equals(other.Unit));
            }
        }
    }
}
