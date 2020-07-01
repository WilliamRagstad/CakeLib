using System;
using System.Collections.Generic;
using System.Text;

namespace CakeLang.Cake
{
    /// <summary>
    /// This class provides all the methods from the language 'Cake' in the CakeLang framework.
    /// </summary>
    public class Program
    {
        #region Snippets
        public static string[] Execute(bool includeExecute, string type, Selector selector, params string[] code)
        {
            List<string> result = new List<string>();
            for (int i = 0; i < code.Length; i++) result.Add((includeExecute ? "execute " : "") + type + ' ' + selector + ' ' + code[i]);
            return result.ToArray();
        }
        public static string[] Execute(bool includeExecute, string type, Position position, string block, params string[] code)
        {
            List<string> result = new List<string>();
            for (int i = 0; i < code.Length; i++) result.Add((includeExecute ? "execute " : "") + type + ' ' + position + ' ' + block + ' ' + code[i]);
            return result.ToArray();
        }

        public static string[] As(bool includeExecute, Selector selector, params string[] code) => Execute(includeExecute, "as", selector, code);
        public static string[] As(Selector selector, params string[] code) => As(true, selector, code);
        public static string[] At(bool includeExecute, Selector selector, params string[] code) => Execute(includeExecute, "at", selector, code);
        public static string[] At(Selector selector, params string[] code) => At(true, selector, code);
        public static string[] AsAt(bool includeExecute, Selector selector, params string[] code) => As(includeExecute, selector, At(false, Selector.Executor, code));
        /// <summary>
        /// Execute code as and at the targeted selector(s).
        /// </summary>
        public static string[] AsAt(Selector selector, params string[] code) => AsAt(true, selector, code);
        public abstract class Conditional
        {
            protected string prefix;
            public string[] Entity(bool includeExecute, Selector selector, params string[] code) => Execute(includeExecute, prefix + " entity", selector, code);
            public string[] Entity(Selector selector, params string[] code) => Entity(true, selector, code);
            public string[] Block(bool includeExecute, Position position, string block, params string[] code) => Execute(includeExecute, prefix + " block", position, block, code);
            public string[] Block(Position position, string block, params string[] code) => Block(true, position, block, code);

        }
        public class ConditionalUnless : Conditional
        {
            public ConditionalUnless() { prefix = "unless"; }
        }
        public class ConditionalIf : Conditional
        {
            public ConditionalIf() { prefix = "if"; }
        }

        public static ConditionalUnless Unless = new ConditionalUnless();
        public static ConditionalIf If = new ConditionalIf();

        public static string[] Run(params string[] code)
        {
            List<string> result = new List<string>();
            for (int i = 0; i < code.Length; i++) result.Add("run " + code[i]);
            return result.ToArray(); 
        }

        public static string Namespace(DataPack dataPack, string path)
        {
            return dataPack.Namespace + ':' + path;
        }
        public static string Namespace(string path)
        {
            return "minecraft:" + path;
        }

        #endregion

        #region Commands

        /* TODO:
         *  - Add explainations for all commands and arguments from the https://minecraft.gamepedia.com!
         *  - Add hint to which commands or classes that may help writing the correct parameter.
         */

        public static string Ability(Selector selector, string ability, Bool value) => "ability " + selector + ' ' + ability + ' ' + value;
        public static class Attribute
        {
            private static string Prefix(Selector selector, string attribute) => "attribute " + selector + ' ' + attribute + ' ';
            public static string Get(Selector selector, string attribute, float scale) => Prefix(selector, attribute) + "base get " + scale;
            public static string Set(Selector selector, string attribute, string value) => Prefix(selector, attribute) + "base set " + value;

            public static class Modifier
            {
                public static string Add(Selector selector, string attribute, string uuid, string name, string value, string modifier) => Prefix(selector, attribute) + "modifier add " + uuid + ' ' + name + ' ' + value + ' ' + modifier;
                public static string Remove(Selector selector, string attribute, string uuid) => Prefix(selector, attribute) + "modifier remove " + uuid;
                public static string ValueGet(Selector selector, string attribute, string uuid, float scale) => Prefix(selector, attribute) + "modifier get " + uuid + ' ' + scale;

                public static class Modifiers
                {
                    public static string Add { get { return "add"; } }
                    public static string Multiply { get { return "multiply"; } }
                    public static string MultiplyBase { get { return "multiply_base"; } }
                }
            }
        }
        public static class Advancement
        {
            private static string Prefix(Selector selector, string type) => "advancement " + type + ' ' + selector + ' ';
            public static string Everything(Selector selector, string type) => Prefix(selector, type) + "everything";
            public static string Only(Selector selector, string type, string advancement, string criterion) => Prefix(selector, type) + "only " + advancement + ' ' + criterion;
            public static string From(Selector selector, string type, string advancement) => Prefix(selector, type) + "from " + advancement;
            public static string Through(Selector selector, string type, string advancement) => Prefix(selector, type) + "through " + advancement;
            public static string Until(Selector selector, string type, string advancement) => Prefix(selector, type) + "until " + advancement;
        }
        public static string Ban(Selector selector, string reason) => "ban " + selector + ' ' + reason;
        public static string BanIP(Selector selector, string reason) => "ban-ip " + selector + ' ' + reason;
        public static string BanList(string banlist) => "banlist " + banlist;

