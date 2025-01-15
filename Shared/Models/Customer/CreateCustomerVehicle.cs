using System.ComponentModel.DataAnnotations;

namespace Shared.Models.Customer;

public class CreateCustomerVehicle
{
    [Required(AllowEmptyStrings = false, ErrorMessage = "License Plate is required")]
    public string LicensePlate { get; set; }
    [Required(AllowEmptyStrings = false, ErrorMessage = "Chassin Vin is required")]
    public string ChassinVin { get; set; }
    [Range(1, int.MaxValue, ErrorMessage = "Please select a vehicle")]
    public int VehicleId { get; set; }
    [Range(1, double.MaxValue, ErrorMessage = "Please enter a average vehicle valuation")]
    public decimal AverageVehicleValuation { get; set; }
    [Required(ErrorMessage = "Next Service Date required"), DataType(DataType.DateTime)]
    public DateTime? NextServiceDate { get; set; }
    [Required(ErrorMessage = "Next Service Date required"), DataType(DataType.DateTime)]
    public DateTime? RegistrationExpiryDate { get; set; }
    [Required(AllowEmptyStrings = false, ErrorMessage = "Registration File is required")]
    public string RegistrationFile { get; set; }
    [Range(1, int.MaxValue, ErrorMessage = "Please select a service advisor")]
    public int ServiceAdvisorId { get; set; }
}