namespace StravaSharp
{
    public interface IClub : IClubSummary
    {
        string Description { get; }

        /// <summary>
        /// casual_club, racing_team, shop, company, other
        /// </summary>
        string ClubType { get; }

        /// <summary>
        /// membership status of the requesting athlete 
        /// "member", "pending", null (not a member and have not requested join)
        /// </summary>
        string Membership { get; }

        /// <summary>
        /// true only if the requesting athlete is a club admin
        /// </summary>
        bool Admin { get; }

        /// <summary>
        /// true only if the requesting athlete is the club owner
        /// </summary>
        bool Owner { get; }

        /// <summary>
        /// total number of members the authenticated user is currently following
        /// </summary>
        bool FollowingCount { get; }
    }
}
