//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DB
{
    using System;
    using System.Collections.Generic;
    
    public partial class MessageConversation
    {
        public int ID { get; set; }
        public int MessageID { get; set; }
        public int ConversationID { get; set; }
    
        public virtual Conversation Conversation { get; set; }
        public virtual Message Message { get; set; }
    }
}
