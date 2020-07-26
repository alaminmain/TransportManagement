using System.Data;
using System.Data.SqlClient;
using Microsoft.ApplicationBlocks.Data;
using TransportManagerLibrary.UtilityClass;
using System;

namespace TransportManagerLibrary.DAL
{
    public class UserGateway:IDisposable
    {
        public bool validateUser(string UserId, string Password)
        {
            string selectSql = "SELECT     ComCode, UserName, UserPwd, User_Perm, User_Right, Store_Id, Role_Id FROM         UserInfo WHERE     (UserName = @userName) AND (UserPwd = @pass)";
            SqlParameter[] parameter ={
                                              new SqlParameter("@userName", UserId),
                                              new SqlParameter("@pass", Password)
                                          };
            try
            {
                DataSet dataSet = SqlHelper.ExecuteDataset(StringUtility.GetAppConnectionString(), CommandType.Text, selectSql, parameter);

                if (dataSet != null && dataSet.Tables.Count > 0 && dataSet.Tables[0].Rows.Count > 0)
                    return true;
            }
            catch (Exception ex)
            {
                Logger.LogError(ex.ToString(), new object[0]);
                throw;
            }
            return false;
        }

        public DataTable GetUserInfo(int comCode,string UserId)
        {
            string selectSql = "SELECT     ComCode, UserName, User_Perm, User_Right, Store_Id, Role_Id FROM         UserInfo WHERE     (UserName = @userName) AND (ComCode = @ComCode)";
            SqlParameter[] parameter ={
                                              new SqlParameter("@ComCode", comCode),
                                              new SqlParameter("@userName", UserId)
                                          };
            try
            {
                DataSet dataSet = SqlHelper.ExecuteDataset(StringUtility.GetAppConnectionString(), CommandType.Text, selectSql, parameter);

                if (dataSet != null && dataSet.Tables.Count > 0 && dataSet.Tables[0].Rows.Count > 0)
                    return dataSet.Tables[0]; ;
            }
            catch (Exception ex)
            {
                Logger.LogError(ex.ToString(), new object[0]);
                throw;
            }
            return null;
        }

        public DataTable GetUserInfo(int comCode)
        {
            string selectSql = "SELECT     UserInfo.ComCode, UserInfo.UserName, UserInfo.User_Perm, UserInfo.User_Right, UserInfo.Store_Id, UserInfo.Role_Id, UserRole.RoleName, "
                      +" StoreLocation.StoreName FROM         UserInfo INNER JOIN  StoreLocation ON UserInfo.Store_Id = StoreLocation.StoreCode INNER JOIN "
                      +" UserRole ON UserInfo.Role_Id = UserRole.RoleId WHERE     (UserInfo.ComCode = @ComCode)";
            SqlParameter[] parameter ={
                                              new SqlParameter("@ComCode", comCode),
                                              
                                          };
            try
            {
                DataSet dataSet = SqlHelper.ExecuteDataset(StringUtility.GetAppConnectionString(), CommandType.Text, selectSql, parameter);

                if (dataSet != null && dataSet.Tables.Count > 0 && dataSet.Tables[0].Rows.Count > 0)
                    return dataSet.Tables[0]; ;
            }
            catch (Exception ex)
            {
                Logger.LogError(ex.ToString(), new object[0]);
                throw;
            }
            return null;
        }

        public DataTable GetAllRoles(int comCode)
        {
            string selectSql = "SELECT  RoleId, RoleName, Remarks FROM         UserRole where ComCode=@ComCode";
            SqlParameter[] parameter ={
                                              new SqlParameter("@ComCode", comCode)
                                              
                                          };
            try
            {
                DataSet dataSet = SqlHelper.ExecuteDataset(StringUtility.GetAppConnectionString(), CommandType.Text, selectSql, parameter);

                if (dataSet != null && dataSet.Tables.Count > 0 && dataSet.Tables[0].Rows.Count > 0)
                    return dataSet.Tables[0]; ;
            }
            catch (Exception ex)
            {
                Logger.LogError(ex.ToString(), new object[0]);
                throw;
            }
            return null;
        }

