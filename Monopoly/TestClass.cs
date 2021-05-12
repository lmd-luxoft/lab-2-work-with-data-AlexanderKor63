// NUnit 3 tests
// See documentation : https://github.com/nunit/docs/wiki/NUnit-Documentation
using System;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;

namespace nspMonopoly
{
    [TestFixture]
    public class TestClass
    {
        [Test]
        public void GetPlayersListReturnCorrectList()
        {
            string[] players = new string[]{ "Peter","Ekaterina","Alexander" };
            Player[] expectedPlayers = new Player[]
            {
                new Player("Peter",     6000),
                new Player("Ekaterina", 6000),
                new Player("Alexander", 6000)
            };
            Monopoly monopoly = new Monopoly(players);
            Player[] actualPlayers = monopoly.GetPlayersList().ToArray();

            Assert.AreEqual(expectedPlayers, actualPlayers);
        }
        [Test]
        public void GetFieldsListReturnCorrectList()
        {
            Cell[] expectedCompanies = new Cell[] {
                    new Cell("Ford",     new Monopoly_AUTO(), 0, false),
                    new Cell("MCDonald", new Monopoly_FOOD(), 0, false),
                    new Cell("Lamoda",   new Monopoly_CLOTHER(), 0, false),
                    new Cell("Air Baltic", new Monopoly_TRAVEL(), 0, false),
                    new Cell("Nordavia", new Monopoly_TRAVEL(), 0, false),
                    new Cell("Prison",   new Monopoly_PRISON(), 0, false),
                    new Cell("MCDonald", new Monopoly_FOOD(), 0, false),
                    new Cell("TESLA",    new Monopoly_AUTO(), 0, false)
            };
            //List<Cell> fields = new List<Cell>();

            //fields.Add(new Cell("Ford", new Monopoly_AUTO(), 0, false));
            //fields.Add(new Cell("MCDonald", new Monopoly_FOOD(), 0, false));
            //fields.Add(new Cell("Lamoda", new Monopoly_CLOTHER(), 0, false));
            //fields.Add(new Cell("Air Baltic", new Monopoly_TRAVEL(), 0, false));
            //fields.Add(new Cell("Nordavia", new Monopoly_TRAVEL(), 0, false));
            //fields.Add(new Cell("Prison", new Monopoly_PRISON(), 0, false));
            //fields.Add(new Cell("MCDonald", new Monopoly_FOOD(), 0, false));
            //fields.Add(new Cell("TESLA", new Monopoly_AUTO(), 0, false));

            string[] players = new string[] { "Peter", "Ekaterina", "Alexander" };
            Monopoly monopoly = new Monopoly(players);
            Cell[] actualCompanies = monopoly.GetFieldsList().ToArray();
            //Cell[] expected = fields.ToArray();
            Assert.AreEqual(expectedCompanies, actualCompanies);
        }
        [Test]
        public void PlayerBuyAUTO()
        {
            string[] players = new string[] { "Peter", "Ekaterina", "Alexander" };
            Monopoly monopoly = new Monopoly(players);
            Cell x = monopoly.GetFieldByName("Ford");
            monopoly.Buy(1, x);
            Player actualPlayer = monopoly.GetPlayerInfo(1);
            Player expectedPlayer = new Player("Peter", 5500);
            Assert.AreEqual(expectedPlayer, actualPlayer);
            Cell actualField = monopoly.GetFieldByName("Ford");
            Assert.AreEqual(1, actualField.Item3);
        }
        [Test]
        public void PlayerBuyFOOD()
        {
            string[] players = new string[] { "Peter", "Ekaterina", "Alexander" };
            Monopoly monopoly = new Monopoly(players);
            Cell x = monopoly.GetFieldByName("MCDonald");
            monopoly.Buy(1, x);
            Player actualPlayer = monopoly.GetPlayerInfo(1);
            Player expectedPlayer = new Player("Peter", 5750);
            Assert.AreEqual(expectedPlayer, actualPlayer);
            Cell actualField = monopoly.GetFieldByName("MCDonald");
            Assert.AreEqual(1, actualField.Item3);
        }
        [Test]
        public void PlayerBuyCLOTHER()
        {
            string[] players = new string[] { "Peter", "Ekaterina", "Alexander" };
            Monopoly monopoly = new Monopoly(players);
            Cell x = monopoly.GetFieldByName("Lamoda");
            monopoly.Buy(1, x);
            Player actualPlayer = monopoly.GetPlayerInfo(1);
            Player expectedPlayer = new Player("Peter", 5600);
            Assert.AreEqual(expectedPlayer, actualPlayer);
            Cell actualField = monopoly.GetFieldByName("Lamoda");
            Assert.AreEqual(1, actualField.Item3);
        }
        [Test]
        public void PlayerBuyTRAVEL()
        {
            string[] players = new string[] { "Peter", "Ekaterina", "Alexander" };
            Monopoly monopoly = new Monopoly(players);
            Cell x = monopoly.GetFieldByName("Nordavia");
            monopoly.Buy(1, x);
            Player actualPlayer = monopoly.GetPlayerInfo(1);
            Player expectedPlayer = new Player("Peter", 5200);
            Assert.AreEqual(expectedPlayer, actualPlayer);
            Cell actualField = monopoly.GetFieldByName("Nordavia");
            Assert.AreEqual(1, actualField.Item3);
        }

