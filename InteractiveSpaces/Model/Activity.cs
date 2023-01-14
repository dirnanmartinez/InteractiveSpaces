using InteractiveSpaces.Model.ActivityDTO;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace InteractiveSpaces.Models;

public  class Activity 
{
    public Activity()
    {
    }

    public Activity(ActivityCreateDTO activityCreateDTO, 
        Resource? activityImage, Resource? initialHelp, Resource? finalMessage)
    {
        Name = activityCreateDTO.Name;
        Description = activityCreateDTO.Description;
        CreationDate = DateTime.Now;
        FinalMessageOK = activityCreateDTO.FinalMessageOK;
        FinalMessageError = activityCreateDTO.FinalMessageError;
        MaxTime = activityCreateDTO.MaxTime;
        ActivityImage = activityImage;
        InitialHelp = initialHelp;
        FinalMessage = finalMessage;
        Owner= activityCreateDTO.Owner;
    }

    [Key]
    public int Id { get; set; }

    [Required]
    [StringLength(100, ErrorMessageResourceName = "MessageErrorForName", MinimumLength = 5)]
    public string Name { get; set; }
    public string Description { get; set; }
    [Precision(2)] //how many digits are used to store the seconds
    public DateTime CreationDate { get; set; }

    public string FinalMessageOK { get; set; }

    public string FinalMessageError { get; set; }

    [Precision(5,2)]
    public decimal MaxTime { get; set; }

    [ForeignKey("FirstStepId")]
    public Step? FirstStep { get; set; }

    public int? FirstStepId { get; set; }

    [ForeignKey("LastStepId")]
    public Step? LastStep { get; set; }

    public int? LastStepId { get; set; }

    [InverseProperty("Activity")]
    public IList<Step> Steps { get; set; }

    public Resource? ActivityImage {get;set;}
    public Resource? InitialHelp { get; set; }
    public Resource? FinalMessage { get; set; }

    [EmailAddress]
    public string Owner { get; set; }

    [NotMapped]
    public bool NoStepsDefined
    {

        get
        {
            return FirstStep == null;
        }
    }



}