using Dapper;
using Firebase.Auth;
using MySql.Data.MySqlClient;
using static System.Net.Mime.MediaTypeNames;

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
        public List<Comment> GetAllComments(int id)
        {
            using var con = new MySqlConnection(s);
            return con.Query<Comment>("Select * from comments " +
                                    "Where idtrip = @id " +
                                    "AND deleted = 'N'",
                                    new {id = id}).ToList();
        }
        public Trip GetTrip(int id)
        {
            using var con = new MySqlConnection(s);
            return (Trip)con.Query<Trip>("Select * from trips " +
                                    "Where id = @id",
                                    new { id = id }).FirstOrDefault();
        }
        public List<Class> GetClasses()
        {
            using var con = new MySqlConnection(s);
            return con.Query<Class>("Select * from classes").ToList();
        }
        public User GetUser(string uid)
        {
            using var con = new MySqlConnection(s);
            return con.Query<User>("Select * from users " +
                                    "Where uid = @uid",
                                    new { uid = uid }).FirstOrDefault();
        }
        public List<Comment> GetComments()
        {
            using var con = new MySqlConnection(s);
            return con.Query<Comment>("Select * from comments").ToList();
        }
        public List<Comment> GetComments(int id)
        {
            using var con = new MySqlConnection(s);
            return con.Query<Comment>("SELECT * FROM comments " +
                                      "WHERE idtrip = @id " +
                                      "AND state = 'approved' " +
                                      "AND deleted = 'N'",
                                      new { id = id }
                                     ).ToList();
        }
        public bool InsertComment(Comment comment)
        {
            using var con = new MySqlConnection(s);
            string query = @"Insert into Comments(text,state,rating,authorid,deleted,idTrip) values(@date,@text,@state,@rating,@authorid,@deleted,@idTrip)";
            var param = new { text = comment.text, state = "pending", rating = comment.rating, authorid = comment.authorid, deleted = "N", idTrip = comment.idTrip };
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
        
        public bool InsertTrip(Trip trip, int tripId ,IFormFile file)
        {
            SaveImage(file, tripId);
            using var con = new MySqlConnection(s);
            string query = @"Insert into Trip(date,location,duration,type,price,picture,description)
                    values(@date,@location,@duration,@type,@price,@picture,@description)";
            var param = new { date = trip.date, location = trip.location, duration = trip.duration, type = trip.type, price = trip.price, picture = $"{tripId}_{file.FileName}", description = trip.description };
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

        public bool UpdateStateComment(int id, string state)
        {
            using var con = new MySqlConnection(s);
            string query = @"Update comments set state = @state where id = @id";
            var param = new { state = state, id = id };
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
        public bool DeleteComment(int id)
        {
            using var con = new MySqlConnection(s);
            string query = @"Update comments set deleted = 'Y' where id = @id";
            var param = new { id = id };
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
        public void SaveImage(IFormFile file, int tripId)
        {
            string fileName = $"{tripId}_{file.FileName}";
            string filePath = Path.Combine("wwwroot/images", fileName);

            if (!System.IO.File.Exists(filePath))
            {
                using (FileStream stream = System.IO.File.Create(filePath))
                {
                    file.CopyTo(stream);
                }
            }
        }
    }
}
