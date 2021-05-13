using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;

namespace Monopoly
{
    [StructLayout(LayoutKind.Sequential)]
    struct Cell    {
        internal string Item1;           // Название компании
        internal MonopolyType Item2;     // Тип компании
        internal int Item3;              // Владелец
        internal bool Item4;             // Поле про запас

        internal Cell(string name, MonopolyType type, int player, bool flag)        {
            Item1 = name;
            Item2 = type;
            Item3 = player;
            Item4 = flag;
        }
        public override bool Equals(object obj) {
            if (!(obj is Cell))                     return false;
            Cell cell = (Cell)obj;
            if (Item1 == cell.Item1 && Item3 == cell.Item3 && Item4 == cell.Item4
                && Item2.GetType()==cell.Item2.GetType())  return true;
            return false;
        }
    }

    [StructLayout(LayoutKind.Sequential)]
    struct Player    {
        internal string Item1;              // Имя
        internal int    Item2;              // Наличка

        internal Player(string name, int cash)        {
            Item1 = name;
            Item2 = cash;
        }
    }

    abstract class MonopolyType {
        internal abstract int Rent { get; }
        internal abstract void FindRent(ref Player o, ref Player z);
        internal abstract void FindCash(List<Player> players, int v, Cell k, Player x);
    }

    abstract class MonopolySell    : MonopolyType {
        internal abstract int Sell { get; }
        internal override void FindRent(ref Player o, ref Player z) {
            z = new Player(z.Item1, z.Item2 - Rent);
            o = new Player(o.Item1, o.Item2 + Rent);
        }
        internal override void FindCash(List<Player> players, int v, Cell k, Player x) {
            if (k.Item3 == 0) players[v - 1] = new Player(x.Item1, x.Item2 - Sell);
        }
    }

    abstract class MonopolyNotSell : MonopolyType {
        internal override void FindRent(ref Player o, ref Player z) {
            z = new Player(z.Item1, z.Item2 - Rent);
        }
        internal override void FindCash(List<Player> players, int v, Cell k, Player x) { }
    }

    class Monopoly_AUTO : MonopolySell {
        internal override int Rent { get { return sell/2; } }
        internal override int Sell { get { return sell; } }
        private const int sell = 500;
    }

    class Monopoly_FOOD : MonopolySell {
        internal override int Rent { get { return sell / 2; } }
        internal override int Sell { get { return sell; } }
        private const int sell = 250;
    }

    class Monopoly_CLOTHER : MonopolySell {
        internal override int Rent { get { return sell / 2; } }
        internal override int Sell { get { return sell; } }
        private const int sell = 400;
    }

    class Monopoly_TRAVEL : MonopolySell {
        internal override int Rent { get { return sell / 2; } }
        internal override int Sell { get { return sell; } }
        private const int sell = 800;
    }

    class Monopoly_PRISON : MonopolyNotSell {
        internal override int Rent { get { return 1000; } }
    }

    class Monopoly_BANK : MonopolyNotSell {
        internal override int Rent { get { return 700; } }
    }

    class Monopoly
    {
        internal const int startCash = 6000;
        private List<Player> players = new List<Player>();
        private List<Cell>   fields  = new List<Cell>();

        public Monopoly(string[] p)
        {
            for (int i = 0; i < p.Length; i++)
            {
                players.Add(new Player(p[i], startCash));     
            }
            fields.Add(new Cell("Ford", new Monopoly_AUTO(), 0, false));
            fields.Add(new Cell("MCDonald", new Monopoly_FOOD(), 0, false));
            fields.Add(new Cell("Lamoda", new Monopoly_CLOTHER(), 0, false));
            fields.Add(new Cell("Air Baltic", new Monopoly_TRAVEL(), 0, false));
            fields.Add(new Cell("Nordavia", new Monopoly_TRAVEL(), 0, false));
            fields.Add(new Cell("Prison", new Monopoly_PRISON(), 0, false));
            fields.Add(new Cell("MCDonald", new Monopoly_FOOD(), 0, false));
            fields.Add(new Cell("TESLA", new Monopoly_AUTO(), 0, false));
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
