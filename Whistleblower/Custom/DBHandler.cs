using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Whistleblower.Models;

namespace Whistleblower.Custom
{
    public class DBHandler
    {
        public static bool Post(Object AddObject)
        { 
            using (var db = new DB.DBEntity())
            {

                switch (AddObject?.GetType().Name.ToLower())
                {
                    case "whistle":
                        db.Whistle.Add((DB.Whistle)AddObject);
                        break;

                    case "user":
                        db.User.Add((DB.User)AddObject);
                        break;

                    default:
                        return false;
                }

                db.SaveChanges();
                return true;
            }
        }
        public static DB.Whistle PostWhistle(DB.Whistle whistle)
        {
            using (var db = new DB.DBEntity())
            {
                db.Whistle.Add(whistle);
                db.SaveChanges();
                return whistle;
            }            
        }
        public static DB.User PostUser(DB.User user)
        {
            using (var db = new DB.DBEntity())
            {
                db.User.Add(user);
                db.SaveChanges();
                return user;
            }
        }
        public static bool Put(Object AddObject)
        {
            using (var db = new DB.DBEntity())
            {

                switch (AddObject?.GetType().Name.ToLower())
                {
                    case "whistle":
                        db.Whistle.FirstOrDefault(m => m.WhistleID == ((WhistleModel)AddObject).WhistleID).CurrentStatus = ((WhistleModel)AddObject).CurrentStatus;
                        db.SaveChanges();
                        break;


                    default:
                        return false;
                }

                db.SaveChanges();
                return true;
            }
        }

    }
}