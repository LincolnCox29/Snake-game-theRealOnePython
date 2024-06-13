using System.Data;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace theRealOnePython
{
	class Settings 
	{
        private int milliseconds;
        private int formSize;
        private int tileSize;

        public Settings() 
        {
            Dictionary<string, int> jsonData = LoadJson();
            milliseconds = jsonData["milliseconds"];
            formSize = jsonData["formSize"];
            tileSize = jsonData["tileSize"];
        }

        public int getMilliseconds
        {
            get => milliseconds;
        }

        public int getFormSize
        { 
            get => formSize;
        }
        
        public int getTileSize
        {
            get => tileSize;
        }

        private Dictionary<string,int>? LoadJson()
        {
            using (StreamReader r = new StreamReader("settings.json"))
            {
                string json = r.ReadToEnd();
                try
                {
                    return JsonSerializer.Deserialize<Dictionary<string, int>>(json);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    return null;
                }
            }
        }
    }
}