        public static class Bossbar
        {
            private static string Prefix() => "bossbar ";
            public static string Add(string id, string name) => Prefix() + "add " + id + ' ' + name;
            public static string Get(string id, string bossbarGetType) => Prefix() + "add " + id + ' ' + bossbarGetType;
            public static string List() => Prefix() + "list";
            public static string Remove(string id) => Prefix() + "remove " + id;
        }

        // Lots of commands ...

        public static string Tellraw(Selector selector, string textJson) => "tellraw " + selector + ' ' + textJson;

        #endregion

        #region Command String Enums

        public static class Abilities
        {
            public static string WorldBuilder { get { return "worldbuilder"; } }
            public static string MayFly { get { return "mayfly"; } }
            public static string Mute { get { return "mute"; } }
        }
        public static class BanLists
        {
            public static string IPs { get { return "ips"; } }
            public static string Players { get { return "players"; } }
        }

        public static class BossbarGetTypes
        {
            public static string Max { get { return "max"; } }
            public static string Players { get { return "players"; } }
            public static string Value { get { return "value"; } }
            public static string Visible { get { return "visible"; } }
        }

        public static class Colors
        {
            public static string Aqua { get { return "aqua"; } }
            public static string Blue { get { return "blue"; } }
            public static string Green { get { return "green"; } }
            public static string Pink { get { return "pink"; } }
            public static string Purple { get { return "purple"; } }
            public static string LightPurple { get { return "light_purple"; } }
            public static string Red { get { return "red"; } }
            public static string Yellow { get { return "yellow"; } }
            public static string Gold { get { return "gold"; } }
            public static string White { get { return "white"; } }
            public static string Gray { get { return "gray"; } }
            public static string DarkGray { get { return "dark_gray"; } }
            public static string Black { get { return "blac"; } }
            public static string DarkAqua { get { return "dark_aqua"; } }
            public static string DarkBlue { get { return "dark_blue"; } }
            public static string DarkGreen { get { return "dark_green"; } }
            public static string DarkRed { get { return "dark_red"; } }
            public static string DarkPurple { get { return "dark_purple"; } }

            public static string FromColor(System.Drawing.Color color) => '#' + ColorToHexString(color);
        }
        public static class FormatCodes
        {
            public static class Obfuscated
            {
                /// <summary>
                /// The internal code for this format in chat.
                /// </summary>
                public static string ChatCode { get { return "§k"; } }
                /// <summary>
                /// The internal code for this format in the "Message Of The Day".
                /// </summary>
                public static string MOTDCode { get { return @"\u00A7k"; } }
            }
            public static class Bold
            {
                /// <summary>
                /// The internal code for this format in chat.
                /// </summary>
                public static string ChatCode { get { return "§l"; } }
                /// <summary>
                /// The internal code for this format in the "Message Of The Day".
                /// </summary>
                public static string MOTDCode { get { return @"\u00A7l"; } }
            }
            public static class Strikethrough
            {
                /// <summary>
                /// The internal code for this format in chat.
                /// </summary>
                public static string ChatCode { get { return "§m"; } }
                /// <summary>
                /// The internal code for this format in the "Message Of The Day".
                /// </summary>
                public static string MOTDCode { get { return @"\u00A7m"; } }
            }
            public static class Underline
            {
                /// <summary>
                /// The internal code for this format in chat.
                /// </summary>
                public static string ChatCode { get { return "§n"; } }
                /// <summary>
                /// The internal code for this format in the "Message Of The Day".
                /// </summary>
                public static string MOTDCode { get { return @"\u00A7n"; } }
            }
            public static class Italic
            {
                /// <summary>
                /// The internal code for this format in chat.
                /// </summary>
                public static string ChatCode { get { return "§o"; } }
                /// <summary>
                /// The internal code for this format in the "Message Of The Day".
                /// </summary>
                public static string MOTDCode { get { return @"\u00A7o"; } }
            }
            public static class Reset
            {
                /// <summary>
                /// The internal code for this format in chat.
                /// </summary>
                public static string ChatCode { get { return "§r"; } }
                /// <summary>
                /// The internal code for this format in the "Message Of The Day".
                /// </summary>
                public static string MOTDCode { get { return @"\u00A7r"; } }
            }
        }

        #endregion

        #region Snippets
        static char[] hexDigits = {
         '0', '1', '2', '3', '4', '5', '6', '7',
         '8', '9', 'A', 'B', 'C', 'D', 'E', 'F'};

        /// <summary>
        /// Convert a .NET Color to a hex string.
        /// </summary>
        /// <returns>ex: "FFFFFF", "AB12E9"</returns>
        internal static string ColorToHexString(System.Drawing.Color color)
        {
            byte[] bytes = new byte[3];
            bytes[0] = color.R;
            bytes[1] = color.G;
            bytes[2] = color.B;
            char[] chars = new char[bytes.Length * 2];
            for (int i = 0; i < bytes.Length; i++)
            {
                int b = bytes[i];
                chars[i * 2] = hexDigits[b >> 4];
                chars[i * 2 + 1] = hexDigits[b & 0xF];
            }
            return new string(chars);
        }

        #endregion
    }
}
