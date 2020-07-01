using System;
using System.Collections.Generic;
using System.Text;

namespace CakeLang.Cake
{
    public class Selector
    {
        /// <summary>
        /// @s
        /// </summary>
        public static readonly Selector Executor = new Selector('s');
        /// <summary>
        /// @a
        /// </summary>
        public static readonly Selector All = new Selector('a');
        /// <summary>
        /// @p
        /// </summary>
        public static readonly Selector NearestPlayer = new Selector('p');
        /// <summary>
        /// @r
        /// </summary>
        public static readonly Selector RandomPlayer = new Selector('r');
        /// <summary>
        /// @e[sort=random]
        /// </summary>
        public static readonly Selector RandomEntity = new Selector("e[sort=random]");

        private readonly string selector;

        /// <summary>
        /// Specifies a target selector
        /// </summary>
        /// <param name="selector">Does not require the '@' prefix</param>
        /// <returns>A new selector</returns>
        public Selector(string selector)
        {
            this.selector = selector.Trim().TrimStart('@');
        }
        public Selector(char selector)
        {
            this.selector = selector.ToString();
        }

        public static implicit operator Selector(string selector) => new Selector(selector);
        public static implicit operator Selector(char selector) => new Selector(selector);

        public override string ToString() => '@' + selector;
    }
}
