using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using SourceCodeBL;
using Logger;

namespace SourceCodeDAL
{
    public class SourceDataAccessLayer
    {
        /*Declaration*/
        Action action = null;
        SqlCommand command = null;
        /// <summary>
        /// Initialising connection object
        /// </summary>
        public SourceDataAccessLayer()
        {
            action = new DataAccessAction();
        }
        /// <summary>
        /// GetPanelsByRoleId
        /// </summary>
        /// <returns>DataTable</returns>
        public DataTable GetPanelsByRole(Roles role, out int retVal)
        {
            DataTable dt = null;
            SqlParameter roleName = null;
            SqlParameter returnVal = null;
            retVal = 1;
            try
            {
                roleName = new SqlParameter("@ROLE_NAME", SqlDbType.NChar);
                roleName.Direction = ParameterDirection.Input;
                roleName.Value = role.RoleName;

                returnVal = new SqlParameter("@RETURN_VALUE", SqlDbType.Int);
                returnVal.Direction = ParameterDirection.Output;
                dt = action.ExecuteDataTable(out command, StoredProcedures.GET_PANEL_BY_ROLE, roleName, returnVal);
                retVal = Int32.Parse(returnVal.Value.ToString());
            }
            catch (Exception dalException)
            {
                ErrorLog.ErrorRoutine(dalException);
            }
            return dt;
        }
        /// <summary>
        /// GetAllRoles
        /// </summary>
        /// <returns>DataTable</returns>
        public DataTable GetAllRoles()
        {
            DataTable dt = null;
            try
            {
              dt = action.ExecuteDataTable(out command,StoredProcedures.GET_ALL_ROLES);
            }
            catch (Exception dalException)
            {
                ErrorLog.ErrorRoutine(dalException);
            }
            return dt;
        }
        /// <summary>
        /// GetAllPanels
        /// </summary>
        /// <returns>DataTable</returns>
        public DataTable GetAllPanels()
        {
            DataTable dt = null;
            try
            {
                dt = action.ExecuteDataTable(out command, StoredProcedures.GET_ALL_PANELS);
            }
            catch (Exception dalException)
            {
                ErrorLog.ErrorRoutine(dalException);
            }
            return dt;
        }
        /// <summary>
        /// AuthenticateUser
        /// </summary>
        /// <param name="users"></param>
        /// <returns>DataTable</returns>
        public DataTable AuthenticateUser(Users users,out int retVal)
        {
            retVal = -9;
            SqlParameter userId = null;
            SqlParameter password = null;
            SqlParameter errorCode = null;
            DataTable dt = null;
            try
            {
                userId = new SqlParameter("@USER_ID", SqlDbType.NChar);
                userId.Direction = ParameterDirection.Input;
                userId.Value = users.UserId;

                password = new SqlParameter("@USER_PASSWORD", SqlDbType.NChar);
                password.Direction = ParameterDirection.Input;
                password.Value = users.Password;

                errorCode = new SqlParameter("@ERROR_CODE", SqlDbType.Int);
                errorCode.Direction = ParameterDirection.Output;

                dt = action.ExecuteDataTable(out command, StoredProcedures.AUTHENTICATE_USER, userId, password, errorCode);
                retVal = Int16.Parse(errorCode.Value.ToString());
            }
            catch (Exception dalException)
            {
                ErrorLog.ErrorRoutine(dalException);
            }
            return dt;
        }
        public DataTable ForgotUserPassword(Users users, out bool success)
        {
            Int16 retVal = 1;
            success = false;
            SqlParameter userId = null;
            SqlParameter errorCode = null;
            DataTable dt = null;
            try
            {
                userId = new SqlParameter("@USER_ID", SqlDbType.NChar);
                userId.Direction = ParameterDirection.Input;
                userId.Value = users.UserId;

                errorCode = new SqlParameter("@ERROR_CODE", SqlDbType.Int);
                errorCode.Direction = ParameterDirection.Output;

                dt = action.ExecuteDataTable(out command, StoredProcedures.FORGOT_USER_PASSWORD, userId, errorCode);
                retVal = Int16.Parse(errorCode.Value.ToString());
                if (retVal == 1)
                {
                    success = false;
                }
                else if (retVal == 0)
                {
                    success = true;
                }
            }
            catch (Exception dalException)
            {
                ErrorLog.ErrorRoutine(dalException);
            }
            return dt;
        }
        /// <summary>
        /// ForgotUserPassword
        /// Handles the forgot user password functionality
        /// </summary>
        /// <param name="users"></param>
        /// <returns>DataTable</returns>
        public DataTable ForgotUserPassword(Users users, out int retVal)
        {
            retVal = -9;
            SqlParameter userParameter = null;
            SqlParameter errorCode = null;
            DataTable dt = null;
            try
            {
                userParameter = new SqlParameter("@USER_ID", SqlDbType.NChar);
                userParameter.Direction = ParameterDirection.Input;
                if (!string.IsNullOrEmpty(users.UserId))
                    userParameter.Value = users.UserId;
                else
                    userParameter.Value = users.EmailId;

                errorCode = new SqlParameter("@ERROR_CODE", SqlDbType.Int);
                errorCode.Direction = ParameterDirection.Output;

                dt = action.ExecuteDataTable(out command, StoredProcedures.FORGOT_USER_PASSWORD, userParameter, errorCode);
                retVal = Int16.Parse(errorCode.Value.ToString());
            }
            catch (Exception dalException)
            {
                ErrorLog.ErrorRoutine(dalException);
            }
            return dt;
        }
        /// <summary>
        /// UpdateTabsByRole
        /// </summary>
        /// <param name="users"></param>
        /// <param name="retVal"></param>
        /// <returns>bool</returns>
        public bool UpdateTabsByRole(Roles role, string tabList)
        {
            bool flag = false;
            int returnValue = 0;
            SqlParameter roleName = null;
            SqlParameter tabListParam = null;
            SqlParameter errorCode = null;
            try
            {
                roleName = new SqlParameter("@ROLE_NAME", SqlDbType.NChar);
                roleName.Direction = ParameterDirection.Input;
                roleName.Value = role.RoleName;

                tabListParam = new SqlParameter("@PANEL_NAMES", SqlDbType.VarChar);
                tabListParam.Direction = ParameterDirection.Input;
                tabListParam.Value = tabList;

                errorCode = new SqlParameter("@RETURN_VALUE", SqlDbType.Int);
                errorCode.Direction = ParameterDirection.Output;

                action.ExecuteUpdate(out command, StoredProcedures.UPDATE_ROLES_BY_PANEL, roleName, tabListParam, errorCode);
                returnValue = Int16.Parse(errorCode.Value.ToString());
                if (returnValue == 0)//NO ERROR
                    flag = true;
                else if (returnValue == 1)//SOME ERROR
                    flag = false;
            }
            catch (Exception dalException)
            {
                ErrorLog.ErrorRoutine(dalException);
            }
            return flag;
        }
        /// <summary>
        /// FetchAllUsers
        /// </summary>
        /// <param name="users"></param>
        /// <param name="retVal"></param>
        /// <returns>DataTable</returns>
        public DataTable FetchAllUsers()
        {
            DataTable dt = null;

            try
            {
                dt = action.ExecuteDataTable(out command, StoredProcedures.GET_ALL_USERS);
            }
            catch (Exception dalException)
            {
                ErrorLog.ErrorRoutine(dalException);
            }
            return dt;
        }
        /// <summary>
        /// GetRoleNameByRoleId
        /// </summary>
        /// <returns>string</returns>
        public string GetRoleNameByRoleId(string roleId)
        {
            string roleName = string.Empty;
            SqlParameter roleIdParam = null;
            DataTable dt = null;
            try
            {
                roleIdParam = new SqlParameter("@ROLE_ID", SqlDbType.NChar);
                roleIdParam.Direction = ParameterDirection.Input;
                roleIdParam.Value = roleId;

                dt = action.ExecuteFunction(out command, StoredProcedures.GET_ROLE_NAME_BY_ROLE_ID, roleIdParam);
                roleName = dt.Rows[0][0].ToString().Trim();
            }
            catch (Exception dalException)
            {
                ErrorLog.ErrorRoutine(dalException);
            }
            return roleName;
        }
        /// <summary>
        /// Gets the max role_id
        /// </summary>
        /// <returns></returns>
        public string GetMaxRoleId()
        {
            string roleId = string.Empty;
            DataTable dt = null;
            try
            {
                dt = action.ExecuteFunction(out command, StoredProcedures.MAX_ROLE_ID);
                roleId = dt.Rows[0][0].ToString().Trim();
            }
            catch (Exception dalException)
            {
                ErrorLog.ErrorRoutine(dalException);
            }
            return roleId;
        }
        /// <summary>
        /// Get max program id
        /// </summary>
        /// <returns></returns>
        public string GetMaxProgId()
        {
            string progId = string.Empty;
            DataTable dt = null;
            try
            {
                dt = action.ExecuteFunction(out command, StoredProcedures.MAX_PROG_ID);
                progId = dt.Rows[0][0].ToString().Trim();
            }
            catch (Exception dalException)
            {
                ErrorLog.ErrorRoutine(dalException);
            }
            return progId;
        }
        /// <summary>
        /// Gets max user_id
        /// </summary>
        /// <returns></returns>
        public string GetMaxUserId()
        {
            string userId = string.Empty;
            DataTable dt = null;
            try
            {
                dt = action.ExecuteFunction(out command, StoredProcedures.MAX_USER_ID);
                userId = dt.Rows[0][0].ToString().Trim();
            }
            catch (Exception dalException)
            {
                ErrorLog.ErrorRoutine(dalException);
            }
            return userId;
        }
        /// <summary>
        /// GetRoleIdByRoleName
        /// </summary>
        /// <param name="?"></param>
        /// <returns>string</returns>
        public string GetRoleIdByRoleName(string roleName)
        {
            string roleId = string.Empty;
            SqlParameter roleNameParam = null;
            DataTable dt = null;
            try
            {
                roleNameParam = new SqlParameter("@ROLE_NAME", SqlDbType.NChar);
                roleNameParam.Direction = ParameterDirection.Input;
                roleNameParam.Value = roleName;

                dt = action.ExecuteFunction(out command, StoredProcedures.GET_ROLE_ID_BY_ROLE_NAME, roleNameParam);
                roleId = dt.Rows[0][0].ToString().Trim();
            }
            catch (Exception dalException)
            {
                ErrorLog.ErrorRoutine(dalException);
            }
            return roleId;
        }
        /// <summary>
        /// UpdateUserData
        /// </summary>
        /// <param name="role"></param>
        /// <param name="tabList"></param>
        /// <returns>bool</returns>
        public bool UpdateUserData(DataTable dt,int rowId, out int error_code)
        {
            error_code = 0;
            bool flag = false;
            SqlParameter userId = null;
            SqlParameter roleId = null;
            SqlParameter firstName = null;
            SqlParameter lastName = null;
            SqlParameter userPassword = null;
            SqlParameter userEmailId = null;
            SqlParameter dateOfBirth = null;
            SqlParameter isLocked = null;
            SqlParameter isDeleted = null;
            SqlParameter errorCode = null;
            try
            {
                userId = new SqlParameter("@USER_ID", SqlDbType.NChar);
                userId.Direction = ParameterDirection.Input;
                userId.Value = dt.Rows[rowId]["USER_ID"].ToString();

                roleId = new SqlParameter("@ROLE_ID", SqlDbType.NChar);
                roleId.Direction = ParameterDirection.Input;
                roleId.Value = dt.Rows[rowId]["ROLE_ID"].ToString();

                firstName = new SqlParameter("@FIRST_NAME", SqlDbType.NChar);
                firstName.Direction = ParameterDirection.Input;
                firstName.Value = dt.Rows[rowId]["FIRST_NAME"].ToString();

                lastName = new SqlParameter("@LAST_NAME", SqlDbType.NChar);
                lastName.Direction = ParameterDirection.Input;
                lastName.Value = dt.Rows[rowId]["LAST_NAME"].ToString();

                userPassword = new SqlParameter("@USER_PASSWORD", SqlDbType.NChar);
                userPassword.Direction = ParameterDirection.Input;
                userPassword.Value = dt.Rows[rowId]["USER_PASSWORD"].ToString();

                userEmailId = new SqlParameter("@EMAIL_ID", SqlDbType.NChar);
                userEmailId.Direction = ParameterDirection.Input;
                userEmailId.Value = dt.Rows[rowId]["EMAIL_ID"].ToString();
                
                dateOfBirth = new SqlParameter("@DATE_OF_BIRTH", SqlDbType.NChar);
                dateOfBirth.Direction = ParameterDirection.Input;
                dateOfBirth.Value = dt.Rows[rowId]["DATE_OF_BIRTH"].ToString();

                isLocked = new SqlParameter("@IS_LOCKED", SqlDbType.NChar);
                isLocked.Direction = ParameterDirection.Input;
                isLocked.Value = dt.Rows[rowId]["IS_LOCKED"].ToString();

                isDeleted = new SqlParameter("@IS_DELETED", SqlDbType.NChar);
                isDeleted.Direction = ParameterDirection.Input;
                isDeleted.Value = dt.Rows[rowId]["IS_DELETED"].ToString();


                errorCode = new SqlParameter("@ERROR_CODE", SqlDbType.Int);
                errorCode.Direction = ParameterDirection.Output;

                action.ExecuteUpdate(out command, StoredProcedures.UPDATE_USER_DATA, userId, roleId, firstName, lastName, userPassword, userEmailId, dateOfBirth, isLocked, isDeleted, errorCode);
                error_code = Int32.Parse(errorCode.Value.ToString());
                if (error_code == 0)
                    flag = true;
                else if (error_code == 1)
                    flag = false;
            }
            catch (Exception dalException)
            {
                ErrorLog.ErrorRoutine(dalException);
            }
            return flag;
        }
        /// <summary>
        /// InsertUserData
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="rowId"></param>
        /// <param name="error_code"></param>
        /// <returns>bool</returns>
        public bool InsertUserData(Users user)
        {
            bool flag = false;
            SqlParameter userId = null;
            SqlParameter roleId = null;
            SqlParameter firstName = null;
            SqlParameter lastName = null;
            SqlParameter userPassword = null;
            SqlParameter userEmailId = null;
            SqlParameter dateOfBirth = null;
            SqlParameter isLocked = null;
            SqlParameter isDeleted = null;
            SqlParameter errorCode = null;
            try
            {
                userId = new SqlParameter("@USER_ID", SqlDbType.NChar);
                userId.Direction = ParameterDirection.Input;
                userId.Value = user.UserId;

                roleId = new SqlParameter("@ROLE_ID", SqlDbType.NChar);
                roleId.Direction = ParameterDirection.Input;
                roleId.Value = user.Role.RoleId;

                firstName = new SqlParameter("@FIRST_NAME", SqlDbType.NChar);
                firstName.Direction = ParameterDirection.Input;
                firstName.Value = user.FirstName;

                lastName = new SqlParameter("@LAST_NAME", SqlDbType.NChar);
                lastName.Direction = ParameterDirection.Input;
                lastName.Value = user.LastName;

                userPassword = new SqlParameter("@USER_PASSWORD", SqlDbType.NChar);
                userPassword.Direction = ParameterDirection.Input;
                userPassword.Value = user.Password;

                userEmailId = new SqlParameter("@EMAIL_ID", SqlDbType.NChar);
                userEmailId.Direction = ParameterDirection.Input;
                userEmailId.Value = user.EmailId;

                dateOfBirth = new SqlParameter("@DATE_OF_BIRTH", SqlDbType.NChar);
                dateOfBirth.Direction = ParameterDirection.Input;
                dateOfBirth.Value = user.DateOfBirth;

                isLocked = new SqlParameter("@IS_LOCKED", SqlDbType.NChar);
                isLocked.Direction = ParameterDirection.Input;
                isLocked.Value = user.IsLocked;

                isDeleted = new SqlParameter("@IS_DELETED", SqlDbType.NChar);
                isDeleted.Direction = ParameterDirection.Input;
                isDeleted.Value = user.IsDeleted;

                errorCode = new SqlParameter("@ERROR_CODE", SqlDbType.Int);
                errorCode.Direction = ParameterDirection.Output;

                action.ExecuteUpdate(out command, StoredProcedures.INSERT_USERDATA, userId, roleId, firstName, lastName, userPassword, userEmailId, dateOfBirth, isLocked, isDeleted, errorCode);
                int result = Int32.Parse(errorCode.Value.ToString());
                if (result == 0)
                    flag = true;
                else if (result == 1)
                    flag = false;
            }
            catch (Exception dalException)
            {
                ErrorLog.ErrorRoutine(dalException);
            }
            return flag;
        }
        /// <summary>
        /// InsertOrUpdateRoles
        /// </summary>
        /// <param name="role"></param>
        /// <returns>bool</returns>
        public bool InsertOrUpdateRoles(Roles role)
        {
            bool flag = false;
            SqlParameter roleId = null;
            SqlParameter roleName = null;
            SqlParameter roleDescription = null;
            SqlParameter panelList = null;
            SqlParameter deleted = null;
            SqlParameter errorCode = null;
            try
            {
                roleId = new SqlParameter("@ROLE_ID", SqlDbType.NChar);
                roleId.Direction = ParameterDirection.Input;
                roleId.Value = role.RoleId;

                roleName = new SqlParameter("@ROLE_NAME", SqlDbType.NChar);
                roleName.Direction = ParameterDirection.Input;
                roleName.Value = role.RoleName;

                roleDescription = new SqlParameter("@ROLE_DESC", SqlDbType.VarChar);
                roleDescription.Direction = ParameterDirection.Input;
                roleDescription.Value = role.RoleDescription;

                panelList = new SqlParameter("@PANEL_LIST", SqlDbType.VarChar);
                panelList.Direction = ParameterDirection.Input;
                panelList.Value = role.AccessibleTabList;

                deleted = new SqlParameter("@DELETED", SqlDbType.Int);
                deleted.Direction = ParameterDirection.Input;
                deleted.Value = role.Deleted;

                errorCode = new SqlParameter("@ERROR_CODE", SqlDbType.Int);
                errorCode.Direction = ParameterDirection.Output;


                action.ExecuteUpdate(out command, StoredProcedures.INSERT_OR_UPDATE_ROLES, roleId, roleName, roleDescription, panelList, deleted, errorCode);
                int result = Int32.Parse(errorCode.Value.ToString());
                if (result == 0)
                    flag = true;
                else if (result == 1)
                    flag = false;
            }
            catch (Exception dalException)
            {
                ErrorLog.ErrorRoutine(dalException);
            }
            return flag;
        }
        /// <summary>
        /// DeleteRoleByRoleId
        /// </summary>
        /// <param name="role"></param>
        /// <param name="message"></param>
        /// <param name="user"></param>
        /// <returns>bool</returns>
        public bool DeleteRoleByRoleId(Roles role,out string message,out string user)
        {
            message = string.Empty;
            user = string.Empty;
            bool flag = false;
            SqlParameter roleId = null;
            SqlParameter userId = null;
            SqlParameter errorCode = null;
            try
            {
                roleId = new SqlParameter("@ROLE_ID", SqlDbType.NChar);
                roleId.Direction = ParameterDirection.Input;
                roleId.Value = role.RoleId;

                userId = new SqlParameter("@USER_ID", SqlDbType.NChar, 30);
                userId.Direction = ParameterDirection.Output;

                errorCode = new SqlParameter("@ERROR_CODE", SqlDbType.Int);
                errorCode.Direction = ParameterDirection.Output;

                action.ExecuteUpdate(out command, StoredProcedures.DELETE_ROLE_BY_ROLE_ID, roleId, userId, errorCode);
                int result = Int32.Parse(errorCode.Value.ToString());
                if (result == 0)
                {
                    flag = true;
                    message = MESSAGE_LABEL.SUCCESS_MESSSAGE;
                }
                else if (result == 1)
                {
                    flag = false;
                    message = MESSAGE_LABEL.ERROR_MESSAGE;
                    user = Convert.ToString(userId.Value.ToString().Trim());
                }
                else if (result == -1)
                {
                    flag = false;
                    message = MESSAGE_LABEL.EXCEPTION_MESSAGE;
                }
            }
            catch (Exception dalException)
            {
                ErrorLog.ErrorRoutine(dalException);
            }
            return flag;
        }
        public DataTable FetchAllLanguageCode()
        {
            DataTable dt = null;
            try
            {
                dt = action.ExecuteDataTable(out command, StoredProcedures.GET_ALL_LANGUAGE_CODE);
            }
            catch (Exception dalException)
            {
                ErrorLog.ErrorRoutine(dalException);
            }
            return dt;
        }
        public DataTable FetchAllPrograms()
        {
            DataTable dt = null;
            try
            {
                dt = action.ExecuteDataTable(out command, StoredProcedures.GET_ALL_PROGRAM_NAMES);
            }
            catch (Exception dalException)
            {
                ErrorLog.ErrorRoutine(dalException);
            }
            return dt;
        }
        public DataTable FetchAllDeletedPrograms()
        {
            DataTable dt = null;
            try
            {
                dt = action.ExecuteDataTable(out command, StoredProcedures.GET_ALL_DELETED_PROGRAM_NAMES);
            }
            catch (Exception dalException)
            {
                ErrorLog.ErrorRoutine(dalException);
            }
            return dt;
        }
        public string GetLangNameByLangId(string langId)
        {
            string langName = string.Empty;
            SqlParameter langIdParam = null;
            DataTable dt = null;
            try
            {
                langIdParam = new SqlParameter("@LANG_ID", SqlDbType.NChar);
                langIdParam.Direction = ParameterDirection.Input;
                langIdParam.Value = langId;

                dt = action.ExecuteFunction(out command, StoredProcedures.GET_LANG_NAME_BY_LANG_ID, langIdParam);
                langName = dt.Rows[0][0].ToString().Trim();
            }
            catch (Exception dalException)
            {
                ErrorLog.ErrorRoutine(dalException);
            }
            return langName;
        }

