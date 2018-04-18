using System;
using System.IO;
using System.Reflection;
using System.Text;

namespace rbg.smite.Resources
{
    public class Resources
    {
        public static string GetJSONFromResources(string filename)
        {
            var assembly = typeof(App).GetTypeInfo().Assembly;
            StringBuilder sb = new StringBuilder();
            string json = null;
            try
            {
                var stream = assembly.GetManifestResourceStream("rbg.smite.Resources." + filename);
                using (BufferedStream bs = new BufferedStream(stream))
                using (StreamReader sr = new StreamReader(bs))
                {
                    while (!sr.EndOfStream)
                    {
                        sb.Append(sr.ReadLine());
                    }
                    stream.Close();
                }
                json = sb.ToString();
            }
            catch (Exception e) {

            }
            return json;
        }
    }
}
