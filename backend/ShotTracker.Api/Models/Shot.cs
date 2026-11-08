namespace ShotTracker.Api.Models;

public class Shot
{
    public int Id { get; set; }
    public double X { get; set; }
    public double Y { get; set; }
    public bool Made { get; set; }
    public DateTime CreatedAt { get; set; }
}