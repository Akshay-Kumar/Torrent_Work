using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace LiveLogViewer
{
    internal class LogWatcher : FileSystemWatcher
    {
        //The name of the file to monitor
        internal string FileName = string.Empty;

        //The FileStream for reading the text from the file
        FileStream Stream;
        //The StreamReader for reading the text from the FileStream
        StreamReader Reader;

        //Constructor for the LogWatcher class
        public LogWatcher(string FileName)
        {
            //Subscribe to the Changed event of the base FileSystemWatcher class
            this.Changed += OnChanged;

            //Set the filename of the file to watch
            this.FileName = FileName;

            //Create the FileStream and StreamReader object for the file
            Stream = new System.IO.FileStream(FileName,FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
            Reader = new System.IO.StreamReader(Stream);
            
            //Set the position of the stream to the end of the file
            Stream.Position =  Stream.Length;
        }

        //Occurs when the file is changed
        public void OnChanged(object o, FileSystemEventArgs e)
        {
            //Read the new text from the file
            string Contents = Reader.ReadToEnd();

            //Fire the TextChanged event
            LogWatcherEventArgs args = new LogWatcherEventArgs(Contents);
            if (TextChanged != null) TextChanged(this, args);
        }

        public delegate void LogWatcherEventHandler(object sender, LogWatcherEventArgs e);

        //Event that is fired when the file is changed
        public event LogWatcherEventHandler TextChanged;
    }

    public class LogWatcherEventArgs : EventArgs
    {
        public string Contents;

        public LogWatcherEventArgs(string Contents)
        {
            this.Contents = Contents;
        }
    }
}
