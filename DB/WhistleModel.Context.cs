﻿

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
using System.Data.Entity;
using System.Data.Entity.Infrastructure;


public partial class DBEntity : DbContext
{
    public DBEntity()
        : base("name=DBEntity")
    {

    }

    protected override void OnModelCreating(DbModelBuilder modelBuilder)
    {
        throw new UnintentionalCodeFirstException();
    }


    public virtual DbSet<Admin> Admin { get; set; }

    public virtual DbSet<Conversation> Conversation { get; set; }

    public virtual DbSet<File> File { get; set; }

    public virtual DbSet<Lawyer> Lawyer { get; set; }

    public virtual DbSet<Message> Message { get; set; }

    public virtual DbSet<Whistle> Whistle { get; set; }

    public virtual DbSet<MessageConversation> MessageConversation { get; set; }

    public virtual DbSet<User> User { get; set; }

}

}

