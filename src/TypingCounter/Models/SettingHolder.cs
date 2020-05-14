using Newtonsoft.Json;
using Prism.Mvvm;
using PropertyChanged;
using System;
using System.IO;
using TypingCounter.Models.Data;

namespace TypingCounter.Models
{
    [AddINotifyPropertyChangedInterface]
    public class SettingHolder : BindableBase
    {
        private readonly string _jsonFilePath;

        public Setting Setting { get; set; }

        public SettingHolder(string jsonFilePath)
        {
            _jsonFilePath = jsonFilePath;
        }

        public SettingHolder Load()
        {
            var serializer = new JsonSerializer();
            using (var sr = new StreamReader(_jsonFilePath))
            using (var reader = new JsonTextReader(sr))
            {
                Setting = serializer.Deserialize<Setting>(reader);
            }
            return this;
        }

        public void Update(Action<Setting> setFunc)
        {
            setFunc(Setting);

            var serializer = new JsonSerializer
            {
                Formatting = Formatting.Indented
            };
            using var sw = new StreamWriter(_jsonFilePath);
            using var writer = new JsonTextWriter(sw);
            serializer.Serialize(writer, Setting);
        }
    }
}