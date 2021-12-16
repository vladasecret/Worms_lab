using System;

namespace Worms_lab.Simulator
{
    public struct Position
    {
        public int X { get; private set; }
        public int Y { get; private set; }

        public Position(int x = 0, int y = 0)
        {
            X = x;
            Y = y;
        }

        override public string ToString()
        {
            return $"({X}, {Y})";
        }

        public int Distance(Position position)
        {
            return Math.Abs(X - position.X) + Math.Abs(Y - position.Y);
        }

        public static Position operator +(Position pos1, Position pos2)
        {
            return new Position(pos1.X + pos2.X, pos1.Y + pos2.Y);
        }

        public static Position operator -(Position pos1, Position pos2)
        {
            return new Position(pos1.X - pos2.X, pos1.Y - pos2.Y);
        }

        public override bool Equals(object obj)
        {
            if (obj is null || obj is not Position)
                return false;
            Position tmp = (Position)obj;
            return this.X == tmp.X && this.Y == tmp.Y;
            
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(X, Y);
        }

        public static bool operator ==(Position pos1, Position pos2)
        {
            return pos1.X == pos2.X && pos1.Y == pos2.Y;
        }

        public static bool operator !=(Position pos1, Position pos2)
        {
            return pos1.X != pos2.X || pos1.Y != pos2.Y;
        }
    }
}
