using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AndroidPitalica.DAL.Entities
{
    public partial class Exam
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("User")]
        public int CreatorId { get; set; }
        public string ExamTitle { get; set; }

        public virtual User Creator { get; set; }
        public virtual ICollection<Question> Questions { get; set; }
        public virtual ICollection<UserExamTaken> Students { get; set; }
    }
}
