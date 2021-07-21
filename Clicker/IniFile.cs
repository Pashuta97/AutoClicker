using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;

namespace Clicker
{
    class IniFile
    {
        #region Import
        [DllImport("kernel32")]
        static extern long WritePrivateProfileString(string Section, string Key, string Value, string FilePath);

        [DllImport("kernel32")]
        static extern int GetPrivateProfileString(string Section, string Key, string Default, StringBuilder RetVal, int Size, string FilePath);
        #endregion

        private readonly string _path; //File full name

        public IniFile(string iniPath)
        {
            _path = new FileInfo(iniPath).FullName.ToString();
        }

        /// <summary>
        /// Read ini-file and return value in choosen key and section
        /// </summary>
        public string Read(string section, string key)
        {
            var keyValue = new StringBuilder(255);
            GetPrivateProfileString(section, key, "", keyValue, 255, _path);
            return keyValue.ToString();
        }

        /// <summary>
        /// Write value in ini-file in choosen section and key
        /// </summary>
        public void Write(string section, string key, string keyValue)
        {
            WritePrivateProfileString(section, key, keyValue, _path);
        }

        /// <summary>
        /// Delete key in chosen section
        /// </summary>
        public void DeleteKey(string key, string section = null)
        {
            Write(section, key, null);
        }

        /// <summary>
        /// Delete chosen section
        /// </summary>
        public void DeleteSection(string section = null)
        {
            Write(section, null, null);
        }
        
        /// <summary>
        ///Check key in choosen section
        /// </summary>
        public bool KeyExists(string key, string section = null)
        {
            return Read(section, key).Length > 0;
        }
    }
}
