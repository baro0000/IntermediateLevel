namespace GameEntitiesBase.Data.Entities.Extensions
{
    public interface IEntityWithStatistics
    {
        public int StatisticsId { get; set; }
        Statistics Stats { get; }
    }
}
