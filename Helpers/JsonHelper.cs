using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookshelf.Helpers {
    internal class JsonHelper {
        public static string GetConfigurationData(string parentKey, string dataKey) {
            string settingsFileName = "appsettings.json";
            string settingsFolderName = "Configuration";
            string baseReposPath = AppDomain.CurrentDomain.BaseDirectory.Split("bin")[0]; //Dont work in prod :(
            string fullPath = Path.Combine(Path.Combine(baseReposPath, settingsFolderName, settingsFileName));

            try {
                string jsonContent = File.ReadAllText(fullPath);
                JObject jsonObject = JObject.Parse(jsonContent);

                JObject parentObject = jsonObject[parentKey]!.ToObject<JObject>()!;
                JToken dataToken = parentObject[dataKey]!;

                if (dataToken != null) {
                    if (dataToken.Type == JTokenType.Array) {
                        JArray jsonArray = (JArray)dataToken;
                        string[] values = jsonArray.ToObject<string[]>()!;
                        return string.Join(", ", values);
                    } else {
                        return dataToken.ToString(); ;
                    }
                } else {
                    return "Key not found.";
                }
            } catch (Exception ex) {
                throw new Exception("JsonHelper error: " + ex.Message);
            }
        }

        public static string[] GetConfigurationDataArray(string parentKey, string dataKey) {
            string settingsFileName = "appsettings.json";
            string settingsFolderName = "Configuration";
            string baseReposPath = AppDomain.CurrentDomain.BaseDirectory.Split("bin")[0]; //Dont work in prod :(
            string fullPath = Path.Combine(Path.Combine(baseReposPath, settingsFolderName, settingsFileName));

            try {
                string jsonContent = File.ReadAllText(fullPath);
                JObject jsonObject = JObject.Parse(jsonContent);

                JObject parentObject = jsonObject[parentKey]!.ToObject<JObject>()!;
                JToken dataToken = parentObject[dataKey]!;

                if (dataToken != null) {
                    if (dataToken.Type == JTokenType.Array) {
                        JArray jsonArray = (JArray)dataToken;
                        string[] values = jsonArray.ToObject<string[]>()!;
                        return values;
                    } else {
                        return new string[] { dataToken.ToString() };
                    }
                } else {
                    throw new Exception("Key not found");
                }
            } catch (Exception ex) {
                throw new Exception("JsonHelper error: " + ex.Message);
            }
        }
    }
}
