using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AndroidPitalica.DAL.Entities
{
    public partial class Question
    {
        [Key]
        public int Id { get; set; }
        public string QuestionText { get; set; }
        public string CorrectAnswer { get; set; }
        public string WrongAnswers { get; set; }
        public int Score { get; set; }

        [ForeignKey("Exam")]
        public int ExamId { get; set; }
        public int Row { get; set; }
        public string Definition { get; set; }
        public Boolean Visibility { get; set; }

        public virtual Exam Exam { get; set; }
    }
}
