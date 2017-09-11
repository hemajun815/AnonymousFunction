using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.SqlClient;

namespace Helper
{
    /// <summary>
    /// 数据库访问类
    /// 默认连接名DbDefaultConnStr
    /// 2015-10-24 10:17 hemajun
    /// </summary>
    public class HelperDB
    {
        //数据库连接字符串(web.config来配置)，可以动态更改connectionString支持多数据库.
        public static string connString
        {
            get 
            {
                return System.Configuration.ConfigurationManager.ConnectionStrings["DbDefaultConnStr"].ToString();
            }
        }

        #region 公用方法

        /// <summary>
        /// 获取某列的最大值（列类型必须为数字型）
        /// </summary>
        /// <param name="FieldName">表中列名</param>
        /// <param name="TableName">表名</param>
        /// <returns>该列最大值</returns>
        public static int GetMaxID(string FieldName, string TableName)
        {
            string strsql = String.Format("SELECT MAX({0})+1 FROM {1}", FieldName, TableName);
            object obj = HelperDB.GetSingleValue(strsql);
            if (obj == null)
                return -1;
            return int.Parse(obj.ToString());
        }
        public static bool Exists(string strSql, params SqlParameter[] cmdParms)
        {
            object obj = HelperDB.GetSingleValue(strSql, cmdParms);
            if (Object.Equals(obj, null))
                return false;
            return true;
        }

        #endregion

        #region  执行简单SQL语句
        /// <summary>
        /// 执行SQL语句(insert or update)，返回影响的记录数
        /// </summary>
        /// <param name="strSql">SQL语句</param>
        /// <returns>影响的记录数</returns>
        public static int InsertOrUpdate(string strSql)
        {
            return InsertOrUpdate(connString, strSql);
        }
        /// <summary>
        /// 执行SQL语句(insert or update)，返回影响的记录数
        /// </summary>
        /// <param name="strSql">SQL语句</param>
        /// <param name="connString">数据库连接字符串</param>
        /// <returns>影响的记录数</returns>
        public static int InsertOrUpdate(string connString, string strSql)
        {
            using (SqlConnection conn = new SqlConnection(connString))
            {
                SqlCommand cmd = new SqlCommand(strSql, conn);
                try
                {
                    conn.Open();
                    return cmd.ExecuteNonQuery();
                }
                catch
                {
                    return -1;
                }
                finally
                {
                    cmd.Dispose();
                    conn.Close();
                }
            }
        }

        /// <summary>
        /// 执行多条SQL语句(insert or update)，实现数据库事务。
        /// </summary>
        /// <param name="lStrSql">多条SQL语句</param>
        /// <returns>影响的记录数</returns>
        public static int InsertOrUpdateForTran(List<String> lStrSql)
        {
            return InsertOrUpdateForTran(connString, lStrSql);
        }
        /// <summary>
        /// 执行多条SQL语句(insert or update)，实现数据库事务。
        /// </summary>
        /// <param name="connString">数据库连接字符串</param>
        /// <param name="lStrSql">多条SQL语句</param>
        /// <returns>影响的记录数</returns>
        public static int InsertOrUpdateForTran(string connString, List<String> lStrSql)
        {
            SqlConnection conn = new SqlConnection(connString);
            SqlCommand cmd = new SqlCommand();
            SqlTransaction trans = null;
            try
            {
                conn.Open();
                trans = conn.BeginTransaction();
                cmd.Connection = conn;
                cmd.Transaction = trans;

                int count = 0;
                for (int n = 0; n < lStrSql.Count; n++)
                {
                    string strsql = lStrSql[n];
                    if (strsql.Trim().Length > 1)
                    {
                        cmd.CommandText = strsql;
                        count += cmd.ExecuteNonQuery();
                    }
                }
                trans.Commit();
                return count;
            }
            catch
            {
                trans.Rollback();
                return -1;
            }
            finally
            {
                cmd.Dispose();
                trans.Dispose();
                conn.Close();
            }
        }

