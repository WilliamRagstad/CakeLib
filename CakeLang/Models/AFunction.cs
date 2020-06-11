namespace CakeLang
{
    public abstract class AFunction : IMCModel
    {
        protected AFunction(string name, params string[] content)
        {
            Name = name;
            Content = content;
        }

        public string Name { get; set; }
        public string[] Content { get; set; }

        File[] IMCModel.ToFiles()
        {
            return new[] {
                new File(Name + ".mcfunction", Content)
            };
        }
    }
}
