using MMORPG.Domain;
using static MMORPG.PaladinTest;
using static MMORPG.RogueTest;
using static MMORPG.WizardTest;

namespace MMORPG
{
    class CharacterTest
    {

        [Test]
        public void Ensure_that_hps_not_negative()
        {
            Character character = new Paladin();
            character.Defend(character.InitHp + 1000);
            Assert.True(character.Hp >= 0);
        }


        [Test]
        public void Ensure_char_dead_if_hps_zero()
        {
            Character character = new Paladin();
            character.Defend(character.InitHp + 1000);
            Assert.True(character.IsDead);
        }


        [Test]
        public void Ensure_char_alive_if_hps_greater_than_zero()
        {
            Character character = new Paladin();
            character.Defend(character.InitHp - 1);
            Assert.True(character.IsAlive);
        }

        [Test]
        public void Ensure_negative_damage_is_ignored()
        {
            Character character = new Paladin();
            double initialHp = character.Hp;
            character.Defend(-10);
            Assert.That(character.Hp, Is.EqualTo(initialHp));
        }

        [Test]
        public void Ensure_that_dead_char_not_inflict_damage()
        {
            Character paladin = new Paladin();
            Character rogue = new Rogue();
            double initRougeHp = rogue.Hp;

            // Kill paladin
            paladin.Defend(paladin.InitHp + 1000);
            paladin.Attack(rogue);
            Assert.AreEqual(initRougeHp, rogue.Hp);
        }

        [Test]
        public void Ensure_that_hps_increase_when_healed()
        {
            Character paladin = new Paladin(new PaladinRandomDataMocked());
            Character rogue = new Rogue(new RogueRandomDataMocked());
            Faction faction = new Faction();
            paladin.JoinFaction(faction);
            rogue.JoinFaction(faction);
            paladin.Attack(rogue);
            double remainingHps = rogue.Hp;
            paladin.Heal(rogue);
            double expected = Math.Min(remainingHps + 5, rogue.InitHp);
            Assert.AreEqual(expected, rogue.Hp);
        }

        [Test]
        public void Ensure_that_hps_do_not_exceed_init_hp_when_healed()
        {
            Character paladin = new Paladin(new PaladinRandomDataMocked());
            Character rogue = new Rogue(new RogueRandomDataMocked());
            double initHp = rogue.Hp;
            paladin.Heal(rogue);
            Assert.AreEqual(initHp, rogue.Hp);
        }

        [Test]
        public void Ensure_that_dead_characters_cannot_be_healed()
        {
            Character paladin = new Paladin(new PaladinRandomDataMocked());
            Character rogue = new Rogue(new RogueRandomDataMocked());
            rogue.Defend(rogue.InitHp + 1000);
            paladin.Heal(rogue);
            Assert.True(rogue.IsDead);
        }

        public void Ensure_that_dead_character_cannot_heal()
        {
            Character paladin = new Paladin(new PaladinRandomDataMocked());
            Character rogue = new Rogue(new RogueRandomDataMocked());
            paladin.Defend(paladin.InitHp + 1000);
            rogue.Defend(10);
            double remainingHps = rogue.Hp;
            paladin.Heal(rogue);
            Assert.AreEqual(remainingHps, rogue.Hp);
        }

        public void Ensure_that_character_cannot_deal_dmg_to_itself()
        {
            Character paladin = new Paladin(new PaladinRandomDataMocked());
            paladin.Attack(paladin);
            Assert.AreEqual(paladin.InitHp, paladin.Hp);
        }

        public void Ensure_that_dmg_is_reduce_by_50_percent_if_enemy_too_many_level_above()
        {
            Character paladin = new Paladin(new PaladinRandomDataMocked());
            Character rogue = new Rogue(new RogueRandomDataMocked());
            double sameLevelDamage = paladin.CalculateTotalDamage(rogue);
            rogue.Level = 6;
            double diffLevelDamage = paladin.CalculateTotalDamage(rogue);
            Assert.AreEqual(sameLevelDamage * 0.5, diffLevelDamage);
        }

