using System;
using System.Collections.Generic;

namespace SLHDotNetTrainingBatch2.Database.AppDbContextModels;

public partial class TblCategory
{
    public string CategoryId { get; set; } = null!;

    public string CategoryCode { get; set; } = null!;

    public string CategoryName { get; set; } = null!;
}
