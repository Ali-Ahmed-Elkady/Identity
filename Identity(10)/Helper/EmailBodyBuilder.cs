using System.Collections.Generic;

namespace Identity_10_.Helper
{
    public static class EmailBodyBuilder
    {
        public static string GenerateEmailBody(string tempelate , Dictionary<string , string>TempelateModel)
        {
            var TempPath = $"{Directory.GetCurrentDirectory()}/Tempelates/{tempelate}.html";
            var streamReader = new StreamReader(TempPath);
            var body = streamReader.ReadToEnd();
            streamReader.Close();
            foreach (var item in TempelateModel)
            {
                body = body.Replace(item.Key, item.Value);
            }
            return body;
        }
    }
}
