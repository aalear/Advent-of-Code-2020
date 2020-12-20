using System;
using System.IO;

namespace Day12
{
    class Program
    {
        static void Main(string[] args)
        {
            var input = File.ReadAllLines("input.txt");

            var ship1 = new Ship();

            var ship2 = new Ship();
            var waypoint = new Waypoint();

            foreach(var line in input)
            {
                var instruction = line[0];
                var val = int.Parse(line.Substring(1));

                switch(instruction) 
                {
                    case 'N':
                        ship1.Move(val, Direction.North);
                        waypoint.Move(val, Direction.North);
                        break;
                    case 'S':
                        ship1.Move(val, Direction.South);
                        waypoint.Move(val, Direction.South);
                        break;
                    case 'E':
                        ship1.Move(val, Direction.East);
                        waypoint.Move(val, Direction.East);
                        break;
                    case 'W':
                        ship1.Move(val, Direction.West);
                        waypoint.Move(val, Direction.West);
                        break;
                    case 'R':
                        ship1.Turn(val, rightTurn: true);
                        waypoint.Rotate(val, rightTurn: true);
                        break;
                    case 'L':
                        ship1.Turn(val, rightTurn: false);
                        waypoint.Rotate(val, rightTurn: false);
                        break;
                    case 'F':
                        ship1.Move(val);
                        ship2.Move(val, waypoint);
                        break;
                };
            }
            Console.WriteLine("Part 1: " + ship1.ManhattanDistanceFromOrigin);
            Console.WriteLine("Part 2: " + ship2.ManhattanDistanceFromOrigin);
        }
    }

    class Ship : MovableObject
    {
        public Ship() : base(0, 0) {}
        public Direction Direction { get; set; } = Direction.East;

        public int ManhattanDistanceFromOrigin => Math.Abs(X) + Math.Abs(Y);

        public void Move(int distance, Direction? dir = null)
        {
            dir = dir ?? Direction;
            base.Move(distance, dir.Value);
        }

        public void Move(int steps, Waypoint waypoint)
        {
            var horizontalDirection = waypoint.X > 0 ? Direction.East : Direction.West;
            var verticalDirection = waypoint.Y > 0 ? Direction.South : Direction.North;
            Move(steps * Math.Abs(waypoint.X), horizontalDirection);
            Move(steps * Math.Abs(waypoint.Y), verticalDirection);
        }

        public void Turn(int degrees, bool rightTurn)
        {
            var numTurns = degrees / 90;

            var currentDir = (int)Direction;
            var newDir = rightTurn ? currentDir + numTurns : currentDir - numTurns;
            newDir %= 4;

            if(newDir < 0)
            {
                newDir += 4;
            }

            Direction = (Direction)newDir;
        }
    }
    
    class Waypoint : MovableObject
    {
        public Waypoint() : base(10, -1) {}

        public void Rotate(int degrees, bool rightTurn)
        {
            if(degrees == 180)
            {
                X = -X;
                Y = -Y;
                return;
            }

            var tmp = X;
            if(degrees == 90)
            {
                if(rightTurn)
                {
                    X = -Y;
                    Y = tmp;
                }
                else
                {
                    X = Y;
                    Y = -tmp;
                }
                return;
            }
            Rotate(90, !rightTurn);
        }
    }

    class MovableObject
    {
        protected MovableObject(int x, int y)
        {
            X = x;
            Y = y;
        }

        public int X { get; set; }
        public int Y { get; set; }
        
        public virtual void Move(int distance, Direction direction)
        {
            switch(direction)
            {
                case Direction.North:
                    Y -= distance;
                    break;
                case Direction.South:
                    Y += distance;
                    break;
                case Direction.East:
                    X += distance;
                    break;
                case Direction.West:
                    X -= distance;
                    break;
            }
        }
    }

    enum Direction
    {
        North = 0,
        East = 1,
        South = 2,
        West = 3
    }
}
