using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.DirectoryServices;
using System.DirectoryServices.ActiveDirectory;
namespace NjhLib.Utils
{
    public class ActiveDirectoryHelper
    {
        // Fields
        private string _filterAttribute;
        private string _path;

        // Methods
        public ActiveDirectoryHelper(string path)
        {
            this._path = path;
        }

        public string GetGroups()
        {
            DirectorySearcher searcher = new DirectorySearcher(this._path);
            searcher.Filter = "(cn=" + this._filterAttribute + ")";
            searcher.PropertiesToLoad.Add("memberOf");
            StringBuilder builder = new StringBuilder();
            try
            {
                SearchResult result = searcher.FindOne();
                int count = result.Properties["memberOf"].Count;
                for (int i = 0; i < count; i++)
                {
                    string str = (string)result.Properties["memberOf"][i];
                    int index = str.IndexOf("=", 1);
                    int num3 = str.IndexOf(",", 1);
                    if (-1 == index)
                    {
                        return null;
                    }
                    builder.Append(str.Substring(index + 1, (num3 - index) - 1));
                    builder.Append("|");
                }
            }
            catch (Exception exception)
            {
                throw new Exception("Error obtaining group names. " + exception.Message);
            }
            return builder.ToString();
        }

        public bool IsAuthenticated(string domain, string username, string pwd)
        {
            string str = domain + @"\" + username;
            DirectoryEntry searchRoot = new DirectoryEntry(this._path, str, pwd);
            try
            {
                object nativeObject = searchRoot.NativeObject;
                DirectorySearcher searcher = new DirectorySearcher(searchRoot);
                searcher.Filter = "(SAMAccountName=" + username + ")";
                searcher.PropertiesToLoad.Add("cn");
                SearchResult result = searcher.FindOne();
                if (null == result)
                {
                    return false;
                }
                this._path = result.Path;
                this._filterAttribute = (string)result.Properties["cn"][0];
            }
            catch (Exception exception)
            {
                throw new Exception("Error authenticating user. " + exception.Message);
            }
            return true;
        }
    }
}
