using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Oracle.ManagedDataAccess.Client;
using System.Configuration;

namespace Common
{
    /// <summary>
    /// 操作Oracle数据库
    /// </summary>
    public class OracleHelper
    {
        public static string connString = ConfigurationManager.ConnectionStrings["oracleConnString"].ToString();
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
            OracleConnection conn = new OracleConnection(connString);
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
        /// 返回单一结果
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public static object GetSingleResult(string sql)
        {
            OracleConnection conn = new OracleConnection(connString);
            OracleCommand cmd = new OracleCommand(sql, conn);
            try
            {
                conn.Open();
                return cmd.ExecuteScalar();
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

        public static object GetSingleResult(string sql, OracleParameter[] parameters)
        {
            OracleConnection conn = new OracleConnection(connString);
            OracleCommand cmd = new OracleCommand(sql, conn);
            if (parameters != null)
            {
                cmd.Parameters.AddRange(parameters);
            }
            try
            {
                conn.Open();
                return cmd.ExecuteScalar();
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
        public static int Update(string sql)
        {
            OracleConnection conn = new OracleConnection(connString);
            OracleCommand cmd = new OracleCommand(sql, conn);
            try
            {
                conn.Open();
                return cmd.ExecuteNonQuery();
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
        public static int Update(string sql, OracleParameter[] parameters)
        {
            OracleConnection conn = new OracleConnection(connString);
            OracleCommand cmd = new OracleCommand(sql, conn);
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
        /// 返回DataReader对像
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public static OracleDataReader GetDataReader(string sql)
        {
            OracleConnection conn = new OracleConnection(connString);
            OracleCommand cmd = new OracleCommand(sql, conn);
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
        /// 返回SqlDataReader 对像
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public static OracleDataReader GetSqlDataReader(string sql, OracleParameter[] parameters)
        {
            OracleConnection conn = new OracleConnection(connString);
            OracleCommand cmd = new OracleCommand(sql, conn);
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
        public static DataSet GetDataSet(string sql)
        {
            OracleConnection conn = new OracleConnection(connString);
            OracleCommand cmd = new OracleCommand(sql, conn);
            DataSet ds = new DataSet();
            try
            {
                conn.Open();
                OracleDataAdapter da = new OracleDataAdapter(cmd);
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
        public static DataTable GetDataTable(string sql)
        {
            OracleConnection conn = new OracleConnection(connString);
            OracleCommand cmd = new OracleCommand(sql, conn);
            DataTable dt = new DataTable();

            try
            {
                conn.Open();
                OracleDataAdapter da = new OracleDataAdapter(cmd);
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
        /// 返回DataTable对像
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public static DataTable GetDataTable(string sql, OracleParameter[] parameters)
        {
            OracleConnection conn = new OracleConnection(connString);
            OracleCommand cmd = new OracleCommand(sql, conn);
            cmd.CommandTimeout = execTimeout;
            if (parameters != null)
            {
                cmd.Parameters.AddRange(parameters);
            }
            DataTable dt = new DataTable();
            try
            {
                conn.Open();
                OracleDataAdapter da = new OracleDataAdapter(cmd);
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


    }
}
