using System;
using System.Collections.Generic;
using WebApplicationProduct.Models;
using System.Data.SqlClient;
using System.Diagnostics;

namespace WebApplicationProduct
{
    class SqlDbApiConcrete : IDataBase
    {
        SqlConnection sqlConnection;
        public SqlDbApiConcrete(string ConnectionParams)
        {
            sqlConnection = new SqlConnection
            {
                ConnectionString = ConnectionParams
            };
            sqlConnection.Open();
        }
        public void DeleteProduct(Guid id)
        {
            String deleteProductCommand = "DELETE FROM Products WHERE Id = '" + id.ToString() + "'";
            CreateAndCommitTransaction(deleteProductCommand);
        }
        public Product GetProduct(Guid id)
        {
            String selectAddedIdCommand = "SELECT * FROM Products WHERE Id='" + id.ToString() + "'; ";
            Product product = new Product();
            using (SqlCommand command = new SqlCommand(selectAddedIdCommand, sqlConnection))
            using (SqlDataReader dr = command.ExecuteReader())
            {
                if (dr != null)
                    while (dr.Read())
                    {
                        product.Id = (Guid)(dr["Id"]);
                        product.Name = (String)(dr["Name"]);
                        product.Price = (Decimal)(dr["Price"]);
                    }
                dr.Close();
            }
            return product;
        }
        public List<Product> GetProducts()
        {
            List<Product> products = new List<Product>();
            SqlCommand command = new SqlCommand("SELECT * FROM Products;", sqlConnection);
            using (SqlDataReader dr = command.ExecuteReader())
            {
                while (dr.Read()) 
                {
                    Guid id = (Guid)(dr["Id"]);
                    String name = (String)(dr["Name"]);
                    Decimal price = (Decimal)(dr["Price"]);
                    Product product = new Product(name, price);
                    product.Id = id;
                    products.Add(product);
                    }
                dr.Close();
            }
            sqlConnection.Close();
            return products;
        }
        public Guid AddProduct(ProductCreateRequestDto request)
        {
            Guid id = new Guid();
            addTransaction(request);
            id = GetProductId(request);
            return id;
        }
        private void addTransaction(ProductCreateRequestDto request)
        {
            String addProductCommand = "INSERT INTO Products (Id,Name,Price) VALUES(default, '" + request.Name + "', " + FormatWithComma(request.Price) + ")";
            CreateAndCommitTransaction(addProductCommand);
        }
        private Guid GetProductId(ProductCreateRequestDto request)
        {
            Guid id = new Guid();
            String selectAddedIdCommand = "SELECT Id FROM Products WHERE Name='" + request.Name + "' AND Price=" + FormatWithComma(request.Price) + "; ";
            using (SqlCommand command = new SqlCommand(selectAddedIdCommand, sqlConnection))
                using (SqlDataReader dr = command.ExecuteReader())
                {
                    if (dr != null)
                        while (dr.Read())
                        {
                            id = (Guid)(dr["Id"]);
                        }
                    dr.Close();
                }
            return id;
        }
        public void UpdateProduct(ProductUpdateRequestDto request)
        {
            string updateProductCommand = "UPDATE Products SET Name = '" + request.NewName + "', Price = " + FormatWithComma(request.NewPrice) + " WHERE Id='" + request.Id + "';";
            CreateAndCommitTransaction(updateProductCommand);
        }
        private void CreateAndCommitTransaction(string command)
        {
            Trace.WriteLine(command);
            using (SqlTransaction sqlTransaction = sqlConnection.BeginTransaction("NewProduct"))
            {
                using (SqlCommand cmd = new SqlCommand(command, sqlConnection, sqlTransaction))
                {
                    cmd.ExecuteNonQuery();
                    sqlTransaction.Commit();
                }
            }
        }
        private String FormatWithComma(decimal number)
        {
            System.Globalization.CultureInfo invariantCulture = System.Globalization.CultureInfo.InvariantCulture;
            return number.ToString(invariantCulture);
        }
    }

}
