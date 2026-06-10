namespace HillarysHaircare.Models;

public class Appointment
{
    public int Id { get; set; }
    public int StylistId { get; set; }
    public int CustomerId { get; set; }
    public DateTime AppointmentDate { get; set; }
    public bool IsCanceled { get; set; }
}