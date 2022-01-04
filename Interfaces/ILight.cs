namespace MyResolutions.Interfaces
{
  public interface ILight
  {
    int Energy { get; set; }
    int Lumens { get; set; }

    int ProduceLight(int power);
  }
}