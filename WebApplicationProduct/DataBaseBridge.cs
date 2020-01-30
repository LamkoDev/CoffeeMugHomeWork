using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplicationProduct.Models;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Diagnostics;

namespace WebApplicationProduct
{
    public class DataBaseBridge
    {
        protected IDataBase DbImplementation;
        public void UseSqlDbImplementation(string connectionString)
        {
            DbImplementation = new SqlDbApiConcrete();
            string[] config = new string[] { connectionString};
            DbImplementation.Configure(config);
        }
        public List<Models.Product> GetProducts() 
        {
            return DbImplementation.GetProducts();
        }
        public Guid AddProduct(ProductCreateRequestDto request)
        {
            return DbImplementation.AddProduct(request);
        }
    }
    public interface IDataBase
    {
        public void Configure(String []ConnectionParams);
        List<Models.Product> GetProducts();
        Models.Product GetProduct(Guid id);
        Guid AddProduct(Models.ProductCreateRequestDto request);
        void UpdateProduct(Models.ProductCreateRequestDto request);
        void DeleteProduct(Guid id);
    }
    class SqlDbApiConcrete : IDataBase
    {
        System.Data.SqlClient.SqlConnection sqlConnection;

        public void DeleteProduct(Guid id)
        {
            throw new NotImplementedException();
        }

        public Product GetProduct(Guid id)
        {
            throw new NotImplementedException();
        }

        public List<Product> GetProducts()
        {
            List<Product> products = new List<Product>();
            OpenConnection(sqlConnection);

            SqlCommand cmd1 = new SqlCommand("SELECT * FROM Products;", sqlConnection);
            SqlDataReader dr = cmd1.ExecuteReader();
            while(dr.Read())
            {
                Guid id = (Guid)(dr["Id"]);
                String name = (String)(dr["Name"]);
                Decimal price = (Decimal)(dr["Price"]);
                Product product = new Product(name, price);
                product.Id = id;
                products.Add(product);
            }
            CloseConnection(sqlConnection);
            return products;
        }
        string[] ConnectionParams;
        public void Configure(string[] ConnectionParams)
        {
            this.ConnectionParams = ConnectionParams;
            sqlConnection = new System.Data.SqlClient.SqlConnection();
            sqlConnection.ConnectionString = ConnectionParams[0];
        }
        public void OpenConnection(SqlConnection sqlConnection)
        {
            sqlConnection.Open();
        }
        public void CloseConnection(SqlConnection sqlConnection)
        {
            sqlConnection.Close();
        }
        public Guid AddProduct(ProductCreateRequestDto request)
        {
            Trace.WriteLine(request.Name);
            Trace.WriteLine(request.Price);

            OpenConnection(sqlConnection);
            SqlTransaction sqlTransaction = sqlConnection.BeginTransaction("NewProduct");
            String addProductCommand = "INSERT INTO Products (Id,Name,Price) VALUES(default, '"+request.Name+"', "+request.Price+")";
            SqlCommand cmd1 = new SqlCommand(addProductCommand, sqlConnection,sqlTransaction);
            cmd1.ExecuteNonQuery();
            sqlTransaction.Commit();


            SqlTransaction sqlTransaction2 = sqlConnection.BeginTransaction("GetNewProductID");
            String selectAddedIdCommand = "SELECT Id FROM Products WHERE Name='" 
                + request.Name + 
                "' AND Price=" 
                + request.Price + "; ";
            SqlCommand cmd2 = new SqlCommand(selectAddedIdCommand, sqlConnection, sqlTransaction2);
            SqlDataReader dr = cmd2.ExecuteReader();
            Guid id = new Guid();
            while (dr.Read())
            {
                id = (Guid)(dr["Id"]);
            }
            cmd2.ExecuteNonQuery();
            sqlTransaction2.Commit();
            CloseConnection(sqlConnection);
            return id;
        }
        public void UpdateProduct(ProductCreateRequestDto request)
        {
            throw new NotImplementedException();
        }

    }

}
