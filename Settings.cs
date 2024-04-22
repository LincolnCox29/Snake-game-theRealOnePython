using System.Data;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace theRealOnePython
{
	internal class Settings 
	{
        public Dictionary<string,int> LoadJson()
        {
            using (StreamReader r = new StreamReader("settings.json"))
            {
                string json = r.ReadToEnd();
                return JsonSerializer.Deserialize<Dictionary<string, int>>(json);
            }
        }
    }
}
