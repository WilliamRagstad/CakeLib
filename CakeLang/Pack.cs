using System;
using System.IO;

namespace CakeLang
{
    public class Pack
    {
        private readonly Directory root;

        internal Pack(Directory root)
        {
            this.root = root;
        }
        public void Inject(string mcWorldName)
        {
            string path =
                Path.Combine(
                Path.Combine(
                Path.Combine(
                Path.Combine(
                    Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                    ".minecraft"),
                    "saves"),
                    mcWorldName),
                    "datapacks");
            if (!System.IO.Directory.Exists(path))
            {
                ErrorManager.InjectError("Minecraft world '" + mcWorldName + "' could not be found! Please make sure that the world is generated before attempting to inject any data packs...");
                return;
            }
            root.Create(path);
        }
    }
}