        /// <summary>
        /// 执行单条SQL语句(insert or update)，实现数据库事务。
        /// </summary>
        /// <param name="lStrSql">多条SQL语句</param>
        /// <returns>影响的记录数</returns>
        public static int InsertOrUpdateForTran(string strSql)
        {
            return InsertOrUpdateForTran(connString, strSql);
        }
        /// <summary>
        /// 执行多条SQL语句(insert or update)，实现数据库事务。
        /// </summary>
        /// <param name="connString">数据库连接字符串</param>
        /// <param name="lStrSql">多条SQL语句</param>
        /// <returns>影响的记录数</returns>
        public static int InsertOrUpdateForTran(string connString, string strSql)
        {
            SqlConnection conn = new SqlConnection(connString);
            SqlCommand cmd = new SqlCommand();
            SqlTransaction trans = null;
            try
            {
                conn.Open();
                trans = conn.BeginTransaction();
                cmd.Connection = conn;
                cmd.Transaction = trans;
                cmd.CommandText = strSql;

                int count = cmd.ExecuteNonQuery();

                trans.Commit();
                return count;
            }
            catch
            {
                trans.Rollback();
                return -1;
            }
            finally
            {
                cmd.Dispose();
                trans.Dispose();
                conn.Close();
            }
        }

        /// <summary>
        /// 执行一条计算查询结果语句，返回一个值（string）
        /// </summary>
        /// <param name="strSql">计算查询结果语句</param>
        /// <returns>查询结果（string）</returns>
        public static string GetSingleValue(string strSql)
        {
            return GetSingleValue(connString, strSql);
        }
        /// <summary>
        /// 执行一条计算查询结果语句，返回一个值（string）
        /// </summary>
        /// <param name="strSql">计算查询结果语句</param>
        /// <param name="connString">数据库连接字符串</param>
        /// <returns>查询结果（string）</returns>
        public static string GetSingleValue(string connString, string strSql)
        {
            using (SqlConnection conn = new SqlConnection(connString))
            {
                SqlCommand cmd = new SqlCommand(strSql, conn);
                try
                {
                    conn.Open();
                    object obj = cmd.ExecuteScalar();
                    if ((Object.Equals(obj, null)) || (Object.Equals(obj, System.DBNull.Value)))
                        return string.Empty;
                    else
                        return obj.ToString();
                }
                catch
                {
                    return string.Empty;
                }
                finally
                {
                    cmd.Dispose();
                    conn.Close();
                }
            }
        }

        /// <summary>
        /// 执行查询语句，返回SqlDataReader ( 注意：调用该方法后，一定要对SqlDataReader进行Close )
        /// </summary>
        /// <param name="strSql">查询语句</param>
        /// <returns>SqlDataReader</returns>
        public static SqlDataReader GetDataReader(string strSql)
        {
            return GetDataReader(connString, strSql);
        }
        /// <summary>
        /// 执行查询语句，返回SqlDataReader ( 注意：调用该方法后，一定要对SqlDataReader进行Close )
        /// </summary>
        /// <param name="strSql">查询语句</param>
        /// <param name="connString">数据库连接字符串</param>
        /// <returns>SqlDataReader</returns>
        public static SqlDataReader GetDataReader(string connString, string strSql)
        {
            using (SqlConnection conn = new SqlConnection(connString))
            {
                SqlCommand cmd = new SqlCommand(strSql, conn);
                try
                {
                    conn.Open();
                    return cmd.ExecuteReader(CommandBehavior.CloseConnection);
                }
                catch
                {
                    return null;
                }
                finally
                {
                    cmd.Dispose();
                    conn.Close();
                }
            }
        }

        /// <summary>
        /// 执行查询语句，返回DataTable
        /// </summary>
        /// <param name="strSql">查询语句</param>
        /// <returns>DataTable</returns>
        public static DataTable GetDataTable(string strSql)
        {
            return GetDataTable(connString, strSql);
        }
        /// <summary>
        /// 执行查询语句，返回DataTable
        /// </summary>
        /// <param name="strSql">查询语句</param>
        /// <param name="connString">数据库连接字符串</param>
        /// <returns>DataTable</returns>
        public static DataTable GetDataTable(string connString, string strSql)
        {
            using (SqlConnection conn = new SqlConnection(connString))
            {
                DataTable dt = new DataTable();
                SqlDataAdapter adapter = null;
                try
                {
                    conn.Open();
                    adapter = new SqlDataAdapter(strSql, conn);
                    adapter.Fill(dt);

                    return dt;
                }
                catch { return dt; }
                finally
                {
                    dt.Dispose();
                    if (adapter != null)
                        adapter.Dispose();
                    conn.Close();
                }
            }
        }