        public DataTable GetProgramCodeByProgramId(string programId)
        {
            SqlParameter progId = null;
            DataTable dt = null;
            try
            {
                progId = new SqlParameter("@PROG_ID", SqlDbType.NChar);
                progId.Direction = ParameterDirection.Input;
                progId.Value = programId;
                dt = action.ExecuteDataTable(out command, StoredProcedures.GET_PROGRAM_CODE_BY_PROGRAM_ID, progId);
            }
            catch (Exception dalException)
            {
                ErrorLog.ErrorRoutine(dalException);
            }
            return dt;
        }
        public bool DeleteProgramCode(ProgramCode code)
        {
            bool flag = false;
            SqlParameter programId = null;
            SqlParameter errorCode = null;
            try
            {
                programId = new SqlParameter("@PROGRAM_ID", SqlDbType.NChar);
                programId.Direction = ParameterDirection.Input;
                programId.Value = code.ProgramId;

                errorCode = new SqlParameter("@ERROR_CODE", SqlDbType.Int);
                errorCode.Direction = ParameterDirection.Output;

                action.ExecuteUpdate(out command, StoredProcedures.DELETE_PROGRAM_CODE, programId, errorCode);
                int result = Int32.Parse(errorCode.Value.ToString());
                if (result == 0)
                    flag = true;
                else if (result == 1)
                    flag = false;
            }
            catch (Exception dalException)
            {
                ErrorLog.ErrorRoutine(dalException);
            }
            return flag;
        }
        public bool UndoDeleteProgramCode(ProgramCode code)
        {
            bool flag = false;
            SqlParameter programId = null;
            SqlParameter errorCode = null;
            try
            {
                programId = new SqlParameter("@PROGRAM_ID", SqlDbType.NChar);
                programId.Direction = ParameterDirection.Input;
                programId.Value = code.ProgramId;

                errorCode = new SqlParameter("@ERROR_CODE", SqlDbType.Int);
                errorCode.Direction = ParameterDirection.Output;

                action.ExecuteUpdate(out command, StoredProcedures.UNDELETE_PROGRAM_CODE, programId, errorCode);
                int result = Int32.Parse(errorCode.Value.ToString());
                if (result == 0)
                    flag = true;
                else if (result == 1)
                    flag = false;
            }
            catch (Exception dalException)
            {
                ErrorLog.ErrorRoutine(dalException);
            }
            return flag;
        }
        
