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
        public List<Class> GetClasses()
        {
            using var con = new MySqlConnection(s);
            return con.Query<Class>("Select * from classes").ToList();
        }
        public Models.User GetUser(string uid)
        {
            using var con = new MySqlConnection(s);
            return (User)con.Query<Models.User>("Select * from users" +
                                    "Where uid = @uid",
                                    new {uid = uid});
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
        public bool InsertComment(Comment comment)
        {
            using var con = new MySqlConnection(s);
            string query = @"Insert into Comments(text,state,rating,author,deleted,idTrip)
                            values(@date,@text,@state,@rating,@author,@deleted,@idTrip)";
            var param = new { text = comment.text, state = comment.state, rating = comment.rating, author = comment.author,deleted = comment.deleted,idTrip = comment.idTrip};
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
        public bool InsertUser(User user)
        {
            using var con = new MySqlConnection(s);
            string query = @"Insert into users(uid,name,classid)
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
