using System;
using System.Collections.Generic;
using System.Text;

namespace SkillMapWPF.Models
{
    public static class UserSession
    {
        public static int UserId { get; set; }
        public static string FirstName { get; set; }

        public static int RoleId { get; set; }
        public static string RoleCode { get; set; } // Например, "admin" или "seeker"
    }
}
