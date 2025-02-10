namespace api.Enums
{
    public enum UserRole
    {
        RegisteredUser = 1, // Authenticated users with basic privileges like saving favorites or posting comments or buying.
        Moderator = 2,      // Users responsible for moderating content (e.g., approving or removing posts).
        Admin = 3,  
    }
}