﻿using Binus.SampleWebAPI.Model.AppModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Binus.SampleWebAPI.Web.ViewModels
{
    public class BorrowBookViewModel
    {
        public List<BorrowBookModel> BorrowedBooks { get; set; }
        public string username { get; set; }
    }
}