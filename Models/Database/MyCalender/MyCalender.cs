using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace HomeAutomation.Models.Database.MyCalender
{
  public class MyCalender
  {
    public int Id { get; set; }
    public string Name { get; set; }
    public string FileName { get; set; }
    public Guid OwnerId { get; set; }
    public bool DisplayPublic { get; set; }
    public string Password { get; set; }
    public string Description { get; set; }
    public int UploadFileTypeId { get; set; }
    public int LanguageId { get; set; }

    public virtual Language Language { get; set; }
    public virtual UploadFileType UploadFileType { get; set; }

    public virtual List<MyCalenderCategrory> CalenderCategrories { get; set; }
  }
}