using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Kroger.Models;
using System.Data.SqlClient;
using Npgsql;
using Dapper;

namespace Kroger.Repositories
{
    public class ProductRepository
    {

        string _connectionString = "Server=localhost;Database=MyGroceryData;Trusted_Connection=True;";
        //set up a static credential class to keep my username and password private on my local machine
        //static Secret credentials = new Secret();
        //static string _password = credentials.secret;
        //static string _username = credentials.username;
        //string _connectionString = $@"Username={_username};Password={_password};Host=ec2-174-129-253-47.compute-1.amazonaws.com;Database=d5be1shopark8h;Port=5432;SSL Mode=Require;Trust Server Certificate=True;";

        public IEnumerable<Product> GetAllProducts()
        {
            using (var db = new SqlConnection(_connectionString))
            {
                var sql = @"with maxDate as (
                                SELECT 
                                    max(Capturedate) as max_date 
                                FROM daily_product_snapshot
                            )
                            SELECT 
                                dps.*,
                                pd.productname
                            FROM daily_product_snapshot dps
                                JOIN products pd on pd.productId = dps.productId
                                JOIN maxDate mx on mx.max_date = dps.Capturedate";
                var product = db.Query<Product>(sql);
                return product;
            };
        }

        public Product GetSingleProductInformation(string productId)
        {
            using (var db = new SqlConnection(_connectionString))
            {
                var sql = @"with maxDate as (
                                SELECT 
                                    max(Capturedate) as max_date 
                                FROM daily_product_snapshot
                            )
                            SELECT 
                                dps.*
                            FROM daily_product_snapshot dps
                                join maxDate mx on mx.max_date = dps.Capturedate
                            WHERE dps.Productid = @product";
                var param = new { product = productId };
                var product = db.QueryFirst<Product>(sql, param);
                return product;
            };
        }

        public float GetMaximumPriceByProduct(string productId)
        {
            using (var db = new SqlConnection(_connectionString))
            {
                var sql = @"SELECT 
                                cast(max(productRegularPrice) as float) as productRegularPrice
                            FROM daily_product_snapshot 
                            WHERE Productid = '4046';";
                var param = new { product = productId };
                var product = db.QueryFirst<float>(sql, param);
                return product;
            };
        }

        public float GetMinimumPriceByProduct(string productId)
        {
            using (var db = new SqlConnection(_connectionString))
            {
                var sql = @"SELECT 
                                coalesce(min(productPromoPrice), min(productRegularPrice))::float 
                            FROM daily_product_snapshot 
                            WHERE Productid = @product;";
                var param = new { product = productId };
                var product = db.QueryFirst<float>(sql, param);
                return product;
            };
        }

        public ProductDetails GetProductSummaryInformation(string productId, string locationid)
        {
            using (var db = new SqlConnection(_connectionString))
            {
                var sql = @"with maxDate as (
                                                        SELECT 
                                                            max(Capturedate) as max_date 
                                                        FROM daily_product_snapshot
                                                    )
                            , todays_product_info as (
                                                        SELECT 
                                                            dps.*
                                                        FROM daily_product_snapshot dps
                                                            join maxDate mx on mx.max_date = dps.Capturedate
                                                        WHERE dps.productid = '0001111091131' and dps.locationid = '02600567'
                            						)
                            						
                            , product_summary_info as (
                            							Select 
                            							    productid,
                            								max(productregularprice) as max_regular_price,
                            	                            max(productpromoprice) as max_promo_price,
                            	                            min(productregularprice) as min_regular_price,
                            	                            min(productpromoprice) filter (where productpromoprice <> 0) as         min_promo_price
                            							FROM daily_product_snapshot dps
                            	                        WHERE dps.productid = '0001111091131' and dps.locationid = '02600567'
                            							GROUP BY 1
                            						)
                            
                            SELECT td.productid,
                                   td.locationid,
                                   td.productname,
                              	   case when td.productpromoprice = 0 then td.productregularprice else td.productpromoprice end as          pricetoday,
                              	   pr.max_regular_price as maxprice,
                              	   case when pr.max_promo_price = 0 then pr.min_regular_price else pr.min_promo_price end as    minprice
                            FROM todays_product_info td
	                            JOIN product_summary_info pr
		                            on pr.productid = td.productid";
                var param = new { ProductID = productId, LocationId = locationid };
                var productdetails = db.QueryFirst<ProductDetails>(sql, param);
                return productdetails;
            };
        }
    }
}
