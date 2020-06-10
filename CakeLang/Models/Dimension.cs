using System;

namespace CakeLang
{
    public class Dimension
    {
        public Dimension(string _namespace)
        {
            Namespace = _namespace;
            throw new NotImplementedException();
        }

        public string Namespace { get; }

        public Directory ToDimensionType()
        {
            // Return the namespace root directory for (dimension type).json
            throw new NotImplementedException();
        }
        public Directory ToDimension()
        {
            // Return the namespace root directory for (dimension).json
            throw new NotImplementedException();
        }
    }
}