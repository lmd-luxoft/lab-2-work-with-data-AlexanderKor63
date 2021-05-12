using System;
using System.Collections.Generic;
using System.Linq;

namespace Monopoly
{
    struct Cell    {
        internal string Item1;           // Название компании
        internal MonopolyType Item2;     // Тип компании
        internal int Item3;              // Владелец
        internal bool Item4;             // Поле про запас

        internal Cell(string name, MonopolyType type, int cash, bool flag)        {
            Item1 = name;
            Item2 = type;
            Item3 = cash;
            Item4 = flag;
        }
    }
    struct Player    {
        internal string Item1;              // Имя
        internal int    Item2;              // Наличка

        internal Player(string name, int cash)        {
            Item1 = name;
            Item2 = cash;
        }
    }

    abstract class MonopolyType {
        internal virtual void FindRent(ref Player o, ref Player z)                      { }
        internal virtual void FindCash(List<Player> players, int v, Cell k, Player x)   { }
    }
    abstract class MonopolySell    : MonopolyType { }
    abstract class MonopolyNotSell : MonopolyType { }

    class Monopoly_AUTO : MonopolySell {
        internal override void FindRent(ref Player o, ref Player z) {
            z = new Player(z.Item1, z.Item2 - rent);
            o = new Player(o.Item1, o.Item2 + rent);
        }
        internal override void FindCash(List<Player> players, int v, Cell k, Player x) {
            if (k.Item3 == 0)  players[v-1] = new Player(x.Item1, x.Item2 - sell);
        }
        private const int sell = 500;
        private const int rent = sell / 2;
    }
    class Monopoly_FOOD : MonopolySell {
        internal override void FindRent(ref Player o, ref Player z) {
            z = new Player(z.Item1, z.Item2 - rent);
            o = new Player(o.Item1, o.Item2 + rent);
        }
        internal override void FindCash(List<Player> players, int v, Cell k, Player x) {
            if (k.Item3 == 0) players[v - 1] = new Player(x.Item1, x.Item2 - sell);
        }
        private const int sell = 250;
        private const int rent = sell / 2;
    }
    class Monopoly_CLOTHER : MonopolySell {
        internal override void FindRent(ref Player o, ref Player z) {
            z = new Player(z.Item1, z.Item2 - rent);
            o = new Player(o.Item1, o.Item2 + rent);
        }
        internal override void FindCash(List<Player> players, int v, Cell k, Player x) {
            if (k.Item3 == 0) players[v - 1] = new Player(x.Item1, x.Item2 - sell);
        }
        private const int sell = 800;
        private const int rent = sell / 2;
    }
    class Monopoly_TRAVEL : MonopolySell {
        internal override void FindRent(ref Player o, ref Player z) {
            z = new Player(z.Item1, z.Item2 - rent);
            o = new Player(o.Item1, o.Item2 + rent);
        }
        internal override void FindCash(List<Player> players, int v, Cell k, Player x) {
            if (k.Item3 == 0) players[v - 1] = new Player(x.Item1, x.Item2 - sell);
        }
        private const int sell = 100;
        private const int rent = sell / 2;
    }
    class Monopoly_PRISON : MonopolyNotSell {
        internal override void FindRent(ref Player o, ref Player z) {
            z = new Player(z.Item1, z.Item2 - rent);
        }
        private const int rent = 1000;
    }
    class Monopoly_BANK : MonopolyNotSell {
        internal override void FindRent(ref Player o, ref Player z) {
            z = new Player(z.Item1, z.Item2 - rent);
        }
        private const int rent = 700;
    }

    class Monopoly
    {
        internal const int startCash = 6000;
        private List<Player> players = new List<Player>();
        private List<Cell> fields = new List<Cell>();

        public Monopoly(string[] p)
        {
            for (int i = 0; i < p.Length; i++)
            {
                players.Add(new Player(p[i], startCash));     
            }
            fields.Add(new Cell("Ford",     new Monopoly_AUTO(), 0, false));
            fields.Add(new Cell("MCDonald", new Monopoly_FOOD(), 0, false));
            fields.Add(new Cell("Lamoda",   new Monopoly_CLOTHER(), 0, false));
            fields.Add(new Cell("Air Baltic", new Monopoly_TRAVEL(), 0, false));
            fields.Add(new Cell("Nordavia", new Monopoly_TRAVEL(), 0, false));
            fields.Add(new Cell("Prison",   new Monopoly_PRISON(), 0, false));
            fields.Add(new Cell("MCDonald", new Monopoly_FOOD(), 0, false));
            fields.Add(new Cell("TESLA",    new Monopoly_AUTO(), 0, false));
        }

        internal List<Player> GetPlayersList()      { return players;}
        internal List<Cell> GetFieldsList()         { return fields; }
        internal Player GetPlayerInfo(int v)        { return players[v-1]; }

        internal Cell GetFieldByName(string v) {
            return (from p in fields where p.Item1 == v select p).FirstOrDefault();
        }

        internal bool Buy(int v, Cell k) {
            k.Item2.FindCash(GetPlayersList(), v, k, GetPlayerInfo(v));
            
            int i = players.Select((item, index) => new { name = item.Item1, index = index })
                .Where(n => n.name == GetPlayerInfo(v).Item1)
                .Select(p => p.index).FirstOrDefault();
            fields[i] = new Cell(k.Item1, k.Item2, v, k.Item4);
            return true;
        }

        internal bool Renta(int v, Cell k)
        {
            var z = GetPlayerInfo(v);

            if (k.Item3 != 0) {
                 Player o = GetPlayerInfo(k.Item3);
                 k.Item2.FindRent(ref o, ref z);
                 players[k.Item3 - 1] = o;
            }
            else k.Item2.FindRent(ref z, ref z);

            players[v - 1] = z;
            return true;
        }
    }
}
