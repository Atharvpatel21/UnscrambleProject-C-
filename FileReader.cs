using System;
using System.IO;
namespace UnscrambleProj3.Workers
{
    class FileReader
    {
        private string[] filContent;

        public string[] Read(string filename)
        {
            try
            {
                string[] fileContent = File.ReadAllLines(filename);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return filContent;
        }

        internal string Read(object wordListFileName)
        {
            throw new NotImplementedException();
        }
    }
}
