using System;
using SQLite;

namespace InputMvvm.Models
{
    public class User
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Username { get; set; }
        public string Catname { get; set; }
        public string Dogname { get; set; }
    }
}