        [Test]
        public void RentAUTO()
        {
            string[] players = new string[] { "Peter", "Ekaterina", "Alexander" };
            Monopoly monopoly = new Monopoly(players);
            Cell  x = monopoly.GetFieldByName("Ford");
            monopoly.Buy(1, x);
            x = monopoly.GetFieldByName("Ford");
            monopoly.Renta(2, x);
            Player player1 = monopoly.GetPlayerInfo(1);
            Assert.AreEqual(5750, player1.Item2);
            Player player2 = monopoly.GetPlayerInfo(2);
            Assert.AreEqual(5750, player2.Item2);
        }
        [Test]
        public void RentFOOD()
        {
            string[] players = new string[] { "Peter", "Ekaterina", "Alexander" };
            Monopoly monopoly = new Monopoly(players);
            Cell x = monopoly.GetFieldByName("MCDonald");
            monopoly.Buy(1, x);
            x = monopoly.GetFieldByName("MCDonald");
            monopoly.Renta(2, x);
            Player player1 = monopoly.GetPlayerInfo(1);
            Assert.AreEqual(5875, player1.Item2);
            Player player2 = monopoly.GetPlayerInfo(2);
            Assert.AreEqual(5875, player2.Item2);
        }
        [Test]
        public void RentCLOTHER()
        {
            string[] players = new string[] { "Peter", "Ekaterina", "Alexander" };
            Monopoly monopoly = new Monopoly(players);
            Cell x = monopoly.GetFieldByName("Lamoda");
            monopoly.Buy(1, x);
            x = monopoly.GetFieldByName("Lamoda");
            monopoly.Renta(2, x);
            Player player1 = monopoly.GetPlayerInfo(1);
            Assert.AreEqual(5800, player1.Item2);
            Player player2 = monopoly.GetPlayerInfo(2);
            Assert.AreEqual(5800, player2.Item2);
        }
        [Test]
        public void RentTRAVEL()
        {
            string[] players = new string[] { "Peter", "Ekaterina", "Alexander" };
            Monopoly monopoly = new Monopoly(players);
            Cell x = monopoly.GetFieldByName("Nordavia");
            monopoly.Buy(1, x);
            x = monopoly.GetFieldByName("Nordavia");
            monopoly.Renta(2, x);
            Player player1 = monopoly.GetPlayerInfo(1);
            Assert.AreEqual(5600, player1.Item2);
            Player player2 = monopoly.GetPlayerInfo(2);
            Assert.AreEqual(5600, player2.Item2);
        }
        [Test]
        public void RentPrison()
        {
            string[] players = new string[] { "Peter", "Ekaterina", "Alexander" };
            Monopoly monopoly = new Monopoly(players);
            Cell x = monopoly.GetFieldByName("Prison");
            monopoly.Renta(3, x);
            Player player1 = monopoly.GetPlayerInfo(3);
            Assert.AreEqual(5000, player1.Item2);
        }
    }
}
