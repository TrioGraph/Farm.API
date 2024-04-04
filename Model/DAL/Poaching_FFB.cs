namespace Farm.Models
{
    public class Poaching_FFB
    {
        public int? Id { get; set; }
	  public int? Farmer_Id { get; set; }
	  public int? Field_Id { get; set; }
	  public int? Photo_Id { get; set; }
	  public DateTime? Poaching_Date { get; set; }
	  public string? Notes { get; set; }
	  public bool? IsActive { get; set; }
	  

	  
    }
}
