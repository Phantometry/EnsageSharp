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
using SharpDX.Direct3D9;

namespace Timings
{
    internal class Program
    {
        private static List<test> Cake = new List<test>();
        private static Modifier Mod;

        private static test Removal;

        private static Vector2 PicPos;
        private static Font _text;

        private static float scaleX;
        private static float scaleY;

        private static ParticleEffect effect;
        private static readonly List<ParticleEffect> Effects = new List<ParticleEffect>();

        private static float LinaTime, BHTime, TorrentTime, LinaTimer, BHTimer, TorrentTimer, LeshTime, LeshTimer, StormTime, StormTimer;
        private static string Time = "cakescript1224";

        private static List<string> Ignore = new List<string>()
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
              "modifier_spirit_breaker_empowering_haste_aura",

            };

        private static List<string> SpecialCases = new List<string>()
            {
              "modifier_lina_light_strike_array",
              "modifier_kunkka_torrent_thinker",
              "modifier_enigma_black_hole_thinker",
              "modifier_leshrac_split_earth_thinker",
              "modifier_spirit_breaker_charge_of_darkness_vision",
              "modifier_storm_spirit_static_remnant_thinker",
              "modifier_invoker_sun_strike",
              "modifier_invoker_sun_strike"
            };

        public static void Main(string[] Cyka)
        {
            scaleX = ((float)Drawing.Width / 1366);
            scaleY = ((float)Drawing.Height / 768);

            _text = new Font(
                Drawing.Direct3DDevice9,
                new FontDescription
                {
                    FaceName = "Palatino",
                    Height = (int)(19 * scaleY),
                    Width = (int)(8 * scaleX),
                    Weight = FontWeight.UltraBold,
                    OutputPrecision = FontPrecision.Default,
                    Quality = FontQuality.Draft
                });

            Unit.OnModifierAdded += ModifierAdded;
            Unit.OnModifierRemoved += ModifierRemoved;
            Drawing.OnPreReset += Drawing_OnPreReset;
            Drawing.OnPostReset += Drawing_OnPostReset;
            Drawing.OnEndScene += Drawing_OnEndScene;
            Drawing.OnDraw += OnDraw;
            AppDomain.CurrentDomain.DomainUnload += CurrentDomain_DomainUnload;
        }

        static void CurrentDomain_DomainUnload(object sender, EventArgs e)
        {
            _text.Dispose();
        }

        static void Drawing_OnPostReset(EventArgs args)
        {
            _text.OnResetDevice();
        }

        static void Drawing_OnPreReset(EventArgs args)
        {
            _text.OnLostDevice();
        }

        static void Drawing_OnEndScene(EventArgs args)
        {
            if (!Game.IsInGame)
                return;

            var player = ObjectMgr.LocalPlayer;
            if (player == null || player.Team == Team.Observer)
                return;

            try
            {
                var Units = ObjectMgr.GetEntities<Unit>().Where(x => x.IsVisible && x.IsAlive && x.IsValid).ToList();
                foreach (var unit in Units)
                {
                    if (Cake.Any(x => x.Unit.Name == unit.Name))
                    {
                        Vector2 screenPos;
                        var unitPos = unit.Position + new Vector3(0, 0, unit.HealthBarOffset);
                        if (!Drawing.WorldToScreen(unitPos, out screenPos))
                            continue;

                        Mod = Cake.Where(x => x.Unit.Name == unit.Name).FirstOrDefault().Modifier;

                        bool Random = true;

                        if (SpecialCases.Contains(Mod.Name))
                        {
                            Random = false;
                            switch (Mod.Name)
                            {
                                case "modifier_lina_light_strike_array":
                                    LinaTimer = (float)(LinaTime + 0.5) - Game.GameTime;
                                    Time = LinaTimer.ToString("0.0");
                                    break;
                                case "modifier_kunkka_torrent_thinker":
                                    TorrentTimer = (float)(TorrentTime + 1.6) - Game.GameTime;
                                    Time = TorrentTimer.ToString("0.0");
                                    break;
                                case "modifier_enigma_black_hole_thinker":
                                    BHTimer = (float)(BHTime + 4) - Game.GameTime;
                                    Time = BHTimer.ToString("0.0");
                                    break;
                                case "modifier_leshrac_split_earth_thinker":
                                    LeshTimer = (float)(LeshTime + 0.35) - Game.GameTime;
                                    Time = LeshTimer.ToString("0.0");
                                    break;
                                case "modifier_storm_spirit_static_remnant_thinker":
                                    StormTimer = (float)(StormTime + 12) - Game.GameTime;
                                    Time = StormTimer.ToString("0.0");
                                    break;
                                case "modifier_invoker_sun_strike":
                                    Random = true;
                                    break;
                                case "modifier_spirit_breaker_charge_of_darkness_vision":
                                    Time = "ARGHH !";
                                    break;
                                default:
                                    Random = true;
                                    break;
                            }
                            if (Time.Contains("-"))
                                return;
                        }

                        // Draw text
                        string text = Random ? Mod.RemainingTime.ToString("0.0") : Time;

                        double value;

                        if (!(Mod.Name == "modifier_spirit_breaker_charge_of_darkness_vision"))
                            value = Double.Parse(text);
                        else
                            value = 1.2;

                        if (value < 0.05)
                            continue;

                        var v = Cake.FirstOrDefault(x => x.Unit.Name == unit.Name);

                        Vector2 textPos = new Vector2((v.Vector2.X) + (27 * scaleX), (v.Vector2.Y - (0 * scaleY)));

                        _text.DrawText(null, text, (int)textPos.X, (int)textPos.Y, value < 1 ? Color.Red : Color.Aquamarine);

                    }
                }
            }
            catch (Exception)
            {
                //
            }
        }

