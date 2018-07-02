﻿using System;
using System.Data;
using Npgsql;
using Dapper;
using System.Security.Cryptography;
using StagingWizard.DataLayerContracts;
using StagingWizard.UIContracts;
using System.Text;

namespace StagingWizard.DataLayer
{
    public class UserRepository : IUserRepository
    {
        public UserRepository(string connectionString)
        {
            ConnectionString = connectionString;
        }

        public string ConnectionString { get; }

        public IDbConnection OpenConnection(string connStr)
        {
            var conn = new NpgsqlConnection(connStr);
            conn.Open();
            return conn;
        }

        public string SignInCheck(User user)
        {
            using (var conn = OpenConnection(ConnectionString))
            {
                try
                {
                    user.Password = GetHashPassword(user);
                    var reader = conn.ExecuteReader("SELECT * FROM users WHERE login = @Login AND password = @Password", new { user.Login, user.Password });

                    while (reader.Read())
                    {
                        return UpdateToken(user);
                    }
                    return "Вход не выполнен";
                }
                catch
                {
                    return "Вход не выполнен!";
                }
            }
        }

        private string GetHashPassword(User user)
        {

            string result = "";

            byte[] hash = Encoding.ASCII.GetBytes(user.Password);
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] hashEnc = md5.ComputeHash(hash);

            foreach (var b in hashEnc)
                result += b.ToString("x2");
            return result;
        }

        public string AddUser(User user)
        {
            using (var conn = OpenConnection(ConnectionString))
            {
                try
                {
                    user.Password = GetHashPassword(user);
                    conn.Execute("INSERT INTO users(login, password) VALUES(@Login, @Password)", new { user.Login, user.Password });
                    return "Пользователь успешно добавлен";
                }
                catch
                {
                    return "Произошла ошибка в добавлении нового пользователя";
                }
            }
        }

        private string GetToken()
        {
            Random rnd = new Random();
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < 50; i++)
            {
                char c = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * rnd.NextDouble() + 65)));
                builder.Append(c);
            }
            return builder.ToString();
        }

        private string UpdateToken(User user)
        {
            using (var conn = OpenConnection(ConnectionString))
            {
                string token = GetToken();
                var command = conn.Execute("UPDATE users SET token = @Token WHERE login = @Login", new { token, user.Login });
                user.Token = token;
                return "Токен: " + token;
            }
        }
        
        public bool CheckToken(string token)
        {
            using (var conn = OpenConnection(ConnectionString))
            {
                var reader = conn.ExecuteReader("SELECT * FROM users WHERE token = @Token", new { token });
                while (reader.Read())
                {
                    return true;
                }
                return false;
            }
        }
    }
}