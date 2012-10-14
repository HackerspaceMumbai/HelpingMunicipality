using System;
using System.Collections.Generic;
using Exceptions;
using System.Data.Common;
using System.Data.Odbc;
using System.Text;
using System.Data;
using MySql.Data.MySqlClient;
using System.Configuration;
using CommonUtils;

namespace DataObjects
{
    public class ParentData
    {
        private MySqlConnection mySqlConnection = null;
        private List<DbParameter> lstDbParameters = null;
        private string _selectedCon = string.Empty;
        public string SetConnection
        {
            get { return _selectedCon; }
            set { _selectedCon = value; }
        }

        private void GetConnection()
        {
      
            Dictionary<string, string> connections = new Dictionary<string, string>();
            string connectionString = string.Empty;
            
            connections.Add("local", ConfigurationManager.ConnectionStrings["local"].ConnectionString);
            //connections.Add("Server175", ConfigurationManager.ConnectionStrings["Server175"].ConnectionString);
            if (!string.IsNullOrEmpty(_selectedCon))
                connections.TryGetValue(_selectedCon, out connectionString);
            else
                connections.TryGetValue(Enums.Connections.local.ToString(), out connectionString);


            try
            {
                mySqlConnection = new MySqlConnection(connectionString);

            }
            catch
            {
                mySqlConnection = new MySqlConnection(connectionString);
            }
            mySqlConnection.Open();

        }

        private void GetConnection_archive()
        {
            try
            {
                mySqlConnection = new MySqlConnection(ConfigurationManager.ConnectionStrings["ConnString"].ConnectionString);
            }
            catch
            {
                mySqlConnection = new MySqlConnection(ConfigurationManager.AppSettings["ConnString"].ToString());
            }
            mySqlConnection.Open();

        }

        private void CloseConnection()
        {
            if (mySqlConnection.State == ConnectionState.Open)
            {
                mySqlConnection.Close();
            }
        }

        protected int ExecuteNonQuery(string sqlCommandString)
        {
            int iRetVal = -9;
            try
            {
                if ((lstDbParameters != null) && (lstDbParameters.Count > 0))
                {

                    for (int i = 0; i < lstDbParameters.Count; i++)
                    {
                        string parameter = lstDbParameters[i].ParameterName.ToString();
                        string value = lstDbParameters[i].Value.ToString();
                        if (!string.IsNullOrEmpty(lstDbParameters[i].ParameterName.ToString()))
                        {
                            lstDbParameters[i].Value = lstDbParameters[i].Value.ToString().Replace("'", "''").ToString().Trim();
                            sqlCommandString = sqlCommandString.Replace(lstDbParameters[i].ParameterName.ToString(),
                                               "'" + lstDbParameters[i].Value.ToString() + "'");
                        }
                    }
                }
                GetConnection();
                MySqlCommand mySqlCommand = new MySqlCommand(sqlCommandString, mySqlConnection);
                if (lstDbParameters != null)
                {
                    lstDbParameters.Clear();
                }
                iRetVal = mySqlCommand.ExecuteNonQuery();
            }
            catch (MySqlException ex)
            {
                MySqlEXP mySqlExp = new MySqlEXP(ex.Message + "\r\n Query : " + sqlCommandString, ex);
                throw mySqlExp;
            }
            catch (Exception ex)
            {
                throw ex;
            }

            finally
            {
                CloseConnection();
            }
            CloseConnection();
            return iRetVal;
        }

        protected void AddParameter(string key, string val, int value)
        {
            if (lstDbParameters == null)
            {
                lstDbParameters = new List<DbParameter>();
            }
            OdbcParameter oParam = new OdbcParameter();
            oParam.ParameterName = key;
            oParam.Value = val;
            lstDbParameters.Add(oParam);
        }