        static void OnDraw(EventArgs args)
        {
            if (!Game.IsInGame)
                return;

            var player = ObjectMgr.LocalPlayer;
            if (player == null || player.Team == Team.Observer)
                return;

            var Units = ObjectMgr.GetEntities<Unit>().Where(x => x.IsVisible && x.IsAlive).ToList();

            foreach (var unit in Units)
            {
                foreach (var v in Cake)
                {
                    if (v.Unit.Handle == unit.Handle)
                    {
                        Vector2 screenPos;
                        var unitPos = unit.Position + new Vector3(0, 0, unit.HealthBarOffset);
                        if (!Drawing.WorldToScreen(unitPos, out screenPos))
                            continue;

                        try
                        {
                            Mod = v.Modifier;

                            var start = screenPos + new Vector2(-51 * scaleX, -22 * scaleY);

                            v.Vector2 = start + new Vector2((float)62.5 * scaleX, 14 * scaleY);

                            Drawing.DrawRect(v.Vector2, new Vector2(25 * scaleX, 25 * scaleY), Drawing.GetTexture(string.Format("materials/ensage_ui/modifier_textures/{0}.vmat", Mod.TextureName)));
                        }
                        catch (Exception)
                        {
                            //
                        }
                    }
                }
            }
        }

        static void ModifierAdded(Unit Alien, ModifierChangedEventArgs IsTheBestCoderEver)
        {
            var Modif = IsTheBestCoderEver.Modifier;

            try
            {
                if (Ignore.Contains(IsTheBestCoderEver.Modifier.Name)
                    || ((IsTheBestCoderEver.Modifier.Name.Contains("item") || IsTheBestCoderEver.Modifier.Name.Contains("truesight") || IsTheBestCoderEver.Modifier.Name.Contains("glyph") || IsTheBestCoderEver.Modifier.Name.Contains("aura"))
                    && !((IsTheBestCoderEver.Modifier.Name.Contains("item_pipe_barrier") || (IsTheBestCoderEver.Modifier.Name.Contains("windwalk"))))))
                    return;

                if (IsTheBestCoderEver.Modifier.RemainingTime < 150 && (IsTheBestCoderEver.Modifier.RemainingTime > 0.1 || SpecialCases.Contains(IsTheBestCoderEver.Modifier.Name))
                    && (SpecialCases.Contains(IsTheBestCoderEver.Modifier.Name) || Alien.ClassID == ClassID.CDOTA_BaseNPC || IsTheBestCoderEver.Modifier.IsDebuff || IsTheBestCoderEver.Modifier.RemainingTime >= GetCurrentTime(Alien))
                    && Alien.ClassID != ClassID.CDOTA_BaseNPC_Creep
                    && Alien.ClassID != ClassID.CDOTA_BaseNPC_Creep_Lane
                    && Alien.ClassID != ClassID.CDOTA_BaseNPC_Creep_Neutral
                    && Alien.ClassID != ClassID.CDOTA_BaseNPC_Creep_Siege
                    && Alien.ClassID != ClassID.CDOTA_BaseNPC_Tower)
                {
                    if (SpecialCases.Contains(IsTheBestCoderEver.Modifier.Name))
                        switch (Modif.Name)
                        {
                            case "modifier_lina_light_strike_array":
                                LinaTime = Game.GameTime;
                                effect = Alien.AddParticleEffect(@"particles\ui_mouseactions\range_display.vpcf");
                                effect.SetControlPoint(1, new Vector3(225, 0, 0));
                                break;
                            case "modifier_kunkka_torrent_thinker":
                                TorrentTime = Game.GameTime;
                                effect = Alien.AddParticleEffect(@"particles\ui_mouseactions\range_display.vpcf");
                                effect.SetControlPoint(1, new Vector3(225, 0, 0));
                                break;
                            case "modifier_enigma_black_hole_thinker":
                                BHTime = Game.GameTime;
                                break;
                            case "modifier_storm_spirit_static_remnant_thinker":
                                StormTime = Game.GameTime;
                                break;
                            case "modifier_leshrac_split_earth_thinker":
                                LeshTime = Game.GameTime;
                                var Lesh = ObjectMgr.GetEntities<Hero>().Where(x => x.ClassID == ClassID.CDOTA_Unit_Hero_Leshrac).FirstOrDefault();
                                effect = Alien.AddParticleEffect(@"particles\ui_mouseactions\range_display.vpcf");
                                effect.SetControlPoint(1, new Vector3(Lesh.Spellbook.SpellQ.AbilityData.FirstOrDefault(x => x.Name == "radius").GetValue(Lesh.Spellbook.SpellQ.Level - 1), 0, 0));
                                break;
                            case "modifier_invoker_sun_strike":
                                effect = Alien.AddParticleEffect(@"particles\ui_mouseactions\range_display.vpcf");
                                effect.SetControlPoint(1, new Vector3(175, 0, 0));
                                break;
                        }

                    var r = Cake.Where(x => x.Unit.Name == Alien.Name).FirstOrDefault();
                    Console.WriteLine(Modif.Name);

                    if (Alien.Name != "npc_dota_thinker" && r != null && GetCurrentTime(Alien) >= 0 && !SpecialCases.Contains(IsTheBestCoderEver.Modifier.Name))
                        Cake.Remove(r);

                    Cake.Add(new test(Modif, Alien, new Vector2(0, 0)));
                }
            }
            catch (Exception)
            {
                Cake.Clear();
                Cake.Add(new test(Modif, Alien, new Vector2(0, 0)));
            }

        }

