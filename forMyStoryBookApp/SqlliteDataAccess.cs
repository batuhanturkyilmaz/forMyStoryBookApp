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
    public class SqlliteDataAccess //class r
    {
        public static PersonModel LoadPersonByName(string Name)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {

                string query = "SELECT * FROM person WHERE Name = @Name";

                // Veritabanından gelen sonucu al
                var result = cnn.QueryFirstOrDefault<PersonModel>(query, new { Name = Name });

                // Eğer sonuç null ise, uygun bir işlem yapabilirsiniz
                if (result == null)
                {
                    // İsterseniz null döndürebilirsiniz ya da varsayılan bir değer döndürebilirsiniz
                    return null; // ya da return new PersonModel(); şeklinde varsayılan bir nesne dönebilirsiniz
                }

                return result;  // Eğer kayıt bulunduysa, sonucu döndürüyoruz
            }
        }

        public static void DeletePersonByName(string name)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                string query = "DELETE FROM person WHERE Name = @Name"; // İsimle eşleşen kaydı siler
                cnn.Execute(query, new { Name = name });
            }
        }

        public static List<PersonModel> LoadAllPersons()
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                string query = "SELECT * FROM person"; // Tüm verileri seç
                return cnn.Query<PersonModel>(query).ToList(); // Verileri bir listeye döndür
            }
        }


        public static List<PersonModel> LoadPeople()
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                var output = cnn.Query < PersonModel > ("select * from person", new DynamicParameters());
                return output.ToList();
            }

        } 

        public static void SavePerson(PersonModel person)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                cnn.Execute("insert into person (ID, Name, Age, AverageLife, HP, MP, SP,SkillPoints, Strength, Intelligence, Agility, Durability, Intuition, Class, Title, Level ) values (@ID, @Name, @Age, @AverageLife, @HP, @MP, @SP, @SkillPoints, @Strength, @Intelligence, @Agility, @Durability, @Intuition, @Class, @Title, @Level ) ", person);
            }
        }

        private static string LoadConnectionString(String id = "Default" )
        {
            return ConfigurationManager.ConnectionStrings[id].ConnectionString;
        }



    }
}
