using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;


namespace AjaxPeopleTable.Data
{
    public class PersonDb
    {
        private string _connection;
        public PersonDb(string conn)
        {
            _connection = conn;
        }
        public IEnumerable<Person> GetAllPeople()
        {
            using (var conn = new SqlConnection(_connection))
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandText = "Select * from People";
                conn.Open();
                var result = new List<Person>();
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    result.Add(new Person
                    {
                        Id = (int)reader["Id"],
                        FirstName = (string)reader["FirstName"],
                        LastName = (string)reader["LastName"],
                        Age = (int)reader["Age"]
                    });
                }
                return result;
            }
        }
        public void AddPerson(Person p)
        {
            using (var conn = new SqlConnection(_connection))
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandText = "Insert Into People(FirstName, LastName, Age)" +
                                   "Values(@first, @last, @age) Select Scope_Identity()";
                cmd.Parameters.AddWithValue("@first", p.FirstName);
                cmd.Parameters.AddWithValue("@last", p.LastName);
                cmd.Parameters.AddWithValue("@age", p.Age);
                conn.Open();
                var id = (int)(decimal)cmd.ExecuteScalar();
            }
        }
        public void EditPerson(Person p)
        {
            using (var conn = new SqlConnection(_connection))
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandText = @"Update People Set FirstName = @first, LastName = @last, Age = @age
                                    Where Id = @id";
                cmd.Parameters.AddWithValue("@first", p.FirstName);
                cmd.Parameters.AddWithValue("@last", p.LastName);
                cmd.Parameters.AddWithValue("@age", p.Age);
                cmd.Parameters.AddWithValue("@id", p.Id);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }
        public void DeletePerson(int id)
        {
            using (var conn = new SqlConnection(_connection))
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandText = "Delete From People Where Id = @id";               
                cmd.Parameters.AddWithValue("@id", id);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }
}
