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
                    

                    default:
                        return false;
                }

                db.SaveChanges();
                return true;
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