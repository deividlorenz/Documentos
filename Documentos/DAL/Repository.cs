using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;

namespace Documentos.DAL
{
    public class Repository
    {
        public MySqlConnection conexaoMySQL;
        public Repository()
        {
            conexaoMySQL = CreateConnection();
        }

        public string GetConnectionString()
        {
            string conn = ConfigurationManager.ConnectionStrings["MySQLConnectionString"].ToString();
            return conn;
        }

        private MySqlConnection CreateConnection() => new MySqlConnection(GetConnectionString());

        public MySqlDataReader getReader(string mSQL)
        {
            MySqlDataReader reader = null;

            try
            {
                if (conexaoMySQL.State != ConnectionState.Open)
                    conexaoMySQL.Open();

                MySqlCommand cmd = new MySqlCommand(mSQL, conexaoMySQL);

                reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

            }
            catch (MySqlException msqle)
            {
                // MessageBox.Show("Erro de acesso ao MySQL : " + msqle.Message, "Erro");
                string strErro = msqle.Message;
            }

            return reader;
        }


        public MySqlDataReader getReaderWithParameters(string mSQL, Dictionary<string, string> parameters)
        {
            MySqlDataReader reader = null;

            try
            {
                if (conexaoMySQL.State != ConnectionState.Open)
                    conexaoMySQL.Open();

                MySqlCommand cmd = new MySqlCommand(mSQL, conexaoMySQL);
                foreach (var item in parameters)
                {
                    cmd.Parameters.AddWithValue(item.Key, item.Value);
                }
                reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

            }
            catch (MySqlException msqle)
            {
                // MessageBox.Show("Erro de acesso ao MySQL : " + msqle.Message, "Erro");
                string strErro = msqle.Message;
            }

            return reader;
        }


        public List<Documento> LoadDados()
        {
            string strSQL = "SELECT * FROM documento";

            var connection = new Repository();

            var result = new List<Documento>();

            try
            {

                var reader = connection.getReader(strSQL);
               

                while (reader.Read())
                {
                    result.Add(new Documento { 
                    Id = reader.GetInt32("id"),
                    Code = reader.GetString("code"),
                    Title = reader.GetString("title"),
                    Rev = reader.GetString("rev"),
                    Planned_Date = reader.GetDateTime("planned_date").ToString("dd/MM/yyyy"),
                    DocValue = reader.GetFloat("DocValue"),
                    FileName = reader.GetString("filename")
                    });
                }

                reader.Close();
                this.conexaoMySQL.Close();

                return result;
            }
            catch
            {            
                return result;
            }
        }

        public int CountDados()
        {
            string strSQL = "SELECT COUNT(*) FROM documento";

            var connection = new Repository();

            int result = 0;

            try
            {

                var reader = connection.getReader(strSQL);


                if (reader.Read())
                {
                    result = reader.GetInt32(0);
                }

                reader.Close();
                this.conexaoMySQL.Close();

                return result;
            }
            catch
            {
                return result;
            }
        }


