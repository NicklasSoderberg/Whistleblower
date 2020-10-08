using DB;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common.CommandTrees;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Whistleblower.Models;

namespace Whistleblower.Custom
{
    public class DBHandler
    {
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
                db.Whistle.FirstOrDefault(m => m.WhistleID == ((WhistleModel)AddObject).WhistleID).CurrentStatus = ((WhistleModel)AddObject).CurrentStatus;
                db.SaveChanges();
                return true;
            }
        }

        public static string LawyerNameByID(int id)
        {
            using (var db = new DB.DBEntity())
            {
                DB.Lawyer lawyer = db.Lawyer.Where(L => L.LawyerID == id).FirstOrDefault();
                return lawyer.Username;
            }
        }

        public static List<DB.Lawyer> GetLawyers()
        {
            using (var db = new DB.DBEntity())
            {
                return db.Lawyer.ToList();
            }
        }

        public static void UpdateAdminWhistle(DB.Whistle UpdateWhistle)
        {
            using (var db = new DB.DBEntity())
            {
                var result = db.Whistle.SingleOrDefault(b => b.WhistleID == UpdateWhistle.WhistleID);
                if (result != null)
                {
                    result.About = UpdateWhistle.About;
                    result.LawyerID = UpdateWhistle.LawyerID;
                    db.SaveChanges();
                }
            }
        }

        public static List<string> GetSubjectList()
        {
            return new List<string> {
            "Mutor, korruption & förfalskning",
            "Dataskydd och brott mot IT-säkerhet",
            "Diskriminering, trakasserier och andra arbetsrelaterade lagproblem",
            "Bedrägeri, missbruk och stöld",
            "Hälsa, säkerhet & miljö",
            "Penningtvätt",
            "Personal",
            "Annat" };
        }

        public static List<string> GetLawyerList()
        {
            List<DB.Lawyer> lawyers = GetLawyers();
            List<string> Names = new List<string>();
            foreach (DB.Lawyer L in lawyers)
                Names.Add(L.Username);

            return Names;
        }

        public static List<string> GetLawyerList(string removeThis)
        {
            List<DB.Lawyer> lawyers = GetLawyers();
            List<string> Names = new List<string>();
            foreach (DB.Lawyer L in lawyers)
                Names.Add(L.Username);

            Names.Remove(removeThis);
            return Names;
        }

        public static List<DB.Whistle> GetAllWhistles()
        {
            using (var db = new DB.DBEntity())
            {
                return db.Whistle.ToList();
            }
        }

        public static List<WhistleModel> GetWhistles()
        {
            using(var db = new DB.DBEntity())
            {
                List<WhistleModel> WhistleList = new List<WhistleModel>();
                List<DB.Whistle> templist = db.Whistle.Where(w => w.LawyerID == LawyerId).ToList();
                foreach(DB.Whistle w in templist)
                {
                    WhistleModel whistle = new WhistleModel
                    {
                        About = w.About,
                        CurrentStatus = w.CurrentStatus,
                        Description = w.Description,
                        Description_OtherEmployees = w.Description_OtherEmployees,
                        When = w.C_When,
                        Where = w.C_Where,
                        WhistleID = w.WhistleID
                    };
                    WhistleList.Add(whistle);
                }
                return WhistleList;
            }
        }
        public static List<DB.User> GetUser()
        {               
            using (var db = new DB.DBEntity())
            {                
                return db.User.ToList();
            }
        }
    }
}