        public DataTable GetRolesByRoleId(int ComCode, int RoleId)
        {
            string selectSql = "SELECT    RoleId, RoleName, Remarks FROM         UserRole WHERE ComCode=@ComCode and     (RoleId = @RoleId) ";
            SqlParameter[] parameter ={
                                           new SqlParameter("@ComCode", ComCode),
                                              new SqlParameter("@RoleId", RoleId)
                                              
                                          };
            try
            {
                DataSet dataSet = SqlHelper.ExecuteDataset(StringUtility.GetAppConnectionString(), CommandType.Text, selectSql, parameter);

                if (dataSet != null && dataSet.Tables.Count > 0 && dataSet.Tables[0].Rows.Count > 0)
                    return dataSet.Tables[0]; ;
            }
            catch (Exception ex)
            {
                Logger.LogError(ex.ToString(), new object[0]);
                throw;
            }
            return null;
        }

        public DataTable GetAllMenu(int ComCode)
        {
            string selectSql = "SELECT     menuId, menuName, menuUrl, isActive FROM         UserMenu where ComCode=@ComCode";
            SqlParameter[] parameter ={
                                               new SqlParameter("@ComCode", ComCode)
                                              
                                          };
            try
            {
                DataSet dataSet = SqlHelper.ExecuteDataset(StringUtility.GetAppConnectionString(), CommandType.Text, selectSql, parameter);

                if (dataSet != null && dataSet.Tables.Count > 0 && dataSet.Tables[0].Rows.Count > 0)
                    return dataSet.Tables[0]; ;
            }
            catch (Exception ex)
            {
                Logger.LogError(ex.ToString(), new object[0]);
                throw;
            }
            return null;
        }

        public DataTable GetMenuByMenuId(int ComCode, int MenuId)
        {
            string selectSql = "SELECT     menuId, menuName, menuUrl, isActive FROM         UserMenu where ComCode=@ComCode and menuId=@MenuId ";
            SqlParameter[] parameter ={
                                              new SqlParameter("@ComCode", ComCode),
                                              new SqlParameter("@MenuId", MenuId)
                                              
                                          };
            try
            {
                DataSet dataSet = SqlHelper.ExecuteDataset(StringUtility.GetAppConnectionString(), CommandType.Text, selectSql, parameter);

                if (dataSet != null && dataSet.Tables.Count > 0 && dataSet.Tables[0].Rows.Count > 0)
                    return dataSet.Tables[0]; ;
            }
            catch (Exception ex)
            {
                Logger.LogError(ex.ToString(), new object[0]);
                throw;
            }
            return null;
        }

       
        public DataTable GetRolesMenuAll(int ComCode,int userRolesId)
        {
            string selectSql = "SELECT      RoleMenu.userRoleId, RoleMenu.userMenuId, UserRole.RoleName, UserMenu.menuName, UserMenu.menuUrl"
                            + " FROM         RoleMenu INNER JOIN     UserRole ON RoleMenu.userRoleId = UserRole.RoleId INNER JOIN UserMenu ON RoleMenu.userMenuId = UserMenu.menuId WHERE  RoleMenu.ComCode=@ComCode and    (userRoleId = @userRoleId) ";
            SqlParameter[] parameter ={
                                           new SqlParameter("@ComCode", ComCode),
                                              new SqlParameter("@userRoleId", userRolesId)
                                              
                                          };
            try
            {
                DataSet dataSet = SqlHelper.ExecuteDataset(StringUtility.GetAppConnectionString(), CommandType.Text, selectSql, parameter);

                if (dataSet != null && dataSet.Tables.Count > 0 && dataSet.Tables[0].Rows.Count > 0)
                    return dataSet.Tables[0]; ;
            }
            catch (Exception ex)
            {
                Logger.LogError(ex.ToString(), new object[0]);
                throw;
            }
            return null;
        }

        public DataTable GetRolesMenuAll(int ComCode)
        {
            string selectSql = "SELECT      RoleMenu.userRoleId, RoleMenu.userMenuId, UserRole.RoleName, UserMenu.menuName, UserMenu.menuUrl"
                            + " FROM         RoleMenu INNER JOIN     UserRole ON RoleMenu.userRoleId = UserRole.RoleId INNER JOIN UserMenu ON RoleMenu.userMenuId = UserMenu.menuId WHERE     (userRoleId = @userRoleId) ";
            SqlParameter[] parameter ={
                                              new SqlParameter("@ComCode", ComCode)
                                              
                                          };
            try
            {
                DataSet dataSet = SqlHelper.ExecuteDataset(StringUtility.GetAppConnectionString(), CommandType.Text, selectSql, parameter);

                if (dataSet != null && dataSet.Tables.Count > 0 && dataSet.Tables[0].Rows.Count > 0)
                    return dataSet.Tables[0]; ;
            }
            catch (Exception ex)
            {
                Logger.LogError(ex.ToString(), new object[0]);
                throw;
            }
            return null;
        }