        public bool InsertOrUpdateRolePermissions(RolePermissions[] panelDataCollection)
        {
            bool flag = true;
            try
            {

                Dictionary<string, SqlDbType> dbParams = new Dictionary<string, SqlDbType>
                {
                    {"@ROLE_ID",SqlDbType.NChar},
                    {"@PANEL_NAME",SqlDbType.NChar},
                    {"@PANEL_CONTROL_ID",SqlDbType.VarChar},
                    {"@INVISIBLE",SqlDbType.Int},
                    {"@DISABLED",SqlDbType.Int},
                    {"@ACCESS",SqlDbType.Int},
                    {"@ERROR_CODE",SqlDbType.Int}
                };

                SqlParameter[] paramCollection = new SqlParameter[dbParams.Count];
                SqlParameter param;
                int index;
                foreach (RolePermissions panelData in panelDataCollection)
                {
                    index = 0;
                    foreach (var pair in dbParams)
                    {
                        param = new SqlParameter(pair.Key, pair.Value);
                        switch (pair.Key)
                        {
                            case "@ROLE_ID":
                                param.Direction = ParameterDirection.Input;
                                param.Value = panelData.Role.RoleId.Trim();
                                break;
                            case "@PANEL_NAME":
                                param.Direction = ParameterDirection.Input;
                                param.Value = panelData.PanelName.Trim();
                                break;
                            case "@PANEL_CONTROL_ID":
                                param.Direction = ParameterDirection.Input;
                                param.Value = panelData.PanelControlId.Trim();
                                break;
                            case "@INVISIBLE":
                                param.Direction = ParameterDirection.Input;
                                param.Value = panelData.Invisible;
                                break;
                            case "@DISABLED":
                                param.Direction = ParameterDirection.Input;
                                param.Value = panelData.Disabled;
                                break;
                            case "@ACCESS":
                                param.Direction = ParameterDirection.Input;
                                param.Value = panelData.Access;
                                break;
                            default: param.Direction = ParameterDirection.Output;
                                break;
                        }
                        paramCollection[index++] = param;
                    }
                    action.ExecuteUpdate(out command, StoredProcedures.INSERT_OR_UPDATE_ROLE_PERMISSIONS, paramCollection);
                    int result = Int32.Parse(command.Parameters["@ERROR_CODE"].Value.ToString().Trim());
                    if (result == 1)
                        flag = false;
                }
            }
            catch (Exception dalException)
            {
                ErrorLog.ErrorRoutine(dalException);
                flag = false;
            }
            return flag;
        }
        
