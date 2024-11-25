using System;
using System.Collections.Generic;

namespace ecommerce_dash_api.Models;

public partial class ApiLog
{
    public DateTime? Timestamp { get; set; }

    public int Id { get; set; }

    public string? LogLevel { get; set; }

    public string? Message { get; set; }

    public string? StackTrace { get; set; }

    public string? Username { get; set; }

    public string? IpAddress { get; set; }

    public string? FunctionName { get; set; }

    public string? FunctionParameters { get; set; }
}
