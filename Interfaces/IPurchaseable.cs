namespace MyResolutions.Interfaces
{
  public interface IPurchaseable
  {
    float Price { get; set; }
    string UPC { get; set; }
    float CalcTax();
  }
}