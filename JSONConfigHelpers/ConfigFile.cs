using System;
using System.IO;

namespace JSONConfigHelpers
{
    /// <summary>
    /// A class representing a daemons configuration file
    /// </summary>
    public class ConfigFile
    {
        private readonly String _filename;
        private FileStream _file;
        private FileStream _lockFile;

        public ConfigFile(String filename)
        {
            _filename = filename;
            OpenFile();
        }

        protected void OpenFile()
        {
            try
            {
                _file = File.Open(_filename, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                _lockFile = File.Open(_filename + ".lock", FileMode.OpenOrCreate, FileAccess.Read, FileShare.None);
            }
            catch (DirectoryNotFoundException)
            {
                throw new Exception("Couldnt find config base directory, config file: " + _filename);
            }
            catch (FileNotFoundException)
            {
                throw new Exception("Couldnt find config file: " + _filename);
            }
            catch (Exception)
            {
                throw new Exception("Unknown error opening config file: " + _filename);
            }
        }

        ~ConfigFile()
        {
            if (_file != null)
                _file.Close();
            if (_lockFile != null)
            {
                _lockFile.Close();
            }
        }

        public FileStream Open()
        {
            if (_file == null)
            {
                OpenFile();
            }
            return _file;
        }
    }
}