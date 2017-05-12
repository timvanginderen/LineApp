﻿using MijnLijn.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MijnLijn.Data.Remote
{
    public class LineManager
    {
        IRestService service;

        public LineManager(IRestService service)
        {
            this.service = service;
        }
        public Task<ApiResponse> GetLines()
        {
            return service.PostToGetLines();
        }
    }
}
