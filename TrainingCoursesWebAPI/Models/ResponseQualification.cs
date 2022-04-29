using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TrainingCoursesWebAPI.Entities;

namespace TrainingCoursesWebAPI.Models
{
    public class ResponseQualification
    {
        public ResponseQualification(Qualification qualification)
        {
            QualificationID = qualification.QualificationID;
            Title = qualification.Title;
        }
        public int QualificationID { get; set; }
        public string Title { get; set; }
    }
}