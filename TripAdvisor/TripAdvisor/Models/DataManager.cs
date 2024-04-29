using Dapper;
using MySql.Data.MySqlClient;

namespace TripAdvisor.Models
{
    public class DataManager
    {
        private string s;
        public DataManager(IConfiguration configuration)
        {
            s = configuration.GetConnectionString("DBConnection");
        }

        public List<Trip> GetTrips()
        {
            using var con = new MySqlConnection(s);
            return con.Query<Trip>("Select * from trips").ToList();
        }

        public List<Comment> GetComments(Trip trip)
        {
            using var con = new MySqlConnection(s);
            return con.Query<Comment>("Select * from comments" +
                                        " where idtrip = @id" +
                                        "AND state = approved" +
                                        "AND deleted = 'N'",
                                        new { id = trip.id }
                                        ).ToList();
        }
        public bool InsertTrip(Trip trip)
        {
            using var con = new MySqlConnection(s);
            string query = @"Insert into Trip(date,location,duration,type,price,picture,description)
                            values(@date,@location,@duration,@type,@price,@picture,@description)";
            var param = new { date = trip.date, location = trip.location, duration = trip.duration, type = trip.type, price = trip.price, picture = trip.picture, description = trip.description };
            bool esito;
            try
            {
                con.Execute(query, param);
                esito = true;
            }
            catch (Exception e)
            {
                esito = false;
            }
            return esito;
        }
        public bool InsertUser(user user)
        {
            using var con = new MySqlConnection(s);
            string query = @"Insert into user(uid,name,classid)
                            values(@uid,@name,@classid)";
            var param = new { uid = user.uid, name = user.name, classid = user.classid };
            bool esito;
            try
            {
                con.Execute(query, param);
                esito = true;
            }
            catch (Exception e)
            {
                esito = false;
            }
            return esito;
        }
    }
}
