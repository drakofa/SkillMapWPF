namespace SkillMapWPF.Models
{
    public static class UserSession
    {
        public static int UserId { get; set; }
        public static string? FirstName { get; set; } // Добавлен ?
        public static int RoleId { get; set; }
        public static string? RoleCode { get; set; } // Добавлен ?
    }
}