﻿using System;
using System.Collections.Generic;

namespace Database.DbContext.DbModels;

public partial class Log
{
    public int Id { get; set; }

    public string? Message { get; set; }

    public string? MessageTemplate { get; set; }

    public string? Level { get; set; }

    public DateTime? TimeStamp { get; set; }

    public string? Exception { get; set; }

    public string? Properties { get; set; }

    public string? LogSource { get; set; }
}
