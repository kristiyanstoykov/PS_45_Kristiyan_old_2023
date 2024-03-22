using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Welcome.Model;

namespace DataLayer.Model
{
    public class DatabaseUser : User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public override int Id { get; set; }
        public virtual ICollection<DatabaseSubject> DatabaseSubjects { get; set; }

        [NotMapped] // Ensures EF Core does not try to map this to your database
        public string SubjectsList => DatabaseSubjects != null
            ? String.Join(", ", DatabaseSubjects.Select(s => s.Name))
            : string.Empty;

    }
}
