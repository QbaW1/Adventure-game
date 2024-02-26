using PO___gra;
using POProjekt;

namespace TestGra
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestEnemy()
        {
            string name = "Slime";
            int hp = 30;
            int dmg = 30;
            int def = 30;
            Difficulty difficulty = Difficulty.easy;
            Item item = new(ItemType.HealthPotion);

            Enemy enemy = new(name, hp, dmg, def, difficulty, item);

            Assert.AreEqual(name, enemy.Name);
            Assert.AreEqual(hp, enemy.Hp);
            Assert.AreEqual(dmg, enemy.Dmg);
            Assert.AreEqual(def, enemy.Def);
            Assert.AreEqual(difficulty, enemy.Difficulty);
            Assert.AreEqual(item, enemy.EnemyItem);
        }

        [TestMethod]
        public void TestBoss()
        {
            string name = "KingSlime";
            int hp = 30;
            int dmg = 30;
            int def = 30;
            Difficulty difficulty = Difficulty.easy;
            Item artifact = new(ItemType.Key);

            Boss boss = new(name, hp, dmg, def, difficulty, artifact);

            Assert.AreEqual(name, boss.Name);
            Assert.AreEqual(hp, boss.Hp);
            Assert.AreEqual(dmg, boss.Dmg);
            Assert.AreEqual(def, boss.Def);
            Assert.AreEqual(difficulty, boss.Difficulty);
            Assert.AreEqual(artifact, boss.Artifact);
        }

        [TestMethod]
        public void TestMyError()
        {
            string errorMessage = "Test Error Message";
            string extraInfo = "Additional Information";

            MyError myError = new MyError(errorMessage, extraInfo);

            Assert.AreEqual(errorMessage, myError.Message);
            Assert.AreEqual(extraInfo, myError.ExtraInfo);
        }

        [TestMethod]
        public void TestItemCompareTo()
        {
            Item item1 = new Item(ItemType.MagicScepter);
            Item item2 = new Item(ItemType.MagicScepter);

            Assert.AreEqual(0, item1.CompareTo(item2));
        }

        [TestMethod]
        public void TestZobacz_przedmiot()
        {
            Item item = new Item(ItemType.HealthPotion);

            Assert.IsNotNull(item.zobacz_przedmiot());
        }

        [TestMethod]
        public void TestStartingLocalization()
        {
            Localization localization = new StartingLocation();

            Assert.AreEqual(1, (int)localization.Amount_of_items);
            Assert.IsNull(localization.Item);
        }

        [TestMethod]
        public void TestOpenTheGates()
        {
            Player player = new Player { Key_count = 3 };
            Gates gates = new Gates();

            Assert.IsFalse(gates.OpenTheGates(player));
        }

        public void TestRemoveItem()
        {
            Equipment equipment = new Equipment();
            Item item1 = new Item(ItemType.Dagger);
            Item item2 = new Item(ItemType.MagicScepter);
            Item item3 = new Item(ItemType.Armor);
            equipment.AddItem(item1);
            equipment.AddItem(item2);
            equipment.AddItem(item3);

            Assert.AreEqual(item2, equipment.RemoveItemAt(1));
        }

        [TestMethod]
        public void TestPlayer()
        {
            string name = "Tester";
            HeroType playerClass = HeroType.Warrior;

            Player player = new(name, playerClass);

            Assert.AreEqual(name, player.Nickname);
            Assert.AreEqual(playerClass, player.PlayerClass);
        }

        [TestMethod]
        public void TestHeroBuffEq()
        {
            Player player = new Player();
            Item item = new Item(ItemType.Dagger);

            player.HeroBuffEq(item);

            Assert.AreEqual(10, player.Dmg);
        }
    }
}