        public List<Documento> LoadDados(string sortField, string sortOrder, bool isSearch, int pageIndex, int pageSize, string filters, string searchField, string searchString, string searchOper)
        {
            var offset = (pageIndex - 1) * pageSize;

            if (sortField == "")
            {
                sortField = "Id";
                sortOrder = "ASC";
            }
            if (searchString == "")
            {
                searchString = "0";
            }
            if (searchField == "")
            {
                searchField = "Id";
            }

            var dictionary = new Dictionary<string, string>();

            switch (searchOper)
            {
                case "eq": //igual a 
                    searchOper = "=";
                    dictionary.Add("@SearchString", $"{searchString}");
                    searchString = "@SearchString";
                    break;
                case "ne": //diferente de
                    searchOper = "!=";
                    break;
                case "bw": //inicia com
                    searchOper = "LIKE";
                    dictionary.Add("@SearchString", $"{searchString}%");
                    searchString = "@SearchString";                    
                    break;
                case "bn": //nao inicia com
                    searchOper = "NOT LIKE";
                    dictionary.Add("@SearchString", $"{searchString}%");
                    searchString = "@SearchString";                   
                    break;
                case "ew": //termina com
                    searchOper = "LIKE";
                    dictionary.Add("@SearchString", $"%{searchString}");
                    searchString = "@SearchString";                    
                    break;
                case "en": //não termina com
                    searchOper = "NOT LIKE";
                    dictionary.Add("@SearchString", $"%{searchString}");
                    searchString = "@SearchString";                    
                    break;
                case "cn": //contém
                    searchOper = "LIKE";
                    dictionary.Add("@SearchString", $"%{searchString}%");
                    searchString = "@SearchString";                    
                    break;
                case "nc": //Não contém
                    searchOper = "NOT LIKE";
                    dictionary.Add("@SearchString", $"%{searchString}%");
                    searchString = "@SearchString";                    
                    break;
                case "nu": //null
                    searchOper = "IS NULL";
                    searchString = "";
                    dictionary.Add("@SearchString", $"{searchString}");
                    searchString = "@SearchString";                    
                    break;
                case "nn": //not null
                    searchOper = "IS NOT NULL";
                    searchString = "";
                    dictionary.Add("@SearchString", $"{searchString}");
                    searchString = "@SearchString";
                    break;
                case "in": //está em
                    searchOper = "IN";
                    dictionary.Add("@SearchString", $"{searchString}");
                    searchString = "@SearchString";                    
                    break;
                case "ni": //não está em
                    searchOper = "NOT IN";
                    dictionary.Add("@SearchString", $"{searchString}");
                    searchString = "@SearchString";                    
                    break;
                default:
                    searchOper = ">";
                    break;

            }

                

            string strSQL = $"SELECT * FROM documento WHERE {searchField} {searchOper} {searchString} ORDER BY {sortField} {sortOrder} LIMIT {offset}, {pageSize}";

            var connection = new Repository();

            var result = new List<Documento>();

            try
            {

                var reader = connection.getReaderWithParameters(strSQL, dictionary);


                while (reader.Read())
                {
                    result.Add(new Documento
                    {
                        Id = reader.GetInt32("id"),
                        Code = reader.GetString("code"),
                        Title = reader.GetString("title"),
                        Rev = reader.GetString("rev"),
                        Planned_Date = reader.GetDateTime("planned_date").ToString("dd/MM/yyyy"),
                        DocValue = reader.GetFloat("DocValue"),
                        FileName = reader.GetString("filename")
                    });
                }

                reader.Close();
                this.conexaoMySQL.Close();

                return result;
            }
            catch
            {
                return result;
            }
        }



        public Documento Get(int id)
        {
            string strSQL = "SELECT * FROM documento where id = " + id;

            var connection = new Repository();

            var result = new Documento();

            try
            {

                var reader = connection.getReader(strSQL);


                while (reader.Read())
                {
                    result = new Documento
                    {
                        Code = reader.GetString("code"),
                        Title = reader.GetString("title"),
                        Rev = reader.GetString("rev"),
                        Planned_Date = reader.GetDateTime("planned_date").ToString("dd/MM/yyyy"),
                        DocValue = reader.GetFloat("DocValue"),
                        FileName = reader.GetString("filename")
                    };
                }

                reader.Close();
                this.conexaoMySQL.Close();

                return result;
            }
            catch
            {
                return result;
            }
        }


        public string Save(string mSQL, string[] campos, string[] values, string[] key, bool getid)
        {
            int ret = 0;
            string result;
            long idinsert = 0;

            try
            {
                conexaoMySQL.Open();

                MySqlCommand cmd = new MySqlCommand(mSQL, conexaoMySQL);

                for (int x = 0; x < campos.Length; x++)
                    cmd.Parameters.AddWithValue(campos[x].ToString(), values[x]);

                if (key.Length > 0)
                    cmd.Parameters.AddWithValue(key.GetValue(0).ToString(), key.GetValue(1).ToString());

                if (key.Length > 3)
                    cmd.Parameters.AddWithValue(key.GetValue(2).ToString(), key.GetValue(3).ToString());

                if (key.Length > 5)
                    cmd.Parameters.AddWithValue(key.GetValue(4).ToString(), key.GetValue(5).ToString());

                if (key.Length > 7)
                    cmd.Parameters.AddWithValue(key.GetValue(6).ToString(), key.GetValue(7).ToString());

                if (key.Length > 9)
                    cmd.Parameters.AddWithValue(key.GetValue(8).ToString(), key.GetValue(9).ToString());

                ret = cmd.ExecuteNonQuery();
                idinsert = cmd.LastInsertedId;
                result = ret.ToString();

            }
            catch (MySqlException msqle)
            {
                // MessageBox.Show("Erro de acesso ao MySQL : " + msqle.Message, "Erro");
                result = msqle.Message.ToString();
            }
            finally
            {
                this.conexaoMySQL.Close();
                this.conexaoMySQL.Dispose();
            }

            if (getid && ret == 1)
                return idinsert.ToString();
            else
                return FormatMessageErro(result);
        }