        static float GetCurrentTime(Unit unit)
        {
            var Unit = Cake.Where(x => x.Unit.ClassID == unit.ClassID).FirstOrDefault();

            if (Unit == null)
                return -1;

            return Unit.Modifier.RemainingTime;
        }

        static string GetCurrentName(Unit unit)
        {
            var Unit = Cake.Where(x => x.Unit.ClassID == unit.ClassID).FirstOrDefault();

            if (Unit == null)
                return "none";

            return Unit.Modifier.Name;
        }

        static void ModifierRemoved(Unit Alien, ModifierChangedEventArgs IsTheBestCoderEver)
        {
            Modifier Modif = IsTheBestCoderEver.Modifier;
            if (Ignore.Contains(IsTheBestCoderEver.Modifier.Name)
                || ((IsTheBestCoderEver.Modifier.Name.Contains("item") || IsTheBestCoderEver.Modifier.Name.Contains("truesight") || IsTheBestCoderEver.Modifier.Name.Contains("glyph"))
                && !((IsTheBestCoderEver.Modifier.Name.Contains("item_pipe_barrier") || (IsTheBestCoderEver.Modifier.Name.Contains("windwalk"))))))
                return;

            try
            {
                var r = Cake.Where(x => x.Unit.Name == Alien.Name && x.Modifier.Name == IsTheBestCoderEver.Modifier.Name).FirstOrDefault();

                if (r != null)
                    Cake.Remove(r);
            }
            catch (Exception)
            {

            }
        }

        public class test : IEquatable<test>
        {
            public test(Modifier modif, Unit unit, Vector2 PicPos)
            {
                this.Modifier = modif;
                this.Unit = unit;
                this.Vector2 = PicPos;
            }
            public Modifier Modifier { get; set; }
            public Unit Unit { get; set; }
            public Vector2 Vector2 { get; set; }
            public bool Equals(test other)
            {
                if (other == null) return false;
                return (this.Unit.Equals(other.Unit));
            }
        }
    }
}
