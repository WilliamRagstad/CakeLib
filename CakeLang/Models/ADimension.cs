using System;

namespace CakeLang
{
    public abstract class ADimension
    {
        public ADimension(string _namespace)
        {
            Namespace = _namespace;
            throw new NotImplementedException();
        }

        public string Namespace { get; }

        internal Directory ToDimensionType()
        {
            // Return the namespace root directory for (dimension type).json
            throw new NotImplementedException();
        }
        internal Directory ToDimension()
        {
            // Return the namespace root directory for (dimension).json
            throw new NotImplementedException();
        }
    }
}