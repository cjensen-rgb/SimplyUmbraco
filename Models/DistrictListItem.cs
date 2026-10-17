namespace SimplyUmbraco.Models;

public class DistrictListItem
{
    public Guid DistrictID { get; set; }

    public string DistrictName { get; set; } = "";

    public bool IsActive { get; set; }

    public DateTime? ActiveFrom { get; set; }

    public DateTime? ActiveTo { get; set; }

    public string StatusText { get; set; } = "";
}