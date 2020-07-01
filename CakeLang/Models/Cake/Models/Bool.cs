using System;
using System.Collections.Generic;
using System.Text;

namespace CakeLang.Cake
{
    public class Bool : object
    {
        private readonly bool value;

        public Bool(bool value)
        {
            this.value = value;
        }

        public static implicit operator Bool(bool value) => new Bool(value);
        public override string ToString() => value.ToString().ToLower();
    }
}