        public bool InsertOrUpdatePanels(RolePermissions[] panelDataCollection)
        {
            bool flag = true;
            try
            {

                Dictionary<string, SqlDbType> dbParams = new Dictionary<string, SqlDbType>
                {
                    {"@PANEL_NAME",SqlDbType.NChar},
                    {"@PANEL_CONTROL_ID",SqlDbType.VarChar},
                    {"@ERROR_CODE",SqlDbType.Int}
                };

                SqlParameter[] paramCollection = new SqlParameter[dbParams.Count];
                SqlParameter param;
                int index;
                foreach (RolePermissions panelData in panelDataCollection)
                {
                    index = 0;
                    foreach (var pair in dbParams)
                    {
                        param = new SqlParameter(pair.Key, pair.Value);
                        switch (pair.Key)
                        {
                            case "@PANEL_NAME":
                                param.Direction = ParameterDirection.Input;
                                param.Value = panelData.PanelName.Trim();
                                break;
                            case "@PANEL_CONTROL_ID":
                                param.Direction = ParameterDirection.Input;
                                param.Value = panelData.PanelControlId.Trim();
                                break;
                            default: param.Direction = ParameterDirection.Output;
                                break;
                        }
                        paramCollection[index++] = param;
                    }
                    action.ExecuteUpdate(out command, StoredProcedures.INSERT_OR_UPDATE_PANELS, paramCollection);
                    int result = Int32.Parse(command.Parameters["@ERROR_CODE"].Value.ToString().Trim());
                    if (result == 1)
                        flag = false;
                }
            }
            catch (Exception dalException)
            {
                ErrorLog.ErrorRoutine(dalException);
                flag = false;
            }
            return flag;
        }

