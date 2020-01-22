using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Kroger.Models;
using Npgsql;
using Kroger.Commands;
using Dapper;

namespace Kroger.Repositories
{
    public class UserRepository
    {
        //set up a static credential class to keep my username and password private on my local machine
        static Secret credentials = new Secret();
        static string _password = credentials.secret;
        static string _username = credentials.username;
        string _connectionString = $@"Username={_username};Password={_password};Host=ec2-174-129-253-47.compute-1.amazonaws.com;Database=d5be1shopark8h;Port=5432;SSL Mode=Require;Trust Server Certificate=True;";

        public int CheckUser(string userFirebaseId)
        {
            using (var db = new NpgsqlConnection(_connectionString))
            {
                var sql = @" SELECT 
                                count(*)
                            FROM users u
                            WHERE u.firebaseid = @FirebaseID;";
                var para = new { FireBaseID = userFirebaseId };
                var check = db.QueryFirst(sql, para);
                return check;
            };
        }

        public void CreateUser(UserCommand usercommand)
        {
            using (var db = new NpgsqlConnection(_connectionString))
            {
                var sql = @"Insert into users (firebaseId, defaultlocationid, firstname, lastname, createddate)
                            values (@firebaseid, @defaultlocationid, @firstname, @lastname, now())";
                var para = new
                {
                    firebaseid = usercommand.FirebaseId,
                    defaultlocationid = usercommand.DefaultLocationId,
                    firstname = usercommand.FirstName,
                    lastname = usercommand.LastName,
                };
                db.Execute(sql, para);
            }
        }
    }
}
