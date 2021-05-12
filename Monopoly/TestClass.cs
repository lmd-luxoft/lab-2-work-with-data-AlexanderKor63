// NUnit 3 tests
// See documentation : https://github.com/nunit/docs/wiki/NUnit-Documentation
using System;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;

namespace Monopoly
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
                new Player("Peter",Monopoly.startCash),
                new Player("Ekaterina",Monopoly.startCash),
                new Player("Alexander",Monopoly.startCash)
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
                new Cell("Lamoda", new Monopoly_CLOTHER(), 0, false),
                new Cell("Air Baltic", new Monopoly_TRAVEL(), 0, false),
                new Cell("Nordavia", new Monopoly_TRAVEL(), 0, false),
                new Cell("Prison", new Monopoly_PRISON(), 0, false),
                new Cell("MCDonald", new Monopoly_FOOD(), 0, false),
                new Cell("TESLA", new Monopoly_AUTO(), 0, false)
        };            string[] players = new string[] { "Peter", "Ekaterina", "Alexander" };
            Monopoly monopoly = new Monopoly(players);
            Cell[] actualCompanies = monopoly.GetFieldsList().ToArray();
            Assert.AreEqual(expectedCompanies, actualCompanies);
        }
        [Test]
        public void PlayerBuyNoOwnedCompanies()
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
        public void RentaShouldBeCorrectTransferMoney()
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
    }
}
