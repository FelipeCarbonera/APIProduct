using ApiProduct.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ApiProduct.DataContext
{
    public class DataBaseConnection
    {
        private MySqlConnection _conn;
        private string _cs;

        public DataBaseConnection()
        {
            try
            {
                // lê o arquivo 'ConfigDB.txt' com a connection string do banco
                var path = System.IO.Directory.GetCurrentDirectory();
                _cs = File.ReadAllText(path + @"\..\ConfigDB.txt");

                _conn = new MySqlConnection(_cs);
            }
            catch (Exception)
            {
                _cs = "";
                _conn = null;
            }
        }

        private bool OpenConn()
        {
            try
            {
                _conn.Open();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        private bool CloseConn()
        {
            try
            {
                if (_conn.State != System.Data.ConnectionState.Closed)
                    _conn.Close();

                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        internal List<Product> GetAllProducts()
        {
            try
            {
                OpenConn();
                var command = _conn.CreateCommand();
                command.CommandText = "SELECT * FROM PRODUCT";
                MySqlDataReader dr = command.ExecuteReader();

                List<Product> _products = new List<Product>();

                while (dr.Read())
                {
                    _products.Add(new Product
                    {
                        Id = Convert.ToInt32(dr["ID"]),
                        Name = dr["NAME"].ToString(),
                        Description = dr["DESCRIPTION"].ToString(),
                        Price = Convert.ToDecimal(dr["PRICE"]),
                        Date = Convert.ToDateTime(dr["DATE"]),
                        UrlImage = dr["URLIMAGE"].ToString(),

                        ID_Category = Convert.ToInt32(dr["ID_CATEGORY"])
                    });
                }

                CloseConn();

                return _products;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        internal Product GetProductById(long id)
        {
            try
            {
                OpenConn();

                var command = _conn.CreateCommand();
                command.CommandText = "SELECT * FROM PRODUCT WHERE ID = @ID";
                command.Parameters.AddWithValue("@ID", id);

                MySqlDataReader dr = command.ExecuteReader();
                Product _product = new Product();

                while (dr.Read())
                {
                    _product = new Product
                    {
                        Id = Convert.ToInt32(dr["ID"]),
                        Name = dr["NAME"].ToString(),
                        Description = dr["DESCRIPTION"].ToString(),
                        Price = Convert.ToDecimal(dr["PRICE"]),
                        Date = Convert.ToDateTime(dr["DATE"]),
                        UrlImage = dr["URLIMAGE"].ToString(),

                        ID_Category = Convert.ToInt32(dr["ID_CATEGORY"])
                    };
                }

                CloseConn();

                return _product;

            }
            catch (Exception e)
            {
                return null;
            }
        }

        internal bool InsertProduct(Product newProd)
        {
            try
            {
                OpenConn();

                var command = _conn.CreateCommand();
                command.CommandText = "INSERT INTO PRODUCT (NAME, DESCRIPTION, PRICE, ID_CATEGORY, URLIMAGE) VALUES (@NAME, @DESCRIPTION, @PRICE, @ID_CATEGORY, @URLIMAGE)";

                command.Parameters.AddWithValue("@NAME", newProd.Name);
                command.Parameters.AddWithValue("@DESCRIPTION", newProd.Description);
                command.Parameters.AddWithValue("@PRICE", newProd.Price);
                command.Parameters.AddWithValue("@ID_CATEGORY", newProd.ID_Category);
                command.Parameters.AddWithValue("@URLIMAGE", newProd.UrlImage);

                command.ExecuteNonQuery();

                CloseConn();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        internal bool UpdateProduct(Product newProd)
        {
            try
            {
                OpenConn();

                var command = _conn.CreateCommand();
                command.CommandText = "UPDATE PRODUCT SET NAME = @NAME, DESCRIPTION = @DESCRIPTION, PRICE = @PRICE, ID_CATEGORY = @ID_CATEGORY, URLIMAGE = @URLIMAGE  WHERE ID = @ID";
                command.Parameters.AddWithValue("@ID", newProd.Id);
                command.Parameters.AddWithValue("@NAME", newProd.Name);
                command.Parameters.AddWithValue("@DESCRIPTION", newProd.Description);
                command.Parameters.AddWithValue("@PRICE", newProd.Price);
                command.Parameters.AddWithValue("@ID_CATEGORY", newProd.ID_Category);
                command.Parameters.AddWithValue("@URLIMAGE", newProd.UrlImage);
                
                command.ExecuteNonQuery();

                CloseConn();

                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        internal bool DeletProductById(long id)
        {
            try
            {
                OpenConn();

                var command = _conn.CreateCommand();
                command.CommandText = "DELETE FROM PRODUCT WHERE ID = @ID";
                command.Parameters.AddWithValue("@ID", id);

                command.ExecuteNonQuery();

                CloseConn();

                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
    }
}
