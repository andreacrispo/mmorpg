using MMORPG.Domain;
using static MMORPG.RogueTest;
using static MMORPG.WizardTest;

namespace MMORPG
{
    class BattleTest
    {
        [Test]
        public void Ensure_paladin_inflicts_50_percent_more_dmg_to_rogue()
        {
            Character paladin = new Paladin();
            Character rogue = new Rogue();
            double rogueInitialHp = rogue.Hp;
            Battle battle = new Battle(paladin, rogue);
            battle.Fight();
            Assert.True(rogue.Hp >= (rogueInitialHp - ((8 * 1.5) / 3)) && rogue.Hp <= (rogueInitialHp - ((5 * 1.5) / 3)));
        }

        [Test]
        public void Ensure_rogue_inflicts_50_percent_more_dmg_to_wizard()
        {
            Character rogue = new Rogue(new RogueRandomDataMocked());
            Character wizard = new Wizard(new WizardRandomDataMocked());
            double wizardInitialHp = wizard.Hp;
            rogue.Attack(wizard);
            Assert.True(wizard.Hp <= (wizardInitialHp - ((9 * 1.5) / 2) * 2));
        }

        [Test]
        public void Ensure_wizard_inflicts_50_percent_more_dmg_to_paladin()
        {
            Character wizard = new Wizard();
            Character paladin = new Paladin();
            double paladinInitialHp = paladin.Hp;
            Battle battle = new Battle(wizard, paladin);
            battle.Fight();
            Assert.True(paladin.Hp >= (paladinInitialHp - ((16 * 1.5) / 4)) && paladin.Hp <= (paladinInitialHp - ((13 * 1.5) / 4)));
        }


    }
}