        public string InsertUpdateRoles(int dmlType,int ComCode, int roleId,string roleName, string Remarks)
        {
            using (var conn = new SqlConnection(StringUtility.GetAppConnectionString()))
            {
                //          @DMLType	int,
    //@ComCode int,
    //  @RoleID 	int,
    //  @RoleName 	nvarchar(50),
    //  @Remarks 	nvarchar(50)
                CommonGateway CG = new CommonGateway();
                if (String.IsNullOrEmpty(roleId.ToString()))
                {
                    roleId = CG.GetMaxNo("UserRole", "RoleID") + 1;
                }
                SqlCommand commandObj = new SqlCommand();
                commandObj.CommandType = CommandType.StoredProcedure;
                commandObj.CommandText = "usp_UserRole";
                commandObj.Connection = conn;

                commandObj.Parameters.Add("@DMLType", SqlDbType.SmallInt).Value = dmlType;
                commandObj.Parameters.Add("@ComCode", SqlDbType.SmallInt).Value = ComCode;
                commandObj.Parameters.Add("@RoleID", SqlDbType.Int, 50).Value = roleId;
                commandObj.Parameters.Add("@RoleName", SqlDbType.VarChar, 20).Value = roleName;
                commandObj.Parameters.Add("@Remarks", SqlDbType.VarChar, 20).Value = Remarks;
               


                conn.Open();
                var transaction = conn.BeginTransaction(IsolationLevel.ReadCommitted);
                commandObj.Transaction = transaction;
                try
                {

                    int rowEffected = commandObj.ExecuteNonQuery();
                    transaction.Commit();
                    conn.Close();

                    return roleId.ToString();
                }

                catch (Exception ex)
                {
                    transaction.Rollback();
                    Logger.LogError(ex.ToString());
                    throw new ApplicationException("Cannot Inserted!!!", ex);
                    throw;
                }
                finally
                {
                    conn.Close();
                    transaction.Dispose();
                    conn.Dispose();
                }


            }
        }
        public string InsertUpdateMenu(int dmlType, int ComCode, string MenuId,string MenuName, string MenuUrl, string IsActive)
        {
            using (var conn = new SqlConnection(StringUtility.GetAppConnectionString()))
            {
              
                CommonGateway CG = new CommonGateway();
                if (String.IsNullOrEmpty(MenuId.ToString()))
                {
                    MenuId = Convert.ToString(CG.GetMaxNo("UserMenu", "menuId") + 1);
                }
                SqlCommand commandObj = new SqlCommand();
                commandObj.CommandType = CommandType.StoredProcedure;
                commandObj.CommandText = "usp_UserMenu";
                commandObj.Connection = conn;

                commandObj.Parameters.Add("@DMLType", SqlDbType.SmallInt).Value = dmlType;
                commandObj.Parameters.Add("@ComCode", SqlDbType.SmallInt).Value = ComCode;
                commandObj.Parameters.Add("@MenuId", SqlDbType.Int).Value =MenuId ;
                commandObj.Parameters.Add("@MenuName", SqlDbType.VarChar, 50).Value = MenuName;
                commandObj.Parameters.Add("@MenuUrl", SqlDbType.VarChar, 50).Value = MenuUrl;
                commandObj.Parameters.Add("@IsActive", SqlDbType.VarChar, 50).Value = IsActive;



                conn.Open();
                var transaction = conn.BeginTransaction(IsolationLevel.ReadCommitted);
                commandObj.Transaction = transaction;
                try
                {

                    int rowEffected = commandObj.ExecuteNonQuery();
                    transaction.Commit();
                    conn.Close();

                    return MenuId.ToString();
                }

                catch (Exception ex)
                {
                    transaction.Rollback();
                    Logger.LogError(ex.ToString());
                    throw new ApplicationException("Cannot Inserted!!!", ex);
                    throw;
                }
                finally
                {
                    conn.Close();
                    transaction.Dispose();
                    conn.Dispose();
                }


            }
        }
        public string InsertUpdateRoleMenu(DataTable dt)
        {
            using (var conn = new SqlConnection(StringUtility.GetAppConnectionString()))
            {
      //          @DMLType	int,
      //  @ComCode int,
      //@userRoleId 	int,
      //@MenuId int


                SqlCommand commandDetailObj = new SqlCommand();
                commandDetailObj.CommandType = CommandType.StoredProcedure;
                commandDetailObj.CommandText = "usp_RoleMenu";
                commandDetailObj.Connection = conn;

                SqlCommand comDeleteDetailObj = new SqlCommand();
                comDeleteDetailObj.CommandType = CommandType.Text;
                comDeleteDetailObj.CommandText = "DELETE RoleMenu WHERE userRoleId = @userRoleId  and ComCode=@ComCode";

                comDeleteDetailObj.Connection = conn;



                conn.Open();
                var transaction = conn.BeginTransaction(IsolationLevel.ReadCommitted);
                commandDetailObj.Transaction = transaction;
                comDeleteDetailObj.Transaction = transaction;
                try
                {
                    comDeleteDetailObj.Parameters.Add("@userRoleId", SqlDbType.SmallInt, 20).Value = Convert.ToInt32(dt.Rows[0][0]);
                    comDeleteDetailObj.Parameters.Add("@ComCode", SqlDbType.SmallInt).Value = Convert.ToInt32(dt.Rows[0][2]);

                    comDeleteDetailObj.ExecuteNonQuery();

                    commandDetailObj.Parameters.Add(new SqlParameter("@DMLType", SqlDbType.SmallInt));
                    commandDetailObj.Parameters.Add(new SqlParameter("@ComCode", SqlDbType.Int));
                    commandDetailObj.Parameters.Add(new SqlParameter("@userRoleId", SqlDbType.Int));

                    commandDetailObj.Parameters.Add(new SqlParameter("@MenuId", SqlDbType.Int));
                    


                    foreach (DataRow dr in dt.Rows)
                    {
                        

                        //commandDetailObj.Parameters.Clear();
                        commandDetailObj.Parameters[0].Value = 1;
                        commandDetailObj.Parameters[1].Value = dr["ComCode"];
                        commandDetailObj.Parameters[2].Value = dr["userRoleId"].ToString();
                        commandDetailObj.Parameters[3].Value = dr["MenuId"].ToString();


                        int detailRowEffected = commandDetailObj.ExecuteNonQuery();
                       
                    }

                    int rowEffected = commandDetailObj.ExecuteNonQuery();
                    transaction.Commit();
                    conn.Close();

                    return rowEffected.ToString();
                }

                catch (Exception ex)
                {
                    transaction.Rollback();
                    Logger.LogError(ex.ToString());
                    throw new ApplicationException("Cannot Inserted!!!", ex);
                    throw;
                }
                finally
                {
                    conn.Close();
                    transaction.Dispose();
                    conn.Dispose();
                }


            }
        }

       
        

