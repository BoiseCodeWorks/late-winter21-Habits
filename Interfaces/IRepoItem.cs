using System;

namespace MyResolutions.Interfaces
{
  public interface IRepoItem
  {
    int Id { get; set; }
    DateTime CreatedAt { get; set; }
    DateTime UpdatedAt { get; set; }
  }
}