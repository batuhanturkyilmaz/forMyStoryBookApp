using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;

namespace forMyStoryBookApp
{
    public class SqlliteDataAccess
    {
        public static List<PersonModel> LoadPeople()
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                var output = cnn.Query < PersonModel > ("select from person", new DynamicParameters());
                return output.ToList();
            }

        } 

        public static void SavePerson(PersonModel person)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                cnn.Execute("insert into person (ID, Name, Age, AverageLife, HP, MP, SP,SkillPoints, Strength, Intelligence, Agility, Durability, Intuition, Class ) values (@ID, @Name, @Age, @AverageLife, @HP, @MP, @SP, @SkillPoints, @Strength, @Intelligence, @Agility, @Durability, @Intuition, @Class ) ", person);
            }
        }

        private static string LoadConnectionString(String id = "Default" )
        {
            return ConfigurationManager.ConnectionStrings[id].ConnectionString;
        }

    }
}