        /// <summary>
        /// 执行查询语句，返回DataSet
        /// </summary>
        /// <param name="sqlText">查询语句</param>
        /// <returns>DataSet</returns>
        public static DataSet GetDataSet(string strSql)
        {
            return GetDataSet(connString, strSql);
        }

        /// <summary>
        /// 执行查询语句，返回DataSet
        /// </summary>
        /// <param name="sqlText">查询语句</param>
        /// <returns>DataSet</returns>
        public static DataSet GetDataSet(string connString, string strSql)
        {
            using (SqlConnection conn = new SqlConnection(connString))
            {
                DataSet ds = new DataSet();
                SqlDataAdapter adapter = null;
                try
                {
                    conn.Open();
                    adapter = new SqlDataAdapter(strSql, conn);
                    adapter.Fill(ds, "ds");
                    return ds;
                }
                catch { return ds; }
                finally
                {
                    ds.Dispose();
                    adapter.Dispose();
                    conn.Close();
                }
            }
        }

        /// <summary>
        /// 使用SqlBulkCopy方式批量插入数据
        /// </summary>
        /// <param name="dt">要插入的DataTable数据源</param>
        /// <returns>是否插入成功</returns>
        public static bool InsertByBulk(string tableName, string[] soureColumns, string[] destColumns, DataTable dt)
        {
            SqlConnection conn = new SqlConnection(connString);
            SqlBulkCopy bulk = new SqlBulkCopy(conn);
            try
            {
                conn.Open();
                bulk.BulkCopyTimeout = 60;
                bulk.DestinationTableName = tableName;
                if (soureColumns.Length == destColumns.Length)
                {
                    for (int i = 0; i < soureColumns.Length; i++)
                        bulk.ColumnMappings.Add(soureColumns[i], destColumns[i]);
                }
                else
                    return false;

                bulk.WriteToServer(dt);
                return true;
            }
            catch { return false; }
            finally
            {
                dt.Dispose();
                bulk.Close();
                conn.Dispose();
            }
        }
        public static bool CopyByBulk(string tableName, string[] soureColumns, string[] destColumns, IDataReader dr)
        {
            SqlConnection conn = new SqlConnection(connString);
            SqlBulkCopy bulk = new SqlBulkCopy(conn);
            try
            {
                conn.Open();
                bulk.BulkCopyTimeout = 60;
                bulk.DestinationTableName = tableName;
                if (soureColumns.Length == destColumns.Length)
                {
                    for (int i = 0; i < soureColumns.Length; i++)
                        bulk.ColumnMappings.Add(soureColumns[i], destColumns[i]);
                }
                else
                    return false;

                bulk.WriteToServer(dr);
                return true;
            }
            catch { return false; }
            finally
            {
                dr.Dispose();
                bulk.Close();
                conn.Dispose();
            }

        }


        #endregion

        #region 执行带参数的SQL语句

        /// <summary>
        /// 执行SQL语句，返回影响的记录数
        /// </summary>
        /// <param name="strSql">SQL语句</param>
        /// <param name="cmdParms"></param>
        /// <returns>影响的记录数</returns>
        public static int InsertOrUpdate(string strSql, params SqlParameter[] cmdParms)
        {
            return InsertOrUpdate(connString, strSql, cmdParms);
        }
        /// <summary>
        /// 执行SQL语句，返回影响的记录数
        /// </summary>
        /// <param name="strSql">SQL语句</param>
        /// <param name="connString">数据库连接字符串</param>
        /// <param name="cmdParms">SQL语句参数</param>
        /// <returns>受影响的行数</returns>
        public static int InsertOrUpdate(string connString, string strSql, params SqlParameter[] cmdParms)
        {
            using (SqlConnection conn = new SqlConnection(connString))
            {
                SqlCommand cmd = new SqlCommand();
                try
                {
                    PrepareCommand(cmd, conn, null, strSql, cmdParms);
                    int rows = cmd.ExecuteNonQuery();
                    cmd.Parameters.Clear();
                    return rows;
                }
                catch
                {
                    return -1;
                }
                finally
                {
                    cmd.Dispose();
                    conn.Close();
                }
            }
        }