        [Test]
        public void Ensure_that_dmg_is_increase_by_50_percent_if_enemy_too_many_level_below()
        {
            Character paladin = new Paladin(new PaladinRandomDataMocked());
            Character rogue = new Rogue(new RogueRandomDataMocked());
            double sameLevelDamage = paladin.CalculateTotalDamage(rogue);
            paladin.Level = 6;
            double diffLevelDamage = paladin.CalculateTotalDamage(rogue);
            Assert.AreEqual(sameLevelDamage + (sameLevelDamage * 0.5), diffLevelDamage / paladin.Level);
        }

        [Test]
        public void Ensure_character_can_attack_if_enemy_in_range()
        {
            Character paladin = new Paladin(new PaladinRandomDataMocked());
            Character wizard = new Wizard(new WizardRandomDataMocked());
            paladin.Position = Position.At(10, 0);
            wizard.Position = Position.At(5, 0);
            wizard.Attack(paladin);
            Assert.True(paladin.Hp < paladin.InitHp);
        }

        [Test]
        public void Ensure_character_cannot_attack_if_enemy_not_in_range()
        {
            Character paladin = new Paladin(new PaladinRandomDataMocked());
            Character wizard = new Wizard(new WizardRandomDataMocked());
            paladin.Position = Position.At(1500, 0);
            wizard.Position = Position.At(5, 0);
            wizard.Attack(paladin);
            Assert.AreEqual(paladin.InitHp, paladin.Hp);
        }

        [Test]
        public void Ensure_new_character_can_join_faction()
        {
            Character paladin = new Paladin(new PaladinRandomDataMocked());
            Faction faction = new Faction();
            paladin.JoinFaction(faction);
            Assert.AreEqual(faction, paladin.Faction);
        }

        [Test]
        public void Ensure_character_cannot_join_faction_if_already_belongs_to_faction()
        {
            Character paladin = new Paladin(new PaladinRandomDataMocked());
            Faction faction1 = new Faction();
            paladin.JoinFaction(faction1);
            Faction faction2 = new Faction();
            paladin.JoinFaction(faction2);
            Assert.AreEqual(faction1, paladin.Faction);
        }

        [Test]
        public void Ensure_character_leaves_faction_when_requested()
        {
            Character paladin = new Paladin(new PaladinRandomDataMocked());
            Faction faction = new Faction();
            paladin.JoinFaction(faction);
            paladin.LeaveFaction();
            Assert.IsNull(paladin.Faction);
        }

        [Test]
        public void Ensure_same_faction_characters_are_allies()
        {
            Character paladin1 = new Paladin(new PaladinRandomDataMocked());
            Character paladin2 = new Paladin(new PaladinRandomDataMocked());
            Faction faction = new Faction();
            paladin1.JoinFaction(faction);
            paladin2.JoinFaction(faction);
            Assert.True(paladin1.IsAlly(paladin2));
        }

        [Test]
        public void Ensure_different_faction_characters_are_not_allies()
        {
            Character paladin1 = new Paladin(new PaladinRandomDataMocked());
            Character paladin2 = new Paladin(new PaladinRandomDataMocked());
            Faction faction1 = new Faction();
            paladin1.JoinFaction(faction1);
            Assert.False(paladin1.IsAlly(paladin2));
        }

        [Test]
        public void Ensure_that_allies_cannot_deal_damage_to_one_another()
        {
            Character paladin1 = new Paladin(new PaladinRandomDataMocked());
            Character paladin2 = new Paladin(new PaladinRandomDataMocked());
            Faction faction = new Faction();
            paladin1.JoinFaction(faction);
            paladin2.JoinFaction(faction);
            paladin1.Attack(paladin2);
            Assert.AreEqual(paladin2.Hp, paladin2.InitHp);
        }

        [Test]
        public void Ensure_that_char_only_heal_himself_if_no_faction()
        {
            Character paladin1 = new Paladin(new PaladinRandomDataMocked());
            Character paladin2 = new Paladin(new PaladinRandomDataMocked());
            paladin2.Defend(10);
            double remainingHpPaladin2 = paladin2.Hp;
            paladin1.Heal(paladin2);
            paladin1.Defend(10);
            double remainingHpPaladin1 = paladin1.Hp;
            paladin1.Heal(paladin1);
            Assert.True(paladin1.Hp > remainingHpPaladin1);
            Assert.AreEqual(paladin2.Hp, remainingHpPaladin2);
        }
    }
}