namespace Farm.Models
{
    public class Farmer_trip_sheets
    {
        public int? Id { get; set; }
	  public DateTime? Arrived_Date { get; set; }
	  public decimal? Net_Weight { get; set; }
	  public string? Slip_No { get; set; }
	  public string? Collection_Centre_Received { get; set; }
	  public string? Farmer_Tally_Code { get; set; }
	  public int? Farmer_Field_Id { get; set; }
	  public bool? IsActive { get; set; }
	  

	  
    }
}