        /// <summary>
        /// 执行多条SQL语句，实现数据库事务。
        /// </summary>
        /// <param name="htStrSql">SQL语句的哈希表（key为sql语句，value是该语句的SqlParameter[]）</param>
        /// <returns>是否执行成功</returns>
        public static bool InsertOrUpdateForTran(Hashtable htStrSql)
        {
            return InsertOrUpdateForTran(connString, htStrSql);
        }
        /// <summary>
        /// 执行多条SQL语句，实现数据库事务。
        /// </summary>
        /// <param name="htStrSql">SQL语句的哈希表（key为sql语句，value是该语句的SqlParameter[]）</param>
        /// <param name="connString">数据库连接字符串</param>
        /// <returns>是否执行成功</returns>
        public static bool InsertOrUpdateForTran(string connString, Hashtable htStrSql)
        {
            using (SqlConnection conn = new SqlConnection(connString))
            {
                SqlCommand cmd = new SqlCommand();
                SqlTransaction trans = null;
                try
                {
                    conn.Open();
                    trans = conn.BeginTransaction();
                    foreach (DictionaryEntry myDE in htStrSql)
                    {
                        string cmdText = myDE.Key.ToString();
                        SqlParameter[] cmdParms = (SqlParameter[])myDE.Value;
                        PrepareCommand(cmd, conn, trans, cmdText, cmdParms);
                        cmd.ExecuteNonQuery();
                        cmd.Parameters.Clear();
                    }
                    trans.Commit();
                    return true;
                }
                catch
                {
                    trans.Rollback();
                    return false;
                }
                finally
                {
                    cmd.Dispose();
                    trans.Dispose();
                    conn.Close();
                }
            }
        }

        /// <summary>
        /// 执行一条计算查询结果语句，返回一个值（string）。
        /// </summary>
        /// <param name="strSql">计算查询结果语句</param>
        /// <returns>查询结果（string）</returns>
        public static string GetSingleValue(string strSql, params SqlParameter[] cmdParms)
        {
            return GetSingleValue(connString, strSql, cmdParms);
        }
        /// <summary>
        /// 执行一条计算查询结果语句，返回一个值（string）。如果为空或出错，返回string.Empty
        /// </summary>
        /// <param name="strSql">计算查询结果语句</param>
        /// <param name="connString">数据库连接字符串</param>
        /// <returns>查询结果（string）</returns>
        public static string GetSingleValue(string connString, string strSql, params SqlParameter[] cmdParms)
        {
            using (SqlConnection conn = new SqlConnection(connString))
            {
                SqlCommand cmd = new SqlCommand();
                try
                {
                    PrepareCommand(cmd, conn, null, strSql, cmdParms);
                    object obj = cmd.ExecuteScalar();
                    cmd.Parameters.Clear();
                    if (Object.Equals(obj, null) || Object.Equals(obj, System.DBNull.Value))
                        return string.Empty;
                    else
                        return obj.ToString();
                }
                catch
                {
                    return string.Empty;
                }
                finally
                {
                    cmd.Dispose();
                    conn.Close();
                }
            }
        }

        /// <summary>
        /// 执行查询语句，返回SqlDataReader ( 注意：调用该方法后，一定要对SqlDataReader进行Close )
        /// </summary>
        /// <param name="strSQL">查询语句</param>
        /// <returns>SqlDataReader</returns>
        public static SqlDataReader GetDataReader(string strSql, params SqlParameter[] cmdParms)
        {
            return GetDataReader(connString, strSql, cmdParms);
        }
        /// <summary>
        /// 执行查询语句，返回SqlDataReader ( 注意：调用该方法后，一定要对SqlDataReader进行Close )
        /// </summary>
        /// <param name="strSQL">查询语句</param>
        /// <param name="connString">数据库连接字符串</param>
        /// <returns>SqlDataReader</returns>
        public static SqlDataReader GetDataReader(string connString, string strSql, params SqlParameter[] cmdParms)
        {
            using (SqlConnection conn = new SqlConnection(connString))
            {
                SqlCommand cmd = new SqlCommand();
                SqlDataReader dr = null;
                try
                {
                    PrepareCommand(cmd, conn, null, strSql, cmdParms);
                    dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                    cmd.Parameters.Clear();
                    return dr;
                }
                catch
                {
                    return null;
                }
                finally
                {
                    cmd.Dispose();
                    dr.Dispose();
                    conn.Close();
                }
            }
        }

