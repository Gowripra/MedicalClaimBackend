using System.Reflection;

namespace Medical_Claim.Logger
{
    public static class Log
    {
        private static string FolderPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "logging";
        private static string FilePath = "log_{date}.log";
        /// <summary>
        /// method for logging
        /// </summary>
        /// <param name="logmessage"></param>
        public static void logWrite(string logmessage)
        {
            if (!Directory.Exists(FolderPath))
            {
                Directory.CreateDirectory(FolderPath);
            }
            var fullFilePath = string.Format("{0}/{1}",
                  FolderPath,
                  FilePath.
                  Replace("{date}",
                  DateTime.Now.ToString("yyyyMMdd")));
            var logs = string.Format("{0} {1}",
                                         DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                                         logmessage);

            try
            {
                using (StreamWriter sw = new StreamWriter(fullFilePath, true)) { sw.WriteLine(logs); }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
    }
}