        protected DataSet ExecuteDataSet(string sqlCommandString)
        {
            DataSet retDataSet = new DataSet();
            try
            {
                if ((lstDbParameters != null) && (lstDbParameters.Count > 0))
                {
                    for (int i = 0; i < lstDbParameters.Count; i++)
                    {
                        string parameter = lstDbParameters[i].ParameterName.ToString();
                        string value = lstDbParameters[i].Value.ToString();
                        if (!string.IsNullOrEmpty(parameter))
                        {
                            if (value.IndexOf('\'') > -1)
                            {
                                value = value.Replace("\'", "\'\'");
                            }
                            sqlCommandString = sqlCommandString.Replace(parameter,
                                               "'" + value + "'");
                        }
                    }
                }
                GetConnection();
                MySqlDataAdapter mySqlDataAdapter = new MySqlDataAdapter();
                MySqlCommand mySqlCommand = new MySqlCommand(sqlCommandString, mySqlConnection);
                mySqlDataAdapter.SelectCommand = mySqlCommand;
                mySqlDataAdapter.Fill(retDataSet);
            }
            catch (MySqlException ex)
            {
                MySqlEXP mySqlExp = new MySqlEXP(ex.Message + "\r\n Query : " + sqlCommandString, ex);
                throw mySqlExp;
            }
            catch (Exception ex)
            {
                throw ex;
            }

            finally
            {
                CloseConnection();
            }
            CloseConnection();
            return retDataSet;
        }
        protected DataSet ExecuteDataSet_archive(string sqlCommandString)
        {
            DataSet retDataSet = new DataSet();
            try
            {
                if ((lstDbParameters != null) && (lstDbParameters.Count > 0))
                {
                    for (int i = 0; i < lstDbParameters.Count; i++)
                    {
                        if (!string.IsNullOrEmpty(lstDbParameters[i].ParameterName.ToString()))
                        {
                            sqlCommandString = sqlCommandString.Replace(lstDbParameters[i].ParameterName.ToString(),
                                               "'" + lstDbParameters[i].Value.ToString() + "'");
                        }
                    }
                }
                GetConnection_archive();
                MySqlDataAdapter mySqlDataAdapter = new MySqlDataAdapter();
                MySqlCommand mySqlCommand = new MySqlCommand(sqlCommandString, mySqlConnection);
                mySqlDataAdapter.SelectCommand = mySqlCommand;
                mySqlDataAdapter.Fill(retDataSet);
            }
            catch (MySqlException ex)
            {
                MySqlEXP mySqlExp = new MySqlEXP(ex.Message + "\r\n Query : " + sqlCommandString, ex);
                throw mySqlExp;
            }
            catch (Exception ex)
            {
                throw ex;
            }

            finally
            {
                CloseConnection();
            }
            CloseConnection();
            return retDataSet;
        }
        protected object ExecuteScalar(string sqlCommandString)
        {
            object iRetVal = -9;
            try
            {

                if ((lstDbParameters != null) && (lstDbParameters.Count > 0))
                {
                    for (int i = 0; i < lstDbParameters.Count; i++)
                    {
                        if (!string.IsNullOrEmpty(lstDbParameters[i].ParameterName.ToString()))
                        {
                            sqlCommandString = sqlCommandString.Replace(lstDbParameters[i].ParameterName.ToString(),
                                               "'" + lstDbParameters[i].Value.ToString() + "'");
                        }
                    }
                }
                GetConnection();
                MySqlCommand mySqlCommand = new MySqlCommand(sqlCommandString, mySqlConnection);
                if (lstDbParameters != null)
                {
                    lstDbParameters.Clear();
                }
                iRetVal = mySqlCommand.ExecuteScalar();
                if (iRetVal == null)
                {
                    iRetVal = -9;
                }

            }
            catch (MySqlException ex)
            {
                MySqlEXP mySqlExp = new MySqlEXP(ex.Message + "\r\n Query : " + sqlCommandString, ex);
                throw mySqlExp;
            }
            catch (Exception ex)
            {
                throw ex;
            }

            finally
            {
                CloseConnection();
            }
            CloseConnection();
            return iRetVal;
        }

        #region "Gets"
        protected string GetString(object value)
        {
            if (value != null)
            {
                return value.ToString();
            }
            else
            {
                return value.ToString();
            }
        }

        protected double GetDouble(object value)
        {
            double retVal;
            if (value != null && double.TryParse(value.ToString(), out retVal))
            {
                return retVal;
            }
            else
            {
                return 0;
            }
        }

        protected long GetLong(object value)
        {
            long retVal;
            if (value != null && long.TryParse(value.ToString(), out retVal))
            {
                return retVal;
            }
            else
            {
                return 0;
            }
        }

        protected int GetInt(object value)
        {
            int retVal;
            if (value != null && int.TryParse(value.ToString(), out retVal))
            {
                return retVal;
            }
            else
            {
                return 0;
            }
        }

        protected bool GetBool(object value)
        {
            bool retVal;
            if (value != null && bool.TryParse(value.ToString(), out retVal))
            {
                return retVal;
            }
            else
            {
                return false;
            }
        }

        protected DateTime GetDate(object value)
        {
            DateTime retVal;
            if (value != null && DateTime.TryParse(value.ToString(), out retVal))
            {
                return retVal;
            }
            else
            {
                return DateTime.MinValue;
            }
        }

        protected char GetChar(object value)
        {
            char retVal;
            if (value != null && char.TryParse(value.ToString(), out retVal))
            {
                return retVal;
            }
            else { return ' '; }
        }
        #endregion


    }
}