        /// <summary>
        /// 执行查询语句，返回DataTable
        /// </summary>
        /// <param name="strSql">SQL查询语句</param>
        /// <returns>DataSet</returns>
        public static DataTable GetDataTable(string strSql, params SqlParameter[] cmdParms)
        {
            return GetDataTable(connString, strSql, cmdParms);
        }
        /// <summary>
        /// 执行查询语句，返回DataTable
        /// </summary>
        /// <param name="strSql">SQL查询语句</param>
        /// <param name="connString">数据库连接字符串</param>
        /// <returns>DataTable</returns>
        public static DataTable GetDataTable(string connString, string strSql, params SqlParameter[] cmdParms)
        {
            using (SqlConnection conn = new SqlConnection(connString))
            {
                SqlCommand cmd = new SqlCommand();
                PrepareCommand(cmd, conn, null, strSql, cmdParms);
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    DataTable dt = new DataTable();
                    try
                    {
                        da.Fill(dt);
                        cmd.Parameters.Clear();
                        return dt;
                    }
                    catch
                    {
                        return dt;
                    }
                    finally
                    {
                        dt.Dispose();
                        cmd.Dispose();
                        conn.Close();
                    }
                }
            }
        }

        /// <summary>
        /// 执行查询语句，返回DataSet
        /// </summary>
        /// <param name="sqlText">查询语句</param>
        /// <returns>DataSet</returns>
        public static DataSet GetDataSet(string strSql, params SqlParameter[] cmdParms)
        {
            return GetDataSet(connString, strSql, cmdParms);
        }

        /// <summary>
        /// 执行查询语句，返回DataSet
        /// </summary>
        /// <param name="sqlText">查询语句</param>
        /// <returns>DataSet</returns>
        public static DataSet GetDataSet(string connString, string strSql, params SqlParameter[] cmdParms)
        {
            using (SqlConnection conn = new SqlConnection(connString))
            {
                SqlCommand cmd = new SqlCommand();
                PrepareCommand(cmd, conn, null, strSql, cmdParms);
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    DataSet ds = new DataSet();
                    try
                    {
                        da.Fill(ds, "ds");
                        cmd.Parameters.Clear();
                        return ds;
                    }
                    catch
                    {
                        return ds;
                    }
                    finally
                    {
                        ds.Dispose();
                        cmd.Dispose();
                        conn.Close();
                    }
                }
            }
        }

        private static void PrepareCommand(SqlCommand cmd, SqlConnection conn, SqlTransaction trans, string cmdText, SqlParameter[] cmdParms)
        {
            if (conn.State != ConnectionState.Open)
                conn.Open();

            cmd.Connection = conn;
            cmd.CommandType = CommandType.Text;//cmdType;
            cmd.CommandText = cmdText;

            if (trans != null)
                cmd.Transaction = trans;

            if (cmdParms != null)
            {
                cmd.Parameters.Clear();
                foreach (SqlParameter parameter in cmdParms)
                {
                    if ((parameter.Direction == ParameterDirection.InputOutput || parameter.Direction == ParameterDirection.Input) && (parameter.Value == null))
                        parameter.Value = DBNull.Value;
                    cmd.Parameters.Add(parameter);
                }
            }
        }

        public static void RecordKeyValueChange(string KeyMark, string sContent, string sNewvalue, string sOldvalue, string sLogin, string sZonecode)
        {
            string sqlStr = String.Format("INSERT INTO DocKeyValueChange (keyvalue, changecontent, newvalue, oldvalue, loginid, zonecode) VALUES ('{0}', '{1}', '{2}', '{3}', '{4}', '{5}' )", KeyMark.Trim(), sContent, sNewvalue.Trim(), sOldvalue.Trim(), sLogin, sZonecode);
            InsertOrUpdate(sqlStr);
        }

        #endregion
    }
}
