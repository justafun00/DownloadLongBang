using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

namespace Common
{
    public class SQLHelper
    {
        /// <summary>
        /// sql连接字符串
        /// </summary>
        public static string connString = ConfigurationManager.ConnectionStrings["connString"].ToString();
        /// <summary>
        /// 执行超时时间
        /// </summary>
        public static int execTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["Timeout"].ToString());

        /// <summary>
        /// 连接测试
        /// </summary>
        /// <returns></returns>
        public static bool TestConnection()
        {
            SqlConnection conn = new SqlConnection(connString);
            //SqlConnection conn = new SqlConnection { ConnectionString = connString };
            try
            {
                conn.Open();
                return true;
            }
            catch (Exception)
            {
                return false;
                //throw new Exception("数据库连接失败，请检查连接!");
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
        }

        /// <summary>
        /// 返回单个结果
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public static object GetSingleResult(string sql,SqlParameter[] parameters)
        {
            SqlConnection conn = new SqlConnection(connString);
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.CommandTimeout = execTimeout;
            if (parameters != null)
            {
                cmd.Parameters.AddRange(parameters);
            }
            try
            {
                conn.Open();
                object objResult = cmd.ExecuteScalar();
                return objResult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }
        }

        /// <summary>
        /// 执行insert、update、delete操作
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public static int Update(string sql, SqlParameter[] parameters)
        {
            SqlConnection conn = new SqlConnection(connString);
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.CommandTimeout = execTimeout;
            if (parameters != null)
            {
                cmd.Parameters.AddRange(parameters);
            }
            try
            {
                conn.Open();
                int result = cmd.ExecuteNonQuery();
                return result;

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }
        }
        /// <summary>
        /// 带事务执行insert、update、delete操作
        /// </summary>
        /// <param name="sqlList"></param>
        /// <returns></returns>
        public static int UpdateByTran(List<string> sqlList, List<SqlParameter[]> parameters)
        {
            SqlConnection conn = new SqlConnection(connString);
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandTimeout = execTimeout;
            try
            {
                conn.Open();
                cmd.Transaction = conn.BeginTransaction();//开启事务
                int result = 0;
                //foreach (string sql in sqlList)
                //{
                //    cmd.CommandText = sql;
                //    result += cmd.ExecuteNonQuery();
                //}
                for (int i = 0; i < sqlList.Count; i++)
                {
                    cmd.CommandText = sqlList[i];
                    if (parameters !=null && parameters.Count>0 && parameters[i] !=null )
                    {
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddRange(parameters[i]);
                    }
                    result += cmd.ExecuteNonQuery();
                }
                cmd.Transaction.Commit();//提交事务
                return result;
            }
            catch (Exception ex)
            {
                //写入日志...
                if (cmd.Transaction != null)
                    cmd.Transaction.Rollback();//回滚事务
                throw new Exception("调用方法UpdateByTran时出现异常：" + ex.Message);
            }
            finally
            {
                if (cmd.Transaction != null)
                    cmd.Transaction = null;//清除事务
                conn.Close();
            }
        }

        /// <summary>
        /// 返回SqlDataReader 对像
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public static SqlDataReader GetSqlDataReader(string sql, SqlParameter[] parameters)
        {
            SqlConnection conn = new SqlConnection(connString);
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.CommandTimeout = execTimeout;
            if (parameters != null)
            {
                cmd.Parameters.AddRange(parameters);
            }
            try
            {
                conn.Open();
                return cmd.ExecuteReader(CommandBehavior.CloseConnection);
            }
            catch (Exception ex)
            {
                conn.Close();
                throw ex;
            }
        }

        /// <summary>
        /// 返回DataSet对像
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public static DataSet GetDataSet(string sql, SqlParameter[] parameters)
        {
            SqlConnection conn = new SqlConnection(SQLHelper.connString);
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.CommandTimeout = execTimeout;
            if (parameters != null)
            {
                cmd.Parameters.AddRange(parameters);
            }
            DataSet ds = new DataSet();
            try
            {
                conn.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }
        }

        /// <summary>
        /// 返回DataTable对像
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public static DataTable GetDataTable(string sql, SqlParameter[] parameters)
        {
            SqlConnection conn = new SqlConnection(SQLHelper.connString);
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.CommandTimeout = execTimeout;
            if (parameters != null)
            {
                cmd.Parameters.AddRange(parameters);
            }
            DataTable dt = new DataTable();
            try
            {
                conn.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }
        }

        /// <summary>
        /// 执行带参数的存储过程
        /// </summary>
        /// <param name="procName">要执行的存储过程名</param>
        /// <param name="parameters">参数数组</param>
        /// <param name="ds">保存结果</param>
        /// <returns></returns>
        public static bool ExecProcByParams(string procName, SqlParameter[] parameters, DataSet ds)
        {
            SqlConnection conn = new SqlConnection(SQLHelper.connString);
            SqlCommand cmd = new SqlCommand(procName, conn);
            cmd.CommandTimeout = execTimeout;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddRange(parameters);
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                conn.Open();
                da.SelectCommand = cmd;
                int result = da.Fill(ds);

                if (result >= 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }
        }
    }
}
