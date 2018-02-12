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
        public int Answered { get; set; }
        public int CorrectAnswer { get; set; }

        [ForeignKey("Question")]
        public int QuestionId { get; set; }
        [ForeignKey("User")]
        public int UserId { get; set; }

        public virtual User User { get; set; }
        public virtual Question Question { get; set; }
    }
}
