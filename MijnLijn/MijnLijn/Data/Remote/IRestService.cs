﻿using MijnLijn.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MijnLijn.Data.Remote
{
    public interface IRestService
    {
        Task<List<Line>> PostToGetLines();
       
    }
}