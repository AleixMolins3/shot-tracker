namespace ShotTracker.Api.Services;

public static class ZoneClassifier
{
    private const double BasketX = 0.5;
    private const double BasketY = 0.1;

    public static string GetZone(double x, double y)
    {
        var dx = x - BasketX;
        var dy = y - BasketY;
        var distance = Math.Sqrt(dx * dx + dy * dy);

        if (distance < 0.2) return "Sota cistella";
        if (distance < 0.45) return "Mitja distància";
        return "Triple";
    }
}