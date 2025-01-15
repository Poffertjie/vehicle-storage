namespace Shared.Models.CustomerVehicle;

public class CustomerVehicleCheckInRequestModel
{
    public int CustomerStorageContractId { get; set; }
    public int CustomerVehicleId { get; set; }

    public string Detailing { get; set; } = null!;

    public string TyreMake { get; set; } = null!;

    public int TyreYearModel { get; set; }

    public decimal TyreTreadMeasure { get; set; }

    public bool DeepWash { get; set; }

    public string? DiagnosticReportFile { get; set; }

    public string NumberPlateImage { get; set; } = null!;

    public string ChassisNumberImage { get; set; } = null!;

    public string FrontImage { get; set; } = null!;

    public string LeftFrontImage { get; set; } = null!;

    public string LeftFenderWheelImage { get; set; } = null!;

    public string LeftDoorsImage { get; set; } = null!;

    public string LeftRearFenderWheelImage { get; set; } = null!;

    public string RearImage { get; set; } = null!;

    public string RightFrontImage { get; set; } = null!;

    public string RightFenderWheelImage { get; set; } = null!;

    public string RightDoorsImage { get; set; } = null!;

    public string RightRearFenderWheelImage { get; set; } = null!;
    public string WarningLightsKilometers { get; set; } = null!;
}