        public bool InsertOrUpdateProgramCode(ProgramCode code,Users user)
        {
            bool flag = false;
            SqlParameter programId = null;
            SqlParameter progName = null;
            SqlParameter progCode = null;
            SqlParameter langId = null;
            SqlParameter userId = null;
            SqlParameter lastModifiedDate = null;
            SqlParameter createdDate = null;
            SqlParameter filePath = null;
            SqlParameter errorCode = null;
            try
            {
                programId = new SqlParameter("@PROGRAM_ID", SqlDbType.NChar);
                programId.Direction = ParameterDirection.Input;
                programId.Value = code.ProgramId;

                progName = new SqlParameter("@PROGRAM_NAME", SqlDbType.NChar);
                progName.Direction = ParameterDirection.Input;
                progName.Value = code.ProgName;

                progCode = new SqlParameter("@PROGRAM_CODE", SqlDbType.VarChar);
                progCode.Direction = ParameterDirection.Input;
                progCode.Value = code.ProgCode;

                langId = new SqlParameter("@LANG_ID", SqlDbType.NChar);
                langId.Direction = ParameterDirection.Input;
                langId.Value = code.LangId;

                userId = new SqlParameter("@USER_ID", SqlDbType.VarChar);
                userId.Direction = ParameterDirection.Input;
                userId.Value = user.UserId;


                lastModifiedDate = new SqlParameter("@LAST_MODIFIED_DATE", SqlDbType.VarChar);
                lastModifiedDate.Direction = ParameterDirection.Input;
                lastModifiedDate.Value = code.LastModified;


                createdDate = new SqlParameter("@CREATED_DATE", SqlDbType.VarChar);
                createdDate.Direction = ParameterDirection.Input;
                createdDate.Value = code.CreatedDate;

                filePath = new SqlParameter("@FILE_PATH", SqlDbType.VarChar);
                filePath.Direction = ParameterDirection.Input;
                filePath.Value = code.Filepath;

                errorCode = new SqlParameter("@ERROR_CODE", SqlDbType.Int);
                errorCode.Direction = ParameterDirection.Output;

                action.ExecuteUpdate(out command, StoredProcedures.INSERT_OR_UPDATE_PROGRAM_CODE,programId,progName,progCode,langId,userId,lastModifiedDate,createdDate,filePath,errorCode);
                int result = Int32.Parse(errorCode.Value.ToString());
                if (result == 0)
                    flag = true;
                else if (result == 1)
                    flag = false;
            }
            catch (Exception dalException)
            {
                ErrorLog.ErrorRoutine(dalException);
            }
            return flag;
        }
    }
}
