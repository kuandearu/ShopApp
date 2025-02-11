namespace api.Enums
{
    public enum BannerStatus
    {
        Active = 1 ,         // The banner is visible and currently active
        Inactive = 2,       // The banner exists but is not displayed
        Scheduled = 3,      // The banner is scheduled to be displayed in the future
        Expired = 4,        // The banner's display period has ended
        Deleted = 5         // The banner has been marked as deleted
    }
}