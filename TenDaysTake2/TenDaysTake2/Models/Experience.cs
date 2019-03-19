using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace TenDaysTake2.Models
{
    public class Experience
    {
        [PrimaryKey, AutoIncrement] // added using SQLite;
        public int Id { get; set; }

        [MaxLength(50)]
        public string Title { get; set; }

        public string Content { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }

        public string VenueName { get; set; } // NEW

        public string VenueCategory { get; set; } // NEW

        public float VenueLat { get; set; } // NEW

        public float VenueLng { get; set; } // NEW

        public override string ToString()
        {
            return Title;
        }

        public static List<Experience> GetExperiences()
        {
            // added using System.Collections.Generic;
            using (SQLiteConnection conn = new SQLiteConnection(App.DatabasePath))
            {
                conn.CreateTable<Experience>();
                return conn.Table<Experience>().ToList();
            }
        }
    }
}
