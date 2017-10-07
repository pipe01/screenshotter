using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Screenshotter
{
    public class Configuration
    {
        public string FileFormat { get; set; } = "Png";
        public string StoreFolder { get; set; } = null;
        public float ImageScale { get; set; } = 1.0f;
        public bool ScreenEffect { get; set; } = true;

        public void SetFileFormat(ImageFormat format)
        {
            foreach (var item in typeof(ImageFormat).GetProperties())
            {
                if (!item.GetMethod.IsStatic)
                    continue;

                if (format == item.GetValue(null))
                {
                    FileFormat = item.Name;
                    return;
                }
            }
            throw new ArgumentException();
        }
        public ImageFormat GetFileFormat()
        {
            return typeof(ImageFormat).GetProperty(FileFormat)?.GetValue(null) as ImageFormat;
        }


        public static Configuration Load()
        {
            if (!File.Exists("config.json"))
                new Configuration().Save();

            return JsonConvert.DeserializeObject<Configuration>(File.ReadAllText("config.json"));
        }

        public void Save()
        {
            File.WriteAllText("config.json", JsonConvert.SerializeObject(this));
        }
    }
}
