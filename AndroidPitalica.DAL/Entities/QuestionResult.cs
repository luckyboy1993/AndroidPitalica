using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AndroidPitalica.DAL.Entities
{
    public partial class QuestionResult
    {
        [Key]
        public int Id { get; set; }
        public int Score { get; set; }
        public string Answered { get; set; }
        public string CorrectAnswer { get; set; }

        [ForeignKey("Question")]
        public int QuestionId { get; set; }
        [ForeignKey("User")]
        public int UserId { get; set; }
        [ForeignKey("Exam")]
        public int ExamId { get; set; }

        public virtual User User { get; set; }
        public virtual Question Question { get; set; }
        public virtual Exam Exam { get; set; }
    }
}
