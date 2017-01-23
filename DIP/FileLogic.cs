using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DIP
{
    class FileLogic
    {
        FormMain caller;
        string fileName;

        public FileLogic(FormMain caller)
        {
            this.caller = caller;
        }

        public void AddFile(string fileName)
        {
            this.fileName = fileName;
            caller.AddFile(fileName);

            //isFile1 = true;
            /*buttonFile1Clear.Enabled = true;
            panel1.Visible = false;*/
        }
        
        public string GetNameAndSize()
        {
            if (File.Exists(fileName))
            {
                string stat = fileName;
                stat += new FileInfo(fileName).Length.ToString();
            }
            return null;
        }
       
    }
}
