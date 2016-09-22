using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SourceCodeDAL
{
    public class StoredProcedures
    {
        public static string GET_PANEL_BY_ROLE = "[dbo].[GET_PANEL_BY_ROLE]";
        public static string GET_ALL_ROLES = "[dbo].[GET_ALL_ROLES]";
        public static string GET_ALL_PANELS = "[dbo].[GET_ALL_PANELS]";
        public static string AUTHENTICATE_USER = "[dbo].[AUTHENTICATE_USER]";
        public static string UPDATE_ROLES_BY_PANEL = "[dbo].[UPDATE_ROLES_BY_PANEL]";
        public static string GET_ALL_USERS = "[dbo].[GET_ALL_USERS]";
        public static string UPDATE_USER_DATA = "[dbo].[UPDATE_USER_DATA]";
        public static string GET_ROLE_NAME_BY_ROLE_ID = "[dbo].[GET_ROLE_NAME_BY_ROLE_ID](@ROLE_ID)";
        public static string GET_ROLE_ID_BY_ROLE_NAME = "[dbo].[GET_ROLE_ID_BY_ROLE_NAME](@ROLE_NAME)";
        public static string MAX_USER_ID = "[dbo].[MAX_USER_ID]()";
        public static string MAX_ROLE_ID = "[dbo].[MAX_ROLE_ID]()";
        public static string MAX_PROG_ID = "[dbo].[MAX_PROG_ID]()";
        public static string INSERT_USERDATA = "[dbo].[INSERT_USERDATA]";
        public static string INSERT_OR_UPDATE_ROLES = "[dbo].[INSERT_OR_UPDATE_ROLES]";
        public static string DELETE_ROLE_BY_ROLE_ID = "[dbo].[DELETE_ROLE_BY_ROLE_ID]";
        public static string GET_ALL_LANGUAGE_CODE = "[dbo].[GET_ALL_LANGUAGE_CODE]";
        public static string GET_ALL_PROGRAM_NAMES = "[dbo].[GET_ALL_PROGRAM_NAMES]";
        public static string GET_LANG_NAME_BY_LANG_ID = "[dbo].[GET_LANG_NAME_BY_LANG_ID](@LANG_ID)";
        public static string GET_PROGRAM_CODE_BY_PROGRAM_ID = "[dbo].[GET_PROGRAM_CODE_BY_PROGRAM_ID]";
        public static string INSERT_OR_UPDATE_PROGRAM_CODE = "[dbo].[INSERT_OR_UPDATE_PROGRAM_CODE]";
        public static string DELETE_PROGRAM_CODE = "[dbo].[DELETE_PROGRAM_CODE]";
        public static string GET_ALL_DELETED_PROGRAM_NAMES = "[dbo].[GET_ALL_DELETED_PROGRAM_NAMES]";
        public static string UNDELETE_PROGRAM_CODE = "[dbo].[UNDELETE_PROGRAM_CODE]";
        public static string FORGOT_USER_PASSWORD = "[dbo].[FORGOT_USER_PASSWORD]";
        public static string INSERT_OR_UPDATE_ROLE_PERMISSIONS = "[dbo].[INSERT_OR_UPDATE_ROLE_PERMISSIONS]";
        public static string INSERT_OR_UPDATE_PANELS = "[dbo].[INSERT_OR_UPDATE_PANELS]";
    }
}