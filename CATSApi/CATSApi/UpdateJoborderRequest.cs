// 
// UpdateJoborderRequest.cs
//  
// Author:
//       Graham Floyd <gfloyd@catsone.com>
// 
// Copyright (c) 2012 CATS Software, Inc.
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.

using System;

namespace CATS
{
    public class UpdateJoborderRequest
    {
        public int Id = -1;
        public string File = String.Empty;
        public int Recruiter = -1;
        public int Contact = -1;
        public int Company = -1;
        public int EnteredBy = -1;
        public int Owner = -1;
        public string ClientJobId = String.Empty;
        public string Title = String.Empty;
        public string Description = String.Empty;
        public string Notes = String.Empty;
        public CATSApi.JoborderType? Type = null;
        public string Duration = String.Empty;
        public string RateMax = String.Empty;
        public string Salary = String.Empty;
        public string Status = String.Empty;
        public bool? IsHot = null;
        public int Openings = -1;
        public string City = String.Empty;
        public string State = String.Empty;
        public string Zip = String.Empty;
        public DateTime StartDate = DateTime.MinValue;
        public DateTime DateCreated = DateTime.MinValue;
        public DateTime DateModified = DateTime.MinValue;
        public bool? Public = null;
        public int Department = -1;
        public int Sourcer = -1;
        public int OpeningsAvailable = -1;
        public int Country = -1;
        
        public UpdateJoborderRequest()
        {
        }
    }
}

