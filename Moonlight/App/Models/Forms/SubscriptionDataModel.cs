﻿using System.ComponentModel.DataAnnotations;

namespace Moonlight.App.Models.Forms;

public class SubscriptionDataModel
{
    [Required(ErrorMessage = "You need to enter a name")]
    [MaxLength(32, ErrorMessage = "Max lenght for name is 32")]
    public string Name { get; set; } = "";

    [Required(ErrorMessage = "You need to enter a description")]
    public string Description { get; set; } = "";
}