using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace prog10
{
    internal class convert
    {
        private static string desktop = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

        public static void Jsonser<A>(A Mydata, string filename)
        {
            string json = JsonConvert.SerializeObject(Mydata);
            File.WriteAllText(desktop + "\\" + filename, json);
        }
        public static A Jsonviser<A>(string filename, string role)
        {
            string json = "";
            try
            {
                json = File.ReadAllText(desktop + "\\" + filename);

            }
            catch (Exception)
            {
                File.Create(desktop + "\\" + filename).Close();
                json = File.ReadAllText(desktop + "\\" + filename);
            }
            A Mydata = JsonConvert.DeserializeObject<A>(json);
            return Mydata;
        }
    }
}