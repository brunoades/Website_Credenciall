using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
//using System.Windows.Forms;
using System.Data;
using System.Windows.Forms;

namespace Website_Credenciall
{
    public class ConfigDB
    {
        string ConnectionString = "";
        public MySqlConnection connection = null;
        // DB Development
        public string server = "localhost";
        // recebe o endereço do servidor padrão por propriedade dos settings
        public string port = "3306";
        // recebe a porta do servidor padrão por propriedade dos settings
        public string user = "root";
        // recebe o usuario do banco de dados padrão por propriedade dos settings
        public string password = "h3ckl355";
        // recebe a senha do banco de dados padrão por propriedade dos settings
        public string Table = "ecspassdb";
        // recebe a tabela do banco de dados padrão por propriedade dos settings
        public string ssl = "None";
        DataSet ds;
        DataTable dt;
        public string ConnectionType = "";
        string RecordSource = "";

        DataGridView tempdata;


        public ConfigDB()
        {

        }
        public void Connect(string database_name)
        {
            try
            {
                ConnectionString = "SERVER=" + server + ";" +
                    "PORT=" + port + ";" +
                    "DATABASE=" + database_name + ";" +
                    "UID=" + user + ";" +
                    "PASSWORD=" + password + ";" +
                    "SSLMODE=" + ssl + ";";
                connection = new MySqlConnection(ConnectionString);
            }
            catch (Exception E)
            {

                MessageBox.Show("Não foi possivel CONECTAR a nenhum banco de dados!");
            }
        }

        public void nowquiee(string sql_comm)
        {
            try
            {
                MySqlConnection cs = new MySqlConnection(ConnectionString);
                cs.Open();
                MySqlCommand myc = new MySqlCommand(sql_comm, cs);
                myc.ExecuteNonQuery();
                cs.Close();
            }
            catch (Exception err)
            {

                
            }
        }

        public void Execute(string Sql_command)
        {
            RecordSource = Sql_command;
            ConnectionType = Table;
            dt = new DataTable(ConnectionType);
            try
            {
                string command = RecordSource.ToUpper();
                MySqlDataAdapter da2 = new MySqlDataAdapter(RecordSource, connection);

                DataSet tempds = new DataSet();
                da2.Fill(tempds, ConnectionType);
                //da2.Fill(tempds);
            }
            catch (Exception err)
            {

                
            }
        }

        public string Results(int ROW, string COLUMN_NAME)
        {
            try
            {
                return dt.Rows[ROW][COLUMN_NAME].ToString();
            }
            catch (Exception err)
            {

                
                return "";
            }
        }

        public string Results(int ROW, int COLUMN_INDEX)
        {
            try
            {
                return dt.Rows[ROW][COLUMN_INDEX].ToString();
            }
            catch (Exception err)
            {

                
                return dt.Rows[ROW][COLUMN_INDEX].ToString();
            }
        }

        public void ExecuteSelected(string Sql_command)
        {
            RecordSource = Sql_command;
            ConnectionType = Table;
            dt = new DataTable(ConnectionType);
            try
            {
                string command = RecordSource.ToUpper();
                MySqlDataAdapter da = new MySqlDataAdapter(RecordSource, connection);
                ds = new DataSet();
                da.Fill(ds, ConnectionType);
                da.Fill(dt);
                tempdata = new DataGridView();
            }
            catch (Exception)
            {

                
            }
        }

        public int Count()
        {
            return dt.Rows.Count;
        }

    }
}