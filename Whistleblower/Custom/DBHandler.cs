using DB;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common.CommandTrees;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Whistleblower.Models;
using Whistleblower.ViewModels;

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
                return lawyer.Name;
            }
        }

        public static int LawyerIDByName(string Name)
        {
            using (var db = new DB.DBEntity())
            {
                DB.Lawyer lawyer = db.Lawyer.Where(L => L.Name == Name).FirstOrDefault();
                return (lawyer != null) ? lawyer.LawyerID : 0;
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
            DB.Whistle W = UpdateWhistle;
            using (var db = new DB.DBEntity())
            {
                var result = db.Whistle.SingleOrDefault(b => b.WhistleID == UpdateWhistle.WhistleID);
                if (result != null)
                {
                    result.About = W.About;
                    result.LawyerID = DBHandler.LawyerIDByName(W.CurrentStatus);
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
                Names.Add(L.Name);

            return Names;
        }

        public static List<string> GetLawyerList(string removeThis)
        {
            List<DB.Lawyer> lawyers = GetLawyers();
            List<string> Names = new List<string>();
            foreach (DB.Lawyer L in lawyers)
                Names.Add(L.Name);

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
        public static List<WhistleModel> GetWhistles(bool lawyer)
        {
            using(var db = new DB.DBEntity())
            {
                List<WhistleModel> WhistleList = new List<WhistleModel>();
                if (lawyer)
                {
                    List<DB.Whistle> templist = db.Whistle.Where(w => w.LawyerID == LawyerViewmodel.LoggedinID).ToList();
                    foreach (DB.Whistle w in templist)
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
                }
                else
                {
                    List<DB.Whistle> templist = db.Whistle.ToList();
                    foreach (DB.Whistle w in templist)
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

        public static List<DB.Conversation> GetConversation()
        {
            using (var db = new DB.DBEntity())
            {
                return db.Conversation.ToList();
            }
        }

        public static DB.Conversation CreateConversation(DB.Conversation conversation)
        {
            using (var db = new DB.DBEntity())
            {
                db.Conversation.Add(conversation);
                db.SaveChanges();
                return conversation;
            }
        }


        public static void PostMail(Mail mail,int whistleId)
        {
            using (var db = new DB.DBEntity())
            {
                int sender = 0;
                if (mail.MailSenderType == SafeboxViewmodel.MailSenders.Lawyer)
                {
                    sender = 2;
                }else if(mail.MailSenderType == SafeboxViewmodel.MailSenders.Whistler)
                {
                    sender = 0;
                }
                DB.Message Message = new Message {MessageID = mail.MailId,Message1 = mail.Message, Sender = sender,DateSent= DateTime.Now };
                db.Message.Add(Message);
                var conversation = db.Conversation.FirstOrDefault(m => m.WhistleID == whistleId);
                MessageConversation messageCon = new MessageConversation { ConversationID = conversation.ConversationID, MessageID = Message.MessageID };
                db.MessageConversation.Add(messageCon);
                //System.Data.Entity.Infrastructure.DbUpdateException: 
                //'Unable to update the EntitySet 'MessageConversation' because it has a DefiningQuery and no <InsertFunction>
                  //  element exists in the <ModificationFunctionMapping> element to support the current operation.'

                db.SaveChanges();
            }
        }
        public static List<Mail> GetMessages(int whistleId)
        {
            using (var db = new DB.DBEntity())
            {
                List<Mail> MailList = new List<Mail>();
                List<DB.Message> DbMessages = new List<Message>();
              var conversation =  db.Conversation.FirstOrDefault(c => c.WhistleID == whistleId);
                List<DB.MessageConversation> messageConversations = db.MessageConversation.Where(m => m.ConversationID == conversation.ConversationID).ToList();
                foreach(MessageConversation m in messageConversations)
                {
                    var mail = db.Message.FirstOrDefault(c => c.MessageID == m.MessageID);
                    DbMessages.Add(mail);
                }
                foreach (DB.Message w in DbMessages)
                {
                    SafeboxViewmodel.MailSenders sender = SafeboxViewmodel.MailSenders.Lawyer;
                    if(w.Sender == 2)
                    {
                        sender = SafeboxViewmodel.MailSenders.Lawyer;
                    }else if(w.Sender == 0)
                    {
                        sender = SafeboxViewmodel.MailSenders.Whistler;

                    }
                    Mail mail = new Mail {MailId = w.MessageID,Message = w.Message1,MailSenderType = sender, DateSent = w.DateSent};
                    MailList.Add(mail);
                }
                return MailList;
            }
        }
    }
}