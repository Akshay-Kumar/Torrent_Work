using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace ut_amc_automation
{
    public class ErrorLoggerListener
    {
        #region Private Class Data
        private Error_Logger Error_Logger;
        #endregion

        #region Constructor
        public ErrorLoggerListener(string File_Path)
        {
            Error_Logger = new Error_Logger(File_Path);
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Logs an error message to the log file
        /// </summary>
        /// <param name="Error_Message">The error message to write to the log file.</param>
        public void Log_Error(string Error_Message)
        {
            Error_Logger.Log_Error(Error_Message);
        }

        /// <summary>
        /// Clears out the log file
        /// </summary>
        public void Clear_Log_File()
        {
            Error_Logger.Clear_Log_File();
        }

        /// <summary>
        /// Gets the contents of the Log File
        /// </summary>
        /// <returns>String value</returns>
        public string Get_Log_File()
        {
            return Error_Logger.Get_Log_File();
        }
        #endregion
    }

    /// <summary>
    /// This class handles writing error messages to the external error log file.
    /// </summary>
	[Serializable]
	public sealed class Error_Logger
    {
        #region Private Class Data
        private string m_Log_File_Path;
        #endregion

        #region Constructor
		/// <summary>
		/// Creates a new instance of an Error_Logger object
		/// </summary>
		/// <param name="Log_File_Path">The path to the Error Log File</param>
		public Error_Logger(string Log_File_Path)
        {
            m_Log_File_Path = Log_File_Path;
        }
        #endregion

        #region Getters/Setters
        /// <summary>
        /// Gets or sets the Error Log File path value.
        /// </summary>
        public string Log_File_Path
        {
            get { return m_Log_File_Path; }
            set { m_Log_File_Path = value; }
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Gets the contents of the Log File
        /// </summary>
        /// <returns>String value</returns>
        public string Get_Log_File()
        {
            StreamReader file = null;
            string ret = null;

            try
            {
                file = new StreamReader(Log_File_Path);
                ret = file.ReadToEnd();
            }
            catch (Exception)
            { }
            finally
            {
                if (file != null)
                    file.Close();
            }
            return ret;
        }

        /// <summary>
        /// Clears out the log file
        /// </summary>
        public void Clear_Log_File()
        {
            StreamWriter file = null;

            try
            {
                file = new StreamWriter(Log_File_Path, false);
                file.WriteLine("");
            }
            catch (Exception)
            { }
            finally
            {
                if (file != null)
                    file.Close();
            }
        }

        /// <summary>
        /// Write the error to the error log file.
        /// </summary>
        /// <param name="Error">The error message to write to the log file.</param>
        public void Log_Error(string Error)
        {
            StreamWriter file = null;

            try
            {
                file = new StreamWriter(Log_File_Path, true);
                file.WriteLine(Error);
            }
            catch (Exception)
            { }
            finally
            {
                if (file != null)
                    file.Close();
            }
        }
        #endregion
    }
}