        public string InsertUpdateUser(int ComCode, string CustId, string UserName, string UserPwd, string User_Perm, int RoleId, int StoreCode)
        {
            using (var conn = new SqlConnection(StringUtility.GetAppConnectionString()))
            {
                //           @ComCode 	[smallint],
                //@CustId int,
                //@UserName [varchar] (50),
                //@UserPwd [varchar](20),
                //@User_Perm [varchar] (10),
                //@Role_Id int,
                //@Store_Id int

                SqlCommand commandObj = new SqlCommand();
                commandObj.CommandType = CommandType.StoredProcedure;
                commandObj.CommandText = "usp_User_Info_Insert";
                commandObj.Connection = conn;
                commandObj.Parameters.Add("@ComCode", SqlDbType.SmallInt).Value = ComCode;
                commandObj.Parameters.Add("@CustId", SqlDbType.VarChar, 50).Value = CustId;
                commandObj.Parameters.Add("@UserName", SqlDbType.VarChar, 20).Value = UserName;
                commandObj.Parameters.Add("@UserPwd", SqlDbType.VarChar, 20).Value = UserPwd;
                commandObj.Parameters.Add("@User_Perm", SqlDbType.VarChar, 10).Value = User_Perm;
                commandObj.Parameters.Add("@Role_Id", SqlDbType.Int).Value = RoleId;
                commandObj.Parameters.Add("@Store_Id", SqlDbType.Int).Value = StoreCode;


                conn.Open();
                var transaction = conn.BeginTransaction(IsolationLevel.ReadCommitted);
                commandObj.Transaction = transaction;
                try
                {

                    int rowEffected = commandObj.ExecuteNonQuery();
                    transaction.Commit();
                    conn.Close();

                    return UserName;
                }

                catch (Exception ex)
                {
                    transaction.Rollback();
                    Logger.LogError(ex.ToString());
                    throw new ApplicationException("Cannot Inserted!!!", ex);
                    throw;
                }
                finally
                {
                    conn.Close();
                    transaction.Dispose();
                    conn.Dispose();
                }


            }
        }

        #region IDisposable Members

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}
