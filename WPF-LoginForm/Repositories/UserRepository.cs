using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Net;
using WPF_LoginForm.Models;

namespace WPF_LoginForm.Repositories
{
    public class UserRepository : RepositoryBase, IUserRepository
    {
        public void RegistrationUser(UserModel userModel)
        {
            using (var connection = GetConnection())
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "Insert Into [User] (Id, UserName, Password, Name, LastName, Email, AccessLevel) " +
                    "Values(@Id, @Username, @Password, @Name, @LastName, @Email, @AccessLevel)";
                command.Parameters.Add("@Id", SqlDbType.UniqueIdentifier).Value = Guid.NewGuid();
                command.Parameters.Add("@Username", SqlDbType.NVarChar).Value = userModel.Username;
                command.Parameters.Add("@Password", SqlDbType.NVarChar).Value = userModel.Password;
                command.Parameters.Add("@Name", SqlDbType.NVarChar).Value = userModel.Name;
                command.Parameters.Add("@LastName", SqlDbType.NVarChar).Value = userModel.LastName;
                command.Parameters.Add("@Email", SqlDbType.NVarChar).Value = userModel.Email;
                command.Parameters.Add("@AccessLevel", SqlDbType.Int).Value = 1;
                command.ExecuteNonQuery();
                connection.Close();
            }
        }

        public void Add(UserModel userModel)
        {
            using (var connection = GetConnection())
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "Insert Into [User] (Id, UserName, Password, Name, LastName, Email, AccessLevel) " +
                    "Values(@Id, @Username, @Password, @Name, @LastName, @Email, @AccessLevel)";
                command.Parameters.Add("@Id", SqlDbType.UniqueIdentifier).Value = Guid.NewGuid();
                command.Parameters.Add("@Username", SqlDbType.NVarChar).Value = userModel.Username;
                command.Parameters.Add("@Password", SqlDbType.NVarChar).Value = userModel.Password;
                command.Parameters.Add("@Name", SqlDbType.NVarChar).Value = userModel.Name;
                command.Parameters.Add("@LastName", SqlDbType.NVarChar).Value = userModel.LastName;
                command.Parameters.Add("@Email", SqlDbType.NVarChar).Value = userModel.Email;
                command.Parameters.Add("@AccessLevel", SqlDbType.Int).Value = userModel.AccessLevel;
                command.ExecuteNonQuery();
                connection.Close();
            }
        }

        public bool AuthenticateUser(NetworkCredential credential)
        {
            bool validUser;
            using (var connection = GetConnection())
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "select *from [User] where UserName=@Username and [Password]=@Password";
                command.Parameters.Add("@Username", SqlDbType.NVarChar).Value = credential.UserName;
                command.Parameters.Add("@Password", SqlDbType.NVarChar).Value = credential.Password;
                validUser = command.ExecuteScalar() != null;
            }
            return validUser;
        }

        public void Edit(UserModel userModel)
        {
            using (var connection = GetConnection())
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "UPDATE [User] SET Password = @Password, Email = @Email, Name = @Name,LastName = @LastName, AccessLevel=@AccessLevel where Username = @Username";
                command.Parameters.Add("@Username", SqlDbType.NVarChar).Value = userModel.Username;
                command.Parameters.Add("@Password", SqlDbType.NVarChar).Value = userModel.Password;
                command.Parameters.Add("@Name", SqlDbType.NVarChar).Value = userModel.Name;
                command.Parameters.Add("@LastName", SqlDbType.NChar).Value = userModel.LastName;
                command.Parameters.Add("@Email", SqlDbType.NVarChar).Value = userModel.Email;
                command.Parameters.Add("@AccessLevel", SqlDbType.Int).Value = userModel.AccessLevel;
                command.ExecuteNonQuery();
            }
        }

        public List<UserModel> GetByAll()
        {
            List<UserModel> users = new List<UserModel>();
            using (var connection = GetConnection())
            using (var command = new SqlCommand("select * from [User]", connection))
            {
                connection.Open();

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var user = new UserModel
                        {
                            Id = reader["id"].ToString(),
                            Username = reader["UserName"].ToString(),
                            Password = reader["Password"].ToString(),
                            Name = reader["Name"].ToString(),
                            LastName = reader["LastName"].ToString(),
                            Email = reader["Email"].ToString(),
                            AccessLevel = Convert.ToInt32(reader["AccessLevel"])
                        };
                        users.Add(user);
                    }
                }
            }
            return users;
        }

        public UserModel GetByUsername(string username)
        {
            UserModel user = null;
            using (var connection = GetConnection())
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "select *from [User] where UserName=@Username";
                command.Parameters.Add("@Username", SqlDbType.NVarChar).Value = username;
                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        user = new UserModel()
                        {
                            Id = reader[0].ToString(),
                            Username = reader[1].ToString(),
                            Password = reader[2].ToString(),
                            Name = reader[3].ToString(),
                            LastName = reader[4].ToString(),
                            Email = reader[5].ToString(),
                            AccessLevel = Convert.ToInt32(reader[6])
                        };
                    }
                }
            }
            return user;
        }

        public void Remove(string username, string name, string lastname)
        {
            using (var connection = GetConnection())
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "Delete From [User] Where UserName=@UserName and Name=@Name and LastName=@LastName";
                command.Parameters.Add("@UserName", SqlDbType.NVarChar).Value = username;
                command.Parameters.Add("@Name", SqlDbType.NVarChar).Value = name;
                command.Parameters.Add("@LastName", SqlDbType.NVarChar).Value = lastname;
                command.ExecuteNonQuery();
                connection.Close();
            }
        }
    }
}
