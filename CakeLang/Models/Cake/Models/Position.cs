using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace CakeLang.Cake
{
    public class Position
    {
        /// <summary>
        /// ~ ~ ~
        /// </summary>
        public static readonly Selector Executor = new Selector('s');

        public PositionElement X { get; }
        public PositionElement Y { get; }
        public PositionElement Z { get; }

        public Position(PositionElement x, PositionElement y, PositionElement z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public Position(string position) => Parse(position);

        public static implicit operator Position(string position) => Parse(position);

        public static Position Parse(string position)
        {
            MatchCollection matches = Regex.Matches(position, @"(~[-\d]*)|[-\d]+");
            if (matches.Count == 3) return new Position(matches[0].Value, matches[1].Value, matches[2].Value);
            else throw new ArgumentException("The specified position is not valid!");
        }

        public override string ToString() => X + " " + Y + " " + Z;
    }

    public class PositionElement
    {
        public PositionElement(long value, bool isRelative)
        {
            Value = value;
            IsRelative = isRelative;
        }

        public PositionElement(string positionElement) => Parse(positionElement);

        public static PositionElement Parse(string positionElement)
        {
            if (positionElement[0] == '~')
            {
                if (positionElement.Length > 1 && long.TryParse(positionElement.TrimStart('~'), out long value)) return new PositionElement(value, true);
                else if (positionElement.Length == 1) return new PositionElement(0, true);
                else throw new ArgumentException("The specified position element is not valid!");
            }
            else
            {
                if (long.TryParse(positionElement, out long value)) return new PositionElement(value, false);
                else throw new ArgumentException("The specified position element is not valid!");
            }
        }
        public static implicit operator PositionElement(string positionElement) => Parse(positionElement);

        public long Value { get; }
        public bool IsRelative { get; }

        public override string ToString() => (IsRelative ? "~" + (Value != 0 ? Value.ToString() : "") : Value.ToString());
    }
}
