namespace StravaSharp
{
    public interface IActivity : IActivitySummary
    {
        string Description { get; }

        /// <summary>
        /// kilocalories, uses kilojoules for rides and speed/pace for runs
        /// </summary>
        float Calories { get; }

        //gear: object
        //gear summary

        //segment_efforts: array of objects
        // array of summary representations of the segment efforts, segment effort ids must be represented as 64-bit datatypes

        //splits_metric: array of metric split summaries
        // running activities only

        //splits_standard: array of standard split summaries
        // running activities only

        //best_efforts: array of best effort summaries
        // running activities only

        //photos: object
        //photos summary
    }

}
