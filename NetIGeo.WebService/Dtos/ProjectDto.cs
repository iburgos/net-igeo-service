﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace NetIGeo.WebService.Dtos
{
    public class ProjectDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime UpdateDate { get; set; }
        public IEnumerable<PointDto> Points { get; set; } = Enumerable.Empty<PointDto>();
    }
}