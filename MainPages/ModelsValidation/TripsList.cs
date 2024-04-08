using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MainPages.Models;

public partial class TripsList
{
    public int TripNumber { get; set; }

    [Required(ErrorMessage = "Укажите тип похода")]
    public int TripTypeNumber { get; set; }

    [Required(ErrorMessage = "Укажите место")]
    [RegularExpression(DataChecker.Name40, ErrorMessage = "Название должно начинаться с большой буквы и содержать не более 40 символов")]
    public string Place { get; set; } = null!;

    [Required(ErrorMessage = "Укажите дату начала")]
    [DataType(DataType.Date, ErrorMessage = "Дата указана некорректно")]
    public DateOnly StartDate { get; set; }

    [Required(ErrorMessage = "Укажите дату окончания")]
    [DataType(DataType.Date, ErrorMessage = "Дата указана некорректно")]
    public DateOnly EndDate { get; set; }

    [Required(ErrorMessage = "Укажите регион")]
    [RegularExpression(DataChecker.Name40, ErrorMessage = "Название должно начинаться с большой буквы и содержать не более 40 символов")]
    public string Region { get; set; } = null!;

    [Required(ErrorMessage = "Составте описание похода")]
    [MaxLength(300, ErrorMessage = "Описание может содержать не более 300 символов")]
    public string Description { get; set; } = null!;

    public virtual ICollection<BoatsList> BoatsLists { get; set; } = new List<BoatsList>();

    public virtual ICollection<Cost> Costs { get; set; } = new List<Cost>();

    public virtual ICollection<Duty> Duties { get; set; } = new List<Duty>();

    public virtual ICollection<EquipmentList> EquipmentLists { get; set; } = new List<EquipmentList>();

    public virtual ICollection<FirstAidKit> FirstAidKits { get; set; } = new List<FirstAidKit>();

    public virtual ICollection<Food> Foods { get; set; } = new List<Food>();

    public virtual ICollection<Map> Maps { get; set; } = new List<Map>();

    public virtual ICollection<ParticipantsList> ParticipantsLists { get; set; } = new List<ParticipantsList>();

    public virtual ICollection<Plan> Plans { get; set; } = new List<Plan>();

    public virtual ICollection<ProductsList> ProductsLists { get; set; } = new List<ProductsList>();

    public virtual ICollection<RepairKit> RepairKits { get; set; } = new List<RepairKit>();

    public virtual ICollection<Route> Routes { get; set; } = new List<Route>();

    public virtual ICollection<Tent> Tents { get; set; } = new List<Tent>();

    public virtual TripType TripTypeNumberNavigation { get; set; } = null!;
}
