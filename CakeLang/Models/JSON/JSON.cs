using CakeLang.Cake;
using System;
using System.Collections.Generic;
using System.Text;

namespace CakeLang.JSON
{
    public static class JSON
    {
        public static string ArrayBuilder(params string[] json)
        {
            string result = "[";
            for (int i = 0; i < json.Length; i++)
            {
                result += json[i];
                if (i < json.Length - 1) result += ',';
            }
            return result + "]";
        }

        public static string ObjectBuilder(params KeyValuePair<string, object>[] attributes) => ObjectBuilder(false, null, false, attributes);
        public static string ObjectBuilder(bool indent, params KeyValuePair<string, object>[] attributes) => ObjectBuilder(indent, "", false, attributes);
        public static string ObjectBuilder(bool indent, string indentPrefix, bool forceNoQuotation, params KeyValuePair<string, object>[] attributes)
        {
            string result = "{" + (indent ? indentPrefix + '\n' : "");
            for (int i = 0; i < attributes.Length; i++)
            {
                KeyValuePair<string, object> pair = attributes[i];

                result += (indent ? indentPrefix + '\t' : "") + '"' + pair.Key + '"' + ':' + (indent ? " " : "");

                if (pair.Value is float || pair.Value is double || pair.Value is long || pair.Value is int || pair.Value is short) result += pair.Value.ToString();
                else if (pair.Value is bool) result += pair.Value.ToString().ToLower();
                else result += (forceNoQuotation ? "" : "\"") + pair.Value.ToString() + (forceNoQuotation ? "" : "\"");
                
                if (i < attributes.Length - 1) result += ',' + (indent ? "\n" : "");
            }
            return result + (indent ? '\n' + indentPrefix : "") + '}';
        }

        public static string Text(string text, params KeyValuePair<string, object>[] jsonStyleFormattings)
        {
            List<KeyValuePair<string, object>> attributes = new List<KeyValuePair<string, object>>();
            attributes.Add(new KeyValuePair<string, object>("text", text));
            for (int i = 0; i < jsonStyleFormattings.Length; i++) attributes.Add(new KeyValuePair<string, object>(jsonStyleFormattings[i].Key, jsonStyleFormattings[i].Value));
            return ObjectBuilder(attributes.ToArray());
        }

        public static string FunctionVariable(string function, string name, params KeyValuePair<string, object>[] jsonStyleFormattings)
        {
            List<KeyValuePair<string, object>> attributes = new List<KeyValuePair<string, object>>();
            attributes.Add(new KeyValuePair<string, object>("storage", CakeLang.StorageRoot));
            attributes.Add(new KeyValuePair<string, object>("nbt", CakeLang.StorageFunctions + '.' + function + '.' + CakeLang.StorageFunctionsArguments + '.' + name));
            for (int i = 0; i < jsonStyleFormattings.Length; i++) attributes.Add(new KeyValuePair<string, object>(jsonStyleFormattings[i].Key, jsonStyleFormattings[i].Value));
            return ObjectBuilder(attributes.ToArray());
        }
        #region Data
        public static class StyleFormattings
        {
            public static KeyValuePair<string, object> Bold = new KeyValuePair<string, object>("bold", true);
            public static KeyValuePair<string, object> Italic = new KeyValuePair<string, object>("italic", true);
            public static KeyValuePair<string, object> Underlined = new KeyValuePair<string, object>("underlined", true);
            public static KeyValuePair<string, object> Strikethrough = new KeyValuePair<string, object>("strikethrough", true);
            public static KeyValuePair<string, object> Obfuscated = new KeyValuePair<string, object>("obfuscated", true);
            public static KeyValuePair<string, object> Color(string color) => new KeyValuePair<string, object>("color", color);
            public static KeyValuePair<string, object> Font(string namespaceFont) => new KeyValuePair<string, object>("font", namespaceFont);
            public static KeyValuePair<string, object> Insertion(string text) => new KeyValuePair<string, object>("insertion", text);
            public static KeyValuePair<string, object> ClickEvent(string actionJSON, string valueJSON) => new KeyValuePair<string, object>("clickEvent", ObjectBuilder(
                    new KeyValuePair<string, object>("action", actionJSON),
                    new KeyValuePair<string, object>("value", valueJSON))
                );
            public static KeyValuePair<string, object> HoverEvent(string actionJSON, string valueJSON, string contentsJSON) => new KeyValuePair<string, object>("clickEvent", ObjectBuilder(
                    new KeyValuePair<string, object>("action", actionJSON),
                    new KeyValuePair<string, object>("value", valueJSON),
                    new KeyValuePair<string, object>("contents", contentsJSON))
                );
        }
        #endregion
    }
}