        private string FormatMessageErro(string msg)
        {
            string msgFormat = msg;
            if (msg != "1")
            {
                if (msg.IndexOf("Duplicate entry") != -1)
                    msgFormat = "Registro já Existente.";
                if (msg.IndexOf("foreign key") != -1)
                    msgFormat = "Este Registro possui dependências e não foi Excluído!";
            }
            return msgFormat;
        }

        public string SaveDados(string[] campos, string[] values, string[] Key, string strTable, string saveacao, bool getid = false)
        {
            string strSQL = "";
            string tmpStr = "";
            string RetInt;


            switch (saveacao)
            {
                case "add":
                    //Insert 
                    strSQL = "INSERT INTO " + strTable;

                    for (int x = 0; x < campos.Length; x++)
                        tmpStr += ",?" + campos[x].ToString();

                    //Gera Campos
                    strSQL += " (" + tmpStr.Substring(1).Replace('?', ' ') + ")";

                    //Gera Values
                    strSQL += " VALUES ( " + tmpStr.Substring(1) + " )";
                    break;

                case "edit":
                    //Update 
                    strSQL = "UPDATE " + strTable + " SET ";

                    for (int x = 0; x < campos.Length; x++)
                        tmpStr += "," + campos[x].ToString() + "=?" + campos[x].ToString();

                    strSQL += tmpStr.Substring(1);
                    strSQL += " WHERE " + Key.GetValue(0) + "=?" + Key.GetValue(0);

                    break;

                case "edit_3_keys":
                    strSQL = "UPDATE " + strTable + " SET ";

                    for (int x = 0; x < campos.Length; x++)
                        tmpStr += "," + campos[x].ToString() + "=?" + campos[x].ToString();

                    strSQL += tmpStr.Substring(1);
                    strSQL += " WHERE " + Key.GetValue(0) + "=?" + Key.GetValue(0) + " and " + Key.GetValue(2) + "=?" + Key.GetValue(2) + " and " + Key.GetValue(4) + "=?" + Key.GetValue(4);

                    break;

                case "del":

                    strSQL = "DELETE FROM " + strTable + " WHERE " + Key.GetValue(0) + "=?" + Key.GetValue(0);

                    break;

                case "delpermissao":

                    strSQL = "DELETE FROM " + strTable + " WHERE " + Key.GetValue(0) + "=?" + Key.GetValue(0) + " and " + Key.GetValue(2) + "=?" + Key.GetValue(2);
                    break;

                case "delnovarevisao":

                    strSQL = "DELETE FROM " + strTable + " WHERE " + Key.GetValue(0) + "=?" + Key.GetValue(0) + " and " + Key.GetValue(2) + "=?" + Key.GetValue(2) + " and " + Key.GetValue(4) + "=?" + Key.GetValue(4);
                    break;
                case "delMensagem":

                    strSQL = "DELETE FROM " + strTable + " WHERE " + Key.GetValue(0) + "=?" + Key.GetValue(0) + " and " + Key.GetValue(2) + "=?" + Key.GetValue(2) + " and " + Key.GetValue(4) + "=?" + Key.GetValue(4) + " and " + Key.GetValue(6) + "=?" + Key.GetValue(6);
                    break;
                case "del5params":

                    strSQL = "DELETE FROM " + strTable + " WHERE " + Key.GetValue(0) + "=?" + Key.GetValue(0) + " and " + Key.GetValue(2) + "=?" + Key.GetValue(2) + " and " + Key.GetValue(4) + "=?" + Key.GetValue(4) + " and " + Key.GetValue(6) + "=?" + Key.GetValue(6) + " and " + Key.GetValue(8) + "=?" + Key.GetValue(8);
                    break;
            }

            RetInt = Save(strSQL, campos, values, Key, getid);

            return RetInt;
        }


    }
}