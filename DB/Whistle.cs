
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
    
public partial class Whistle
{

    public int WhistleID { get; set; }

    public int LawyerID { get; set; }

    public string About { get; set; }

    public string C_When { get; set; }

    public string C_Where { get; set; }

    public string Description { get; set; }

    public string Description_OtherEmployees { get; set; }

    public Nullable<int> UploadID { get; set; }

    public bool isActive { get; set; }

    public string CurrentStatus { get; set; }

    public System.DateTime DateCreated { get; set; }

}

}
